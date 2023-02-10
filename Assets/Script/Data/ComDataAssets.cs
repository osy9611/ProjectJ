using Module.Automation;
using ProjectJ.ClientVariable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectJ
{
    public class ComDataAssets : MonoBehaviour
    {
        [SerializeField] ComClientVarable clientVariable;
        [SerializeField] ComTableAsset tableAsset;

        public ComClientVarable ClientVarable { get => clientVariable; }
        public ComTableAsset TableAsset { get => tableAsset; }
    }

}
