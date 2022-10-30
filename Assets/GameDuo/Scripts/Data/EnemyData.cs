using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDuo.Data
{
    public class EnemyData
    {
        public List<EnemyInfo> enemies = new List<EnemyInfo>()
        {
            new EnemyInfo()
            {
                attack = 10,
                defense = 1,
                heart = 20,
            },
            new EnemyInfo()
            {
                attack = 15,
                defense = 2,
                heart = 30,
            },
            new EnemyInfo()
            {
                attack = 20,
                defense = 3,
                heart = 40,
            },
            new EnemyInfo()
            {
                attack = 25,
                defense = 4,
                heart = 50,
            },
            new EnemyInfo()
            {
                attack = 30,
                defense = 5,
                heart = 60,
            },
            new EnemyInfo()
            {
                attack = 35,
                defense = 6,
                heart = 70,
            },
        };
    }

    public class EnemyInfo
    {
        public int attack;
        public int defense;
        public int heart;
    }
}