namespace Module.Unity.UGUI.Hud
{
    using System;
    using Module.Unity.Pivot;
    using System.Collections.Generic;
    using UnityEngine;

    public class ComHudAgent : MonoBehaviour
    {
        private PivotInfo pivotInfo;
        public Action<GameObject,bool> onDestoy;
        public virtual void Init(PivotInfo pivotInfo)
        {
            this.pivotInfo = pivotInfo;
        }

        public virtual void Execute()
        {
            CalcTranform();
        }

        protected virtual void CalcTranform()
        {
            if (pivotInfo == null)
                return;

            transform.position = Camera.main.WorldToScreenPoint(pivotInfo.PivotTr.position);
        }

        protected virtual void Destroy()
        {
            if(onDestoy != null)
                onDestoy.Invoke(this.gameObject, false);
        }
    }

}

