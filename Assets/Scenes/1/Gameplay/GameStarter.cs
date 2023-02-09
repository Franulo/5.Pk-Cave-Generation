using UnityEditor;
using UnityEngine;

namespace Gameplay
{
    public class GameStarter : MonoBehaviour
    {
        [SerializeField] CaveGenerator _grid;

        private void Start()
        {
            _grid.Generate();
        }
    }
   
}