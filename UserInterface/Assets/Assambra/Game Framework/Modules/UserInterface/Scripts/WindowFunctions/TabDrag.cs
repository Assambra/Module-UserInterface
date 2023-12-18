using UnityEngine;
using UnityEngine.EventSystems;

public class TabDrag : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] GameObject prefabTabDragObject;

    private GameObject dragObject;
    private TabObject tabObject;

    private bool ignoreFirstMouseClick = false;
    private CanvasGroup[] canvasGroups;

    private float tabDragObjectWidth;
    //private float tabDragObjectHeight;


    private void Awake()
    {
        tabObject = GetComponent<TabObject>();
    }

    private void Update()
    {
        if (dragObject == null)
            return;

        Vector2 position = ConvertMouseInputToAnchoredPosition(Input.mousePosition);
        dragObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(position.x-tabDragObjectWidth, position.y);
        
        if(ignoreFirstMouseClick)
        {
            if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
            {
                DestroyTabDragObject();
                ignoreFirstMouseClick = false;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right && dragObject == null)
        {
            

            dragObject = Instantiate(prefabTabDragObject, UserInterface.Instance.TemporaryObjects.transform);

            TabDragObject tdo = dragObject.GetComponent<TabDragObject>();
            tdo.Name = tabObject.GetTabName();
            UserInterface.Instance.DragObject = tdo;
            UserInterface.Instance.MouseHasDragObject = true;

            tabDragObjectWidth = gameObject.GetComponent<RectTransform>().sizeDelta.x;
            
            canvasGroups = dragObject.GetComponentsInChildren<CanvasGroup>();
            foreach (CanvasGroup canvasGroup in canvasGroups) 
            {
                canvasGroup.blocksRaycasts = false;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            ignoreFirstMouseClick = true;
        }
    }

    private Vector2 ConvertMouseInputToAnchoredPosition(Vector2 mouseInput)
    {
        return new Vector2(mouseInput.x - (Screen.width / 2), Input.mousePosition.y - (Screen.height / 2));
    }

    private void DestroyTabDragObject()
    {
        UserInterface.Instance.MouseHasDragObject = false;
        Destroy(dragObject);
    }
}
