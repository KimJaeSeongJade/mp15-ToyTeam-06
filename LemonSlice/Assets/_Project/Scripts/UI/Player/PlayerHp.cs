using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
	[SerializeField] private Image playerHpBar;

	private int _currentHp;
	private int _maxHp;

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
		playerHpBar.fillAmount = (float)_currentHp / _maxHp;
	}
}
