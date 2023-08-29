using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WindowSettings", menuName = "Assambra/WindowSettings", order = 1)]
public class WindowSettings : ScriptableObject
{
    public KeyCode windowShortcutKey = KeyCode.None;
    public string windowName;
    public Sprite windowIcon;
    public bool IgnoreWindowMaxSize;
    public Vector2 WindowMinsize;
    public Vector2 WindowMaxSize;
    public bool windowStartActivated;
    public bool windowHasCloseButton;
    public bool windowHasResizeButtons;
}
