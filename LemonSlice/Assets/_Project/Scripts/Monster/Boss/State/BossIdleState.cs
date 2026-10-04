public class BossIdleState : StateBase<BossContext>
{
	public BossIdleState(BossContext context, StateMachine<BossContext> stateMachine) : base(context,
		stateMachine)
	{
	}

	public override void Enter()
	{
		_ctx.animHandler.PlayIdleAnim();
	}

	public override void Tick()
	{
		if (_ctx.playerDetection.IsPlayerEnter)
		{
			_fsm.ChangeState(StateType.Move);
		}
	}
}
