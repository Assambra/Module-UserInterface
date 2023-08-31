using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameMenuItemLeft : MonoBehaviour
{
    [SerializeField] private Button buttonGameMenuLeft = null;
    [SerializeField] private Sprite spriteButtonGameMenuLeft= null;
    [SerializeField] private GameObject gameObjectToActivate = null;

    private void Awake()
    {
        buttonGameMenuLeft.image.sprite = spriteButtonGameMenuLeft;
    }

    void Start()
    {

    }

    void Update()
    {
        if (gameObjectToActivate == null)
            return;
        else
            gameObjectToActivate.SetActive(!gameObjectToActivate.activeSelf);
    }
}
