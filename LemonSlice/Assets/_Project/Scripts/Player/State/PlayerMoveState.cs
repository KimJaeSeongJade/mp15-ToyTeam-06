using UnityEngine;

public class PlayerMoveState : StateBase<PlayerContext>
{
	public PlayerMoveState(PlayerContext context, StateMachine<PlayerContext> stateMachine) : base(context,
		stateMachine)
	{
	}

	public override void Enter()
	{
	}

	//----------------State Method------------------
	public override void Tick()
	{
		if (_ctx.input.MoveAxisRaw == Vector3.zero)
		{
			_fsm.ChangeState(StateType.Idle);
			return;
		}

		if (_ctx.input.IsSpacePressed)
		{
			_fsm.ChangeState(StateType.Roll);
			return;
		}

		_ctx.animHandler.SetMoveParam(_ctx.input.MoveAxisRaw);

		Rotate();
	}

	public override void FixedTick()
	{
		Move();
	}

	public override void Exit()
	{
		_ctx.rigidbody.velocity = Vector3.zero;
		_ctx.animHandler.SetMoveParam(Vector3.zero);
	}

	//----------------State Method------------------

	private void Rotate()
	{
		_ctx.transform.Rotate(0, _ctx.input.MouseDelta.y, 0, Space.Self);
	}

	private void Move()
	{
		Vector3 moveDirection = _ctx.transform.TransformDirection(_ctx.input.MoveAxisRaw).normalized;

		_ctx.rigidbody.velocity = new Vector3(
			moveDirection.x * _ctx.stat.MoveSpeed,
			_ctx.rigidbody.velocity.y,
			moveDirection.z * _ctx.stat.MoveSpeed);
	}
}
