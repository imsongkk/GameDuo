using UnityEngine;
using UnityEngine.EventSystems;

namespace GameDuo.UI
{
    public class UI_Button : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
    {
        bool isClicked = false;
        public bool HasAnim { get; set; } = true;

        RectTransform rectTransform;

        Vector3 originScale;

        public enum BUTTONTYPE
        {
            Button,
        }

        public BUTTONTYPE buttonType;

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            originScale = rectTransform.localScale;
        }

        private void Update()
        {
            if (HasAnim)
            {
                if (isClicked)
                    rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, originScale * 1.05f, 0.5f);
                else
                    rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, originScale, 0.5f);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            // 좌클릭만 처리
            if (eventData.button != PointerEventData.InputButton.Left)
                return;
            isClicked = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            // 좌클릭만 처리
            if (eventData.button != PointerEventData.InputButton.Left)
                return;
            isClicked = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            
        }
    }
}
