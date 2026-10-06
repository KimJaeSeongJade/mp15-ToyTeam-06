public class PlayerHitState : StateBase<PlayerContext>
{
	public PlayerHitState(PlayerContext context, StateMachine<PlayerContext> stateMachine) : base(context, stateMachine)
	{
	}

	public override void Enter()
	{
		_ctx.animHandler.PlayHitAnim();
	}

	public override void OnAnimEvent(string animEvent)
	{
		if (animEvent == AnimEvents.EndHitAnim)
		{
			_fsm.ChangeState(StateType.Idle);
		}
	}
}
