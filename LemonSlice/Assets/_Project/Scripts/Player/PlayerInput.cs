using UnityEngine;

public class PlayerInput : MonoBehaviour
{
	[SerializeField] private KeyCode attack = KeyCode.Mouse0;
	[SerializeField] private KeyCode roll = KeyCode.Space;
	[SerializeField] private KeyCode changeLockOn = KeyCode.Tab;
	[SerializeField] private KeyCode lcokOn = KeyCode.Mouse2;

	public Vector3 MouseDelta { get; private set; }
	public Vector3 MoveAxis { get; private set; }
	public Vector3 MoveAxisRaw { get; private set; }

	public bool LockOnPressed { get; private set; }
	public bool TargetChangePressed { get; private set; }
	public bool IsRollPressed { get; private set; }
	public bool IsAttackPressed { get; private set; }

	public void Read()
	{
		ReadMouseDelta();
		ReadMoveAxis();
		ReadMoveAxisRaw();

		IsRollPressed = Input.GetKeyDown(roll);
		LockOnPressed = Input.GetKeyDown(lcokOn);
		TargetChangePressed = Input.GetKeyDown(changeLockOn);
		IsAttackPressed = Input.GetKeyDown(attack);
	}

	private void ReadMouseDelta()
	{
		// (-y, x, 0)
		MouseDelta = new Vector3(
			-Input.GetAxis("Mouse Y"),
			Input.GetAxis("Mouse X"),
			0
		);
	}

	private void ReadMoveAxis()
	{
		// (x, 0 ,z)
		MoveAxis = new Vector3(
			Input.GetAxis("Horizontal"),
			0,
			Input.GetAxis("Vertical")
		);
	}

	private void ReadMoveAxisRaw()
	{
		MoveAxisRaw = new Vector3(
			Input.GetAxisRaw("Horizontal"),
			0,
			Input.GetAxisRaw("Vertical")
		);
	}
}
