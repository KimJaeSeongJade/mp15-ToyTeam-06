using UnityEngine;

public class MutantIdleState : StateBase<MutantContext>
{
	public MutantIdleState(MutantContext context, StateMachine<MutantContext> stateMachine) : base(context, stateMachine)
	{
	}

	public override void Enter()
	{
		_ctx.animHandler.PlayIdleAnim();
	}

	public override void Tick()
	{
		if (_ctx.monsterDetection != null && _ctx.monsterDetection.IsPlayerEnter)
		{
			_fsm.ChangeState(StateType.Move);
		}
	}
}
