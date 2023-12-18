using System;
using TMPro;
using UnityEngine;

public class TabDragObject : MonoBehaviour, IDragObject
{
    public string Name { get => _name; set => _name = value; }

    [SerializeField] TMP_Text textTabName;

    private string _name;
    
    private bool doOnce;

    private void Update()
    {
        if(!doOnce)
        {
            Debug.Log("doOnce");
            if (!String.IsNullOrEmpty(_name))
            {
                doOnce = true;
                textTabName.text = _name;
            }
        }
    }

    
}
