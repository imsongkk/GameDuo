using GameDuo.Managers;
using GameDuo.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace GameDuo.UI.Scene
{
    public class UI_MainScene : UI_Scene
    {
        enum GameObjects
        {
            StartButton,
            DeleteButton,
            QuitButton,
        }

        public override void Init()
        {
            base.Init();
            BindObjects();
        }

        private void BindObjects()
        {
            Bind<GameObject>(typeof(GameObjects));

            GameObject startButton = GetObject((int)GameObjects.StartButton);
            AddUIEvent(startButton, OnClickStartButton, Define.UIEvent.Click);
            AddButtonAnim(startButton);

            GameObject deleteButton = GetObject((int)GameObjects.DeleteButton);
            AddUIEvent(deleteButton, OnClickDeleteButton, Define.UIEvent.Click);
            AddButtonAnim(deleteButton);

            GameObject quitButton = GetObject((int)GameObjects.QuitButton);
            AddUIEvent(quitButton, OnClickQuitButton, Define.UIEvent.Click);
            AddButtonAnim(quitButton);
        }

        private void OnClickQuitButton(PointerEventData obj)
        {
            Application.Quit();
        }

        private void OnClickDeleteButton(PointerEventData obj)
        {
            GameManager.DeleteGameData();
        }

        private void OnClickStartButton(PointerEventData obj)
        {
            SceneManager.LoadScene("InGameScene");
        }
    }
}