using System.Collections.Generic;
using UnityEngine;

public class UserInterface : MonoBehaviour
{
    // Public
    public static UserInterface Instance { get; private set; }
    public int CountUIElements { get; private set; }

    [Header("Windows")]
    [SerializeField] private List<Window> windows = new List<Window>();

    [Header("GameObject references")]
    [SerializeField] private GameObject uIElements = null;
    [field: SerializeField] public GameObject gameObjectUITaskbar { get; private set; }
    [field: SerializeField] public GameObject gameObjectUITaskbarCenter { get; private set; }
    [field: SerializeField] public GameObject gameObjectUIGameMenuRight { get; private set; }

    // Private
    private List<Transform> listOfUIElements = new List<Transform>();


    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        CreateWindows();

        foreach (Transform uIElement in uIElements.GetComponentsInChildren<Transform>(true))
        {
            listOfUIElements.Add(uIElement);
        }
        CountUIElements = listOfUIElements.Count;
    }

    private void CreateWindows()
    {
        foreach(Window window in windows)
        {
            window.CreateWindow(uIElements.transform);
        }
    }
}
