using GameDuo.UI.Popup;
using GameDuo.UI.Scene;
using GameDuo.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace GameDuo.Managers
{
    public class UIManager
    {
        int popupOrder = 10;
        Stack<UI_Popup> popupStack = new Stack<UI_Popup>();
        UI_Scene sceneUI = null;

        public GameObject Root
        {
            get
            {
                GameObject root = GameObject.Find("@UI_Root");
                if (root == null)
                    root = new GameObject { name = "@UI_Root" };
                return root;
            }
        }

        public void SetCanvas(GameObject go, bool sort = true)
        {
            Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.overrideSorting = true;
            if (sort)
                canvas.sortingOrder = popupOrder++;
            else
                canvas.sortingOrder = 0;
        }

        public T ShowSceneUI<T>(string name = null) where T : UI_Scene
        {
            if (string.IsNullOrEmpty(name))
                name = typeof(T).Name;

            GameObject go = GameManager.Resource.Instantiate($"UI/Scene/{name}");

            T sceneUI = Util.GetOrAddComponent<T>(go);
            this.sceneUI = sceneUI;
            go.transform.SetParent(Root.transform);

            return sceneUI;
        }

        public T ShowPopupUI<T>(string name = null) where T : UI_Popup
        {
            if (string.IsNullOrEmpty(name))
                name = typeof(T).Name;

            GameObject go = GameManager.Resource.Instantiate($"UI/Popup/{name}");

            T popupUI = Util.GetOrAddComponent<T>(go);
            popupStack.Push(popupUI);
            go.transform.SetParent(Root.transform);

            return popupUI;
        }

        public void ClosePopupUI(UI_Popup _popupUI)
        {
            if (popupStack.Count == 0)
                return;
            if (popupStack.Peek() != _popupUI)
            {
                Debug.Log("Close Popup Failed!");
                return;
            }

            UI_Popup popupUI = popupStack.Pop();
            GameManager.Resource.Destroy(popupUI.gameObject);
            popupUI = null;
            popupOrder--;
        }

        public void ClosePopupUI()
        {
            if (popupStack.Count == 0)
                return;
            var topUI = popupStack.Peek();
            if (!topUI.IsEscAble)
                return;

            UI_Popup popupUI = popupStack.Pop();
            GameManager.Resource.Destroy(popupUI.gameObject);
            popupUI = null;
            popupOrder--;
        }
    }
}