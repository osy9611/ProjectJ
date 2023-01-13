namespace Module.Unity.Addressables
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine.AddressableAssets;
    using UnityEngine.ResourceManagement.AsyncOperations;
    using Module.Core.Systems.Collections.Generic;

    public enum AssetBagStatus
    {
        None,
        Destroying
    }

    public delegate void AssetBagResultDelegate<T>(T asset) where T : UnityEngine.Object;

    public class AssetBag
    {
        private int m_Id;
        private Dictionary<string, AssetBagStatus> m_Values = new Dictionary<string, AssetBagStatus>();
        public int Id => m_Id;

        internal AssetBag(int id)
        {
            m_Id = id;
        }
       
    }
}