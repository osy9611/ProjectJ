namespace Module.Unity.Core
{
    using UnityEngine;

    public class ComLojr : MonoBehaviour
    {
        static public ComLojr s_Root;

        private void Awake()
        {
            s_Root = this;
        }

        private void OnDestroy()
        {
            s_Root = null;
        }

        static public ComLojr Create()
        {
            if (ComLojr.s_Root) return ComLojr.s_Root;

            GameObject go = new GameObject("[Lojr]");
            DontDestroyOnLoad(go);
            return go.AddComponent<ComLojr>();
        }
    }
}