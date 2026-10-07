using UnityEngine;

public class PlayerUIBinder : MonoBehaviour
{
	[SerializeField] private PlayerHp playerHp;

	private PlayerStat playerStat;

	private void Awake() => CacheComponents();
	private void OnEnable() => BindBossStatChangeEvents();
	private void OnDisable() => UnBindBossStatChangeEvents();

	private void CacheComponents()
	{
		playerStat = GetComponent<PlayerStat>();
	}

	private void BindBossStatChangeEvents()
	{
		playerStat.CurrentHealth.AddListener(playerHp.RefreshCurrentHp);
		playerStat.MaxHealth.AddListener(playerHp.InitMaxHp);
	}

	private void UnBindBossStatChangeEvents()
	{
		playerStat.CurrentHealth.RemoveListener(playerHp.RefreshCurrentHp);
		playerStat.MaxHealth.RemoveListener(playerHp.InitMaxHp);
	}
}
