using UnityEngine;

public class PlayerKnockDownState : StateBase<PlayerContext>
{

	private bool canRollUp;
	public PlayerKnockDownState(PlayerContext context, StateMachine<PlayerContext> stateMachine) : base(context,
		stateMachine)
	{
	}

	public override void Enter()
	{
		canRollUp = false;
		_ctx.stat.DownPoint = _ctx.stat.MaxDownPoint;
		_ctx.stat.SetInvincible(true);
		_ctx.animHandler.PlayKnockDownAnim();

		_ctx.rigidbody.AddForce(_ctx.hitDirection * 3.0f, ForceMode.Impulse);
	}

	public override void Tick()
	{
		if (_ctx.input.IsRollPressed && canRollUp)
		{
			_fsm.ChangeState(StateType.Roll);
			return;
		}
	}

	public override void OnAnimEvent(string animEvent)
	{
		switch (animEvent)
		{
			case AnimEvents.CanRollUp:
				canRollUp = true;
				break;
			case AnimEvents.EndKnockDownAnim:
				canRollUp = false;
				_ctx.animHandler.PlayStandUpAnim();
				break;
			case AnimEvents.EndStandUpAnim:
				_fsm.ChangeState(StateType.Idle);
				_ctx.stat.SetInvincible(false);
				break;
		}
	}
}
