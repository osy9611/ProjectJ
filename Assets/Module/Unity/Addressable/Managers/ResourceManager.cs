namespace Module.Unity.Addressables
{
    using Module.Unity.Core;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.ResourceManagement.AsyncOperations;

    public class ResourceManager
    {
        private bool initCalled;

        public void Initialize()
        {
            if (!initCalled)
            {
                initCalled = true;
                ComLoader.Create();
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

            if (original.GetComponent<Poolable>() != null)
                return ModuleManagers.Pool.Pop(original, parent).gameObject;
            else if(original.GetComponent<Poolable>() == null && isPooling)
                return ModuleManagers.Pool.Pop(original, parent).gameObject;

            GameObject go = Object.Instantiate(original, parent);
            go.name = original.name;
            return go;
        }

        public void Destory(GameObject go)
        {
            if (go == null)
                return;

            Poolable poolable = go.GetComponent<Poolable>();
            if (poolable != null)
            {
                ModuleManagers.Pool.Push(poolable);
                return;
            }

            Object.Destroy(go);
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
            var handle = Addressables.LoadAssetAsync<T>(addressable);

            handle.WaitForCompletion();

            if (handle.Status != AsyncOperationStatus.Succeeded)
                return default(T);


            return handle.Result;
        }


        public void LoadAsset<T>(AssetReference assetRef, System.Action<T> callback, bool autoReleaseOnFail = true)
        {
            ComLoader.s_Root.StartCoroutine(CoLoadAsset<T>(assetRef, callback, autoReleaseOnFail));
        }

        public IEnumerator CoLoadAsset<T>(AssetReference assetRef, System.Action<T> callback, bool autoReleaseOnFail = true)
        {
            var handle = assetRef.LoadAssetAsync<T>();

            yield return handle;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                callback?.Invoke(handle.Result);
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

        public void LoadAsset<T>(string addessable, System.Action<T> callback, bool autoRelease = true)
        {
            ComLoader.s_Root.StartCoroutine(CoLoadAsset<T>(addessable, callback, autoRelease));
        }

        public IEnumerator CoLoadAsset<T>(string addessable, System.Action<T> callback, bool autoRelease = true)
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
        }

        public void Release(AsyncOperationHandle handle)
        {
            Addressables.Release(handle);
        }
    }
}
