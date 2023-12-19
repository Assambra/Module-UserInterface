using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TabObject : MonoBehaviour
{
    public UIWindow window;

    [SerializeField] private TMP_Text tabTMP_Text;
    [SerializeField] public RectTransform topBorderLeft;
    [SerializeField] public RectTransform topBorderRight;

    [SerializeField] private float windowBorder = 3f;

    private ContentSizeFitter[] fitters;

    private bool unconstrained;
    private bool preferred;

    private RectTransform windowRectTransform;
    private RectTransform tabRectTransform;

    private float windowWidth;
    private float tabWidth;
    private float topBorderLeftWidth;

    RectTransform parentRectTransform;
    private float parentLeft;
    private float parentRight;

    private void Awake()
    {   
        tabRectTransform = gameObject.GetComponent<RectTransform>();
        parentRectTransform = transform.parent.GetComponent<RectTransform>();
        
        parentLeft = parentRectTransform.offsetMin.x;
        parentRight = parentRectTransform.offsetMax.x;

        fitters = gameObject.GetComponentsInChildren<ContentSizeFitter>();
        SetContentSizeFitterUnconstrained();
    }

    private void Start()
    {
        windowRectTransform = window.GetComponent<RectTransform>();
        topBorderLeft.sizeDelta = new Vector2(parentLeft - windowBorder, topBorderLeft.sizeDelta.y);
    }

    private void Update()
    {
        windowWidth = windowRectTransform.sizeDelta.x;

        if (windowWidth < tabTMP_Text.preferredWidth - parentRight + parentLeft && !unconstrained)
            SetContentSizeFitterUnconstrained();
        else if(windowWidth > tabTMP_Text.preferredWidth - parentRight + parentLeft && !preferred)
            SetContentSizeFitterPreferred();

        if (window.IsWindowResizing)
        {
            tabWidth = tabRectTransform.sizeDelta.x;
            topBorderLeftWidth = topBorderLeft.sizeDelta.x;

            topBorderRight.sizeDelta = new Vector2(windowWidth - (windowBorder * 2) - topBorderLeftWidth - tabWidth, topBorderRight.sizeDelta.y);
        }
    }

    public void SetTabName(string tabname)
    {
        tabTMP_Text.text = tabname;
    }

    public string GetTabName()
    {
        return tabTMP_Text.text;
    }

    public void OnButtonTab()
    {
        
    }

    private void SetContentSizeFitterPreferred()
    {
        preferred = true;
        unconstrained = false;

        foreach (ContentSizeFitter fitter in fitters)
        {
            fitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        }
    }

    private void SetContentSizeFitterUnconstrained()
    {
        unconstrained = true;
        preferred = false;

        foreach (ContentSizeFitter fitter in fitters)
        {
            fitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
        }
    }
}
