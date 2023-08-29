using System.Collections.Generic;
using UnityEngine;


public class UIGameMenu : MonoBehaviour
{
    //[SerializeField] private MouseHandler mouseHandler = null;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            /*
            if(CanSetActiveToFalse())
            {
                gameObject.SetActive(false);
            }
            */
        }
    }
    /*
    private bool CanSetActiveToFalse()
    {
        List<GameObject> raycastedGameObjects = new List<GameObject>();
        bool returnValue = true;

        raycastedGameObjects = mouseHandler.GetUIGameObjectsMousePointer();

        if (raycastedGameObjects.Count == 0)
            returnValue = true;
        else
        {
            foreach (GameObject raycastedGameObject in raycastedGameObjects)
            {
                if (raycastedGameObject.name == "ButtonGameMenu" || raycastedGameObject.name == "GameMenu")
                {
                    returnValue = false;
                    break;
                }
            }
        }
       
        return returnValue;
    }
    */
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
