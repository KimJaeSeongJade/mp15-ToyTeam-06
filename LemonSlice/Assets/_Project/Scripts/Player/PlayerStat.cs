using UnityEngine;
using UnityEngine.Serialization;

public class PlayerStat : MonoBehaviour
{
	[SerializeField] private int maxHealth; // 최대체력
	[SerializeField] private int maxStamina; // 최대 스테미나
	[SerializeField] private int attackPower; // 공격력
	[SerializeField] private float moveSpeed; // 이동 속도
	[SerializeField] private float invincibleTime; // 무적 시간
	[SerializeField] private int downPoint;
	[SerializeField] private int maxDownPoint;
	[SerializeField] private float knockDownForce;
	[SerializeField] private float rollForce;

	public ObservableProperty<int> currentHealth = new(0); // 현재 체력
	public ObservableProperty<int> currentStamina = new(0); // 현재 스테미나

	private bool _isInvincible;

	public float MoveSpeed => moveSpeed;
	public float RollForce => rollForce;
	public int AttackPower => attackPower;
	public int MaxDownPoint => maxDownPoint;
	public float KnockDownForce => knockDownForce;
	public bool IsInvincible { get => _isInvincible; set => _isInvincible = value; }
	public int DownPoint { get => downPoint; set => downPoint = value; }



	// -------이벤트 함수--------

	private void Start() => Init();

	// --------------------

	private void Init()
	{
		currentHealth.Value = maxHealth;
		currentStamina.Value = maxStamina;
	}
}
