using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private CounterMovement _counterMovement;

    private void Start()
    {
        _counterMovement = GetComponent<CounterMovement>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Drag!");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(eventData.position);
        Vector3 modifiedWorldPosition = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);
        Debug.Log(modifiedWorldPosition);
        _counterMovement.Destination = modifiedWorldPosition;
        _counterMovement.Direction = (modifiedWorldPosition - transform.position).normalized;
        
        //transform.position = new Vector3(worldPosition.x,worldPosition.y,transform.position.z);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }
}
