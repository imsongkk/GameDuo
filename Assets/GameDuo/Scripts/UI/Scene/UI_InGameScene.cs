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
        enum GameObjects
        {
            SelectButton,
            EnforceButton,
            ProduceButton,
            ShopButton,

            SelectWindow,
            EnforceWindow,
            ProduceWindow,
            ShopWindow,

            NameText,
            LevelText,
            MoneyText,

            XpBar,

            AttackEnforceField,
            DefenseEnforceField,
            HeartEnforceField,
        }

        TextMeshProUGUI nameText, moneyText, levelText;
        Image xpBar;
        GameObject selectButton, enforceButton, produceButton, shopButton;
        GameObject selectWindow, enforceWindow, produceWindow, shopWindow;

        GameObject currentSelectedButton;
        GameObject currentWindow;

        EnforceField attackEnforce, defenseEnforce, heartEnforce;

        public override void Init()
        {
            base.Init();
            BindObjects();

            GameManager.User.UI_InGameScene = this;

            InitData();
            UpdateCurrentSelectedButton(selectButton, selectWindow);
        }

        private void BindObjects()
        {
            Bind<GameObject>(typeof(GameObjects));

            nameText = GetObject((int)GameObjects.NameText).GetComponent<TextMeshProUGUI>();
            moneyText = GetObject((int)GameObjects.MoneyText).GetComponent<TextMeshProUGUI>();
            levelText = GetObject((int)GameObjects.LevelText).GetComponent<TextMeshProUGUI>();

            xpBar = GetObject((int)GameObjects.XpBar).GetComponent<Image>();

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

            selectWindow = GetObject((int)GameObjects.SelectWindow);
            enforceWindow = GetObject((int)GameObjects.EnforceWindow);
            produceWindow = GetObject((int)GameObjects.ProduceWindow);
            shopWindow = GetObject((int)GameObjects.ShopWindow);

            attackEnforce = GetObject((int)GameObjects.AttackEnforceField).GetComponent<EnforceField>();
            defenseEnforce = GetObject((int)GameObjects.DefenseEnforceField).GetComponent<EnforceField>();
            heartEnforce = GetObject((int)GameObjects.HeartEnforceField).GetComponent<EnforceField>();
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