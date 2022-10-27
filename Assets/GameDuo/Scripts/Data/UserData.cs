using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameDuo.Components.EnforceField;

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
                Attack = new AttackData() { level = Attack.level, value = Attack.value, upgradeCost = Attack.upgradeCost },
                Defense = new DefenseData() { level = Defense.level, value = Defense.value, upgradeCost = Defense.upgradeCost },
                Heart = new HeartData() { level = Heart.level, value = Heart.value, upgradeCost = Heart.upgradeCost },
            };
        }

        public static UserDataEntity From(UserData userData)
        {
            return new UserDataEntity()
            {
                name = userData.Name,
                money = userData.Money,
                Xp = new XpData() { level = userData.Xp.level, value = userData.Xp.value, maxValue = userData.Xp.maxValue },
                Attack = new AttackData() { level = userData.Attack.level, value = userData.Attack.value, upgradeCost = userData.Attack.upgradeCost },
                Defense = new DefenseData() { level = userData.Defense.level, value = userData.Defense.value, upgradeCost = userData.Defense.upgradeCost },
                Heart = new HeartData() { level = userData.Heart.level, value = userData.Heart.value , upgradeCost = userData.Heart.upgradeCost },
            };
        }
    }

    public abstract class UpgradeData
    {
        public int value;
        public int level;
        public int upgradeCost;

        public static List<Dictionary<int, (int, int)>> UpgradeDataList = new List<Dictionary<int, (int, int)>>()
        {
            // key : level , value : (value, upgradeCost)
            new Dictionary<int, (int, int)>() // attack
            {
                {1 , (10, 10)},
                {2 , (15, 15)},
                {3 , (25, 20)},
                {4 , (40, 25)},
                {5 , (55, 30)},
            },
            new Dictionary<int, (int, int)>() // defense
            {
                {1 , (1, 10)},
                {2 , (3, 15)},
                {3 , (6, 20)},
                {4 , (10, 25)},
                {5 , (15, 30)},
            },
            new Dictionary<int, (int, int)>() // heart
            {
                {1 , (100, 10)},
                {2 , (120, 10)},
                {3 , (160, 10)},
                {4 , (220, 10)},
                {5 , (300, 10)},
            },
        };

        public void Upgrade(EnforceType type)
        {
            var upgradeDict = UpgradeDataList[(int)type];
            upgradeDict.TryGetValue(level + 1, out var upgradeResult);

            value = upgradeResult.Item1;
            upgradeCost = upgradeResult.Item2;
            level = level + 1;
        }
    }

    [Serializable]
    public class AttackData : UpgradeData
    {
        public static AttackData CreateDefaultAttackData()
        {
            AttackData attackData = new AttackData();
            attackData.level = 1;
            UpgradeDataList[(int)EnforceType.Attack].TryGetValue(attackData.level, out var result);
            attackData.value = result.Item1;
            attackData.upgradeCost = result.Item2;
            return attackData;
        }
    }

    [Serializable]
    public class DefenseData : UpgradeData
    {
        public static DefenseData CreateDefaultDefenseData()
        {
            DefenseData defenseData = new DefenseData();
            defenseData.level = 1;
            UpgradeDataList[(int)EnforceType.Defense].TryGetValue(defenseData.level, out var result);
            defenseData.value = result.Item1;
            defenseData.upgradeCost = result.Item2;
            return defenseData;
        }
    }

    [Serializable]
    public class HeartData : UpgradeData
    {
        public static HeartData CreateDefaultHeartData()
        {
            HeartData heartData = new HeartData();
            heartData.level = 1;
            UpgradeDataList[(int)EnforceType.Heart].TryGetValue(heartData.level, out var result);
            heartData.value = result.Item1;
            heartData.upgradeCost = result.Item2;
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