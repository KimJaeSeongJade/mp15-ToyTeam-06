public class PlayerHitState : StateBase<PlayerContext>
{
	public PlayerHitState(PlayerContext context, StateMachine<PlayerContext> stateMachine) : base(context, stateMachine)
	{
	}

	public override void Enter()
	{
		PlayHitAnim();
	}

	public override void Tick()
	{
		if (_ctx.input.IsRollPressed)
		{
			_fsm.ChangeState(StateType.Roll);
			return;
		}
		
		if (_ctx.isUnderAttack)
		{
			PlayHitAnim();
		}
	}
	public override void OnAnimEvent(string animEvent)
	{
		if (animEvent == AnimEvents.EndHitAnim)
		{
			_fsm.ChangeState(StateType.Idle);
		}
	}

	private void PlayHitAnim()
	{
		_ctx.animHandler.PlayHitAnim();
		_ctx.isUnderAttack = false;
	}
}
