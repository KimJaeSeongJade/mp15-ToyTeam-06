using UnityEngine;

public class PlayerStat : MonoBehaviour
{
	[SerializeField] private int maxHealth; // 최대체력
	[SerializeField] private int maxStamina; // 최대 스테미나
	[SerializeField] private int attackPower; // 공격력
	[SerializeField] private float moveSpeed; // 이동 속도
	[SerializeField] private float invincibleTime; // 무적 시간
	[SerializeField] private float rollSpeed;
	[SerializeField] private int downPoint;
	[SerializeField] private int maxDownPoint;

	public ObservableProperty<int> currentHealth = new(0); // 현재 체력
	public ObservableProperty<int> currentStamina = new(0); // 현재 스테미나
	private bool _isInvincible;

	public float MoveSpeed => moveSpeed;
	public float RollSpeed => rollSpeed;
	public int AttackPower => attackPower;
	public int MaxDownPoint => maxDownPoint;
	public int DownPoint { get; set; }
	public bool IsInvincible => _isInvincible;

	// -------이벤트 함수--------

	private void Start() => Init();

	// --------------------

	private void Init()
	{
		currentHealth.Value = maxHealth;
		currentStamina.Value = maxStamina;
	}

	public void SetInvincible(bool invincible)
	{
		_isInvincible = invincible;
	}
}
