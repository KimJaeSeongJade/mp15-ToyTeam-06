using UnityEngine;

public class MutantChaseState : StateBase<MutantContext>
{
	public MutantChaseState(MutantContext context, StateMachine<MutantContext> stateMachine) : base(context, stateMachine)
	{
	}

	public override void Enter()
	{
		_ctx.animHandler.PlayChaseAnim();
	}

	public override void Tick()
	{
		if (_ctx.monsterDetection == null || _ctx.monsterDetection.TargetTransform == null)
		{
			_fsm.ChangeState(StateType.Idle);
			return;
		}

		float distance = Vector3.Distance(_ctx.transform.position, _ctx.monsterDetection.TargetTransform.position);

		_ctx.isInAttackRange = distance < _ctx.stat.AttackDistance;

		if (_ctx.isInAttackRange)
		{
			_fsm.ChangeState(StateType.Attack);
		}
		else if (_ctx.isPhase2 && distance > 8)
		{
			_fsm.ChangeState(StateType.JumpAttack);
		}

		Vector3 direction = (_ctx.monsterDetection.TargetTransform.position - _ctx.transform.position).normalized;
		direction.y = 0;

		if (direction != Vector3.zero)
		{
			Quaternion targetRotation = Quaternion.LookRotation(direction);
			_ctx.transform.rotation = Quaternion.Slerp(_ctx.transform.rotation, targetRotation, Time.deltaTime * _ctx.stat.MoveSpeed);
		}

		_ctx.transform.Translate(Vector3.forward * (_ctx.stat.MoveSpeed * Time.deltaTime));
	}
}
