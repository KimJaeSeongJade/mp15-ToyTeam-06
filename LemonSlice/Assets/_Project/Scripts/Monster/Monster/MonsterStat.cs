using UnityEngine;

public class MonsterStat : MonoBehaviour
{
	[SerializeField] private int maxHealth; // 최대 체력
	[SerializeField] private float moveSpeed; // 이동속도
	[SerializeField] private int attackPower;
	[SerializeField] private float rotationSpeed;
	[SerializeField] private float attackDelay;
	[SerializeField] private float attackDistance;


	public ObservableProperty<int> currentHealth = new(0);
	public float MoveSpeed => moveSpeed;
	public float AttackDelay => attackDelay;
	public float AttackDistance => attackDistance;
	public int AttackPower => attackPower;
	private void Awake() => Init();

	private void Init()
	{
		currentHealth.Value = maxHealth;
	}
}
