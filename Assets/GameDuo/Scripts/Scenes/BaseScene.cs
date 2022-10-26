using UnityEngine;
using UnityEngine.EventSystems;
using System;
using GameDuo.Managers;

namespace GameDuo.Scene
{
    public class BaseScene : MonoBehaviour
    {
        protected Action OnDestroyAction = null;

        protected void Awake() => Init();

        protected virtual void Init()
        {
            UnityEngine.Object eventSystem = GameObject.FindObjectOfType(typeof(EventSystem));
            if (eventSystem == null)
            {
                eventSystem = GameManager.Resource.Instantiate("EventSystem");
                eventSystem.name = "@EventSystem";
                DontDestroyOnLoad(eventSystem);
            }
        }

        private void OnDestroy()
            => OnDestroyAction?.Invoke();

        public void AddOnDestroyAction(Action action)
            => OnDestroyAction += action;
    }
}
