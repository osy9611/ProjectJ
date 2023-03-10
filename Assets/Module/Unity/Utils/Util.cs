namespace Module.Unity.Utils
{
    using Module.Unity.Core;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Util
    {
        public static T GetOrAddComponent<T>(GameObject go) where T : Component
        {
            T component = go.GetComponent<T>();
            if (component == null)
                component = go.AddComponent<T>();
            return component;
        }

        public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
        {
            Transform transform = FindChild<Transform>(go, name, recursive);
            if (transform == null)
                return null;

            return transform.gameObject;
        }

        public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
        {
            if(go == null) 
                return null;

            if(!recursive)
            {
                for(int i=0,range = go.transform.childCount;i<range;++i) 
                {
                    Transform transform = go.transform.GetChild(i);
                    if(string.IsNullOrEmpty(name) || transform.name == name)
                    {
                        T component = transform.GetComponent<T>();
                        if(component !=null)
                            return component;
                    }
                }
            }
            else
            {
                foreach(T component in go.GetComponentsInChildren<T>())
                {
                    if (string.IsNullOrEmpty(name) || component.name == name)
                        return component;
                }
            }

            return null;
        }


        static public void Wait(MonoBehaviour mono, float time, System.Action action)
        {
            mono.StartCoroutine(WaitCorutine(time, action));
        }

        static internal IEnumerator WaitCorutine(float time, System.Action action = null)
        {
            yield return YieldCache.WaitForSeconds(time);

            if (action != null)
                action();
        }


        static public void RandomFloat(float min, float max, out float target)
        {
            target = Random.Range(min, max);
        }

    }

}
