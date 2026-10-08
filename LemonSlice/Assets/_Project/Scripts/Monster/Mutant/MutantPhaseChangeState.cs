using UnityEngine;

public class MutantPhaseChangeState : StateBase<MutantContext>
{
	private MutantController _controller;

	public MutantPhaseChangeState(MutantContext context, StateMachine<MutantContext> stateMachine) : base(context, stateMachine)
	{
	}

	public override void Enter()
	{

		Rigidbody rb = _ctx.transform.GetComponent<Rigidbody>();
		if (rb != null)
		{
			rb.velocity = Vector3.zero;
		}

		_ctx.stat.IsInvincible = true;

		_ctx.stat.CurrentHealth.Value = _ctx.stat.MaxHealth.Value;

		_controller.SetPhase(2);

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
