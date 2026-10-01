using UnityEngine;

public class PlayerIdleState : StateBase<PlayerContext>
{
	public PlayerIdleState(PlayerContext context, StateMachine<PlayerContext> stateMachine) : base(context,
		stateMachine)
	{
	}

	public override void Tick()
	{
		if (_ctx.input.MoveAxis != Vector3.zero)
		{
			_fsm.ChangeState(StateType.Move);
		}

		Rotate();
	}

	private void Rotate()
	{
		_ctx.transform.Rotate(0, _ctx.input.MouseDelta.y, 0, Space.Self);
	}
}
