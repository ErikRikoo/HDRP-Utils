using com.rikoo.HDRPUtilities.Runtime.CustomPass.Utilities.RenderingLayer;
using UnityEngine;

namespace com.rikoo.HDRPUtilities.Runtime.CustomPass.Utilities.RenderingLayer
{
    [RequireComponent(typeof(Renderer))]
    public class RenderingLayerEnabler : MonoBehaviour
    {
        [SerializeField] private RenderingLayerWrapper m_RenderingLayer;

        private uint m_OriginalLayer;
        private Renderer m_Renderer;

        private void Awake()
        {
            m_Renderer = GetComponent<Renderer>();
        }

        private void OnEnable()
        {
            m_OriginalLayer = m_Renderer.renderingLayerMask;
            m_Renderer.renderingLayerMask |= m_RenderingLayer.ToBitSet();
        }

        private void OnDisable()
        {
            m_Renderer.renderingLayerMask = m_OriginalLayer;
        }
    }
}
