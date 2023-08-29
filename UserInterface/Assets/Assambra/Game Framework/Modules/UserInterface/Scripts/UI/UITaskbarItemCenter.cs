using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UITaskbarItemCenter : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Serialized Fields")]
    [SerializeField] private RectTransform rectTransformTaskbarItemCenter = null;
    [SerializeField] private RectTransform rectTransformTaskbarCenter = null;
    [SerializeField] private UITaskbar uITaskbar = null;
    [SerializeField] private Button ButtonReactivateWindow = null;

    [Header("Public Fields")]
    public GameObject GameObjectToSetActive;
    public Sprite SpriteButtonSymbol;

    private Vector2 originalLocalPointerPosition;
    private Vector3 originalPanelLocalPosition;

    private Vector2 lastAnchoredPosition;
    private Vector2 currentAnchoredPosition;
    private Vector2 differencePosition;

    /// <summary>
    /// This Vector is only to correct so that we have 0 based calculation;
    /// </summary>
    private Vector2 correctionVector = new Vector2(15,-15);
    private float stepsize;
    private float stepCounter = 0;

    private void Awake()
    {
        uITaskbar = FindObjectOfType<UITaskbar>();
        rectTransformTaskbarCenter = GameObject.Find("TaskbarCenter").GetComponent<RectTransform>();
    }

    private void Start()
    {
        ButtonReactivateWindow.image.sprite = SpriteButtonSymbol;
    }

    public void ButtonReactivateWindowFromTaskbarFunction()
    {
        GameObjectToSetActive.SetActive(!GameObjectToSetActive.activeSelf);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        lastAnchoredPosition = rectTransformTaskbarItemCenter.anchoredPosition - correctionVector;

        originalPanelLocalPosition = rectTransformTaskbarItemCenter.localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransformTaskbarCenter, eventData.position, eventData.pressEventCamera, out originalLocalPointerPosition);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        uITaskbar.CreateListTaskbarItemsCenter();
        stepsize = rectTransformTaskbarItemCenter.rect.width;
        stepCounter = this.GetComponent<RectTransform>().GetSiblingIndex() * stepsize;
    }

    public void OnDrag(PointerEventData eventData)
    {
        currentAnchoredPosition = rectTransformTaskbarItemCenter.anchoredPosition - correctionVector;
        differencePosition = lastAnchoredPosition + currentAnchoredPosition;
        

        if(differencePosition.x > stepCounter + stepsize + 1 && gameObject.transform.GetSiblingIndex() < uITaskbar.ListTaskbarItemsCenter.Count-1)
        {
            stepCounter += stepsize;
            this.GetComponent<RectTransform>().SetSiblingIndex(this.GetComponent<RectTransform>().GetSiblingIndex() + 1);
        }
        if (differencePosition.x <  stepCounter - stepsize + 1)
        {
            stepCounter -= stepsize;
            this.GetComponent<RectTransform>().SetSiblingIndex(this.GetComponent<RectTransform>().GetSiblingIndex() - 1);
        }
        
        if (rectTransformTaskbarItemCenter == null || rectTransformTaskbarCenter == null)
            return;

        Vector2 localPointerPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransformTaskbarCenter, eventData.position, eventData.pressEventCamera, out localPointerPosition))
        {
            Vector3 offsetToOriginal = localPointerPosition - originalLocalPointerPosition;
            rectTransformTaskbarItemCenter.localPosition = originalPanelLocalPosition + offsetToOriginal;
        }

        ClampToTaskbar();
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        stepCounter = 0f;
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransformTaskbarCenter);
    }

    void ClampToTaskbar()
    {
        Vector3 pos = rectTransformTaskbarItemCenter.localPosition;

        Vector3 minPosition = rectTransformTaskbarCenter.rect.min - rectTransformTaskbarItemCenter.rect.min;
        Vector3 maxPosition = rectTransformTaskbarCenter.rect.max - rectTransformTaskbarItemCenter.rect.max;

        pos.x = Mathf.Clamp(rectTransformTaskbarItemCenter.localPosition.x, minPosition.x, maxPosition.x);
        pos.y = Mathf.Clamp(rectTransformTaskbarItemCenter.localPosition.y, minPosition.y, maxPosition.y);

        rectTransformTaskbarItemCenter.localPosition = pos;
    }
}
