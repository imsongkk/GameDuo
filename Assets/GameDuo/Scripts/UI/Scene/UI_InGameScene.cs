using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GameDuo.UI.Scene
{
    public class UI_InGameScene : UI_Scene
    {
        enum GameObjects
        {
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

            nameText = GetObject((int)GameObjects.NameText).GetComponent<TextMeshProUGUI>();
        }
    }
}