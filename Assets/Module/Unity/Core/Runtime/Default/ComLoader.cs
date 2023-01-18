namespace Module.Unity.Core
{
    using UnityEngine;

    public class ComLoader : MonoBehaviour
    {
        static public ComLoader s_Root;

        private void Awake()
        {
            s_Root = this;
        }

        private void OnDestroy()
        {
            s_Root = null;
        }

        static public ComLoader Create()
        {
            if (ComLoader.s_Root) return ComLoader.s_Root;

            GameObject go = new GameObject("[Lojr]");
            DontDestroyOnLoad(go);
            return go.AddComponent<ComLoader>();
        }
    }
}