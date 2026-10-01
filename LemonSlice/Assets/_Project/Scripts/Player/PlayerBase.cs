using UnityEngine;

public class PlayerBase : MonoBehaviour
{
	[SerializeField] private int playerMaxHealth; // 최대체력
	[SerializeField] private int playerMaxStamina; // 최대 스테미나
	[SerializeField] private int playerAttackPower; // 공격력
	[SerializeField] private float moveSpeed; // 이동 속도
	[SerializeField] private float invincibleTime; // 무적 시간

	public ObservableProperty<int> currentHealth; // 현재 체력
	public ObservableProperty<int> currentStamina; // 현재 스테미나

	private void Start() => Init();

	private void Init()
	{
		currentHealth = new ObservableProperty<int>(playerMaxHealth);
		currentStamina = new ObservableProperty<int>(playerMaxStamina);
	}
}
