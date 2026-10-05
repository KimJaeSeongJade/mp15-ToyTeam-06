using UnityEngine;

public class HealingPack : MonoBehaviour
{
	private PlayerStat playerStat;


	public void Heal(PlayerStat playerStat)
	{
		playerStat.currentHealth.Value += 1;
	}

}
