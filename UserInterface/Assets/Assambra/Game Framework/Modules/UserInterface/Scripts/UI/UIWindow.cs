using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIWindow : MonoBehaviour
{
    public bool IsWindowResizing = false;
    public WindowSettings settings;
    
    [Header("Window")]
    [SerializeField] private string windowName;
    [SerializeField] private Sprite windowIcon;

    [Header("Serialize Fields GameObjects")]
    [SerializeField] private GameObject gameObjectUIWindow = null;
    [SerializeField] private GameObject gameObjectUITaskbar = null;
    [SerializeField] private GameObject gameObjectUITaskbarCenter = null;
    [SerializeField] private GameObject gameObjectUIGameMenuRight = null;
    
    [Header("Serialize Fields Window Buttons")]
    [SerializeField] private GameObject buttonCloseWindow = null;
    [SerializeField] private GameObject buttonDecreaseWindow = null;
    [SerializeField] private GameObject buttonMaximizeWindow = null;
    [SerializeField] private GameObject buttonMinimizeWindow = null;
    
    [Header("Window Text Name")]
    [SerializeField] private TMP_Text textWindowName = null;

    [Header("Window Image Symbol")]
    [SerializeField] private Image imageWindowSymbol = null;

    [Header("Prefabs")]
    [SerializeField] private GameObject prefabTaskbarItemCenter = null;
    [SerializeField] private GameObject prefabGameMenuItemRight = null;

    //[Header("Window Min/Max size")]
    [Tooltip("Ignore WindowMaxSize for fullsized windows")]
    [field: SerializeField] public bool IgnoreWindowMaxSize { get; private set; }
    [field: SerializeField] public Vector2 WindowMinSize { get; private set; }
    [field: SerializeField] public Vector2 WindowMaxSize { get; private set; }


    [Header("Window has function/button and options")]
    [Tooltip("Whether the window is activated at start")]
    public bool windowStartActivated = false;
    [Tooltip("Whether the window has a close function/button")]
    public bool windowHasCloseButton = false;
    [Tooltip("Whether the window has a Size function/button (maximize, minimize and decrease)")]
    public bool windowHasResizeButtons = false;
    
    // Private Fields
    private RectTransform rectTransformUIWindow;
    private Transform transformTaskbarCenter;
    private Transform transformGameMenuRight;

    private Vector2 lastWindowPosition = Vector3.zero;
    private Vector2 lastWindowSize = Vector2.zero;

    private GameObject instanceTaskbarItemCenter;
    private bool isTaskbarItemCenterPresent = false;

    private GameObject instancedGameMenuItemRight;

    private void Awake()
    {
        gameObjectUITaskbar = UserInterface.Instance.gameObjectUITaskbar;
        gameObjectUITaskbarCenter = UserInterface.Instance.gameObjectUITaskbarCenter;
        gameObjectUIGameMenuRight = UserInterface.Instance.gameObjectUIGameMenuRight;

        rectTransformUIWindow = gameObjectUIWindow.GetComponent<RectTransform>();
        transformTaskbarCenter = gameObjectUITaskbarCenter.transform;
        transformGameMenuRight = gameObjectUIGameMenuRight.transform;
    }

    private void Start()
    {
        ApplyWindowSettings();

        textWindowName.text = windowName;
        imageWindowSymbol.sprite = windowIcon;

        WindowSizeCheck();

        if (!IsWindowFullsized() && IgnoreWindowMaxSize || !IsWindowMaximized() && !IgnoreWindowMaxSize)
            SaveWindowValues();

        ActivateWindowButtons();

        CreateGameMenuItemRight();

        if (!windowStartActivated)
            gameObject.SetActive(false);
    }

    private void Update()
    {
        if (gameObject.activeSelf && !isTaskbarItemCenterPresent)
        {
            CreateTaskBarItemCenter();
            isTaskbarItemCenterPresent = true;
        }
    }

    private void ApplyWindowSettings()
    {
        windowName = settings.windowName;
        windowIcon = settings.windowIcon;
        IgnoreWindowMaxSize = settings.IgnoreWindowMaxSize;
        WindowMinSize = settings.WindowMinsize;
        rectTransformUIWindow.sizeDelta = WindowMinSize;
        WindowMaxSize = settings.WindowMaxSize;
        windowStartActivated = settings.windowStartActivated;
        windowHasCloseButton = settings.windowHasCloseButton;
        windowHasResizeButtons = settings.windowHasResizeButtons;
    }

    public void ButtonCloseWindowFunction()
    {
        Helper.Instantiate.DestroyUIElement(instanceTaskbarItemCenter);
        isTaskbarItemCenterPresent = false;
        gameObjectUIWindow.SetActive(false);
    }

    public void ButtonDecreaseWindowFunction()
    {
        if(lastWindowPosition != rectTransformUIWindow.anchoredPosition && !IgnoreWindowMaxSize)
        {
            LoadWindowValues(false, true);
        }
        else
            LoadWindowValues();

        buttonMaximizeWindow.gameObject.SetActive(true);
        buttonDecreaseWindow.gameObject.SetActive(false);
    }

    public void DecreaseWindowBeforeDrag(Vector2 position)
    {
        LoadWindowValues(false, true);

        Vector2 localPosition = rectTransformUIWindow.InverseTransformPoint(new Vector3(position.x, position.y, 0));
        float offsetY = rectTransformUIWindow.sizeDelta.y/2;
        float ratioX = rectTransformUIWindow.sizeDelta.x / Screen.width * localPosition.x;
        // OffsetY - "25" is hardcoded it is in the center of the window header normal the mouse should be without 25 on the top edge of the window but it isnt.
        // We perform some window drag and i think that's where the inaccuracy comes from. Keep it in mind if you change the header size.
        rectTransformUIWindow.anchoredPosition = localPosition - new Vector2(ratioX, offsetY-25);
        
        buttonMaximizeWindow.gameObject.SetActive(true);
        buttonDecreaseWindow.gameObject.SetActive(false);
    }

    public void ButtonMaximizeWindowFunction()
    {
        SaveWindowValues();

        if(IgnoreWindowMaxSize)
        {
            rectTransformUIWindow.sizeDelta = new Vector2(Screen.width, Screen.height - gameObjectUITaskbar.GetComponent<RectTransform>().sizeDelta.y);
            rectTransformUIWindow.anchoredPosition = new Vector2(0, gameObjectUITaskbar.GetComponent<RectTransform>().sizeDelta.y / 2);
        }
        else
        {
            rectTransformUIWindow.sizeDelta = new Vector2(WindowMaxSize.x, WindowMaxSize.y);
        }

        buttonDecreaseWindow.gameObject.SetActive(true);
        buttonMaximizeWindow.gameObject.SetActive(false);
    }

    public void ButtonMinimizeWindowFunction()
    {
        SaveWindowValues();

        gameObjectUIWindow.SetActive(false);
    }

    private void SaveWindowValues()
    {
        lastWindowPosition = rectTransformUIWindow.anchoredPosition;
        lastWindowSize = rectTransformUIWindow.sizeDelta;
    }

    private void LoadWindowValues()
    {
        if (lastWindowSize == Vector2.zero && IsWindowFullsized() || lastWindowSize == Vector2.zero && IsWindowMaximized())
        {
            if (IsWindowFullsized())
            {
                rectTransformUIWindow.sizeDelta = rectTransformUIWindow.sizeDelta - new Vector2(25, 25);
            }
            if (IsWindowMaximized())
            {
                rectTransformUIWindow.sizeDelta = WindowMinSize;
            }
        }
        else
        {
            rectTransformUIWindow.anchoredPosition = lastWindowPosition;
            rectTransformUIWindow.sizeDelta = lastWindowSize;
        }
            
    }

    private void LoadWindowValues(bool position, bool size)
    {
        if(position)
            rectTransformUIWindow.anchoredPosition = lastWindowPosition;
        if(size)
            rectTransformUIWindow.sizeDelta = lastWindowSize;
    }

    /// <summary>
    /// Returns true if the window is fullsized Screen.width and Screen.height
    /// </summary>
    /// <returns></returns>
    public bool IsWindowFullsized()
    {
        if (rectTransformUIWindow.sizeDelta.x == Screen.width && rectTransformUIWindow.sizeDelta.y == Screen.height - gameObjectUITaskbar.GetComponent<RectTransform>().sizeDelta.y)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Returns true if Window is WindowMaxSize
    /// </summary>
    /// <returns></returns>
    public bool IsWindowMaximized()
    {
        if (rectTransformUIWindow.sizeDelta.x == WindowMaxSize.x && rectTransformUIWindow.sizeDelta.y == WindowMaxSize.y)
            return true;
        else
            return false;
    }

    private void ActivateWindowButtons()
    {
        if (windowHasCloseButton)
            buttonCloseWindow.SetActive(true);

        if (windowHasResizeButtons)
        {
            if (IsWindowFullsized() || IsWindowMaximized())
            {
                buttonDecreaseWindow.SetActive(true);
                buttonMaximizeWindow.SetActive(false);
            }
            else
            {
                buttonMaximizeWindow.SetActive(true);
                buttonDecreaseWindow.SetActive(false);
            }

            buttonMinimizeWindow.SetActive(true);
        }
    }

    public void ButtonHandler()
    {
        if (IsWindowFullsized() || IsWindowMaximized())
        {
            buttonDecreaseWindow.SetActive(true);
            buttonMaximizeWindow.SetActive(false);
        }
        else
        {
            buttonMaximizeWindow.SetActive(true);
            buttonDecreaseWindow.SetActive(false);
        }
    }

    private void WindowSizeCheck()
    {
        if (rectTransformUIWindow.sizeDelta.x > WindowMaxSize.x)
        {
            Debug.LogError("WindowSize x greater then WindowMaxSize x");
            //rectTransformUIWindow.sizeDelta = new Vector2(WindowMaxSize.x, rectTransformUIWindow.sizeDelta.y);
        }
        if (rectTransformUIWindow.sizeDelta.y > WindowMaxSize.y)
        {
            Debug.LogError("WindowSize y greater then WindowMaxSize y");
            //rectTransformUIWindow.sizeDelta = new Vector2(rectTransformUIWindow.sizeDelta.x, WindowMaxSize.y);
        }
        if (rectTransformUIWindow.sizeDelta.x < WindowMinSize.x)
        {
            Debug.LogError("WindowSize x smaller then WindowMinSize x");
            //rectTransformUIWindow.sizeDelta = new Vector2(WindowMinSize.x, rectTransformUIWindow.sizeDelta.y);
        }
        if (rectTransformUIWindow.sizeDelta.y < WindowMinSize.y)
        {
            Debug.LogError("WindowSize y smaller then WindowMinSize y");
            //rectTransformUIWindow.sizeDelta = new Vector2(rectTransformUIWindow.sizeDelta.x, WindowMinSize.y);
        } 
    }

    private void CreateTaskBarItemCenter()
    {
        instanceTaskbarItemCenter = Helper.Instantiate.InstantiateUIElement(prefabTaskbarItemCenter, transformTaskbarCenter);
        UITaskbarItemCenter tic = instanceTaskbarItemCenter.GetComponent<UITaskbarItemCenter>();
        tic.GameObjectToSetActive = gameObject;
        tic.SpriteButtonSymbol = windowIcon;
    }

    private void CreateGameMenuItemRight()
    {
        instancedGameMenuItemRight = Helper.Instantiate.InstantiateUIElement(prefabGameMenuItemRight, transformGameMenuRight);
        UIGameMenuItemRight gmir = instancedGameMenuItemRight.GetComponent<UIGameMenuItemRight>();
        gmir.GameObjectToSetActive = gameObject;
        gmir.GameMenuItemName = windowName;
        gmir.SpriteGameMenuItemSymbol = windowIcon;
    }
}
