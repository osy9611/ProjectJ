using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GrabPassFeature : ScriptableRendererFeature
{
    [System.Serializable]
    public class GrabPassSetting
    {
        //렌터 패스가 실행되는 시기를 제어한다.
        public RenderPassEvent Event = RenderPassEvent.AfterRenderingTransparents;
    }

    class GrabPass : ScriptableRenderPass
    {
        static readonly string k_RenderTag = "grab pass";
        RenderTargetIdentifier currentTarget;
        RenderTargetHandle tempColorTarget;
        string m_GrabPassName = "_GrabPassTexture";
        public GrabPass(GrabPassSetting setting)
        {
            renderPassEvent = setting.Event;
            tempColorTarget.Init(m_GrabPassName);
        }

        public void SetUp(RenderTargetIdentifier currentTarget)
        {
            this.currentTarget = currentTarget;
        }

        public override void OnCameraCleanup(CommandBuffer cmd)
        {
            
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            var cmd = CommandBufferPool.Get(k_RenderTag);

            cmd.GetTemporaryRT(tempColorTarget.id, Screen.width, Screen.height);
            cmd.SetGlobalTexture(m_GrabPassName, tempColorTarget.Identifier());
            Blit(cmd, currentTarget, tempColorTarget.Identifier());

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }
    }

    private GrabPass m_ScriptablePass;
    public GrabPassSetting m_Setting;

    public override void Create()
    {
        m_ScriptablePass = new GrabPass(m_Setting);
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (renderingData.cameraData.isSceneViewCamera)
            return;

        if (renderingData.postProcessingEnabled == false)
            return;

        m_ScriptablePass.SetUp(renderer.cameraColorTarget);
        renderer.EnqueuePass(m_ScriptablePass);
    }
}


