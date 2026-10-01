using UnityEngine;

public class BossStat : MonoBehaviour
{
	[SerializeField] private Transform lockonPosition; // 락온포지션
	[SerializeField] private int maxHealth; // 최대 체력
	[SerializeField] private int maxGroggy; // 최대 그로기 수치
	[SerializeField] private float moveSpeed; // 이동속도
	[SerializeField] private int attackPower; // 공격력

	public ObservableProperty<int> currentGroggy = new(0);
	public ObservableProperty<int> currentHealth = new(0);

	// -------이벤트 함수--------

	private void Start() => Init();

	// --------------------

	private void Init()
	{
		currentHealth.Value = maxHealth;
		currentGroggy.Value = maxGroggy;
	}
}
