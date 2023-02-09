using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CaveMonoBehaviour : MonoBehaviour
{
	[SerializeField] protected int _maxWidth;
	[SerializeField] protected int _maxHeight;

	protected int[,] map;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			GenerateMap();
		}


		if (Input.GetKeyDown(KeyCode.Tab))
		{
			map = new int[_maxWidth, _maxHeight];
			StepByStepGeneration(true);
		}


		if (Input.GetKeyDown(KeyCode.Return))
        {
			StepByStepGeneration(false);
		}	

		if (Input.GetKey(KeyCode.RightArrow))
        {
			StepByStepGeneration(false);
		}
	}

	public abstract void GenerateMap();

	public abstract void StepByStepGeneration(bool fistStep);

	protected void OnDrawGizmos()
	{
		if (map != null)
		{
			for (int x = 0; x < _maxWidth; x++)
			{
				for (int y = 0; y < _maxHeight; y++)
				{
					Gizmos.color = (map[x, y] == 1) ? Color.black : Color.white;
					Vector3 pos = new Vector3(-_maxWidth / 2 + x + .5f, -_maxHeight / 2 + y + .5f, 0);
					Gizmos.DrawCube(pos, Vector3.one);
				}
			}
		}
	}

}
