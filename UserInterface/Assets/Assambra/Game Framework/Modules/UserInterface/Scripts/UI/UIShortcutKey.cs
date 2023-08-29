using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShortcutKey : MonoBehaviour
{
    [SerializeField] private GameObject gameObjectToSetActive = null;
    [SerializeField] private KeyCode windowShortcutKey = KeyCode.None;

    void Update()
    {
        if (Input.GetKeyDown(windowShortcutKey))
        {
            gameObjectToSetActive.SetActive(!gameObjectToSetActive.activeSelf);
        } 
    }

    public void SetWindowShortcutKey(KeyCode keyCode)
    {
        windowShortcutKey = keyCode;
    }
}
