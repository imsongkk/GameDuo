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
    public class InventoryComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] Image itemImage;
        [SerializeField] TextMeshProUGUI itemLevelText;

        public ItemData Item { get; set; }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("ENTER");
            GameManager.Item.Inventory = this;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("Exit");
            GameManager.Item.Inventory = null;
        }

        public void OnDrop(ItemComponent droppedItem)
        {
            if (IsExistsItem())
            {
                ItemData temp = new ItemData();
                temp = droppedItem.Item.DeepCopy();
                droppedItem.Item = Item.DeepCopy();
                droppedItem.TryShowItem();
                Item = temp;
                GameManager.Data.UserData.Item = Item;
            }
            else
            {
                Item = droppedItem.Item.DeepCopy();
                GameManager.Data.UserData.Item = Item;
                droppedItem.DeleteItem();
            }

            TryShowInventory();
        }


        void OnEnable()
        {
            Item = GameManager.Data.UserData.Item;
            TryShowInventory();
        }

        void TryShowInventory()
        {
            if (Item.level == 0) return;

            itemLevelText.text = Item.level.ToString();

            itemLevelText.gameObject.SetActive(true);
            itemImage.gameObject.SetActive(true);
        }


        bool IsExistsItem()
            => Item.level != 0;
    }
}