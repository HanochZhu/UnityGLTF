
using GLTF.Schema;
using UnityEngine;

namespace UnityGLTF
{
    public class URPLitGraphMap : BaseGraphMap, IMetalRoughUniformMap, IVolumeMap, ITransmissionMap, IIORMap, IIridescenceMap, ISpecularMap, IClearcoatMap, IDispersionMap
    {
		private const string URPLitShader = "Universal Render Pipeline/Lit";

        public URPLitGraphMap() : this(URPLitShader) {}
		public URPLitGraphMap(Material mat) : base(mat) { }

		protected URPLitGraphMap(string shaderName) : base(shaderName, "") { }

        public new Texture BaseColorTexture 
        {
            get 
            {
                return _material.GetTexture("_BaseMap"); 
            }
            set
            {
                _material.SetTexture("_BaseMap", value);
            }
        }

        public new Color BaseColorFactor
        {
            get
            {
                return _material.GetColor("_BaseColor");
            }
            set
            {
                _material.SetColor("_BaseColor", value);
            }
        }

        public new Vector2 BaseColorXOffset
        {
            get
            {
                return _material.GetTextureOffset("_BaseMap");
            }
            set
            {
                _material.SetTextureOffset("_BaseMap", value);
            }
        }

        public new Vector2 BaseColorScale
        {
            get
            {
                return _material.GetTextureScale("_BaseMap");
            }
            set
            {
                _material.SetTextureScale("_BaseMap", value);
            }
        }

        public double MetallicFactor
        {
            get
            {
                return _material.GetFloat("_Metallic");
            }
            set
            {
                _material.SetFloat("_Metallic", (float)value);
            }
        }

        public double RoughnessFactor
        {
            get
            {
                return _material.GetFloat("_Smoothness");
            }
            set
            {
                _material.SetFloat("_Smoothness", (float)value);
            }
        }

        public Texture MetallicRoughnessTexture
        {
            get
            {
                return _material.GetTexture("_MetallicGlossMap");
            }
            set
            {
                _material.SetTexture("_MetallicGlossMap", value);
            }
        }

        public Vector2 MetallicRoughnessXOffset
        {
            get
            {
                return _material.GetTextureOffset("_MetallicGlossMap");
            }
            set
            {
                _material.SetTextureOffset("_MetallicGlossMap", value);
            }
        }

        public Vector2 MetallicRoughnessXScale
        {
            get
            {
                return _material.GetTextureScale("_MetallicGlossMap");
            }
            set
            {
                _material.SetTextureScale("_MetallicGlossMap", value);
            }
        }

        public Texture NormalTexture 
        { 
            get
            {
                return _material.GetTexture("_BumpMap");
            }
            set
            {
                _material.SetTexture("_BumpMap", value);
            }
        }

		public double NormalTexScale
        {
            get
            {
                return _material.GetFloat("_BumpScale");
            }
            set
            {
                _material.SetFloat("_BumpScale", (float)value);
            }
        }

        public Vector2 NormalXOffset
        {
            get
            {
                return _material.GetTextureOffset("_BumpMap");
            }
            set
            {
                _material.SetTextureOffset("_BumpMap", value);
            }
        }

        public Vector2 NormalXScale
        {
            get
            {
                return _material.GetTextureScale("_BumpMap");
            }
            set
            {
                _material.SetTextureScale("_BumpMap", value);
            }
        }

        public Texture OcclusionTexture
        {
            get
            {
                return _material.GetTexture("_OcclusionMap");
            }
            set
            {
                _material.SetTexture("_OcclusionMap", value);
            }
        }

        public double OcclusionTexStrength
        {
            get
            {
                return _material.GetFloat("_Strength");
            }
            set
            {
                _material.SetFloat("_Strength", (float)value);
            }
        }

        public Vector2 OcclusionXOffset
        {
            get
            {
                return _material.GetTextureOffset("_OcclusionMap");
            }
            set
            {
                _material.SetTextureOffset("_OcclusionMap", value);
            }
        }

        public Vector2 OcclusionXScale
        {
            get
            {
                return _material.GetTextureScale("_OcclusionMap");
            }
            set
            {
                _material.SetTextureScale("_OcclusionMap", value);
            }
        }

        public Texture EmissiveTexture
        {
            get
            {
                return _material.GetTexture("_EmissionMap");
            }
            set
            {
                _material.SetTexture("_EmissionMap", value);
            }
        }

        public Color EmissiveFactor
        {
            get
            {
                return _material.GetColor("_EmissionColor");
            }
            set
            {
                _material.SetColor("_EmissionColor", value);
            }
        }

        public Vector2 EmissiveXOffset
        {
            get
            {
                return _material.GetTextureOffset("_EmissionMap");
            }
            set
            {
                _material.SetTextureOffset("_EmissionMap", value);
            }
        }


        public int MetallicRoughnessTexCoord 
        { 
            get {
                return (int)_material.GetFloat("_MetallicGlossMapTexCoord");
            }
            set
            {
                _material.SetFloat("_MetallicGlossMapTexCoord", value);
            }
        }
        public double MetallicRoughnessXRotation 
        { 
            get
            {
                return _material.GetFloat("_MetallicGlossMapRotation");
            }
            set
            {
                _material.SetFloat("_MetallicGlossMapRotation", (float)value);
            }
        }
        public int MetallicRoughnessXTexCoord { 
            get
            {
                return (int)_material.GetFloat("_MetallicGlossMapTexCoord");
            }
            set
            {
                _material.SetFloat("_MetallicGlossMapTexCoord", value);
            }
        }
        public int NormalTexCoord 
        { 
            get {
                return (int)_material.GetFloat("_BumpMapTexCoord");
            }
            set
            {
                _material.SetFloat("_BumpMapTexCoord", value);
            }
        }
        public double NormalXRotation { 
            get
            {
                return _material.GetFloat("_BumpMapRotation");
            }
            set
            {
                _material.SetFloat("_BumpMapRotation", (float)value);
            }
        }
        public int NormalXTexCoord { 
            get
            {
                return (int)_material.GetFloat("_BumpMapTexCoord");
            }
            set
            {
                _material.SetFloat("_BumpMapTexCoord", value);
            }
         }
        public int OcclusionTexCoord 
        { 
            get 
            {
                return (int)_material.GetFloat("_OcclusionMapTexCoord");
            }
            set
            {
                _material.SetFloat("_OcclusionMapTexCoord", value);
            }
        }
        public double OcclusionXRotation { 
            get
            {
                return _material.GetFloat("_OcclusionMapRotation");
            }
            set
            {
                _material.SetFloat("_OcclusionMapRotation", (float)value);
            }
         }
        public int OcclusionXTexCoord { 
            get
            {
                return (int)_material.GetFloat("_OcclusionMapTexCoord");
            }
            set
            {
                _material.SetFloat("_OcclusionMapTexCoord", value);
            }
        }
        public int EmissiveTexCoord 
        { 
            get 
            {
                return (int)_material.GetFloat("_EmissionMapTexCoord");
            }
            set
            {
                _material.SetFloat("_EmissionMapTexCoord", value);
            }
        }
        public double EmissiveXRotation { 
            get
            {
                return _material.GetFloat("_EmissionMapRotation");
            }
            set
            {
                _material.SetFloat("_EmissionMapRotation", (float)value);
            }
        }
        public Vector2 EmissiveXScale { 
            get
            {
                return _material.GetTextureScale("_EmissionMap");
            }
            set
            {
                _material.SetTextureScale("_EmissionMap", value);
            }
         }
        public int EmissiveXTexCoord {
            get
            {
                return (int)_material.GetFloat("_EmissionMapTexCoord");
            }
            set
            {
                _material.SetFloat("_EmissionMapTexCoord", value);
            }
         }
        public double ThicknessFactor
	    {
		    get => _material.GetFloat("thicknessFactor");
		    set => _material.SetFloat("thicknessFactor", (float) value);
	    }

	    public Texture ThicknessTexture
	    {
		    get => _material.GetTexture("thicknessTexture");
		    set => _material.SetTexture("thicknessTexture", value);
	    }
        public Vector2 ThicknessTextureOffset
	    {
		    get => _material.GetTextureOffset("thicknessTexture");
		    set => _material.SetTextureOffset("thicknessTexture", value);
	    }

        public Vector2 ThicknessTextureScale
	    {
		    get => _material.GetTextureScale("thicknessTexture");
		    set => _material.SetTextureScale("thicknessTexture", value);
	    }	    public int ThicknessTextureTexCoord
	    {
		    get =>  (int)_material.GetFloat("thicknessTextureTexCoord");
		    set => _material.SetFloat("thicknessTextureTexCoord", (float) value);
	    }
       
	    public double AttenuationDistance
	    {
		    get => _material.GetFloat("attenuationDistance");
		    set => _material.SetFloat("attenuationDistance", (float) value);
	    }

	    public Color AttenuationColor
	    {
		    get => _material.GetColor("attenuationColor");
		    set => _material.SetColor("attenuationColor", value);
	    }

	    public double TransmissionFactor
	    {
		    get => _material.GetFloat("transmissionFactor");
		    set => _material.SetFloat("transmissionFactor", (float) value);
	    }

	    public Texture TransmissionTexture
	    {
		    get => _material.GetTexture("transmissionTexture");
		    set => _material.SetTexture("transmissionTexture", value);
	    }

	    public double TransmissionTextureRotation
	    {
		    get => _material.GetFloat("transmissionTextureRotation");
		    set => _material.SetFloat("transmissionTextureRotation", (float) value);
	    }

	    public Vector2 TransmissionTextureOffset
	    {
		    get => _material.GetTextureOffset("transmissionTexture");
		    set => _material.SetTextureOffset("transmissionTexture", value);
	    }

	    public Vector2 TransmissionTextureScale
	    {
		    get => _material.GetTextureScale("transmissionTexture");
		    set => _material.SetTextureScale("transmissionTexture", value);
	    }

	    public int TransmissionTextureTexCoord
	    {
		    get =>  (int)_material.GetFloat("transmissionTextureTexCoord");
		    set => _material.SetFloat("transmissionTextureTexCoord", (float) value);
	    }

	    public double IOR
	    {
		    get => _material.GetFloat("ior");
		    set => _material.SetFloat("ior", (float) value);
	    }

	    public double IridescenceFactor
	    {
		    get => _material.GetFloat("iridescenceFactor");
		    set => _material.SetFloat("iridescenceFactor", (float) value);
	    }

	    public double IridescenceIor
	    {
		    get => _material.GetFloat("iridescenceIor");
		    set => _material.SetFloat("iridescenceIor", (float) value);
	    }

	    public double IridescenceThicknessMinimum
	    {
		    get => _material.GetFloat("iridescenceThicknessMinimum");
		    set => _material.SetFloat("iridescenceThicknessMinimum", (float) value);
	    }

	    public double IridescenceThicknessMaximum
	    {
		    get => _material.GetFloat("iridescenceThicknessMaximum");
		    set => _material.SetFloat("iridescenceThicknessMaximum", (float) value);
	    }

	    public Texture IridescenceTexture
	    {
		    get => _material.GetTexture("iridescenceTexture");
		    set => _material.SetTexture("iridescenceTexture", value);
	    }

	    public double IridescenceTextureRotation
	    {
		    get => _material.GetFloat("iridescenceTextureRotation");
		    set => _material.SetFloat("iridescenceTextureRotation", (float) value);
	    }

	    public Vector2 IridescenceTextureOffset
	    {
		    get => _material.GetTextureOffset("iridescenceTexture");
		    set => _material.SetTextureOffset("iridescenceTexture", value);
	    }

	    public Vector2 IridescenceTextureScale
	    {
		    get => _material.GetTextureScale("iridescenceTexture");
		    set => _material.SetTextureScale("iridescenceTexture", value);
	    }

	    public int IridescenceTextureTexCoord
	    {
		    get =>  (int)_material.GetFloat("iridescenceTextureTexCoord");
		    set => _material.SetFloat("iridescenceTextureTexCoord", (float) value);
	    }

	    public Texture IridescenceThicknessTexture
	    {
		    get => _material.GetTexture("iridescenceThicknessTexture");
		    set => _material.SetTexture("iridescenceThicknessTexture", value);
	    }

	    public double IridescenceThicknessTextureRotation
	    {
		    get => _material.GetFloat("iridescenceThicknessTextureRotation");
		    set => _material.SetFloat("iridescenceThicknessTextureRotation", (float) value);
	    }

	    public Vector2 IridescenceThicknessTextureOffset
	    {
		    get => _material.GetTextureOffset("iridescenceThicknessTexture");
		    set => _material.SetTextureOffset("iridescenceThicknessTexture", value);
	    }

	    public Vector2 IridescenceThicknessTextureScale
	    {
		    get => _material.GetTextureScale("iridescenceThicknessTexture");
		    set => _material.SetTextureScale("iridescenceThicknessTexture", value);
	    }

	    public int IridescenceThicknessTextureTexCoord
	    {
		    get =>  (int)_material.GetFloat("iridescenceThicknessTextureTexCoord");
		    set => _material.SetFloat("iridescenceThicknessTextureTexCoord", (float) value);
	    }

	    public double SpecularFactor
	    {
		    get => _material.GetFloat("specularFactor");
		    set => _material.SetFloat("specularFactor", (float) value);
	    }

	    public Texture SpecularTexture
	    {
		    get => _material.GetTexture("specularTexture");
		    set=> _material.SetTexture("specularTexture", value);
	    }

	    public double SpecularTextureRotation
	    {
		    get => _material.GetFloat("specularTextureRotation");
		    set => _material.SetFloat("specularTextureRotation", (float) value);
	    }

	    public Vector2 SpecularTextureOffset
	    {
		    get => _material.GetTextureOffset("specularTexture");
		    set => _material.SetTextureOffset("specularTexture", value);
	    }

	    public Vector2 SpecularTextureScale
	    {
		    get => _material.GetTextureScale("specularTexture");
		    set => _material.SetTextureScale("specularTexture", value);
	    }

	    public int SpecularTextureTexCoord
	    {
		    get =>  (int)_material.GetFloat("specularTextureTexCoord");
		    set => _material.SetFloat("specularTextureTexCoord", (float) value);
	    }

	    public Color SpecularColorFactor
	    {
		    get => _material.GetColor("specularColorFactor");
		    set => _material.SetColor("specularColorFactor", value);
	    }

	    public Texture SpecularColorTexture
	    {
		    get => _material.GetTexture("specularColorTexture");
		    set => _material.SetTexture("specularColorTexture", value);
	    }

	    public double SpecularColorTextureRotation
	    {
		    get => _material.GetFloat("specularColorTextureRotation");
		    set => _material.SetFloat("specularColorTextureRotation", (float) value);
	    }

	    public Vector2 SpecularColorTextureOffset
	    {
		    get => _material.GetTextureOffset("specularColorTexture");
		    set => _material.SetTextureOffset("specularColorTexture", value);
	    }

	    public Vector2 SpecularColorTextureScale
	    {
		    get => _material.GetTextureScale("specularColorTexture");
		    set => _material.SetTextureScale("specularColorTexture", value);
	    }

	    public int SpecularColorTextureTexCoord
	    {
		    get =>  (int)_material.GetFloat("specularColorTextureTexCoord");
		    set => _material.SetFloat("specularColorTextureTexCoord", (float) value);
	    }

	    public double ClearcoatFactor
	    {
		    get => _material.GetFloat("clearcoatFactor");
		    set => _material.SetFloat("clearcoatFactor", (float) value);
	    }

	    public Texture ClearcoatTexture
	    {
		    get => _material.GetTexture("clearcoatTexture");
		    set => _material.SetTexture("clearcoatTexture", value);
	    }

	    public double ClearcoatTextureRotation
	    {
		    get => _material.GetFloat("clearcoatTextureRotation");
		    set => _material.SetFloat("clearcoatTextureRotation", (float) value);
	    }

	    public Vector2 ClearcoatTextureOffset
	    {
		    get => _material.GetTextureOffset("clearcoatTexture");
		    set => _material.SetTextureOffset("clearcoatTexture", value);
	    }

	    public Vector2 ClearcoatTextureScale
	    {
		    get => _material.GetTextureScale("clearcoatTexture");
		    set => _material.SetTextureScale("clearcoatTexture", value);
	    }

	    public int ClearcoatTextureTexCoord
	    {
		    get =>  (int)_material.GetFloat("clearcoatTextureTexCoord");
		    set => _material.SetFloat("clearcoatTextureTexCoord", (float) value);
	    }

	    public double ClearcoatRoughnessFactor
	    {
		    get => _material.GetFloat("clearcoatRoughnessFactor");
		    set => _material.SetFloat("clearcoatRoughnessFactor", (float) value);
	    }

	    public Texture ClearcoatRoughnessTexture
	    {
		    get => _material.GetTexture("clearcoatRoughnessTexture");
		    set => _material.SetTexture("clearcoatRoughnessTexture", value);
	    }

	    public double ClearcoatRoughnessTextureRotation
	    {
		    get => _material.GetFloat("clearcoatRoughnessTextureRotation");
		    set => _material.SetFloat("clearcoatRoughnessTextureRotation", (float) value);
	    }

	    public Vector2 ClearcoatRoughnessTextureOffset
	    {
		    get => _material.GetTextureOffset("clearcoatRoughnessTexture");
		    set => _material.SetTextureOffset("clearcoatRoughnessTexture", value);
	    }

	    public Vector2 ClearcoatRoughnessTextureScale
	    {
		    get => _material.GetTextureScale("clearcoatRoughnessTexture");
		    set => _material.SetTextureScale("clearcoatRoughnessTexture", value);
	    }

	    public int ClearcoatRoughnessTextureTexCoord
	    {
		    get =>  (int)_material.GetFloat("clearcoatRoughnessTextureTexCoord");
		    set => _material.SetFloat("clearcoatRoughnessTextureTexCoord", (float) value);
	    }
	    
	    public float Dispersion
	    {
		    get =>  _material.GetFloat("dispersion");
		    set => _material.SetFloat("dispersion", value);
	    }
	    public double ThicknessTextureRotation
	    {
		    get => _material.GetFloat("thicknessTextureRotation");
		    set => _material.SetFloat("thicknessTextureRotation", (float) value);
	    }
        public override IUniformMap Clone()
        {
            var clone = new URPLitGraphMap(new Material(_material));
			clone.Material.shaderKeywords = _material.shaderKeywords;
			return clone;
        }

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

		private static readonly string[] emissivePropNames = new[] { "emissiveFactor", "_EmissionColor" };
	}
}