using System;
using UnityEngine;
using UnityEngine.UI;

public class BossHp : MonoBehaviour
{
	[SerializeField] private Image bossHpBar;

	private int _currentHp;
	private int _maxHp;

	private void OnEnable()
	{
		RefreshCurrentHpBar();
	}

	public void RefreshCurrentHp(int currentHp)
	{
		_currentHp = currentHp;
		RefreshCurrentHpBar();
	}

	public void InitMaxHp(int maxHp)
	{
		_maxHp = maxHp;
	}

	public void RefreshCurrentHpBar()
	{
		bossHpBar.fillAmount = (float)_currentHp / _maxHp;
	}
}
