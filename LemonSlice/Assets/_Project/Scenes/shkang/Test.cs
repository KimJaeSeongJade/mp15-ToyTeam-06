using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
	[SerializeField] private Transform target;

	private void Update()
	{
		Vector3 dir = target.position - transform.position;

		Quaternion direction = Quaternion.LookRotation(dir);
		transform.rotation = Quaternion.RotateTowards(transform.rotation,
			direction,
			0.5f);
		transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
	}

}
