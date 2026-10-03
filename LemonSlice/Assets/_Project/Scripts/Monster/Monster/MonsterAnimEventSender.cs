using UnityEngine;

public class MonsterAnimEventSender : MonoBehaviour
{
	private MonsterController _monsterController;

	private void Awake()
	{
		CacheComponents();
	}

	private void CacheComponents()
	{
		_monsterController = GetComponentInParent<MonsterController>();
	}

	public void OnAnimEvent(string animEvent) => _monsterController.OnAnimEvent(animEvent);
}
