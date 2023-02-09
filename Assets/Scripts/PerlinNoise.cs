using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise : CaveMonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] public float _limit;
    [SerializeField] float _resolution;

    public override void GenerateMap()
    {
        map = new int[_maxWidth, _maxHeight];
        int addRandomNumber = Random.Range(0, 10000);
        for (int x = 0; x < _maxWidth; x++)
        {
            for (int y = 0; y < _maxHeight; y++)
            {
                float X = x / _resolution;
                float Y = y / _resolution;

                map[x, y] = Round(Mathf.PerlinNoise(X + addRandomNumber, Y + addRandomNumber), _limit);
            }
        }
    }

    public override void StepByStepGeneration(bool fistStep)
    {
        Debug.Log("There is no such thing!");
    } 

    int Round(float toBeRounded, float Limit)
    {
        if(toBeRounded <= Limit)
        {
            return 0;
        }
        return 1;
    }
}
