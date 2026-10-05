using UnityEngine;

public class Coin : MonoBehaviour, IInteractable, IPoolable
{
	private int score = 1;
	public GameObject GameObject => gameObject;
	private Rigidbody _rigidbody;

	public Rigidbody Rigidbody => _rigidbody;

	private void Awake() => _rigidbody = GetComponent<Rigidbody>();
	public void Interact(GameObject interactor)
	{
		if (interactor.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			// 점수 추가
			GameManager.Instance.AddScore(score);
			PoolManager.Instance.TryReturn(this.gameObject);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			Interact(other.gameObject);
		}
	}

	public PoolType PoolId => PoolType.Coin;
}
