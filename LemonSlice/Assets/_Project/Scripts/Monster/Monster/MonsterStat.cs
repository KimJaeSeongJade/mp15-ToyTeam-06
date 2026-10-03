using UnityEngine;

public class MonsterStat : MonoBehaviour
{
	[SerializeField] private Transform lockonPosition; // 락온포지션
	[SerializeField] private int maxHealth; // 최대 체력
	[SerializeField] private float moveSpeed; // 이동속도
	[SerializeField] private int attackPower;
	[SerializeField] private float rotationSpeed;

	public ObservableProperty<int> currentHealth = new(0);
	public float MoveSpeed => moveSpeed;
	private void Awake() => Init();

	private void Init()
	{
		currentHealth.Value = maxHealth;
	}
}
