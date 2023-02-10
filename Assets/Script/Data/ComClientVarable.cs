using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectJ.ClientVariable
{
    [System.Serializable]
    public class SystemData
    {
        public string test;
    }

    public class ComClientVarable : MonoBehaviour
    {
        public SystemData systemData;
    }

}
