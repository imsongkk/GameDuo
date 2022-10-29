using GameDuo.Components;
using GameDuo.UI.Scene;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameDuo.Managers
{
    public class ItemManager
    {
        public ItemComponent CurrentCursorItem { get; private set; } = null;
        public ItemComponent CurrentDraggingItem { get; private set; } = null;

        public UI_InGameScene UI_InGameScene { get; private set; }

        GameObject draggingUI;
        Transform itemContainer;

        public readonly int maxCount = 10;
        int canCreateItemCount = 10;

        public void InitItemManager(UI_InGameScene target, Transform itemContainer)
        {
            UI_InGameScene = target;
            this.itemContainer = itemContainer;

            canCreateItemCount = GameManager.Data.UserData.CanCreateItemCount;

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

        public bool TryProduceItem(TextMeshProUGUI uiText)
        {
            for(int i=0; i<itemContainer.childCount; i++)
            {
                var itemComponent = itemContainer.GetChild(i).GetComponent<ItemComponent>();

                if(itemComponent.CanCreateItem())
                {
                    Debug.Log(i);
                    Debug.Log(itemComponent.name);
                    itemComponent.CreateItem();
                    return true;
                }
            }

            return false;
        }
    }
}