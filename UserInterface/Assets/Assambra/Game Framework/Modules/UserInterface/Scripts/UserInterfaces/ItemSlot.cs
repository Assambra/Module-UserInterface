using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public int SlotID;
    public bool IsEmpty = true;

    [SerializeField] Button itemButton;
    [SerializeField] Sprite emptySprite;

    private void Awake()
    {
        if (IsEmpty)
        {
            //itemButton.interactable = false;
            itemButton.image.sprite = emptySprite;
        }
           
    }

    private void Update()
    {
        
    }

}
