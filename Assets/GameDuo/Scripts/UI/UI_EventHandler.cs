using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameDuo.UI
{
    public class UI_EventHandler : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler
    {
        public Action<PointerEventData> OnBeginDragHandler = null;
        public Action<PointerEventData> OnEndDragHandler = null;
        public Action<PointerEventData> OnDragHandler = null;
        public Action<PointerEventData> OnClickHandler = null;

        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            OnBeginDragHandler?.Invoke(eventData);
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            OnDragHandler?.Invoke(eventData);
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            OnBeginDragHandler?.Invoke(eventData);
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            OnClickHandler?.Invoke(eventData);
        }
    }
}
