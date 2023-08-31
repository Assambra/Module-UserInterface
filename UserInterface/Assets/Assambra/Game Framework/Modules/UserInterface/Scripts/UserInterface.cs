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
    [SerializeField] private GameObject elements = null;
    [field: SerializeField] public GameObject UITaskbar { get; private set; }
    [field: SerializeField] public GameObject TaskbarCenter { get; private set; }
    [field: SerializeField] public GameObject GameMenuRight { get; private set; }
    [field: SerializeField] public GameObject TemporaryObjects { get; private set; }


    // Private
    private List<Transform> listOfUIElements = new List<Transform>();


    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        CreateWindows();

        foreach (Transform uIElement in elements.GetComponentsInChildren<Transform>(true))
        {
            listOfUIElements.Add(uIElement);
        }
        CountUIElements = listOfUIElements.Count;
    }

    private void CreateWindows()
    {
        foreach(Window window in windows)
        {
            window.CreateWindow(elements.transform);
        }
    }
}
