using UnityEngine;
using UnityEngine.EventSystems;

public class WindowDrag : MonoBehaviour, IDragHandler
{
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
}
