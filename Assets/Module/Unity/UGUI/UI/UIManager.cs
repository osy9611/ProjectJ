namespace Module.Unity.UGUI
{
    using Module.Unity.Addressables;
    using Module.Unity.Utils;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class UIManager
    {
        private int orderLayer = 10;

        private Stack<UI_Popup> popupStack = new Stack<UI_Popup>();
        private UI_Scene sceneUI;

        private ResourceManager resourceManager;

        public Transform root;

        public void Init(ResourceManager resourceManager)
        {
            this.resourceManager = resourceManager;
            if(root == null)
            {
                root = new GameObject { name = "@UI_Root" }.transform;
                Object.DontDestroyOnLoad(root);
            }           
        }

        public void SetCanvas(GameObject go, bool sort = true)
        {
            Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.overrideSorting = true;

            if(sort)
            {
                canvas.sortingOrder = orderLayer;
                orderLayer++;
            }
            else
            {
                canvas.sortingOrder = 0;
            }
        }

        public T ShowSceneUI<T>(string path = null) where T : UI_Scene
        {
            if (string.IsNullOrEmpty(path))
                return default(T);

            GameObject go = resourceManager.LoadAndInisiate(path);
            T sceneUI = Util.GetOrAddComponent<T>(go);
            sceneUI.OnSetCanvasHandler += SetCanvas;
            this.sceneUI = sceneUI;

            go.transform.SetParent(root.transform);
            return sceneUI;
        }

        public T ShowPopupUI<T>(string path = null) where T : UI_Popup
        {
            if (string.IsNullOrEmpty(path))
                return default(T);

            GameObject go = resourceManager.LoadAndInisiate(path);
            T popup = Util.GetOrAddComponent<T>(go);
            popup.OnSetCanvasHandler += SetCanvas;
            popup.OnClosePopupUIHandler += ClosePopupUI;
            popupStack.Push(popup);

            go.transform.SetParent(root.transform);

            return popup;
        }

        public void ClosePopupUI(UI_Popup popup)
        {
            if (popupStack.Count == 0)
                return;
            if(popupStack.Peek() != popup)
            {
                Debug.LogError("Close Popup Fail!");
                return;
            }

            ClosePopupUI();
        }

        public void ClosePopupUI()
        {
            if (popupStack.Count == 0)
                return;
            UI_Popup popup = popupStack.Pop();
            resourceManager.Destory(popup.transform.gameObject);
            popup = null;
            orderLayer--;
        }

        public void CloseAllPopupUI()
        {
            while (popupStack.Count > 0)
                ClosePopupUI();
        }

        public void Clear()
        {
            CloseAllPopupUI();
            sceneUI = null;
            resourceManager = null;
        }
    }

}

