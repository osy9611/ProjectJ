using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ComQViewCamera))]
public class ComQViewCameraInspector : Editor
{
    ComQViewCamera script;
    Object source;

    public override void OnInspectorGUI()
    {
        source = EditorGUILayout.ObjectField(source, typeof(Object), true);

        if(GUILayout.Button("Set Distance Information"))
        {
            if(source==null)
                return;

            script.SetDistanceEditor(source as GameObject);            
        }
        base.OnInspectorGUI();
    }

    private void OnEnable()
    {
        script= (ComQViewCamera)target;
    }
}
