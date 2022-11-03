using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerDown : MonoBehaviour,IPointerDownHandler
{
    private RectTransform _rectTransform;
    
    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if(!GameManager.instance.IsMoving)
        GameManager.instance.CounterToMove = _rectTransform;
    }
}
