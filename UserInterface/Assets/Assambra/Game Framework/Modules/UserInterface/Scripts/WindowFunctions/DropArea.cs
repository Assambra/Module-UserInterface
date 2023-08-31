using UnityEngine;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
    }
}
