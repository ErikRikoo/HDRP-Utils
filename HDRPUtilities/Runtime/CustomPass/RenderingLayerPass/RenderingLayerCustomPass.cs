using System.Collections.Generic;
using com.rikoo.HDRPUtilities.Runtime.CustomPass.Utilities.RenderingLayer;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace com.rikoo.HDRPUtilities.Runtime.CustomPass.RenderingLayerPass
{
    public class RenderingLayerCustomPass : UnityEngine.Rendering.HighDefinition.CustomPass
    {
        [SerializeField] private Material m_OverrideMaterial;

        [SerializeField] private RenderingLayerWrapper m_RenderingLayer;

        [SerializeField] private int m_PassIndex = -1;

        static ShaderTagId[] shaderTags;
    
        // It can be used to configure render targets and their clear state. Also to create temporary render target textures.
        // When empty this render pass will render to the active camera render target.
        // You should never call CommandBuffer.SetRenderTarget. Instead call <c>ConfigureTarget</c> and <c>ConfigureClear</c>.
        // The render pipeline will ensure target setup and clearing happens in an performance manner.
        protected override void Setup(ScriptableRenderContext renderContext, CommandBuffer cmd)
        {
            shaderTags = new ShaderTagId[4]
            {
                new ShaderTagId("Forward"),
                new ShaderTagId("ForwardOnly"),
                new ShaderTagId("SRPDefaultUnlit"),
                new ShaderTagId("FirstPass"),
            };
        }

        protected override void Execute(ScriptableRenderContext renderContext, CommandBuffer cmd, HDCamera hdCamera, CullingResults cullingResult)
        {
            if (m_OverrideMaterial == null || m_RenderingLayer == null)
            {
                return;
            }
        
            var result = new RendererListDesc(shaderTags, cullingResult, hdCamera.camera)
            {
                rendererConfiguration = PerObjectData.None,
                renderQueueRange = RenderQueueRange.all,
                sortingCriteria = SortingCriteria.BackToFront,
                excludeObjectMotionVectors = false,
                overrideMaterial = m_OverrideMaterial,
                overrideMaterialPassIndex = m_PassIndex,
                layerMask = ~0,
            };
        

            RendererList renderList = RendererList.Create(result);
            renderList.filteringSettings.renderingLayerMask = m_RenderingLayer.ToBitSet();
 
            HDUtils.DrawRendererList(renderContext, cmd, renderList);
            // Executed every frame for all the camera inside the pass volume
        }

        protected override void Cleanup()
        {
            // Cleanup code
        }
    
        public override IEnumerable<Material> RegisterMaterialForInspector() { yield return m_OverrideMaterial; }
    }
}