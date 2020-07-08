using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCell : MonoBehaviour
{
	public GameObject referenceObject;

	private void Start()
	{
		referenceObject = GameObject.Find("Player");
	}

	// Update is called once per frame
	void Update()
    {
		Vector3 refVecX = VectToRef();
		refVecX.z = 0;
		Vector3 refVecZ = VectToRef();
		refVecZ.x = 0;

		if (refVecX.magnitude > Bounds.DistanceThreshold.x) MoveTileX();
		if (refVecZ.magnitude > Bounds.DistanceThreshold.y) MoveTileZ();
    }

	private void MoveTileX()
	{
		float jDirX = Mathf.Sign(referenceObject.transform.position.x - transform.position.x);
		Vector3 newPos = new Vector3(jDirX * Bounds.CellSize * Bounds.Cols, 0, 0);
		transform.position += newPos;
	}

	private void MoveTileZ()
	{
		float jDirY = Mathf.Sign(referenceObject.transform.position.z - transform.position.z);
		Vector3 newPos = new Vector3(0, 0, jDirY * Bounds.CellSize * Bounds.Rows);
		transform.position += newPos;
	}

	protected Vector3 VectToRef()
	{
		return referenceObject.transform.position - transform.position ;
	}
}
