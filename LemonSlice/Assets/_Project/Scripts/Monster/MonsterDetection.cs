using System;
using UnityEngine;

public class MonsterDetection : MonoBehaviour
{
	[SerializeField] private LayerMask targetLayer;
	[SerializeField] private bool isBoss; // 보스가 아니라면 체크 해제

	private SphereCollider _sphereCollider;
	private float detectRange;

	public bool IsPlayerEnter { get; private set; }
	public Transform TargetTransform { get; private set; }


	// ------이벤트 함수-------

	private void Awake() => CacheComponents();

	private void Start() => Init();

	private void OnDisable()
	{
		IsPlayerEnter = false;
		TargetTransform = null;

	}

	private void OnDrawGizmos()
	{
		if (detectRange == 0)
		{
			return;
		}

		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, detectRange);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (IsInPlayerLayer(other.gameObject))
		{
			IsPlayerEnter = true;
			TargetTransform = other.transform;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (isBoss)
		{
			return; // 보스면 TriggerExit 로직 수행x
		}

		if (IsInPlayerLayer(other.gameObject))
		{
			IsPlayerEnter = false;
			TargetTransform = null;
		}
	}

	// ------------------

	private void CacheComponents()
	{
		_sphereCollider = GetComponent<SphereCollider>();
	}

	private void Init()
	{
		detectRange = _sphereCollider.radius;
	}

	private bool IsInPlayerLayer(GameObject target)
	{
		return (targetLayer.value & (1 << target.layer)) != 0;
	}
}
