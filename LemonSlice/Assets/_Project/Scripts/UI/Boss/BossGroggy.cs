using UnityEngine;
using UnityEngine.UI;

public class BossGroggy : MonoBehaviour
{
	[SerializeField] private Image bossGroggyBar;

	private int _currentGroggy;
	private int _maxGroggy;

	private void OnEnable()
	{
		RefreshCurrentGroggyBar();
	}

	public void RefreshCurrentGroggy(int currentGroggy)
	{
		_currentGroggy = currentGroggy;
		RefreshCurrentGroggyBar();
	}

	public void InitMaxGroggy(int maxGroggy)
	{
		_maxGroggy = maxGroggy;
	}

	public void RefreshCurrentGroggyBar()
	{
		bossGroggyBar.fillAmount = (float)_currentGroggy / _maxGroggy;
	}
}
