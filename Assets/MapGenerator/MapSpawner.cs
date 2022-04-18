using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSpawner : MonoBehaviour
{
    public int Height = 5;
    public int Weight = 5;


    [SerializeField] List<Vector2> positions;
    [SerializeField] [Range(0, 1)] float _percentOfPosToDestroy = 0.25f; //процент координат, подлежащих уничтожению
    //[SerializeField] GameObject Obj; это для проверки рандомности

    public void Reload() //для удобства, потом удалить нахуй!!!
    {
        SceneManager.LoadScene("Generator");
    }

    void Start()
    {
        MapGeneration _generator = new MapGeneration();
        MapGeneratorStars[,] map = _generator.GenerateMap(Weight * 2, Height * 2);

        for (int x = 0; x < map.GetLength(0); x += 2)
        {
            for (int y = 0; y < map.GetLength(1); y += 2)
            {
                Debug.Log($"{x} {y}");
                positions.Add(new Vector2(x, y));
            }
        }

        ManageVectors();
    }

    private void ManageVectors()
    {
        int _countOfPosToDestroy = Mathf.RoundToInt(_percentOfPosToDestroy * positions.Count);
        for (int r = 0; r < _countOfPosToDestroy;  r++)
        {
            int randStarToRemove = Random.Range(0, positions.Count - 1);
            positions.RemoveAt(randStarToRemove);
        }

        for (int i = 0; i < positions.Count; i++)
        {
            float randPos = Random.Range(0f, 1.7f);
            positions[i] = new Vector2(positions[i].x + randPos, positions[i].y + randPos);
           // Instantiate(Obj, positions[i], Quaternion.identity);          
        }

        for (int first = 0; first < positions.Count; first++)
        {
            List<float> findIndex = new List<float>();
            List<float> dist = new List<float>();
            for (int second = 0; second < positions.Count; second++)
            {
                findIndex.Add(Vector2.Distance(positions[first], positions[second]));

                if (first != second)
                dist.Add(Vector2.Distance(positions[first], positions[second]));
            }
            float _minDist = dist.Min();
            int _index = findIndex.IndexOf(_minDist);
            //мы получили индекс звезды, с которой у нас будет минимальное расстояние; _minDist по индексам = positions
        }
    }

}
