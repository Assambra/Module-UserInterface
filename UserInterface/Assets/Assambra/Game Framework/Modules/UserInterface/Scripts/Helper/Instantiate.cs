using UnityEngine;

namespace Helper
{
    public static class Instantiate
    {
        public static GameObject InstantiateUIElement(GameObject go, Transform placeToInstantiate)
        {
            GameObject gameobject = MonoBehaviour.Instantiate(go, placeToInstantiate);
            gameobject.name = gameobject.name.Replace("(Clone)", null);
            return gameobject;
        }

        public static void DestroyUIElement(GameObject go)
        {
            MonoBehaviour.Destroy(go);
        }
    }
}

