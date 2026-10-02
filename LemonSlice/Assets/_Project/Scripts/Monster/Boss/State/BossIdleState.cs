public class BossIdleState : StateBase<BossContext>
{
	public BossIdleState(BossContext context, StateMachine<BossContext> stateMachine) : base(context,
		stateMachine)
	{
	}
}
