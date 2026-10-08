using UnityEngine;

public class BossStat : MonoBehaviour
{
	[SerializeField] private int maxHealth; // 최대 체력
	[SerializeField] private int maxGroggy; // 최대 그로기 수치
	[SerializeField] private float groggyTime;
	[SerializeField] private float moveSpeed; // 이동속도
	[SerializeField] private int attackPower; // 공격력
	[SerializeField] private float attackDelay;
	[SerializeField] private float attackDistance;

	public ObservableProperty<int> CurrentGroggy = new(0);
	public ObservableProperty<int> CurrentHealth = new(0);
	public ObservableProperty<int> MaxGroggy = new(0);
	public ObservableProperty<int> MaxHealth = new(0);

	public bool IsInvincible { get; set; }
	public float GroggyTime => groggyTime;
	public float MoveSpeed => moveSpeed;
	public int AttackPower => attackPower;
	public float AttackDelay => attackDelay;
	public float AttackDistance => attackDistance;

	// -------이벤트 함수--------

	private void Start() => Init();

	// --------------------

	private void Init()
	{
		CurrentHealth.Value = maxHealth;
		MaxHealth.Value = maxHealth;
		CurrentGroggy.Value = maxGroggy;
		MaxGroggy.Value = maxGroggy;
	}

	public void SetFullGroggy()
	{
		CurrentGroggy.Value = maxGroggy;
	}
}
