using UnityEngine;

public class MainCollider : MonoBehaviour
{
	[SerializeField] private LayerMask targetLayer;
	[SerializeField] private GameObject Mino;
	[SerializeField] private Canvas CantEntercanvas;

	private BoxCollider collider;

	// --------- 이벤트 함수 ------------

	private void Awake() => CacheComponents();

	private void Update()
	{
		if (!Mino.activeSelf)
		{
			collider.isTrigger = true;
			CantEntercanvas.enabled = false;
		}
	}

	// -------------------------------

	private void CacheComponents()
	{
		collider = GetComponent<BoxCollider>();
	}
}
