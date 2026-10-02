using UnityEngine;

public class PlayerIdleState : StateBase<PlayerContext>
{
	public PlayerIdleState(PlayerContext context, StateMachine<PlayerContext> stateMachine) : base(context,
		stateMachine)
	{
	}

	//----------------State Method------------------
	public override void Tick()
	{
		if (_ctx.input.MoveAxisRaw != Vector3.zero)
		{
			_fsm.ChangeState(StateType.Move);
		}

		if (_ctx.input.IsSpacePressed)
		{
			_fsm.ChangeState(StateType.Roll);
			return;
		}

		Rotate();
	}
	//----------------State Method------------------

	private void Rotate()
	{
		_ctx.transform.Rotate(0, _ctx.input.MouseDelta.y, 0, Space.Self);
	}
}
