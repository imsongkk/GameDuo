using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameDuo.Managers
{
    public class GameManager : MonoBehaviour
    {
        static GameManager instance;
        public static GameManager Instance { get { Init(); return instance; } }

        ResourceManager resourceManager = new ResourceManager();
        UIManager uiManager = new UIManager();
        DataManager dataManager = new DataManager();

        public static ResourceManager Resource { get => Instance.resourceManager; }
        public static UIManager UI { get => Instance.uiManager; }
        public static DataManager Data { get => Instance.dataManager; }

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
    }
}
