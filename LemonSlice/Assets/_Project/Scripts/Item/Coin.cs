using UnityEngine;

public class Coin : MonoBehaviour, IInteractable, IPoolable
{
	private int score = 1;
	public GameObject GameObject => gameObject;

	public void Interact(GameObject interactor)
	{
		GameManager.Instance.AddScore(score);
	}

	public PoolType PoolId => PoolType.Coin;
}
