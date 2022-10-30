using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameDuo.Managers
{
    public class GameManager : MonoBehaviour
    {
        static GameManager instance;
        public static GameManager Instance { get { Init(); return instance; } }

        public static Action OnUpdate;

        ResourceManager resourceManager = new ResourceManager();
        UIManager uiManager = new UIManager();
        DataManager dataManager = new DataManager();
        UserManager userManager = new UserManager();
        ItemManager itemManager = new ItemManager();
        EnemyManager enemyManager = new EnemyManager();

        public static ResourceManager Resource { get => Instance.resourceManager; }
        public static UIManager UI { get => Instance.uiManager; }
        public static DataManager Data { get => Instance.dataManager; }
        public static UserManager User { get => Instance.userManager; }
        public static ItemManager Item { get => Instance.itemManager; }
        public static EnemyManager Enemy { get => Instance.enemyManager;}

        public static void Init()
        {
            if (instance == null)
            {
                GameObject go = GameObject.Find("@Managers");
                if (go == null)
                {
                    go = new GameObject { name = "@Managers" };
                    go.AddComponent<GameManager>();
                }
                instance = go.GetComponent<GameManager>();

                DontDestroyOnLoad(instance.gameObject);
            }
        }

        public void Update()
        {
            OnUpdate?.Invoke();
        }

        public static void DeleteGameData()
        {
            File.Delete(Application.persistentDataPath + Data.UserDataJsonName);
            Application.Quit();
        }
    }
}
