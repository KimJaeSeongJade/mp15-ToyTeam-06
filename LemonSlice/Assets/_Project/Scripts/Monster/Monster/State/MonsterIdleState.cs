public class MonsterIdleState : StateBase<MonsterContext>
{
	public MonsterIdleState(MonsterContext context, StateMachine<MonsterContext> stateMachine) : base(context,
		stateMachine)
	{
	}

	public override void Enter()
	{
		_ctx.animHandler.PlayIdleAnim();
	}

	public override void Tick()
	{
		if (_ctx.monsterDetection.IsPlayerEnter)
		{
			_fsm.ChangeState(StateType.Move);
		}
	}
}
