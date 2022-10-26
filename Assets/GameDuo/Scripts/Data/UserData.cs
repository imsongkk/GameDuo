using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDuo.Data
{
    [Serializable]
    public class UserData
    {
        public string name;
        public float xp;
        public int money;
        public AttackData Attack;
        public DefenseData Defense;
        public HeartData Heart;

        public bool IsFirstUser() => string.IsNullOrEmpty(name);

        public void Save()
        {

        }

        public static UserData CreateDefaultUserData()
        {
            UserData userData = new UserData();
            userData.name = null;
            userData.xp = 0;
            userData.money = 100;
            userData.Attack = AttackData.CreateDefaultAttackData();
            userData.Defense = DefenseData.CreateDefaultDefenseData();
            userData.Heart = HeartData.CreateDefaultHeartData();

            return userData;
        }
    }

    [Serializable]
    public class AttackData
    {
        public int value;
        public int level;

        public static Dictionary<int, int> AttackDataDict = new Dictionary<int, int>() // key : level, value : value
        {
            {1 , 10},
            {2 , 15},
            {3 , 20},
            {4 , 25},
            {5 , 30},
        };

        public static AttackData CreateDefaultAttackData()
        {
            AttackData attackData = new AttackData();
            attackData.level = 1;
            AttackDataDict.TryGetValue(attackData.level, out int attackValue);
            attackData.value = attackValue;
            return attackData;
        }
    }

    [Serializable]
    public class DefenseData
    {
        public int value;
        public int level;

        public static Dictionary<int, int> DefenseDataDict = new Dictionary<int, int>() // key : level, value : value
        {
            {1 , 1},
            {2 , 3},
            {3 , 5},
            {4 , 7},
            {5 , 9},
        };

        public static DefenseData CreateDefaultDefenseData()
        {
            DefenseData defenseData = new DefenseData();
            defenseData.level = 1;
            DefenseDataDict.TryGetValue(defenseData.level, out int defenseValue);
            defenseData.value = defenseValue;
            return defenseData;
        }
    }

    [Serializable]
    public class HeartData
    {
        public int value;
        public int level;

        public static Dictionary<int, int> HeartDataDict = new Dictionary<int, int>() // key : level, value : value
        {
            {1 , 100},
            {2 , 120},
            {3 , 140},
            {4 , 160},
            {5 , 180},
        };

        public static HeartData CreateDefaultHeartData()
        {
            HeartData heartData = new HeartData();
            heartData.level = 1;
            HeartDataDict.TryGetValue(heartData.level, out int heartValue);
            heartData.value = heartValue;
            return heartData;
        }
    }
}