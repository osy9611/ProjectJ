namespace Module.Unity.UGUI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class UI_Scene : UI_Base
    {
        public System.Action<GameObject, bool> OnSetCanvasHandler;

        public override void Init()
        {
            if (OnSetCanvasHandler != null)
                OnSetCanvasHandler.Invoke(gameObject, false);
        }
    }

}