using UnityEngine;

public class PlayerUIBinder : MonoBehaviour
{
	[SerializeField] private PlayerHp playerHp;

	private PlayerStat playerStat;

	private void Awake() => CacheComponents();
	private void OnEnable() => BindPlayerStatChangeEvents();
	private void OnDisable() => UnBindPlayerStatChangeEvents();

	private void CacheComponents()
	{
		playerStat = GetComponent<PlayerStat>();
	}

	private void BindPlayerStatChangeEvents()
	{
		playerStat.CurrentHealth.AddListener(playerHp.RefreshCurrentHp);
		playerStat.MaxHealth.AddListener(playerHp.InitMaxHp);
	}

	private void UnBindPlayerStatChangeEvents()
	{
		playerStat.CurrentHealth.RemoveListener(playerHp.RefreshCurrentHp);
		playerStat.MaxHealth.RemoveListener(playerHp.InitMaxHp);
	}
}
