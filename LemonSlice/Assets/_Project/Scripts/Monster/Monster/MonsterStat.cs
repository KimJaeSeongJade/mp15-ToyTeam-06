using UnityEngine;

public class MonsterStat : MonoBehaviour
{
	[SerializeField] private int maxHealth; // 최대 체력
	[SerializeField] private float moveSpeed; // 이동속도
	[SerializeField] private int attackPower;
	[SerializeField] private float rotationSpeed;
	[SerializeField] private float attackDelay;
	[SerializeField] private float attackDistance;
	[SerializeField] private int downPoint;
	[SerializeField] private int maxDownPoint;
	[SerializeField] private float knockDownForce;

	private bool _isInvincible;

	public ObservableProperty<int> currentHealth = new(0);
	public float MoveSpeed => moveSpeed;
	public float AttackDelay => attackDelay;
	public float AttackDistance => attackDistance;
	public int AttackPower => attackPower;
	public int MaxDownPoint => maxDownPoint;
	public float KnockDownForce => knockDownForce;
	public bool IsInvincible { get => _isInvincible; set => _isInvincible = value; }
	public int DownPoint { get => downPoint; set => downPoint = value; }


	private void Awake() => Init();

	private void Init()
	{
		currentHealth.Value = maxHealth;
		downPoint = maxDownPoint;
		_isInvincible = false;
	}
}
