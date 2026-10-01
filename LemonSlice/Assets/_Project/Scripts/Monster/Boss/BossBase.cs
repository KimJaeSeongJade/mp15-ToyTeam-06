using UnityEngine;

public class BossBase : MonoBehaviour, ILockonable
{
	[SerializeField] private Transform lockonPosition; // 락온포지션
	[SerializeField] private int maxhealth; // 최대 체력
	[SerializeField] private int maxGroggy; // 최대 그로기 수치
	[SerializeField] private int moveSpeed; // 이동속도
	[SerializeField] private int attackPower; // 공격력

	public ObservableProperty<int> currentGroggy = new(0);
	public ObservableProperty<int> currentHealth = new(0);

	// ====================

	private void Awake()
	{
	}

	private void Start() => Init();

	// ====================

	public GameObject GameObject
	{
		get => gameObject;
	}

	public Transform GetLockonPosition()
	{
		return lockonPosition;
	}

	private void Init()
	{
		currentHealth.Value = maxhealth;
		currentGroggy.Value = maxGroggy;
	}
}
