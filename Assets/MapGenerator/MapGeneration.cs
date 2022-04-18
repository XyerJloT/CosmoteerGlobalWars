using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration
{

	public int _weight = 10;
	public int _height = 10;

	public MapGeneratorStars[,] GenerateMap(int _w, int _h)
	{
		_weight = _w;
		_height = _h;

		MapGeneratorStars[,] map = new MapGeneratorStars[_weight, _height];

		for (int x = 0; x < map.GetLength(0); x++)
		{
			for (int y = 0; y < map.GetLength(1); y++)
			{
				map[x, y] = new MapGeneratorStars {_x = x, _y = y}; 
			}
		}

		return map;
	}
}

public class MapGeneratorStars
{
	public int _x;
	public int _y;
}
