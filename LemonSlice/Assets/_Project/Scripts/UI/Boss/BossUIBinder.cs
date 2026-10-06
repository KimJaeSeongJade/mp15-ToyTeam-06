using UnityEngine;

public class BossUIBinder : MonoBehaviour
{
	[SerializeField] private BossHp bossHp;
	[SerializeField] private BossGroggy bossGroggy;

	private BossStat bossStat;

	private void Awake() => CacheComponents();
	private void OnEnable() => BindBossStatChangeEvents();
	private void OnDisable() => UnBindBossStatChangeEvents();

	private void CacheComponents()
	{
		bossStat = GetComponent<BossStat>();
	}

	private void BindBossStatChangeEvents()
	{
		bossStat.CurrentHealth.AddListener(bossHp.RefreshCurrentHp);
		bossStat.MaxHealth.AddListener(bossHp.InitMaxHp);
		bossStat.CurrentGroggy.AddListener(bossGroggy.RefreshCurrentGroggy);
		bossStat.MaxGroggy.AddListener(bossGroggy.InitMaxGroggy);
	}

	private void UnBindBossStatChangeEvents()
	{
		bossStat.CurrentHealth.RemoveListener(bossHp.RefreshCurrentHp);
		bossStat.MaxHealth.RemoveListener(bossHp.InitMaxHp);
		bossStat.CurrentGroggy.RemoveListener(bossGroggy.RefreshCurrentGroggy);
		bossStat.MaxGroggy.RemoveListener(bossGroggy.InitMaxGroggy);
	}
}
