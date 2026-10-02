public class MonsterIdleState : StateBase<MonsterContext>
{
	public MonsterIdleState(MonsterContext context, StateMachine<MonsterContext> stateMachine) : base(context,
		stateMachine)
	{
	}
}
