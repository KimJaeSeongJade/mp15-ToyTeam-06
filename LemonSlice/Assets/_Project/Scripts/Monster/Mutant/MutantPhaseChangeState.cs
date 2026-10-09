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
		_ctx.stat.CurrentHealth.Value = _ctx.stat.MaxHealth.Value;
		_ctx.isPhase2 = true;

		_ctx.animHandler.PlayPhaseChangeAnim();
	}

	public override void Exit()
	{
		_ctx.stat.IsInvincible = false;
	}

	public override void OnAnimEvent(string animEvent)
	{
		if (animEvent == _ctx.animHandler.EndPhaseChange)
		{
			_fsm.ChangeState(StateType.Idle);
		}
	}
}
