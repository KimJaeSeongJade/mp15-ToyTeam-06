using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseChangeEffet : MonoBehaviour
{
	[SerializeField] private GameObject _changeEffect;

	public void Play(Vector3 pos)
	{
		if (_changeEffect != null) return;

		Instantiate(_changeEffect, pos, Quaternion.identity);
	}
}
