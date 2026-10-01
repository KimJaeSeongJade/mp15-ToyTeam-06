using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private float moveSpeed;
	private PlayerInput _input;
	private Rigidbody _rigidBody;

	// --------- 이벤트 함수 ------------
	private void Awake()
	{
		CacheComponets();
	}

	private void Update()
	{
		_input.Read();
		Rotate();
	}

	private void FixedUpdate()
	{
		Movement();
	}

	// --------------------------------

	private void Movement()
	{
		Vector3 movement = new Vector3(_input.MoveAxis.x, 0, _input.MoveAxis.z);
		_rigidBody.velocity = movement * moveSpeed;
	}

	private void Rotate()
	{
		//좌우 -> 회전
		transform.Rotate(0, _input.MouseDelta.y, 0, Space.Self);
	}

	private void CacheComponets()
	{
		_rigidBody = GetComponent<Rigidbody>();
		_input = GetComponent<PlayerInput>();
	}
}
