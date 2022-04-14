using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    public static int _height = 5;
    public static int _weight = 5;


    public GameObject[] starsPrefab; 
    // small = 50%, normal = 30%, big = 15%, giant = 5%

    void Start()
    {
        //генерация

        MapGeneration _generator = new MapGeneration();
        MapGeneratorStars[,] _map = _generator.GenerateMap(_weight, _height); //вызываем функцию

        for (int x = 0; x < _map.GetLength(0); x++) //ширина, работаем с первой осью массива
        {
            for (int y = 0; y < _map.GetLength(1); y++) //высота, вторая ось массива
            {
                GameObject _objectToSpawn; //спавн объекта с рандомным шансом
                int _percentToSpawn = Random.Range(1, 100);
                if (_percentToSpawn >= 1 && _percentToSpawn <= 50) _objectToSpawn = starsPrefab[0];
                else if (_percentToSpawn > 50 && _percentToSpawn <= 87) _objectToSpawn = starsPrefab[1];
                else if (_percentToSpawn > 87 && _percentToSpawn <= 97) _objectToSpawn = starsPrefab[2];
                else _objectToSpawn = starsPrefab[3];

                Instantiate(_objectToSpawn, new Vector2(x, y), Quaternion.identity);
            }
        }
    }

}
