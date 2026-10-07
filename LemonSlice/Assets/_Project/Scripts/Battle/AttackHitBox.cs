using System.Collections.Generic;
using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
	[SerializeField] private LayerMask targetLayerMask;

	private HashSet<Collider> _targets = new();
	private bool _isActive;
	private int _damage;
	private int _downValue;

	private void OnTriggerEnter(Collider other)
	{
		if (!_isActive)
		{
			return;
		}

		if (_targets.Contains(other) || !targetLayerMask.Contains(other.gameObject.layer))
		{
			return;
		}

		_targets.Add(other);
		IDamageable target = other.GetComponent<IDamageable>();

		Vector3 hitDirection = transform.forward;
		Vector3 hitPoint = other.ClosestPoint(transform.position);

		target.TakeDamage(new DamageInfo(_damage, _downValue, hitPoint, hitDirection));
	}

	public void Open(int damage, int downValue)
	{
		_isActive = true;
		_damage = damage;
		_downValue = downValue;
	}

	public void Close()
	{
		_targets.Clear();
		_isActive = false;
	}
}
