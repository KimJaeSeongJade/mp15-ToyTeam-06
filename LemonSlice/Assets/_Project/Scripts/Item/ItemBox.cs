using UnityEngine;

public class ItemBox : MonoBehaviour, IInteractable
{
	public LayerMask InteractedLayer;
	public GameObject GameObject => this.gameObject;
	private bool _isDestroyed = false;
	private int _destroy;
	private Animator _animator;

	private void Awake()
	{
		CacheComponents();
		Init();
	}

	public void Interact(GameObject interactor)
	{
		if (LayerMask.LayerToName(interactor.layer) == "Player")
		{
			int layer = (1 << interactor.layer);

			if (InteractedLayer.value == layer)
			{
				SetDestroy();
				// effect.Play 이펙트 효과
				Destroy(gameObject);
			}
		}
	}



	private void SetDestroy()
	{
		_animator.SetBool(_destroy, true);
	}
	private void CacheComponents()
	{
		_animator = GetComponent<Animator>();
	}

	private void Init()
	{
		_destroy = Animator.StringToHash("Destroy");
	}
}
