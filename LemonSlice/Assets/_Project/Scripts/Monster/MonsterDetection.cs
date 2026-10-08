using System;
using UnityEngine;

public class MonsterDetection : MonoBehaviour
{
	[SerializeField] private LayerMask targetLayer;
	[SerializeField] private bool isBoss; // 보스가 아니라면 체크 해제
	[SerializeField] private float detectAngle;

	private SphereCollider sphereCollider;
	private float detectRange;

	public bool IsPlayerEnter { get; private set; }
	public bool IsInSight { get; private set; }
	public Transform TargetTransform { get; private set; }


	// ------이벤트 함수-------

	private void Awake() => CacheComponents();

	private void Start() => Init();

	private void Update() => Detecting();

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

		if (transform.parent == null) return;
		if (detectAngle == 0) return;
		Vector3 leftDir = Quaternion.Euler(0f,-detectAngle / 2 , 0f) * transform.parent.forward;
		Vector3 rightDir = Quaternion.Euler(0f,detectAngle / 2 , 0f) * transform.parent.forward;

		Gizmos.DrawRay(transform.position, leftDir * detectRange);
		Gizmos.DrawRay(transform.position, rightDir * detectRange);
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
		sphereCollider = GetComponent<SphereCollider>();
	}

	private void Init()
	{
		detectRange = sphereCollider.radius;
	}

	private void Detecting()
	{
		if (!IsPlayerEnter) return;

		if (IsPlayerInSight(TargetTransform))
		{
			IsInSight = true;
		}
		else
		{
			IsInSight = false;
		}
	}

	private bool IsInPlayerLayer(GameObject target)
	{
		return (targetLayer.value & (1 << target.layer)) != 0;
	}

	private bool IsPlayerInSight(Transform target)
	{
		Vector3 vectorToTarget = (target.position - transform.position).normalized;

		float targetDot = Vector3.Dot(transform.forward, vectorToTarget);

		float threshold = Mathf.Cos(detectAngle * 0.5f * Mathf.Deg2Rad);

		return (targetDot >= threshold);
	}
}
