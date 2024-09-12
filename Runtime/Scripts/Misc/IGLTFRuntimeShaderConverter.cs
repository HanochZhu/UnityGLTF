using UnityEngine;

namespace UnityGLTF.Misc
{
    public interface IGLTFRuntimeShaderConverter
    {
        bool ConvertShader(Material material, UnityEngine.Shader gltfShader, UnityEngine.Shader newShader);
    }
}