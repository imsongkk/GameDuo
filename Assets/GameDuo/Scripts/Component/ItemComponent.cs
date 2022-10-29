using GameDuo.Data;
using GameDuo.Managers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameDuo.Components
{
    public class ItemComponent : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] Image itemImage;
        [SerializeField] TextMeshProUGUI itemLevelText;
        public ItemData Item { get; set; }
        
        int index;

        public void InitItemComponent(int index, ItemData item)
        {
            this.index = index;
            Item = item;

            TryShowItem();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!IsDragable()) return;

            GameManager.Item.OnBeginDrag(this);
            HideItem();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            GameManager.Item.OnEndDrag();
            if (GameManager.Item.CurrentCursorItem == null || GameManager.Item.CurrentCursorItem == this) // 이상한 곳에 드래그
                TryShowItem();
            else
                GameManager.Item.CurrentCursorItem.OnDrop(eventData, this);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if(IsDragable())
                GameManager.Item.OnDrag(eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            GameManager.Item.OnPointerExit();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            GameManager.Item.OnPointerEnter(this);
        }

        public void OnDrop(PointerEventData evt, ItemComponent droppedComponent)
        {
            if (this == droppedComponent) return; // 자기 자신 무시

            ItemData.TryMerge(droppedComponent.Item, Item, out var mergedItem);

            if(mergedItem != null)
            {
                droppedComponent.DeleteItem();
                this.Item = mergedItem;
                TryShowItem();
            }
            else
                droppedComponent.TryShowItem();
        }

        private bool IsDragable()
            => Item.level != 0;

        public bool CanCreateItem()
            => Item.level == 0;

        public void CreateItem()
        {
            Item = ItemData.CreateFirstItem();
            TryShowItem();
        }

        public void DeleteItem()
        {
            HideItem();
            Item = ItemData.CreateDefaultItemData();
            Debug.Log(Item.level);
        }

        private void TryShowItem()
        {
            if (Item.level == 0) return;

            itemImage.gameObject.SetActive(true);
            itemLevelText.gameObject.SetActive(true);

            itemLevelText.text = Item.level.ToString();
        }

        private void HideItem()
        {
            itemImage.gameObject.SetActive(false);
            itemLevelText.gameObject.SetActive(false);
        }
    }
}