namespace Module.Unity.Addressables
{
    using Module.Unity.Core;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.ResourceManagement.AsyncOperations;

    public class ResourceManager
    {
        private Dictionary<string, AsyncOperationHandle> datas = new Dictionary<string, AsyncOperationHandle>();
        private Func<GameObject, Transform, Poolable> popFunc = null;
        private Func<Poolable,bool> pushFunc = null;
        private bool initCalled;

        public void Initialize(PoolManager pool)
        {
            if (!initCalled)
            {
                initCalled = true;
                ComLoader.Create();
                popFunc = pool.Pop;
                pushFunc = pool.Push;
            }
        }

        public GameObject Inisiate(string path, Transform parent = null, bool isPooling = false)
        {
            GameObject original = LoadAndGet<GameObject>(path);
            if (original == null)
            {
                Debug.Log($"Fail to load prefab : {path} ");
                return null;
            }

            Poolable poolable = original.GetComponent<Poolable>();

            if (popFunc != null && poolable != null)
            {
                return popFunc(original, parent).gameObject;
            }
            else if(popFunc != null && poolable==null && isPooling)
            {
                return popFunc(original, parent).gameObject;
            }

            GameObject go = UnityEngine.Object.Instantiate(original, parent);
            go.name = original.name;
            return go;
        }

        public void Destory(GameObject go)
        {
            if (go == null)
                return;

            Poolable poolable = go.GetComponent<Poolable>();
            if (poolable != null && pushFunc != null)
            {
                pushFunc(poolable);
                return;
            }

            UnityEngine.Object.Destroy(go);
        }

        public void LoadScene(string key, UnityEngine.SceneManagement.LoadSceneMode loadMode, System.Action<bool> resultCallback)
        {
            ComLoader.s_Root.StartCoroutine(CoLoadSceneAsync(key, loadMode, resultCallback));
        }

        public IEnumerator CoLoadSceneAsync(string key, UnityEngine.SceneManagement.LoadSceneMode loadMode, System.Action<bool> resultCallback)
        {
            var handle = Addressables.LoadSceneAsync(key, loadMode);

            yield return handle;

            if (handle.Status == AsyncOperationStatus.Succeeded)
                resultCallback(true);
            else
                resultCallback(false);
        }

        public T LoadAndGet<T>(string addressable)
        {
            if (datas.ContainsKey(addressable))
                return (T)datas[addressable].Result;

            var handle = Addressables.LoadAssetAsync<T>(addressable);

            handle.WaitForCompletion();

            if (handle.Status != AsyncOperationStatus.Succeeded)
                return default(T);

            datas.Add(addressable, handle); 
            return handle.Result;
        }

        public void LoadAsset<T>(AssetReference assetRef, System.Action<T> callback, bool autoReleaseOnFail = true)
        {
            ComLoader.s_Root.StartCoroutine(CoLoadAsset<T>(assetRef, callback, autoReleaseOnFail));
        }

        public IEnumerator CoLoadAsset<T>(AssetReference assetRef, System.Action<T> callback, bool autoReleaseOnFail = true)
        {
            AsyncOperationHandle handle;
            if(datas.ContainsKey(assetRef.AssetGUID))
            {
                handle = datas[assetRef.AssetGUID];
                callback?.Invoke((T)handle.Result);
            }
            else
            {
                handle = assetRef.LoadAssetAsync<T>();

                yield return handle;

                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    callback?.Invoke((T)handle.Result);
                }
                else
                {
                    if (autoReleaseOnFail)
                    {
                        assetRef.ReleaseAsset();
                    }

                    callback?.Invoke(default(T));
                }
            }
        }

        public void LoadAsset<T>(string addessable, System.Action<T> callback, bool autoRelease = true)
        {
            ComLoader.s_Root.StartCoroutine(CoLoadAsset<T>(addessable, callback, autoRelease));
        }

        public IEnumerator CoLoadAsset<T>(string addessable, System.Action<T> callback, bool autoRelease = true)
        {
            if (datas.ContainsKey(addessable))
            {
                var handle = datas[addessable];
                callback?.Invoke((T)handle.Result);
            }
            else
            {
                var handle = Addressables.LoadAssetAsync<T>(addessable);

                yield return handle;

                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    callback?.Invoke(handle.Result);
                }
                else
                {
                    callback?.Invoke(default(T));
                }

                if (autoRelease)
                {
                    if (handle.IsValid())
                        Addressables.Release(handle);
                }
                else
                {
                    datas.Add(addessable, handle);
                }
            }
        }

        public void Release(AsyncOperationHandle handle)
        {
            Addressables.Release(handle);
        }

        public void Release(string addressable)
        {
            Addressables.Release(datas[addressable]);
        }

        public void ReleaseAll()
        {
            foreach(var data in datas.Values)
            {
                Release(data);
            }
            datas.Clear();
        }
    }
}
