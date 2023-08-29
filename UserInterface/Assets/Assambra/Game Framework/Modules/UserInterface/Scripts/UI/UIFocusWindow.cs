using UnityEngine;
using UnityEngine.EventSystems;

public class UIFocusWindow : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private UserInterface userInterface = null;
    [SerializeField] private RectTransform rectTransformWindow = null;

    private void Start()
    {
        userInterface = UserInterface.Instance;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        rectTransformWindow.SetSiblingIndex(userInterface.CountUIElements-1);
    }
}
