
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : MonoBehaviour, IInteractable
{


	public GameObject GameObject => this.gameObject;
	private HealingPack healingPack;

	public void Interact(GameObject interactor)
	{
		
		healingPack.Heal();
	}

}
