public class PlayerHitState : StateBase<PlayerContext>
{
	public PlayerHitState(PlayerContext context, StateMachine<PlayerContext> stateMachine) : base(context, stateMachine)
	{
	}

	public override void Enter()
	{
		_ctx.animHandler.PlayHitAnim();
		_ctx.stat.IsInvincible = true;
	}

	public override void OnAnimEvent(string animEvent)
	{
		if (animEvent == AnimEvents.EndHitAnim)
		{
			_fsm.ChangeState(StateType.Idle);
			_ctx.stat.IsInvincible = false;
		}
	}
}
