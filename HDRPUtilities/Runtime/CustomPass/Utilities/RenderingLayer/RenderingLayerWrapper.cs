using UnityEngine;

namespace com.rikoo.HDRPUtilities.Runtime.CustomPass.Utilities.RenderingLayer
{
    [CreateAssetMenu(fileName = "RenderingLayer", menuName = "RenderingLayer", order = 0)]
    public class RenderingLayerWrapper : ScriptableObject
    {
        [Range(0, 31)]
        [SerializeField] public uint LayerIndex;

        public uint ToBitSet()
        {
            return (uint)(1 << (int) LayerIndex);
        }
    }
}