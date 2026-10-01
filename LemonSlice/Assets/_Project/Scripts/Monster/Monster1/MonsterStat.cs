using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : MonoBehaviour
{
	[SerializeField] private Transform lockonPosition; // 락온포지션
	[SerializeField] private int maxHealth; // 최대 체력
	[SerializeField] private float moveSpeed; // 이동속도
	[SerializeField] private int attackPower;

	public ObservableProperty<int> currentHealth = new(0);

	private void Awake() => Init();

	private void Init()
	{
		currentHealth.Value = maxHealth;
	}
}
