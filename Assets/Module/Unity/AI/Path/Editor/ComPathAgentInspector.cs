namespace Module.Unity.AI
{
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(ComPathAgent))]
    public class ComPathAgentInspector : Editor
    {
        ComPathAgent script;
        public override void OnInspectorGUI()
        {
            if (script.Path.Count != 0)
            {
                if (GUILayout.Button("Generate Path"))
                {
                    script.GenerateData();
                }
            }
            
            if (GUILayout.Button("ClearPath"))
            {
                script.ClearPath();
            }

            if (GUILayout.Button("ClearPathData"))
            {
                script.ClearPath();
            }

            base.OnInspectorGUI();
        }


        private void OnEnable()
        {
            script = (ComPathAgent)target;
        }
    }

}
