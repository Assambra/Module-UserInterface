using TMPro;
using UnityEngine;

public class TabDragObject : MonoBehaviour
{
    [SerializeField] TMP_Text textTabName;

    public void SetTabName(string name)
    {
        textTabName.text = name;
    }
}
