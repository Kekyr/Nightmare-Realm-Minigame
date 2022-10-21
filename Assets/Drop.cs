using UnityEngine;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour,IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (gameObject.transform.childCount == 0)
        {
            eventData.pointerDrag.transform.SetParent(gameObject.transform);
        }
    }
}
