public class PlayerRollState : StateBase<PlayerContext>
{
	public PlayerRollState(PlayerContext context, StateMachine<PlayerContext> stateMachine) : base(context,
		stateMachine)
	{
	}

	public override void Enter()
	{
		_ctx.animHandler.PlayerRollAnim();
		// PlayerLayerMask 변경
	}

	public override void Exit()
	{
		// PlayerLayerMask 원복
	}


}
