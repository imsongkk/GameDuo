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
                xp = 1,
            },
            new EnemyInfo()
            {
                attack = 15,
                defense = 2,
                heart = 30,
                xp = 2,
            },
            new EnemyInfo()
            {
                attack = 20,
                defense = 3,
                heart = 40,
                xp = 3,
            },
            new EnemyInfo()
            {
                attack = 25,
                defense = 4,
                heart = 50,
                xp = 4,
            },
            new EnemyInfo()
            {
                attack = 30,
                defense = 5,
                heart = 60,
                xp = 5,
            },
            new EnemyInfo()
            {
                attack = 35,
                defense = 6,
                heart = 70,
                xp = 6,
            },
        };
    }

    public class EnemyInfo
    {
        public int attack;
        public int defense;
        public int heart;
        public int xp;
    }
}