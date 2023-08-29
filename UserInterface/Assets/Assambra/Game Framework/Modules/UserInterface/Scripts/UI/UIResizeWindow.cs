using UnityEngine;
using UnityEngine.EventSystems;
using Helper;

public class UIResizeWindow : MonoBehaviour, IPointerDownHandler,  IPointerUpHandler, IDragHandler
{
	[Header("References")]
	[SerializeField] private GameObject gameObjectUIWindow = null;

	[SerializeField]
	private enum WindowResizeSide 
	{
		LeftTop,
		Left,
		LeftBottom,
		RightTop,
		Right,
		RightBottom,
		Top,
		Bottom,
		None
	}

	[SerializeField] private WindowResizeSide windowResizeSide = WindowResizeSide.None;

	private UIWindow uIWindow;
    private RectTransform rectTransformUIWindow;

    private Vector2 originalLocalPointerPosition = Vector2.zero;
    private Vector2 originalSizeDelta = Vector2.zero;
    private Vector2 sizeDelta = Vector2.zero;

	void Awake()
	{
		uIWindow = gameObjectUIWindow.GetComponent<UIWindow>();
		rectTransformUIWindow = gameObjectUIWindow.GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData data)
	{
		uIWindow.IsWindowResizing = true;

        originalSizeDelta = rectTransformUIWindow.sizeDelta;

        if (uIWindow.IsWindowFullsized())
			windowResizeSide = WindowResizeSide.None;

		switch (windowResizeSide)
		{
			case WindowResizeSide.LeftTop:
				AnchorPresets.SetPresetBottomRight(rectTransformUIWindow);
				break;
			case WindowResizeSide.LeftBottom:
				AnchorPresets.SetPresetTopRight(rectTransformUIWindow);
				break;
			case WindowResizeSide.RightTop:
				AnchorPresets.SetPresetBottomLeft(rectTransformUIWindow);
				break;
			case WindowResizeSide.Left:
				AnchorPresets.SetPresetTopRight(rectTransformUIWindow);
				break;
			case WindowResizeSide.Right:
				AnchorPresets.SetPresetTopLeft(rectTransformUIWindow);
				break;
			case WindowResizeSide.RightBottom:
				AnchorPresets.SetPresetTopLeft(rectTransformUIWindow);
				break;
			case WindowResizeSide.Top:
				AnchorPresets.SetPresetBottomLeft(rectTransformUIWindow);
				break;
			case WindowResizeSide.Bottom:
				AnchorPresets.SetPresetTopLeft(rectTransformUIWindow);
				break;
			case WindowResizeSide.None:
				AnchorPresets.SetPresetMiddleCenter(rectTransformUIWindow);
				break;
		}

		RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransformUIWindow, data.position, null, out originalLocalPointerPosition);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		uIWindow.IsWindowResizing = false;

		AnchorPresets.SetPresetMiddleCenter(rectTransformUIWindow);
	}

	public void OnDrag(PointerEventData data)
	{
		if (rectTransformUIWindow == null)
			return;

		Vector2 localPointerPosition;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransformUIWindow, data.position, null, out localPointerPosition);
		
		Vector3 offsetToOriginal = localPointerPosition - originalLocalPointerPosition;
		
		
		if (uIWindow.IsWindowFullsized())
			windowResizeSide = WindowResizeSide.None;

        switch (windowResizeSide)
        {
            case WindowResizeSide.LeftTop:
                sizeDelta = originalSizeDelta + new Vector2(-offsetToOriginal.x, offsetToOriginal.y);
                break;
            case WindowResizeSide.LeftBottom:
                sizeDelta = originalSizeDelta + new Vector2(-offsetToOriginal.x, -offsetToOriginal.y);
                break;
            case WindowResizeSide.RightTop:
                sizeDelta = originalSizeDelta + new Vector2(offsetToOriginal.x, offsetToOriginal.y);
                break;
            case WindowResizeSide.Left:
                sizeDelta = originalSizeDelta + new Vector2(-offsetToOriginal.x, 0);
                break;
            case WindowResizeSide.Right:
                sizeDelta = originalSizeDelta + new Vector2(offsetToOriginal.x, 0);
                break;
            case WindowResizeSide.RightBottom:
                sizeDelta = originalSizeDelta + new Vector2(offsetToOriginal.x, -offsetToOriginal.y);
                break;
            case WindowResizeSide.Top:
                sizeDelta = originalSizeDelta + new Vector2(0, offsetToOriginal.y);
                break;
            case WindowResizeSide.Bottom:
                sizeDelta = originalSizeDelta + new Vector2(0, -offsetToOriginal.y);
                break;
            case WindowResizeSide.None:
                sizeDelta = originalSizeDelta;
                break;
        }

        if (uIWindow.IgnoreWindowMaxSize)
			sizeDelta = new Vector2(Mathf.Clamp(sizeDelta.x, uIWindow.WindowMinSize.x, Screen.width),
									Mathf.Clamp(sizeDelta.y, uIWindow.WindowMinSize.y, Screen.height));
		else
			sizeDelta = new Vector2(Mathf.Clamp(sizeDelta.x, uIWindow.WindowMinSize.x, uIWindow.WindowMaxSize.x),
									Mathf.Clamp(sizeDelta.y, uIWindow.WindowMinSize.y, uIWindow.WindowMaxSize.y));
		
		rectTransformUIWindow.sizeDelta = sizeDelta;
		
		uIWindow.ButtonHandler();
	}
}
