using UnityEngine;
using UnityEngine.UI;

public class BossGroggy : MonoBehaviour
{
	[SerializeField] private Image bossGroggyBar;

	private int _currentGroggy;
	private int _maxGroggy;


	public void RefreshCurrentGroggy(int currentGroggy)
	{
		_currentGroggy = currentGroggy;
		RefreshCurrentHpBar();
	}

	public void InitMaxGroggy(int maxGroggy)
	{
		_maxGroggy = maxGroggy;
	}

	public void RefreshCurrentHpBar()
	{
		bossGroggyBar.fillAmount = (float)_currentGroggy / _maxGroggy;
	}
}
