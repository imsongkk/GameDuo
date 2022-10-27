using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using GameDuo.Utils;
using UnityEngine.UI;

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
            XpText,
            MoneyText,
        }

        TextMeshProUGUI nameText, moneyText, xpText;
        GameObject selectButton, enforceButton, produceButton, shopButton;
        GameObject selectWindow, enforceWindow, produceWindow, shopWindow;

        GameObject currentSelectedButton;
        GameObject currentWindow;


        public override void Init()
        {
            base.Init();
            BindObjects();

            UpdateCurrentSelectedButton(selectButton, selectWindow);
        }

        private void BindObjects()
        {
            Bind<GameObject>(typeof(GameObjects));

            nameText = GetObject((int)GameObjects.NameText).GetComponent<TextMeshProUGUI>();
            moneyText = GetObject((int)GameObjects.MoneyText).GetComponent<TextMeshProUGUI>();
            xpText = GetObject((int)GameObjects.XpText).GetComponent<TextMeshProUGUI>();

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
    }
}