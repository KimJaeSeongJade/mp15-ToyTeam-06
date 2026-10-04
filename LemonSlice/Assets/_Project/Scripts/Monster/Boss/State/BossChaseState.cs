using UnityEngine;

public class BossChaseState : StateBase<BossContext>
{
	public BossChaseState(BossContext context, StateMachine<BossContext> stateMachine) : base(context,
		stateMachine)
	{
	}

	public override void Tick()
	{
		float distance = Vector3.Distance(
			_ctx.transform.position,
			_ctx.playerDetection.TargetTransform.position
		);

		if (distance < _ctx.stat.AttackDistance)
		{
			_fsm.ChangeState(StateType.Attack);
		}

		_ctx.transform.LookAt(_ctx.playerDetection.TargetTransform);
	}
}
