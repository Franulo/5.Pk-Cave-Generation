using UnityEngine;
using UnityEngine.Tilemaps;

namespace Gameplay
{
    public class CaveGenerator : MonoBehaviour
    {
        [SerializeField] Tilemap _wallsTilemap;
        [SerializeField] Tilemap _groundTilemap;
        [SerializeField] TileBase[] _wallTiles;
        [SerializeField] TileBase[] _groundTiles;
        [SerializeField] int _maxWidth;
        [SerializeField] int _maxHeight;
        [SerializeField] int _cellsToRemove;
        [SerializeField] int _safeWalls;
        [ContextMenu("Generate Cave")]
        public void Generate()
        {
            var cave = new Cave(_maxWidth, _maxHeight);
            cave.DigCorridors(_cellsToRemove, _safeWalls);
            _wallsTilemap.ClearAllTiles();
            _groundTilemap.ClearAllTiles();
            foreach (var cell in cave.Grid.Cells)
            {
                if (cell.Value)
                {
                    var randomWall = _wallTiles[Random.Range(0, _wallTiles.Length)]; //?
                    _wallsTilemap.SetTile(new Vector3Int(cell.X, cell.Y, 0), randomWall);
                }
                else 
                {
                    var randomGround = _groundTiles[Random.Range(0, _groundTiles.Length)];
                    _groundTilemap.SetTile(new Vector3Int(cell.X, cell.Y, 0), randomGround);
                }
            }
        }

        public void OnValidate()
        {
            //Limits Values to 0
            _safeWalls = Mathf.Max(_safeWalls, 0);
            _cellsToRemove = Mathf.Max(_cellsToRemove, 0);
            _maxHeight = Mathf.Max(_maxHeight, 0);
            _maxWidth = Mathf.Max(_maxWidth, 0);
            //Limits Values so there is no error
            _safeWalls = Mathf.Min(_safeWalls, _maxWidth / 2, _maxHeight / 2);
            _cellsToRemove = Mathf.Min(_cellsToRemove, (_maxHeight - 2 * _safeWalls) * (_maxWidth - 2 * _safeWalls));
        }
    }
}