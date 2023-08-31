using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameMenuItemRight : MonoBehaviour
{
    [SerializeField] private TMP_Text TextGameMenuItemName = null;
    [SerializeField] private Image ImageGameMenuItem = null;

    [Header("Public Fields")]
    public GameObject GameObjectToSetActive;
    public string GameMenuItemName;
    public Sprite SpriteGameMenuItemSymbol;

    private void Start()
    {
        TextGameMenuItemName.text = GameMenuItemName;
        ImageGameMenuItem.sprite = SpriteGameMenuItemSymbol;
    }

    public void ButtonActivateWindowFunction()
    {
        GameObjectToSetActive.SetActive(!GameObjectToSetActive.activeSelf);
    }
}
