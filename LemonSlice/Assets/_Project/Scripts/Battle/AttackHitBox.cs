using System;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
	[SerializeField] private LayerMask targetLayerMask;
	[SerializeField] private Transform owner;

	private HashSet<Collider> _targets = new();
	private bool _isActive;
	private int _damage;
	private int _downValue;
	private Collider _collider;

	private void Awake()
	{
		CacheComponents();
		_collider.enabled = false;
	}

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
		if ((targetLayerMask.value & (1 << other.gameObject.layer)) == 0) return;

		_targets.Add(other);
		IDamageable target = other.GetComponent<IDamageable>();

		if (target != null)
		{
			Vector3 direction = other.transform.position - owner.position;
			Vector3 knockDownDirection = new Vector3(direction.x, 1, direction.z).normalized;
			Vector3 hitDirection = new Vector3(-direction.x, 0, -direction.z).normalized;
			Vector3 hitPoint = other.ClosestPoint(transform.position);
			target.TakeDamage(new DamageInfo(_damage, _downValue, hitPoint, hitDirection, knockDownDirection));
		}
	}

	private void CacheComponents()
	{
		_collider = GetComponent<Collider>();
	}

	public void Open(int damage, int downValue)
	{
		_collider.enabled = true;
		_isActive = true;
		_damage = damage;
		_downValue = downValue;
	}

	public void Close()
	{
		_collider.enabled = false;
		_targets.Clear();
		_isActive = false;
	}
}
