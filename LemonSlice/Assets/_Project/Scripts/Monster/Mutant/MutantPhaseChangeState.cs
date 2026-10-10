using UnityEngine;

public class MutantPhaseChangeState : StateBase<MutantContext>
{
	public MutantPhaseChangeState(MutantContext context, StateMachine<MutantContext> stateMachine) : base(context, stateMachine)
	{
	}

	public override void Enter()
	{

		Rigidbody rigidbody = _ctx.transform.GetComponent<Rigidbody>();
		if (rigidbody != null)
		{
			rigidbody.velocity = Vector3.zero;
		}

		_ctx.stat.IsInvincible = true;
		_ctx.stat.SetFullHealth();
		_ctx.stat.SetFullGroggy();

		_ctx.animHandler.PlayPhaseChangeAnim();
	}

	public override void Exit()
	{
		_ctx.stat.IsInvincible = false;
		_ctx.isPhase2 = true;
	}

	public override void OnAnimEvent(string animEvent)
	{
		if (animEvent == _ctx.animHandler.EndPhaseChange)
		{
			_ctx.transform.forward = _ctx.monsterDetection.TargetTransform.position - _ctx.transform.position;
			_ctx.animHandler.PlayScreamAnim();
		}

		if (animEvent == _ctx.animHandler.EndScream)
		{
			_fsm.ChangeState(StateType.Move);
		}
	}
}
