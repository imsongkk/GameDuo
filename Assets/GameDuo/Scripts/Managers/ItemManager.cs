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

        public UI_InGameScene UI_InGameScene { get; set; }

        GameObject draggingUI;

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
    }
}