using UnityEngine;

[CreateAssetMenu(fileName = "Window", menuName = "Assambra/Window", order = 1)]
public class Window : ScriptableObject
{
    [SerializeField] public GameObject prefabWindow;
    [SerializeField] public WindowSettings settings;

    private GameObject window;

    public void CreateWindow(Transform parent)
    {
        window = Instantiate(prefabWindow, parent);
        window.name = settings.windowName;

        UIShortcutKey uIShortcutKey = window.GetComponent<UIShortcutKey>();
        uIShortcutKey.SetWindowShortcutKey(settings.windowShortcutKey);

        UIWindow uIWindow = window.GetComponentInChildren<UIWindow>();
        uIWindow.settings = settings;
    }
}
