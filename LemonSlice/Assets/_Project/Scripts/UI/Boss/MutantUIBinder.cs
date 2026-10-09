using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantUIBinder : MonoBehaviour
{
    [SerializeField] private BossHp bossHp;
    [SerializeField] private BossGroggy bossGroggy;

    private MutantStat mutantStat;

    private void Awake() => CacheComponents();
    private void OnEnable() => BindBossStatChangeEvents();
    private void OnDisable() => UnBindBossStatChangeEvents();

    private void CacheComponents()
    {
	    mutantStat = GetComponent<MutantStat>();
    }

    private void BindBossStatChangeEvents()
    {
	    mutantStat.CurrentHealth.AddListener(bossHp.RefreshCurrentHp);
	    mutantStat.MaxHealth.AddListener(bossHp.InitMaxHp);
	    mutantStat.CurrentGroggy.AddListener(bossGroggy.RefreshCurrentGroggy);
	    mutantStat.MaxGroggy.AddListener(bossGroggy.InitMaxGroggy);
    }

    private void UnBindBossStatChangeEvents()
    {
	    mutantStat.CurrentHealth.RemoveListener(bossHp.RefreshCurrentHp);
	    mutantStat.MaxHealth.RemoveListener(bossHp.InitMaxHp);
	    mutantStat.CurrentGroggy.RemoveListener(bossGroggy.RefreshCurrentGroggy);
	    mutantStat.MaxGroggy.RemoveListener(bossGroggy.InitMaxGroggy);
    }
}
