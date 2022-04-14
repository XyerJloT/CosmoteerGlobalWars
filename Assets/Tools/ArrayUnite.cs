using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayUnite : MonoBehaviour
{
    public List<GameObject> CombatArrays(GameObject[] gameObjects, GameObject[] gameObjects1, GameObject[] gameObjects2, GameObject[] gameObjects3, GameObject[] gameObjects4)
    {
        List<GameObject> allGameObjects = new List<GameObject>();
        for (int i = 0; i < gameObjects.Length; i++)
        {
            allGameObjects.Add(gameObjects[i]);
        }

        for (int i = 0; i < gameObjects1.Length; i++)
        {
            allGameObjects.Add(gameObjects[i]);
        }

        if (gameObjects2 != null)
        {
            for (int i = 0; i < gameObjects2.Length; i++)
            {
                allGameObjects.Add(gameObjects2[i]);
            }
        }
        else return (allGameObjects);

        if (gameObjects3 != null)
        {
            for (int i = 0; i < gameObjects3.Length; i++)
            {
                allGameObjects.Add(gameObjects3[i]);
            }
        }
        else return (allGameObjects);

        if (gameObjects4 != null)
        {
            for (int i = 0; i < gameObjects4.Length; i++)
            {
                allGameObjects.Add(gameObjects4[i]);
            }
        }
        else return (allGameObjects);

        return (allGameObjects);
    }
}
