using UnityEngine;

public class FillArray : MonoBehaviour
{
    public GameObject[] FillRectSizePlus(GameObject[] gameObjects, Vector2[] vector2s)
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].GetComponent<RectTransform>().sizeDelta += new Vector2(vector2s[i].x / 10, vector2s[i].y / 10);
        }
        return (gameObjects);
    }

    public GameObject[] FillRectSizeMinus(GameObject[] gameObjects, Vector2[] vector2s)
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].GetComponent<RectTransform>().sizeDelta -= new Vector2(vector2s[i].x / 10, vector2s[i].y / 10);
        }
        return (gameObjects);
    }
}
