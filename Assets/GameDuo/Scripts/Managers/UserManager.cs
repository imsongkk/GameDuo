using GameDuo.Data;
using GameDuo.UI.Scene;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameDuo.Components.EnforceField;

namespace GameDuo.Managers
{
    public class UserManager 
    {
        public UI_InGameScene UI_InGameScene { get; set; }
        public Player Player { get; set; }

        public void EnemyKill(int xp)
        {
            GameManager.Data.UserData.Xp.value += xp;
            var result = GameManager.Data.UserData.Xp.TryLevelUp();

            UI_InGameScene.RefreshXp(GameManager.Data.UserData.Xp.value, 
                GameManager.Data.UserData.Xp.GetMaxXp(GameManager.Data.UserData.Xp.level),
                GameManager.Data.UserData.Xp.level);
        }

        public bool TryUpgrade(EnforceType type)
        {
            UpgradeData upgradeData = null;

            if (type == EnforceType.Attack)
                upgradeData = GameManager.Data.UserData.Attack;
            else if (type == EnforceType.Defense)
                upgradeData = GameManager.Data.UserData.Defense;
            else if (type == EnforceType.Heart)
                upgradeData = GameManager.Data.UserData.Heart;

            if (upgradeData.level == 5) return false;

            if (GameManager.Data.UserData.Money >= upgradeData.upgradeCost)
            {
                GameManager.Data.UserData.Money -= upgradeData.upgradeCost;
                UI_InGameScene.RefreshMoneyText();

                upgradeData.Upgrade(type);
                Player.RefreshHP();

                return true;
            }
            return false;
        }

        public void TryUpgradeAttack()
        {

        }

        public void TryUpgradeDefense()
        {


        }

        public void TryUpgradeHeart()
        {

        }
    }
}