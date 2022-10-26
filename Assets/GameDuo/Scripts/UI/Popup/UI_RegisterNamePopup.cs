using GameDuo.Managers;
using GameDuo.UI.Popup;
using GameDuo.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace GameDuo.UI.Popup
{
    public class UI_RegisterNamePopup : UI_Popup
    {
        enum GameObjects
        {
            ConfirmButton,
            NameText,
        }

        TextMeshProUGUI nameText;

        public override void Init()
        {
            base.Init();
            BindObjects();
        }

        private void BindObjects()
        {
            Bind<GameObject>(typeof(GameObjects));

            GameObject confirmButton = GetObject((int)GameObjects.ConfirmButton);
            AddUIEvent(confirmButton, OnClickConfirmButton, Define.UIEvent.Click);
            AddButtonAnim(confirmButton);

            nameText = GetObject((int)GameObjects.NameText).GetComponent<TextMeshProUGUI>();
        }

        private void OnClickConfirmButton(PointerEventData obj)
        {
            var name = nameText.text;

            GameManager.Data.UserData.name = name;
            GameManager.Data.SaveUserData();

            SceneManager.LoadScene("MainScene");
            ClosePopupUI();
        }
    }
}