using System.Linq;
using GLTF.Schema;
using UnityEngine;

namespace UnityGLTF.Misc
{
    public class GltfConverterPBRGraphToURPLit : IGLTFRuntimeShaderConverter
    {

		public static void SetKeyword(Material material, string keyword, bool state)
		{
			if (state)
			{
				material.EnableKeyword(keyword + "_ON");
				material.EnableKeyword(keyword);
			}
			else
			{
				material.DisableKeyword(keyword + "_ON");
				material.DisableKeyword(keyword);
			}

			if (material.HasProperty(keyword))
				material.SetFloat(keyword, state ? 1 : 0);
		}
	
        public bool ConvertShader(Material gltfMaterial, Shader gltfShader, Shader newShader)
        {
            if (!gltfShader.name.StartsWith("Hidden/UnityGLTF/PBRGraph") && !gltfShader.name.StartsWith("Hidden/UnityGLTF/UnlitGraph"))
			{
                return false;
            }

            var oldShader = gltfShader;

            var allowedConversions = new[] {
				StandardShader,
				UnlitColorShader,
				UnlitTextureShader,
				UnlitTransparentShader,
				UnlitTransparentCutoutShader,
				URPLitShader,
				URPUnlitShader,
			};

			var unlitSources = new[] {
				UnlitColorShader,
				UnlitTextureShader,
				URPUnlitShader,
				UnlitTransparentShader,
				UnlitTransparentCutoutShader,
			};

			var birpShaders = new[] {
				StandardShader,
				UnlitColorShader,
				UnlitTextureShader,
				UnlitTransparentShader,
				UnlitTransparentCutoutShader,
			};

			if (!allowedConversions.Contains(newShader.name)) return false;

            var sourceIsUnlit = unlitSources.Contains(newShader.name);
			var targetIsUnlit = newShader.name == URPUnlitShader;
			var sourceIsTransparent = newShader.name == UnlitTransparentShader || newShader.name == UnlitTransparentCutoutShader;

			var sourceIsBirp = birpShaders.Contains(newShader.name);
			var needsEmissiveColorSpaceConversion = sourceIsBirp && QualitySettings.activeColorSpace == ColorSpace.Linear;
			var colorProp = sourceIsBirp ? _Color : _BaseColor;
			var colorTexProp = sourceIsBirp ? _MainTex : _BaseMap;

            // convert from glTF PBR Graph to URP Lit
            var color = gltfMaterial.GetColor(baseColorFactor, Color.white);
			var albedo = gltfMaterial.GetTexture(baseColorTexture, null);
			var albedoOffset = gltfMaterial.GetTextureOffset(baseColorTexture, Vector2.zero);
            var albedoTiling = gltfMaterial.GetTextureScale(baseColorTexture, Vector2.one);
            var isTransparent = gltfMaterial.GetTag("RenderType", false) == "Transparent" || sourceIsTransparent;

			var metallic = gltfMaterial.GetFloat(metallicFactor, 0);
			var smoothness = 1 - gltfMaterial.GetFloat(roughnessFactor);
			var metallicGloss = gltfMaterial.GetTexture(metallicRoughnessTexture, null);
			var normal = gltfMaterial.GetTexture(normalTexture, null);
			var normalStrength = gltfMaterial.GetFloat(normalScale, 1);
			var occlusion = gltfMaterial.GetTexture(occlusionTexture, null);
			var occlusionStrength = gltfMaterial.GetFloat(occlusionStrength1, 1);
			var emission = gltfMaterial.GetTexture(emissiveTexture, null);
			var emissionColor = gltfMaterial.GetColor(emissiveFactor, Color.black);
			var cutoff = gltfMaterial.GetFloat(alphaCutoff, 0.5f);

            var isCutoff = gltfMaterial.IsKeywordEnabled("_ALPHATEST_ON") ||
                gltfMaterial.IsKeywordEnabled("_BUILTIN_ALPHATEST_ON") ||
                gltfMaterial.IsKeywordEnabled("_BUILTIN_AlphaClip") ||
                oldShader.name == UnlitTransparentCutoutShader;



			gltfMaterial.shader = newShader;
			gltfMaterial.SetTextureOffset(colorTexProp, albedoOffset);
			gltfMaterial.SetTextureScale(colorTexProp, albedoTiling);
            if (albedoOffset != Vector2.zero || albedoTiling != Vector2.one)
				SetKeyword(gltfMaterial, "_TEXTURE_TRANSFORM", true);

			gltfMaterial.SetFloat(_Metallic, metallic);
			gltfMaterial.SetColor(colorProp, color);
			gltfMaterial.SetTexture(colorTexProp, albedo);
			var map = new URPLitGraphMap(gltfMaterial);
			map.AlphaMode = isCutoff ? AlphaMode.MASK : (isTransparent ? AlphaMode.BLEND : AlphaMode.OPAQUE);

			if (gltfMaterial.HasProperty(_Smoothness))
			{
				gltfMaterial.SetFloat(_Smoothness, smoothness);
			}
			else 
			{
				if (gltfMaterial.HasProperty(_Glossiness)) 
				{
					gltfMaterial.SetFloat(_Glossiness, smoothness);
				} 
				else 
				{
					gltfMaterial.SetFloat(_Glossiness, 0.5f);
				}
			}
			gltfMaterial.SetTexture(_MetallicGlossMap, metallicGloss);
			gltfMaterial.SetTexture(_BumpMap, normal);
			gltfMaterial.SetFloat(_BumpScale, normalStrength);
			gltfMaterial.SetTexture(_OcclusionMap, occlusion);
			gltfMaterial.SetFloat(_Strength, occlusionStrength);
			gltfMaterial.SetTexture(_EmissionMap, emission);
						// if emission is OFF we don't want to set it to ON during conversion
			if ((newShader.name == StandardShader || newShader.name == URPLitShader) && !gltfMaterial.IsKeywordEnabled("_EMISSION"))
			{
				emission = null;
				emissionColor = Color.black;
			}

			gltfMaterial.SetColor(_EmissionColor, needsEmissiveColorSpaceConversion ? emissionColor.gamma : emissionColor);
			gltfMaterial.SetFloat(_Cutoff, isCutoff ? cutoff : -cutoff); // bit hacky, but that avoids an additional keyword for determining alpha cutoff right now

			// set the flags on conversion, otherwise it's confusing why they're not on - can't easily replicate the magic that Unity does in their inspectors when changing emissive on/off
			if (gltfMaterial.globalIlluminationFlags == MaterialGlobalIlluminationFlags.None || gltfMaterial.globalIlluminationFlags == MaterialGlobalIlluminationFlags.EmissiveIsBlack)
				gltfMaterial.globalIlluminationFlags = MaterialGlobalIlluminationFlags.BakedEmissive;

			// ensure keywords are correctly set after conversion
			ValidateMaterialKeywords(gltfMaterial);

			return true;
        }

		public static void ValidateMaterialKeywords(Material material)
		{
			// TODO ensure we're setting correct keywords for
			// - existence of a normal map
			// - existence of emission color values or texture
			// -

			// var needsVolumeTransmission = false;
			// needsVolumeTransmission |= material.HasProperty(thicknessFactor) && material.GetFloat(thicknessFactor) > 0;
			// needsVolumeTransmission |= material.HasProperty(transmissionFactor) && material.GetFloat(transmissionFactor) > 0;
			// material.SetKeyword("_VOLUME_TRANSMISSION", needsVolumeTransmission);
			//
			// var needsIridescence = material.HasProperty(iridescenceFactor) && material.GetFloat(iridescenceFactor) > 0;
			// material.SetKeyword("_IRIDESCENCE", needsIridescence);
			//
			// var needsSpecular = material.HasProperty(specularFactor) && material.GetFloat(specularFactor) > 0;
			// material.SetKeyword("_SPECULAR", needsSpecular);
			var isImplicitBlendMode = true;
			
			const string blendModeProp = "_OverrideSurfaceMode";
			if (material.HasProperty(blendModeProp))
			{
				var blendMode = material.GetInt(blendModeProp);
				isImplicitBlendMode = blendMode == 0;
			}

			if (isImplicitBlendMode)
			{
				if (material.IsKeywordEnabled("_VOLUME_TRANSMISSION_ON"))
				{
					// We want to enforce opaque rendering if
					// - Transmission is enabled
					// - Roughness is > 0
					// - The material isn't set to explicitly render as transparent
					
					// enforce Opaque
					if (material.HasProperty("_BUILTIN_Surface")) material.SetFloat("_BUILTIN_Surface", 0);
					if (material.HasProperty("_Surface")) material.SetFloat("_Surface", 0);
					material.DisableKeyword("_SURFACE_TYPE_TRANSPARENT");
					material.DisableKeyword("_BUILTIN_SURFACE_TYPE_TRANSPARENT");

					// enforce queue control and render queue 3000
					if (material.HasProperty("_QueueControl")) material.SetFloat("_QueueControl", 1);
					if (material.HasProperty("_BUILTIN_QueueControl")) material.SetFloat("_BUILTIN_QueueControl", 1);

					// not a great choice: using 2999 as magic value for "we automatically set the queue for you"
					// so the change can be reverted if someone toggles transmission on and then off again.
					material.renderQueue = 2999;
				}
				else
				{
					if (material.renderQueue == 2999)
					{
						if (material.HasProperty("_QueueControl")) material.SetFloat("_QueueControl", 0);
						if (material.HasProperty("_BUILTIN_QueueControl")) material.SetFloat("_BUILTIN_QueueControl", 0);
						material.renderQueue = -1;
					}
				}
			}

			// if (material.HasProperty("emissiveFactor"))
			// 	material.globalIlluminationFlags = MaterialEditor.FixupEmissiveFlag(material.GetColor("emissiveFactor"), material.globalIlluminationFlags);
		}

        private const string StandardShader = "Standard";
		private const string UnlitColorShader = "Unlit/Color";
		private const string UnlitTextureShader = "Unlit/Texture";
		private const string UnlitTransparentShader = "Unlit/Transparent";
		private const string UnlitTransparentCutoutShader = "Unlit/Transparent Cutout";
		private const string URPLitShader = "Universal Render Pipeline/Lit";
		private const string URPUnlitShader = "Universal Render Pipeline/Unlit";

		// Standard and URP-Lit property names
		private static readonly int _Color = Shader.PropertyToID("_Color");
		private static readonly int _BaseColor = Shader.PropertyToID("_BaseColor");
		private static readonly int _MainTex = Shader.PropertyToID("_MainTex");
		private static readonly int _BaseMap = Shader.PropertyToID("_BaseMap");
		private static readonly int _Metallic = Shader.PropertyToID("_Metallic");
		private static readonly int _Glossiness = Shader.PropertyToID("_Glossiness");
		private static readonly int _Smoothness = Shader.PropertyToID("_Smoothness");
		private static readonly int _MetallicGlossMap = Shader.PropertyToID("_MetallicGlossMap");
		private static readonly int _BumpMap = Shader.PropertyToID("_BumpMap");
		private static readonly int _BumpScale = Shader.PropertyToID("_BumpScale");
		private static readonly int _OcclusionMap = Shader.PropertyToID("_OcclusionMap");
		private static readonly int _Strength = Shader.PropertyToID("_OcclusionStrength");
		private static readonly int _EmissionMap = Shader.PropertyToID("_EmissionMap");
		private static readonly int _EmissionColor = Shader.PropertyToID("_EmissionColor");
		private static readonly int _Cutoff = Shader.PropertyToID("_Cutoff");

		// glTF property names
		private static readonly int baseColorFactor = Shader.PropertyToID("baseColorFactor");
		private static readonly int baseColorTexture = Shader.PropertyToID("baseColorTexture");
		private static readonly int metallicFactor = Shader.PropertyToID("metallicFactor");
		private static readonly int roughnessFactor = Shader.PropertyToID("roughnessFactor");
		private static readonly int metallicRoughnessTexture = Shader.PropertyToID("metallicRoughnessTexture");
		private static readonly int normalTexture = Shader.PropertyToID("normalTexture");
		private static readonly int normalScale = Shader.PropertyToID("normalScale");
		private static readonly int occlusionTexture = Shader.PropertyToID("occlusionTexture");
		private static readonly int occlusionStrength1 = Shader.PropertyToID("occlusionStrength");
		private static readonly int emissiveTexture = Shader.PropertyToID("emissiveTexture");
		private static readonly int emissiveFactor = Shader.PropertyToID("emissiveFactor");
		private static readonly int alphaCutoff = Shader.PropertyToID("alphaCutoff");
		// ReSharper restore InconsistentNaming

		private static readonly string[] emissivePropNames = new[] { "emissiveFactor", "_EmissionColor" };
    }

    static class MaterialHelper
	{
		public static float GetFloat(this Material material, int propertyIdx, float fallback)
		{
			if (material.HasProperty(propertyIdx))
				return material.GetFloat(propertyIdx);
			return fallback;
		}

		public static Color GetColor(this Material material, int propertyIdx, Color fallback)
		{
			if (material.HasProperty(propertyIdx))
				return material.GetColor(propertyIdx);
			return fallback;
		}

		public static Texture GetTexture(this Material material, int propertyIdx, Texture fallback)
		{
			if (material.HasProperty(propertyIdx))
				return material.GetTexture(propertyIdx);
			return fallback;
		}

		public static Vector2 GetTextureScale(this Material material, int propertyIdx, Vector2 fallback)
		{
			if (material.HasProperty(propertyIdx))
				return material.GetTextureScale(propertyIdx);
			return fallback;
		}

		public static Vector2 GetTextureOffset(this Material material, int propertyIdx, Vector2 fallback)
		{
			if (material.HasProperty(propertyIdx))
				return material.GetTextureOffset(propertyIdx);
			return fallback;
		}
	}
}