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
		Vector3 movement = new Vector3(_ctx.input.MoveAxis.x, 0, _ctx.input.MoveAxis.z);
		_ctx.rigidbody.velocity = movement * _ctx.stat.MoveSpeed;
	}

	private void Rotate()
	{
		_ctx.transform.Rotate(0, _ctx.input.MouseDelta.y, 0, Space.Self);
	}
}
