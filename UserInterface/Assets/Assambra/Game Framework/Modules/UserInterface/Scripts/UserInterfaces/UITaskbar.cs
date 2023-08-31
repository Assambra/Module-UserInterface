using System.Collections.Generic;
using UnityEngine;


public class UITaskbar : MonoBehaviour
{
    [SerializeField] private GameObject UIGameMenu = null;
    //[SerializeField] private GameObject gameObjectUITaskbar = null;
    [SerializeField] private GameObject gameObjectTaskbarCenter = null;
    
    public List<TaskbarItemCenter> ListTaskbarItemsCenter = new List<TaskbarItemCenter>();

    public void CreateListTaskbarItemsCenter()
    {
        ListTaskbarItemsCenter.Clear();
        foreach (TaskbarItemCenter tc in gameObjectTaskbarCenter.GetComponentsInChildren<TaskbarItemCenter>())
        {
            ListTaskbarItemsCenter.Add(tc);
        }
    }
    
    public void OnButtonGameMenu()
    {
        UIGameMenu.SetActive(!UIGameMenu.activeSelf);
    }
}
