using GameDuo.Data;
using GameDuo.Managers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GameDuo.Components
{
    public class EnforceField : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI levelText;
        [SerializeField] TextMeshProUGUI valueText;
        [SerializeField] TextMeshProUGUI nextValueText;
        [SerializeField] TextMeshProUGUI costText;

        public enum EnforceType
        {
            Attack,
            Defense,
            Heart,
        }

        public EnforceType Type { get; set; }

        public void InitEnforceField(EnforceType type)
        {
            var userData = GameManager.Data.UserData;
            Type = type;
            switch(Type)
            {
                case EnforceType.Attack:
                    Init(userData.Attack, type);
                    break;
                case EnforceType.Defense:
                    Init(userData.Defense, type);
                    break;
                case EnforceType.Heart:
                    Init(userData.Heart, type);
                    break;
            }
        }

        private void Init(UpgradeData upgradeData, EnforceType type)
        {
            levelText.text = upgradeData.level.ToString();
            valueText.text = upgradeData.value.ToString();
            costText.text = upgradeData.upgradeCost.ToString();

            if (upgradeData.level == 5) // 최대 강화
            {
                //costText.gameObject.SetActive(false);
                costText.text = "MAX";
                nextValueText.gameObject.SetActive(false);
            }
            else
            {
                UpgradeData.UpgradeDataList[(int)type].TryGetValue(upgradeData.level + 1, out var result);
                nextValueText.text = (result.Item1 - upgradeData.value).ToString();
            }
        }

        public void OnClick()
        {
            var result = GameManager.User.TryUpgrade(Type);
            if(result)
                InitEnforceField(Type);
        }
    }
}