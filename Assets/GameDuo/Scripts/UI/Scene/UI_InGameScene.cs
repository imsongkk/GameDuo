using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using GameDuo.Utils;
using UnityEngine.UI;
using GameDuo.Managers;
using GameDuo.Components;

namespace GameDuo.UI.Scene
{
    public class UI_InGameScene : UI_Scene
    {

        public readonly int maxCreateItemCount = 10;

        enum GameObjects
        {
            SelectButton,
            EnforceButton,
            ProduceButton,
            ShopButton,
            CreateButton,

            SelectWindow,
            EnforceWindow,
            ProduceWindow,
            ShopWindow,

            NameText,
            LevelText,
            MoneyText,
            CreateText,

            ProducingBar,
            XpBar,

            AttackEnforceField,
            DefenseEnforceField,
            HeartEnforceField,

            ItemContainer,
            DraggingUI,
        }

        TextMeshProUGUI nameText, moneyText, levelText, createText;
        Image xpBar, producingBar;
        GameObject selectButton, enforceButton, produceButton, shopButton;
        GameObject selectWindow, enforceWindow, produceWindow, shopWindow;

        GameObject currentSelectedButton;
        GameObject currentWindow;

        EnforceField attackEnforce, defenseEnforce, heartEnforce;

        Transform itemContainer;

        public RectTransform DraggingUI { get; private set; }

        public override void Init()
        {
            base.Init();
            BindObjects();

            GameManager.User.UI_InGameScene = this;

            GameManager.Item.InitItemManager(this, itemContainer);

            InitData();
            UpdateCurrentSelectedButton(selectButton, selectWindow);
        }

        private void BindObjects()
        {
            Bind<GameObject>(typeof(GameObjects));

            nameText = GetObject((int)GameObjects.NameText).GetComponent<TextMeshProUGUI>();
            moneyText = GetObject((int)GameObjects.MoneyText).GetComponent<TextMeshProUGUI>();
            levelText = GetObject((int)GameObjects.LevelText).GetComponent<TextMeshProUGUI>();
            createText = GetObject((int)GameObjects.CreateText).GetComponent<TextMeshProUGUI>();

            xpBar = GetObject((int)GameObjects.XpBar).GetComponent<Image>();
            producingBar = GetObject((int)GameObjects.ProducingBar).GetComponent<Image>();

            selectButton = GetObject((int)GameObjects.SelectButton);
            AddUIEvent(selectButton, OnClickSelectButton, Define.UIEvent.Click);
            AddButtonAnim(selectButton);

            enforceButton = GetObject((int)GameObjects.EnforceButton);
            AddUIEvent(enforceButton, OnClickEnforceButton, Define.UIEvent.Click);
            AddButtonAnim(enforceButton);

            produceButton = GetObject((int)GameObjects.ProduceButton);
            AddUIEvent(produceButton, OnClickProduceButton, Define.UIEvent.Click);
            AddButtonAnim(produceButton);

            shopButton = GetObject((int)GameObjects.ShopButton);
            AddUIEvent(shopButton, OnClickShopButton, Define.UIEvent.Click);
            AddButtonAnim(shopButton);

            GameObject createButton = GetObject((int)GameObjects.CreateButton);
            AddUIEvent(createButton, OnClickCreateButton, Define.UIEvent.Click);
            AddButtonAnim(createButton);

            selectWindow = GetObject((int)GameObjects.SelectWindow);
            enforceWindow = GetObject((int)GameObjects.EnforceWindow);
            produceWindow = GetObject((int)GameObjects.ProduceWindow);
            shopWindow = GetObject((int)GameObjects.ShopWindow);

            attackEnforce = GetObject((int)GameObjects.AttackEnforceField).GetComponent<EnforceField>();
            defenseEnforce = GetObject((int)GameObjects.DefenseEnforceField).GetComponent<EnforceField>();
            heartEnforce = GetObject((int)GameObjects.HeartEnforceField).GetComponent<EnforceField>();

            itemContainer = GetObject((int)GameObjects.ItemContainer).transform;
            DraggingUI = GetObject((int)GameObjects.DraggingUI).GetComponent<RectTransform>();

            for (int i=0; i<itemContainer.childCount; i++)
            {
                var itemComponent = itemContainer.GetChild(i).GetComponent<ItemComponent>();
                itemComponent.InitItemComponent(i, GameManager.Data.UserData.Items[i]);
            }
        }

        private void OnClickCreateButton(PointerEventData obj)
        {
            var result = GameManager.Item.TryProduceItem(producingBar, () =>
            {
                createText.text = $"{GameManager.Data.UserData.CanCreateItemCount} / {maxCreateItemCount}";
            });
        }

        private void OnClickShopButton(PointerEventData obj)
        {
            UpdateCurrentSelectedButton(shopButton, shopWindow);
        }

        private void OnClickProduceButton(PointerEventData obj)
        {
            UpdateCurrentSelectedButton(produceButton, produceWindow);
        }

        private void OnClickEnforceButton(PointerEventData obj)
        {
            UpdateCurrentSelectedButton(enforceButton, enforceWindow);
        }

        private void OnClickSelectButton(PointerEventData obj)
        {
            UpdateCurrentSelectedButton(selectButton, selectWindow);
        }

        private void InitData()
        {
            var userData = GameManager.Data.UserData;
            nameText.text = userData.Name;
            moneyText.text = userData.Money.ToString();
            createText.text = userData.CanCreateItemCount.ToString();
            levelText.text = $"Level : {userData.Xp.level}";
            int curXp = userData.Xp.value;
            int maxXp = userData.Xp.maxValue;
            xpBar.fillAmount = curXp / (float)maxXp;

            InitEnforceField();

            void InitEnforceField()
            {
                attackEnforce.InitEnforceField(EnforceField.EnforceType.Attack);
                defenseEnforce.InitEnforceField(EnforceField.EnforceType.Defense);
                heartEnforce.InitEnforceField(EnforceField.EnforceType.Heart);
            }
        }

        private void UpdateCurrentSelectedButton(GameObject targetButton, GameObject targetWindow)
        {
            // 현재 선택된 버튼이면 무시
            if (currentSelectedButton == targetButton) return;

            // 이전 버튼 알파 조정
            HighlightCurrentSelectedButton(false);

            currentSelectedButton = targetButton;
            UpdateCurrentWindow(targetWindow);

            // 새로운 버튼 알파 조정
            HighlightCurrentSelectedButton(true);
        }

        private void UpdateCurrentWindow(GameObject target)
        {
            currentWindow?.SetActive(false);
            currentWindow = target;
            currentWindow.SetActive(true);
        }

        private void HighlightCurrentSelectedButton(bool isHighlight)
        {
            if (currentSelectedButton == null) return;

            var image = currentSelectedButton.GetComponent<Image>();

            if (isHighlight)
                image.color = new Color(image.color.r, image.color.g, image.color.b, 120 / 255f);
            else
                image.color = new Color(image.color.r, image.color.g, image.color.b, 255 / 255f);
        }

        public void RefreshMoneyText()
        {
            moneyText.text = GameManager.Data.UserData.Money.ToString();
        }
    }
}