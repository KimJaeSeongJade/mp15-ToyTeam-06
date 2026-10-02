using UnityEngine;

public class HealItem : MonoBehaviour, IInteractable
{
	private HealingPack healingPack;


	public GameObject GameObject => gameObject;

	public void Interact(GameObject interactor)
	{
		if (!(interactor.tag == "Player")) // IInteractor 인터패이스가 없어서 태그로 처리했습니다.
		{
			return;
		}

		healingPack.Heal();
		Destroy(gameObject);
	}
}
