using System.Collections.Generic;
using UnityEngine;


public class UITaskbar : MonoBehaviour
{
    [SerializeField] private GameObject UIGameMenu = null;
    //[SerializeField] private GameObject gameObjectUITaskbar = null;
    [SerializeField] private GameObject gameObjectTaskbarCenter = null;
    
    public List<UITaskbarItemCenter> ListTaskbarItemsCenter = new List<UITaskbarItemCenter>();

    public void CreateListTaskbarItemsCenter()
    {
        ListTaskbarItemsCenter.Clear();
        foreach (UITaskbarItemCenter tc in gameObjectTaskbarCenter.GetComponentsInChildren<UITaskbarItemCenter>())
        {
            ListTaskbarItemsCenter.Add(tc);
        }
    }
    
    public void OnButtonGameMenu()
    {
        UIGameMenu.SetActive(!UIGameMenu.activeSelf);
    }
}
