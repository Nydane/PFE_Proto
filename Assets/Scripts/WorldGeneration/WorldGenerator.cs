using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
	public GameObject cellPrefab;

	public void Generate(int cellRows, int cellCols)
	{
		for (int y = 0; y < cellRows; y++)
		{
			for (int x = 0; x < cellCols; x++)
			{
				Vector3 spawnPos = new Vector3(x * Bounds.CellSize, 0, y * Bounds.CellSize);
				GameObject Go = Instantiate(cellPrefab, spawnPos, Quaternion.identity);
			}
		}
	}
}
