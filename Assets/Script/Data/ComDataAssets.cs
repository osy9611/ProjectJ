using Module.Automation;
using Module.Unity.AI;
using ProjectJ.ClientVariable;
using UnityEngine;

namespace ProjectJ
{
    public class ComDataAssets : MonoBehaviour
    {
        [SerializeField] ComClientVarable clientVariable;
        [SerializeField] ComTableAsset tableAsset;
        [SerializeField] ComPathData pathData;

        public ComClientVarable ClientVarable { get => clientVariable; }
        public ComTableAsset TableAsset { get => tableAsset; }

        public ComPathData PathData {get =>pathData; }

        public void Init()
        {
            pathData.Init();
        }
    }

}
