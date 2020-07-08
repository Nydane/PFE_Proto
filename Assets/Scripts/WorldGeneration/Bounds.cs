using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour
{
	public WorldGenerator WG;

	public static float CellSize = 3.0f;
	public float _cellSize = 3.0f;

	public static Vector2 DistanceThreshold;

	public static int Cols = 0;
	public static int Rows = 0;

	public int cols = 0;
	public int rows = 0;

	public float offset = 1.0f;

	private void Start()
	{
		CellSize = _cellSize;
		Cols = cols;
		Rows = rows;
		if(WG != null) WG.Generate(rows, cols);
	}

	private void Update()
	{
		DistanceThreshold = ComputeThreshold();
		Debug.Log(DistanceThreshold);
	}

	protected Vector2 ComputeThreshold()
	{
		float boundX = (Mathf.Floor(Cols / 2) * 1.414f * CellSize) + offset;
		float boundY = (Mathf.Floor(Rows / 2) * 1.414f * CellSize) + offset;
		return new Vector2(boundX, boundY);
	}

}
