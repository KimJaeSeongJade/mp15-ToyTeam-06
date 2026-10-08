using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSearchingState : StateBase<BossContext>
{
	public BossSearchingState(BossContext context, StateMachine<BossContext> stateMachine) : base(context,
		stateMachine)
	{
	}

	public override void Enter()
	{
		_ctx.animHandler.PlayIdleAnim();
	}

	public override void Tick()
	{
		float distance = Vector3.Distance(
			_ctx.transform.position,
			_ctx.monsterDetection.TargetTransform.position
		);

		_ctx.isInAttackRange = distance < _ctx.stat.AttackDistance;

		if (!_ctx.isInAttackRange)
		{
			_fsm.ChangeState(StateType.Move);
		}

		Searching();

		if (_ctx.monsterDetection.IsInSight)
		{
			_fsm.ChangeState(StateType.Attack);
		}
	}

	private void Searching()
	{
		Vector3 dir = _ctx.monsterDetection.TargetTransform.position - _ctx.transform.position;

		Quaternion dirQuaternion = Quaternion.LookRotation(dir);
		_ctx.transform.rotation = Quaternion.RotateTowards(
			_ctx.transform.rotation,
			dirQuaternion,
			4f
		);
		_ctx.transform.rotation = Quaternion.Euler(0f, _ctx.transform.rotation.eulerAngles.y, 0f);
	}
}
