using GameDuo.Data;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace GameDuo.Managers
{
    public class DataManager
    {
        public readonly string UserDataJsonName = "\\UserData.json";

        public UserData UserData { get; private set; } = null;

        public UserData TryLoadUserData()
        {
            bool isExistsUserData = File.Exists(Application.persistentDataPath + UserDataJsonName);
            if (!isExistsUserData) // 데이터 없으면 새로 생성
                UserData = CreateUserData();
            else
                UserData = LoadUserData();

            return UserData;
        }

        private UserData CreateUserData()
        {
            var createdUserData = UserData.CreateDefaultUserData();
            File.WriteAllText(Application.persistentDataPath + UserDataJsonName, JsonUtility.ToJson(createdUserData));
            return createdUserData;
        }

        private UserData LoadUserData()
        {
            var userDataStr = File.ReadAllText(Application.persistentDataPath + UserDataJsonName);
            var userDataEntity = JsonUtility.FromJson<UserDataEntity>(userDataStr);
            return UserData.From(userDataEntity);
        }

        public void SaveUserData()
        {
            var userDataEntity = UserDataEntity.From(UserData);
            File.WriteAllText(Application.persistentDataPath + UserDataJsonName, JsonUtility.ToJson(userDataEntity));
        }
    }
}