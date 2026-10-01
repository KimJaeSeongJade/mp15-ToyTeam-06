using UnityEngine;

public class PlayerBase : MonoBehaviour
{
	private const int PlayerMaxHealth = 100; // 플레이어 최대체력
	private const int PlayerMaxStamina = 100; // 플레이어 최대 스테미나
	[SerializeField] private int playerHealth; // 플레이어 체력
	[SerializeField] private int playerStamina; // 플레이어 스테미나
	[SerializeField] private int attackPower; // 공격력
	[SerializeField] private float invincibleTime; // 무적시간
	[SerializeField] private float moveSpeed; // 이동속도
}
