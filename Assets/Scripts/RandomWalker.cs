using Enums;
using UnityEngine;
using Utils;

public class RandomWalker : CaveMonoBehaviour
{
    [SerializeField] int _cellsToRemove;
    [SerializeField] int _safeWalls;
    Vector2Int save;

    public override void GenerateMap()
    {
        map = new int[_maxWidth, _maxHeight];

        for (int x = 0; x < _maxWidth; x++)
        {
            for (int y = 0; y < _maxHeight; y++)
            {
                map[x, y] = 1;
            }
        }
        DigCorridors(_cellsToRemove, _safeWalls, new(_maxWidth / 2, _maxHeight / 2));
    }

    public override void StepByStepGeneration(bool firstStep)
    {
        if (firstStep)
        {
            for (int x = 0; x < _maxWidth; x++)
            {
                for (int y = 0; y < _maxHeight; y++)
                {
                    map[x, y] = 1;
                }
            }
            save = new(_maxWidth / 2, _maxHeight / 2);
        }
        else
        {
            DigCorridors(1, _safeWalls, save);
        }
    }
    
    void DigCorridors(int CellsToRemove, int safeWalls, Vector2Int walkerPosition)
    {
        while (CellsToRemove > 0)
        {
            var randomDirection = RandomUtils.GetRandomEnumValue<Direction>();
            Vector2Int newWalkerPosition = walkerPosition + randomDirection.ToCoords();

            if (!(newWalkerPosition.x >= 0 + safeWalls && newWalkerPosition.x < _maxWidth - safeWalls && newWalkerPosition.y >= 0 + safeWalls && newWalkerPosition.y < _maxHeight - safeWalls)) continue;
            int x = newWalkerPosition.x;
            int y = newWalkerPosition.y;

            if (map[x, y] == 1)
            {
                map[x, y] = 0;
                CellsToRemove--;
            }
            walkerPosition = newWalkerPosition;
            save = walkerPosition;
        }
    }
}
