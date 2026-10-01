using UnityEngine;

public class PlayerMoveState : StateBase<PlayerContext>
{
	public PlayerMoveState(PlayerContext context, StateMachine<PlayerContext> stateMachine) : base(context,
		stateMachine)
	{
	}

	public override void Tick()
	{
		if (_ctx.input.MoveAxis == Vector3.zero)
		{
			_fsm.ChangeState(StateType.Idle);
			return;
		}

		Rotate();
	}

	public override void FixedTick()
	{
		Move();
	}

	private void Rotate()
	{
		_ctx.transform.Rotate(0, _ctx.input.MouseDelta.y, 0, Space.Self);
	}

	private void Move()
	{
		Vector3 moveDirection = _ctx.transform.TransformDirection(_ctx.input.MoveAxis);

		_ctx.rigidbody.velocity = new Vector3(
			moveDirection.x * _ctx.stat.MoveSpeed,
			_ctx.rigidbody.velocity.y,
			moveDirection.z * _ctx.stat.MoveSpeed);
	}
}
