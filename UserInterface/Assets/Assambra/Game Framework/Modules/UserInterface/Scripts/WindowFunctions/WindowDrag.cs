using UnityEngine;
using UnityEngine.EventSystems;

public class WindowDrag : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
	[SerializeField] private UIWindow window;
	[SerializeField] private UIWindow uIWindow = null;
	[SerializeField] private RectTransform DragRectTransform = null;
	
	public void OnDrag(PointerEventData eventData)
	{
		if(uIWindow != null)
        {
			if (uIWindow.IsWindowFullsized())
			{
				uIWindow.DecreaseWindowBeforeDrag(eventData.position);
			}
		}
		
		DragRectTransform.anchoredPosition += eventData.delta;
	}

    public void OnPointerDown(PointerEventData eventData)
    {
		if(UserInterface.Instance.MouseHasDragObject)
		{
            window.InstantiateTab(UserInterface.Instance.DragObject.Name);
        }
			
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }
}
