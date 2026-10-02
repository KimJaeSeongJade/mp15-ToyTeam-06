using UnityEngine;

public class PlayerAnimEventSender : MonoBehaviour
{
	private PlayerController _playerController;

	private void Awake()
	{
		CacheComponents();
	}

	private void CacheComponents()
	{
		_playerController = GetComponentInParent<PlayerController>();
	}

	public void OnAnimEvent(string animEvent) => _playerController.OnAnimEvent(animEvent);
}
