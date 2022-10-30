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
    public class UI_GameOverPopup : UI_Popup
    {
        enum GameObjects
        {
            GameOverButton,
        }

        public override void Init()
        {
            base.Init();
            BindObjects();
        }

        private void BindObjects()
        {
            Bind<GameObject>(typeof(GameObjects));

            GameObject gameOverButton = GetObject((int)GameObjects.GameOverButton);
            AddUIEvent(gameOverButton, OnClickGameOverButton, Define.UIEvent.Click);
            AddButtonAnim(gameOverButton);
        }

        private void OnClickGameOverButton(PointerEventData obj)
        {
            Application.Quit();
        }
    }
}