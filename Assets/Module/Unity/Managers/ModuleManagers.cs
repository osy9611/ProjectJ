using Module.Unity.Addressables;
using Module.Unity.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

namespace Module.Unity.Managers
{
    public class ModuleManagers : MonoBehaviour
    {
        static ModuleManagers s_instance = null;
        static ModuleManagers Instance { get { Init(); return s_instance; } }
        PoolManager pool = new PoolManager();
        ResourceManager resource = new ResourceManager();
        SpriteAtlasManager atlas = new SpriteAtlasManager();

        public static PoolManager Pool { get => Instance.pool; }
        public static ResourceManager Resource { get => Instance.resource; }
        public static SpriteAtlasManager Atlas { get => Instance.atlas; }


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
                    go.AddComponent<ModuleManagers>();
                }

                DontDestroyOnLoad(go);
                s_instance = go.GetComponent<ModuleManagers>();
                s_instance.pool.Init();
            }
        }

        public static void Clear()
        {
            Pool.Clear();
        }
    }

}
