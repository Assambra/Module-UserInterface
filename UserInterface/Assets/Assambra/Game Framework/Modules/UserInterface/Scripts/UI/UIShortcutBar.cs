using System;
using UnityEngine;
using UnityEngine.UI;

public class UIShortcutBar : MonoBehaviour
{
    public int StartBarID = 0;
    [SerializeField] private int _currentlyActiveBar;

    [SerializeField] private GameObject barPrefab;
    [SerializeField] private RectTransform barDocker;

    [SerializeField] private GameObject slotPrefab;
    
    [SerializeField] private int maxSlots = 10;
    [SerializeField] private int maxBars = 10;

    [SerializeField] private Text displayDigit;

    private bool _isHorizontal = true;

    // Start is called before the first frame update
    void Start()
    {
        displayDigit.text = "";

        for (int i = 0; i < maxBars; i++)
        {
            GameObject goBar = Instantiate(barPrefab, barDocker);
            goBar.name = "Bar" + "[" + i + "]";

            UIBar uIBar = goBar.GetComponent<UIBar>();
            if (uIBar != null)
                uIBar.BarID = i;
            else
                Debug.LogError("UIBar not found!");


            for (int i1 = 0; i1 < maxSlots; i1++)
            {
                GameObject goSlot = Instantiate(slotPrefab, goBar.GetComponent<RectTransform>());

                goSlot.name = "Slot" + "[" + i1 + "]";
            
                UIItemSlot uIItemSlot = goSlot.GetComponent<UIItemSlot>();
                if (uIItemSlot != null)
                    uIItemSlot.SlotID = i1;
                else
                    Debug.LogError("UIItemSlot not found!");
            }

            if (uIBar.BarID != StartBarID)
                goBar.SetActive(false);
            else
            {
                if (!uIBar.gameObject.activeSelf)
                    goBar.SetActive(true);

                displayDigit.text = Convert.ToString(uIBar.BarID);
                _currentlyActiveBar = uIBar.BarID;
            }   
        }
    }

    void Update()
    {
    
    }

    public void ShowNextBar() 
    {

        UIBar[] bars = barDocker.GetComponentsInChildren<UIBar>(true);

        foreach (var bar in bars)
        {
            if (bar.gameObject.activeSelf == true && bar.BarID == _currentlyActiveBar)
            {
                bar.gameObject.SetActive(false);
                
                if (_currentlyActiveBar == 9)
                    _currentlyActiveBar = 0;
                else
                    _currentlyActiveBar++;

                break;
            }
        }

        foreach (var bar in bars)
        {
            if (bar.BarID == _currentlyActiveBar)
            {
                bar.gameObject.SetActive(true);
                displayDigit.text = Convert.ToString(bar.BarID);

                break;
            }
        }
    }

    public void ShowLastBar()
    {
        UIBar[] bars = barDocker.GetComponentsInChildren<UIBar>(true);

        foreach (var bar in bars)
        {
            if (bar.gameObject.activeSelf == true && bar.BarID == _currentlyActiveBar)
            {
                bar.gameObject.SetActive(false);
                
                if (_currentlyActiveBar == 0)
                    _currentlyActiveBar = 9;
                else
                    _currentlyActiveBar--;
                
                break;
            }
        }

        foreach (var bar in bars)
        {
            if (bar.BarID == _currentlyActiveBar)
            {
                bar.gameObject.SetActive(true);
                displayDigit.text = Convert.ToString(bar.BarID);
                
                break;
            }
        }
    }

    public void FlipShortcutBar90degrees()
    {
        if (_isHorizontal)
        {
            _isHorizontal = false;
            gameObject.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0f, 0f, -90f);
            displayDigit.gameObject.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0f, 0f, 90f);
            displayDigit.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(4, 5, 0);
        }
        else
        {
            _isHorizontal = true;
            gameObject.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0f, 0f, 0f);
            displayDigit.gameObject.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0f, 0f, 0f);
            displayDigit.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(5, -4, 0);
        }
            
    }
}
