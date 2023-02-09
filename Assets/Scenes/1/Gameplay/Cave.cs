using Utils;
using UnityEngine;
using Enums;

namespace Gameplay
{
    public class Cave
    {
        private readonly int _maxWidth;
        private readonly int _maxHeight;
      
        public Grid<GridCell<bool>> Grid { get; }

        public Cave(int maxWidth, int maxHeight)
        {
            _maxWidth = maxWidth;
            _maxHeight = maxHeight;
            Grid = new Grid<GridCell<bool>>(maxWidth, maxHeight, InitializeCaveCell);
        }

        private GridCell<bool> InitializeCaveCell(int x, int y)
        {
            return new GridCell<bool>(x, y, true);
        }

        public void DigCorridors(int CellsToRemove,int safeWalls)
        {
            var walkerPosition = new Vector2Int(_maxWidth / 2, _maxHeight / 2);
            while(CellsToRemove > 0)
            {
                var randomDirection = RandomUtils.GetRandomEnumValue<Direction>();
                var newWalkerPosition = walkerPosition + randomDirection.ToCoords();
                if (!Grid.AreCoordsValid(newWalkerPosition, safeWalls)) continue;

                var cell = Grid.Get(newWalkerPosition);
                if(cell.Value)
                {
                    cell.Value = false;
                    CellsToRemove--;
                }
                walkerPosition = newWalkerPosition;
            }
        }
    }
}