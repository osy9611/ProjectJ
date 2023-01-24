using Module.Unity.Addressables;
using Module.Unity.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Module.Unity.Managers
{
    public class Managers : MonoBehaviour
    {
        static Managers s_instance = null;
        static Managers Instance { get { Init(); return s_instance; } }
        PoolManager pool = new PoolManager();
        ResourceManager resource = new ResourceManager();
        SpriteAtlasManager atlas = new SpriteAtlasManager();
        SceneManager scene = new SceneManager();
        public static PoolManager Pool { get => Instance.pool; }
        public static ResourceManager Resource { get => Instance.resource; }
        public static SpriteAtlasManager Atlas { get => Instance.atlas; }
        public static SceneManager Scene { get => Instance.scene; }

        private void Start()
        {
            Init();
        }

        static void Init()
        {
            if (s_instance == null)
            {
                GameObject go = GameObject.Find("@Managers");
                if (go == null)
                {
                    go = new GameObject { name = "@Managers" };
                    go.AddComponent<Managers>();
                }

                DontDestroyOnLoad(go);
                s_instance = go.GetComponent<Managers>();
                s_instance.pool.Init();

                ComLoader.Create();
                Resource.Init(Pool);
            }
        }

        public static void Clear()
        {
            Pool.ClearAll();
            Resource.ReleaseAll();
        }
    }

}
