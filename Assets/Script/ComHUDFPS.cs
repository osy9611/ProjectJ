using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class ComHUDFPS : MonoBehaviour
//{
//    const float fpsMeasurePeriod = 0.5f;

//    public Vector2 m_RectOffset;
//    public TextAnchor m_Alignment;

//    private int m_FpsAccumulator = 0;
//    private float m_FpsNextPeriod = 0;
//    private int m_CurrentFps;

//    private GUIStyle mStyle;

//    void Awake()
//    {
//        mStyle = new GUIStyle();
//        mStyle.alignment = m_Alignment;
//        mStyle.normal.background = null;
//        mStyle.fontSize = 25;
//        mStyle.normal.textColor = new Color(0f, 1f, 0f, 1.0f);
//    }

//    private void Start()
//    {
//        m_FpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;
//    }

//    private void Update()
//    {
//        // measure average frames per second
//        m_FpsAccumulator++;
//        if (Time.realtimeSinceStartup > m_FpsNextPeriod)
//        {
//            m_CurrentFps = (int)(m_FpsAccumulator / fpsMeasurePeriod);
//            m_FpsAccumulator = 0;
//            m_FpsNextPeriod += fpsMeasurePeriod;
//        }
//    }

//    void OnGUI()
//    {
//        int w = Screen.width;
//        int h = Screen.height;
//        float wOffset = m_RectOffset.x * 0.5f;
//        float hOffset = m_RectOffset.y * 0.5f;
//        Rect rect = new Rect(wOffset, hOffset, w - m_RectOffset.x, (h - hOffset) * 2 / 100);
//        string text = string.Format("   {0:0.} FPS", m_CurrentFps);
//        mStyle.alignment = m_Alignment;
//        GUI.Label(rect, text, mStyle);
//    }
//}

public class ComHUDFPS : MonoBehaviour
{
    const float fpsMeasurePeriod = 0.5f;

    public Vector2 m_RectOffset;
    public TextAnchor m_Alignment;

    private int m_FpsAccumulator = 0;
    private float m_FpsNextPeriod = 0;
    private int m_CurrentFps;

    private GUIStyle mStyle;

    void Awake()
    {
        mStyle = new GUIStyle();
        mStyle.alignment = m_Alignment;
        mStyle.normal.background = null;
        mStyle.fontSize = 25;
        mStyle.normal.textColor = new Color(0f, 1f, 0f, 1.0f);

#if UNITY_ANDROID
        Application.targetFrameRate = 60;
#endif
    }

    private void Start()
    {
        m_FpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;
    }

    private void Update()
    {
        // measure average frames per second
        m_FpsAccumulator++;
        if (Time.realtimeSinceStartup > m_FpsNextPeriod)
        {
            m_CurrentFps = (int)(m_FpsAccumulator / fpsMeasurePeriod);
            m_FpsAccumulator = 0;
            m_FpsNextPeriod += fpsMeasurePeriod;
        }
    }

    void OnGUI()
    {
        int w = Screen.width;
        int h = Screen.height;
        float wOffset = m_RectOffset.x * 0.5f;
        float hOffset = m_RectOffset.y * 0.5f;
        Rect rect = new Rect(wOffset, hOffset, w - m_RectOffset.x, (h - hOffset) * 2 / 100);
        string text = string.Format("   {0:0.} FPS", m_CurrentFps);
        mStyle.alignment = m_Alignment;
        GUI.Label(rect, text, mStyle);
    }
}
