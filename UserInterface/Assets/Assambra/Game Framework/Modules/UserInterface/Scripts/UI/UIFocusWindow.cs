using UnityEngine;
using UnityEngine.EventSystems;

public class UIFocusWindow : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private UISystem uISystem = null;
    [SerializeField] private RectTransform rectTransformWindow = null;

    private void Start()
    {
        uISystem = UISystem.Instance;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        rectTransformWindow.SetSiblingIndex(uISystem.CountUIElements-1);
    }
}
