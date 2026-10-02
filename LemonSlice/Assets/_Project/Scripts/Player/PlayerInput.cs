using UnityEngine;

public class PlayerInput : MonoBehaviour
{
	public Vector3 MouseDelta { get; private set; }
	public Vector3 MoveAxis { get; private set; }
	public Vector3 MoveAxisRaw { get; private set; }

	public void Read()
	{
		// (-y, x, 0)
		MouseDelta = new Vector3(
			-Input.GetAxis("Mouse Y"),
			Input.GetAxis("Mouse X"),
			0
		);

		// (x, 0 ,z)
		MoveAxis = new Vector3(
			Input.GetAxis("Horizontal"),
			0,
			Input.GetAxis("Vertical")
		);

		MoveAxisRaw = new Vector3(
			Input.GetAxis("Horizontal"),
			0,
			Input.GetAxis("Vertical")
		);
	}
}
