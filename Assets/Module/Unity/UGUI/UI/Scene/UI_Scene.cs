namespace Module.Unity.UGUI
{
    using Module.Unity.UGUI.Hud;
    using Module.Unity.Utils;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    [System.Serializable]
    public class UI_Scene : UI_Base
    {
        public System.Action<GameObject, bool> OnSetCanvasHandler;
        public System.Action<UI_Popup[]> OnAddPopupHandler;

        [SerializeField] UI_Popup[] popupInfos;
        public UI_Popup[] PopupInfo { get => popupInfos; set => popupInfos = value; }


        private RectTransform hudRoot;
        [HideInInspector] public RectTransform HudRoot {get => hudRoot; set => hudRoot = value; }

        public override void Init()
        {
            if (OnSetCanvasHandler != null)
                OnSetCanvasHandler.Invoke(gameObject, false);

            if (OnAddPopupHandler != null)
                OnAddPopupHandler.Invoke(popupInfos);
        }
    }

}