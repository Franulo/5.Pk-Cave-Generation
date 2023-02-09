using UnityEngine;
using System.Collections;
using System;

public class CellularAutomata : CaveMonoBehaviour
{
	[SerializeField] string seed;
	[SerializeField] bool useRandomSeed;
	[SerializeField] bool game_of_life;
	[Range(0, 100)]
	[SerializeField] int randomFillPercent;
	[SerializeField] int smoothness;

	public override void GenerateMap()
	{
		map = new int[_maxWidth, _maxHeight];
		RandomFillMap();

		for (int i = 0; i < smoothness; i++)
		{
			SmoothMap(game_of_life);
		}
	}

	public override void StepByStepGeneration(bool firstStep)
	{
		if (firstStep)
		{
			RandomFillMap();
		}
		else
        {
			SmoothMap(game_of_life);
		}
	}

	void RandomFillMap()
	{
		if (useRandomSeed)
		{
			seed = Time.time.ToString();
		}

		System.Random pseudoRandom = new System.Random(seed.GetHashCode());

		for (int x = 0; x < _maxWidth; x++)
		{
			for (int y = 0; y < _maxHeight; y++)
			{
				if (x == 0 || x == _maxWidth - 1 || y == 0 || y == _maxHeight - 1)
				{
					map[x, y] = 1;
				}
				else
				{
					map[x, y] = (pseudoRandom.Next(0, 100) < randomFillPercent) ? 1 : 0;
				}
			}
		}
	}

	void SmoothMap(bool game_of_life)
	{
		for (int x = 0; x < _maxWidth; x++)
		{
			for (int y = 0; y < _maxHeight; y++)
			{
				if (game_of_life)
                {
					Game_Of_Life(x, y);
					if (x == 0 || x == _maxWidth-1 || y == 0 || y == _maxHeight-1)
                    {
						map[x, y] = 0;
                    }
                }
				else
                {
					Cave(x, y);
                }

			}
		}
	}

	void Cave(int x, int y)
    {
		int neighbourWallTiles = GetSurroundingWallCount(x, y);

		if (neighbourWallTiles > 4)
			map[x, y] = 1;
		else if (neighbourWallTiles < 4)
			map[x, y] = 0;
	}

	void Game_Of_Life(int x, int y)
    {
		int neighbourWallTiles = GetSurroundingWallCount(x, y);

		if(map[x, y] == 1 && neighbourWallTiles != 2 && neighbourWallTiles != 3)
        {
			map[x, y] = 0;
        }

		if ( map[x, y] == 0 && neighbourWallTiles == 3)
        {
			map[x, y] = 1;
        }
	}

	int GetSurroundingWallCount(int gridX, int gridY)
	{
		int wallCount = 0;
		for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
		{
			for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
			{
				if (neighbourX >= 0 && neighbourX < _maxWidth && neighbourY >= 0 && neighbourY < _maxHeight)
				{
					if (neighbourX != gridX || neighbourY != gridY)
					{
						wallCount += map[neighbourX, neighbourY];
					}
				}
				else
				{
					wallCount++;
				}
			}
		}

		return wallCount;
	}
}
