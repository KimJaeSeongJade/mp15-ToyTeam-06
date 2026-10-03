public class PlayerAttackState : StateBase<PlayerContext>
{
	public PlayerAttackState(PlayerContext context, StateMachine<PlayerContext> stateMachine) : base(context,
		stateMachine)
	{
	}


	//----------------State Method------------------
	public override void Enter()
	{
	}

	public override void Tick()
	{
	}

	public override void Exit()
	{
	}

	public override void OnAnimEvent(string animEvent)
	{
	}
	//----------------State Method------------------
}
