using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
	[SerializeField] private LayerMask layermask;
	[SerializeField] private List<Transform> intriggerEnemies;

	private SphereCollider sphereCollider;

	private float detectRange => sphereCollider.radius;


	// ------이벤트 함수-------

	private void Awake() => CacheComponents();

	private void OnDrawGizmos()
	{
		if (sphereCollider == null)
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
			intriggerEnemies.Add(other.transform);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (IsInPlayerLayer(other.gameObject))
		{
			other.GetComponent<MonsterController>().SetLockOnUi(false);
			intriggerEnemies.Remove(other.transform);
		}
	}

	public List<Transform> GetEnemyList()
	{
		if (intriggerEnemies == null)
		{
			return null;
		}

		return intriggerEnemies;
	}

	// ------------------


	// TODO 추후 부채꼴 감지, 실시간 몬스터 위치에 따른 락온 순서 변경
	// private void DetectInRange(Transform target)
	// {
	// 	Vector3 vectorToTarget = (target.position - transform.position).normalized;
	//
	// 	float targetDot = Vector3.Dot(parentTransform.forward, vectorToTarget);
	//
	// 	float threshold = Mathf.Cos(detectAngle * 0.5f * Mathf.Deg2Rad);
	//
	// 	if (targetDot > threshold && !lockonables.Contains(target.transform))
	// 	{
	// 		//ILockonable target = triggerTransform.GetComponent<ILockonable>();
	// 		lockonables.Add(target.transform);
	// 	}
	// }


	private bool IsInPlayerLayer(GameObject target)
	{
		return (layermask.value & (1 << target.layer)) != 0;
	}

	private void CacheComponents()
	{
		sphereCollider = GetComponent<SphereCollider>();
	}
}
