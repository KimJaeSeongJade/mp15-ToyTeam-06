using UnityEngine;

public class Coin : MonoBehaviour, IInteractable, IPoolable
{
	private int score = 1;
	public GameObject GameObject => gameObject;

	public void Interact(GameObject interactor)
	{
		
	}

	public PoolType PoolId => PoolType.Coin;
}
