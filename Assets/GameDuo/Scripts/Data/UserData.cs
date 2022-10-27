using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDuo.Data
{
    [Serializable]
    public class UserData
    {
        string _name;
        int _money;

        AttackData _attack;
        DefenseData _defense;
        HeartData _heart;
        XpData _xp;

        public string Name { get; set; }
        public int Money { get; set; }
        public AttackData Attack { get; private set; }
        public DefenseData Defense { get; private set; }
        public HeartData Heart { get; private set; }
        public XpData Xp { get; private set; }

        public bool IsFirstUser() => string.IsNullOrEmpty(Name);

        public static UserData CreateDefaultUserData()
        {
            UserData userData = new UserData();
            userData.Name = null;
            userData.Money = 100;
            userData.Xp = XpData.CreateDefaultXpData();
            userData.Attack = AttackData.CreateDefaultAttackData();
            userData.Defense = DefenseData.CreateDefaultDefenseData();
            userData.Heart = HeartData.CreateDefaultHeartData();

            return userData;
        }

        public static UserData From(UserDataEntity entity)
        {
            entity = entity.DeepCopy();

            return new UserData()
            {
                Name = entity.name,
                Money = entity.money,
                Xp = entity.Xp,
                Attack = entity.Attack,
                Defense = entity.Defense,
                Heart = entity.Heart,
            };
        }
    }

    [Serializable]
    public class UserDataEntity
    {
        public string name;
        public int money;

        public AttackData Attack;
        public DefenseData Defense;
        public HeartData Heart;
        public XpData Xp;

        public UserDataEntity DeepCopy()
        {
            return new UserDataEntity()
            {
                name = name,
                money = money,
                Xp = new XpData() { level = Xp.level, value = Xp.value, maxValue = Xp.maxValue },
                Attack = new AttackData() { level = Attack.level, value = Attack.value },
                Defense = new DefenseData() { level = Defense.level, value = Defense.value },
                Heart = new HeartData() { level = Heart.level, value = Heart.value },
            };
        }

        public static UserDataEntity From(UserData userData)
        {
            return new UserDataEntity()
            {
                name = userData.Name,
                money = userData.Money,
                Xp = new XpData() { level = userData.Xp.level, value = userData.Xp.value, maxValue = userData.Xp.maxValue },
                Attack = new AttackData() { level = userData.Attack.level, value = userData.Attack.value },
                Defense = new DefenseData() { level = userData.Defense.level, value = userData.Defense.value },
                Heart = new HeartData() { level = userData.Heart.level, value = userData.Heart.value },
            };
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

    [Serializable]
    public class XpData
    {
        public int value;
        public int maxValue;
        public int level;

        public static Dictionary<int, int> XpDataDict = new Dictionary<int, int>() // key : level, value : maxValue
        {
            {1 , 10},
            {2 , 20},
            {3 , 30},
            {4 , 40},
            {5 , 50},
        };

        public static XpData CreateDefaultXpData()
        {
            XpData xpData = new XpData();
            xpData.level = 1;
            XpDataDict.TryGetValue(xpData.level, out int maxValue);
            xpData.maxValue = maxValue;
            return xpData;
        }
    }
}