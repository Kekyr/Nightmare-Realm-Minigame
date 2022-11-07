using UnityEngine;
using UnityEngine.EventSystems;

public class SlotPointerDown : MonoBehaviour, IPointerDownHandler
{
    private RectTransform _rectTransform;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!GameManager.instance.MovementManager.IsMoving)
            GameManager.instance.MovementManager.Destination = _rectTransform;
    }
}