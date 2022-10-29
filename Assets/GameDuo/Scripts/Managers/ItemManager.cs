using GameDuo.Components;
using GameDuo.UI.Scene;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameDuo.Managers
{
    public class ItemManager
    {
        public ItemComponent CurrentCursorItem { get; private set; } = null;
        public ItemComponent CurrentDraggingItem { get; private set; } = null;

        public UI_InGameScene UI_InGameScene { get; private set; }

        GameObject draggingUI;
        Transform itemContainer;

        bool isProducing = false;

        public void InitItemManager(UI_InGameScene target, Transform itemContainer)
        {
            UI_InGameScene = target;
            this.itemContainer = itemContainer;
        }

        public void OnBeginDrag(ItemComponent draggingItem)
        {
            CurrentDraggingItem = draggingItem;
            draggingUI = UI_InGameScene.DraggingUI.gameObject;
            draggingUI.SetActive(true);
            draggingUI.GetComponentInChildren<TextMeshProUGUI>().text = draggingItem.Item.level.ToString();
        }

        public void OnEndDrag()
        {
            CurrentDraggingItem = null;
            UI_InGameScene.DraggingUI.gameObject.SetActive(false);
        }

        public void OnDrag(PointerEventData evt)
        {
            UI_InGameScene.DraggingUI.position = evt.position;
        }

        public void OnPointerEnter(ItemComponent enterItem)
        {
            CurrentCursorItem = enterItem;
        }

        public void OnPointerExit()
        {
            CurrentCursorItem = null;
        }

        public bool TryProduceItem(Image producingBar, Action UIRefreshAction)
        {
            if (GameManager.Data.UserData.CanCreateItemCount <= 0) return false;

            for(int i=0; i<itemContainer.childCount; i++)
            {
                var itemComponent = itemContainer.GetChild(i).GetComponent<ItemComponent>();

                if(itemComponent.CanCreateItem())
                {
                    itemComponent.CreateItem();
                    UIRefreshAction?.Invoke();
                    if (!isProducing)
                        GameManager.Instance.StartCoroutine(ProduceItem(producingBar, UIRefreshAction));
                    return true;
                }
            }

            return false;
        }

        IEnumerator ProduceItem(Image producingBar, Action UIRefreshAction)
        {
            if (GameManager.Data.UserData.CanCreateItemCount >= 10) yield break;

            isProducing = true;
            while(true)
            {
                if (producingBar.fillAmount >= 1f)
                {
                    producingBar.fillAmount = 0f;
                    isProducing = false;
                    GameManager.Data.UserData.CanCreateItemCount += 1;
                    UIRefreshAction?.Invoke();
                    if (GameManager.Data.UserData.CanCreateItemCount < 10)
                        GameManager.Instance.StartCoroutine(ProduceItem(producingBar, UIRefreshAction));
                    yield break;
                }
                producingBar.fillAmount += 0.01f;
                yield return null;
            }
        }
    }
}