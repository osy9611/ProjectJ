namespace Module.Unity.UGUI
{
    using global::Unity.VisualScripting.Antlr3.Runtime;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.InputSystem.Layouts;
    using UnityEngine.InputSystem.OnScreen;
    public class VirtualJoyStick : OnScreenControl, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [SerializeField] private RectTransform lever;
        [SerializeField] private RectTransform leverBack;
        [SerializeField] private RectTransform leverArea;

        [SerializeField, Range(5f, 50f)]
        private float leverRange;

        [SerializeField, InputControl(layout = "Vector2")]
        private string controlPath;

        private Vector2 pointerDownPos;
        [SerializeField] private Vector2 dir;

        protected override string controlPathInternal 
        {
            get => controlPath;
            set => controlPath = value;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData == null)
                throw new System.ArgumentException(nameof(eventData));
        }


        public void OnDrag(PointerEventData eventData)
        {
            if (eventData == null)
                throw new System.ArgumentException(nameof(eventData));

            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out Vector2 position);
            Vector2 delta = position - pointerDownPos;
            delta = Vector2.ClampMagnitude(delta, leverRange);
            lever.anchoredPosition = pointerDownPos + delta;

            Vector2 newPos = new Vector2(delta.x / leverRange, delta.y / leverRange);
            dir = newPos;
            SendValueToControl(newPos);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (eventData == null)
                throw new System.ArgumentException(nameof(eventData));

            dir = Vector2.zero;
            lever.anchoredPosition = Vector2.zero;
            leverBack.anchoredPosition = Vector2.zero;
            SendValueToControl(Vector2.zero);
        }
    }

}

