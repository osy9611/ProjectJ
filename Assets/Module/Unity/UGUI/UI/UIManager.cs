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
        private Dictionary<string, UI_Popup> PopupInfos = new Dictionary<string, UI_Popup>();
        private UI_Scene sceneUI;

        private ResourceManager resourceManager;

        private Transform root;

        public void Init(ResourceManager resourceManager)
        {
            this.resourceManager = resourceManager;
            if (root == null)
            {
                root = new GameObject { name = "@UI_Root" }.transform;
                Object.DontDestroyOnLoad(root);
            }
        }

        public T Get<T>() where T : UI_Scene
        {
            if (sceneUI == null)
                return default(T);

            return sceneUI as T;
        }

        public void SetCanvas(GameObject go, bool sort = true)
        {
            Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.overrideSorting = true;

            if (sort)
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

            GameObject go = resourceManager.LoadAndPool(path, root.transform, 1);
            T sceneUI = Util.GetOrAddComponent<T>(go);
            sceneUI.OnSetCanvasHandler += SetCanvas;
            sceneUI.OnAddPopupHandler += AddPopupUI;
            this.sceneUI = sceneUI;
            return sceneUI;
        }

        public void ShowPopupUI<T>(string name = null) where T : UI_Popup
        {
            if (string.IsNullOrEmpty(name))
                return;

            if(PopupInfos.TryGetValue(name, out var info))
            {
                info.gameObject.SetActive(true);
                popupStack.Push(info);
            }
        }

        public void AddPopupUI(UI_Popup[] popupInfos)
        {
            foreach (var popup in popupInfos)
            {
                if (popup == null)
                    continue;

                popup.gameObject.SetActive(false);

                popup.OnSetCanvasHandler += SetCanvas;
                popup.OnClosePopupUIHandler += ClosePopupUI;
                PopupInfos.Add(popupInfos.GetType().Name, popup);
            }
        }

        public void CloseSceneUI()
        {
            if (this.sceneUI == null)
                return;

            resourceManager.Destory(this.sceneUI.gameObject);
        }

        public void ClosePopupUI(UI_Popup popup)
        {
            if (popupStack.Count == 0)
                return;
            if (popupStack.Peek() != popup)
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
            popupStack.Pop();
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

