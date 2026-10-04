public class BossAttackState : StateBase<BossContext>
{
	public BossAttackState(BossContext context, StateMachine<BossContext> stateMachine) : base(context,
		stateMachine)
	{
	}
}
