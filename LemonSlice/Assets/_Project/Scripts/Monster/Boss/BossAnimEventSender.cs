using UnityEngine;

public class BossAnimEventSender : MonoBehaviour
{
	private BossController _bossController;


	private void Awake()
	{
		CacheComponents();
	}

	private void CacheComponents()
	{
		_bossController = GetComponentInParent<BossController>();

	}

	public void OnAnimEvent(string animEvent)
	{
		_bossController.OnAnimEvent(animEvent);

	}
}
