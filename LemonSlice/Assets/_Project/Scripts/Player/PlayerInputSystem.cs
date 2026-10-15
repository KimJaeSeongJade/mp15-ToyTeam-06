using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour
{
	[SerializeField] private float mouseSensitivity = 0.5f;
	private PlayerControls _controls;

	public Vector3 MouseDelta { get; private set; }
	public Vector3 MoveAxis { get; private set; }
	public Vector3 MoveAxisRaw { get; private set; }

	public bool LockOnPressed { get; private set; }
	public bool TargetChangePressed { get; private set; }
	public bool IsRollPressed { get; private set; }
	public bool IsAttackPressed { get; private set; }

	private void Awake()
	{
		_controls = new PlayerControls();
	}

	private void OnEnable()
	{
		_controls.Player.Enable();
		BindInputEvents();
	}

	private void OnDisable()
	{
		UnBindInputEvents();
		_controls.Player.Disable();
	}

	public void OnMove(InputAction.CallbackContext context)
	{
			Vector2 move = context.ReadValue<Vector2>();
			MoveAxisRaw = new Vector3(move.x, 0, move.y);
	}

	public void OnMoveStop(InputAction.CallbackContext context) => MoveAxisRaw = Vector3.zero;

	public void OnJump(InputAction.CallbackContext context) => IsRollPressed = true;
	public void OnJumpStop(InputAction.CallbackContext context) => IsRollPressed = false;

	public void OnAttack(InputAction.CallbackContext context) => IsAttackPressed = true;

	public void OnAttackStop(InputAction.CallbackContext context) => IsAttackPressed = false;

	public void OnMouseMove(InputAction.CallbackContext context)
	{
		Vector2 delta = context.ReadValue<Vector2>() * mouseSensitivity;
		MouseDelta = new Vector3(
			-delta.y,
			delta.x,
			0
		);
	}

	public void OnMouseMoveStop(InputAction.CallbackContext context) => MouseDelta = Vector3.zero;

	private void BindInputEvents()
	{
		_controls.Player.Move.performed += OnMove;
		_controls.Player.Move.canceled += OnMoveStop;

		_controls.Player.Jump.performed += OnJump;
		_controls.Player.Jump.canceled += OnJumpStop;

		_controls.Player.Attack.performed += OnAttack;
		_controls.Player.Attack.canceled += OnAttackStop;

		_controls.Player.Mouse.performed += OnMouseMove;
		_controls.Player.Mouse.canceled += OnMouseMoveStop;
	}

	private void UnBindInputEvents()
	{
		_controls.Player.Move.performed -= OnMove;
		_controls.Player.Move.canceled -= OnMoveStop;

		_controls.Player.Jump.performed -= OnJump;
		_controls.Player.Jump.canceled -= OnJumpStop;

		_controls.Player.Attack.performed -= OnAttack;
		_controls.Player.Attack.canceled -= OnAttackStop;

		_controls.Player.Mouse.performed -= OnMouseMove;
		_controls.Player.Mouse.canceled -= OnMouseMoveStop;
	}
}
