using System;
using GameDuo.Managers;

namespace GameDuo.UI.Popup
{
    public class UI_Popup : UI_Base
    {
        public bool IsEscAble { get; protected set; } = true;

        public event Action AfterClosePopup = null;
        public Action OnShowPopup = null;

        public override void Init()
        {
            if (IsInitialized) return;

            IsInitialized = true;

            GameManager.UI.SetCanvas(gameObject, true);
        }

        public virtual void ClosePopupUI()
        {
            GameManager.UI.ClosePopupUI(this);
            AfterClosePopup?.Invoke();
            AfterClosePopup = null;
        }
    }
}
