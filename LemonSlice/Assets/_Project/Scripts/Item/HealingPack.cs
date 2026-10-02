using UnityEngine;

public class HealingPack : MonoBehaviour
{
	private PlayerStat playerStat;


	public void Heal()
	{
		playerStat.currentHealth.Value += 1;
	}

}
