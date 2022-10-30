using GameDuo.Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GameDuo.Components
{
    public class EnemySelectField : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI enemyAttackText;
        [SerializeField] TextMeshProUGUI enemyDefenseText;
        [SerializeField] TextMeshProUGUI enemyHeartText;
        [SerializeField] public GameObject enemySelectButton;

        public void InitEnemySelectField(EnemyInfo enemyInfo)
        {
            enemyAttackText.text = enemyInfo.attack.ToString();
            enemyDefenseText.text = enemyInfo.defense.ToString();
            enemyHeartText.text = enemyInfo.heart.ToString();
        }
    }
}