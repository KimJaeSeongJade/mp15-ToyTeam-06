using UnityEngine;

public class LockRootBonePosition : MonoBehaviour
{
	[SerializeField] private Transform hips;

	private Vector3 startLocalPos;

	private void Start()
	{
		startLocalPos = hips.localPosition;
	}

	private void LateUpdate()
	{
		Vector3 pos = hips.localPosition;
		pos.x = startLocalPos.x;
		pos.z = startLocalPos.z;
		hips.localPosition = pos;
	}
}
