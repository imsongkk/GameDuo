using GameDuo.Managers;
using GameDuo.UI.Scene;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDuo.Scene
{
    public class MainScene : BaseScene
    {
        protected override void Init()
        {
            base.Init();
            GameManager.UI.ShowSceneUI<UI_MainScene>();
        }
    }
}