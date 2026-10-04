using UnityEngine;

public class MonsterChaseState : StateBase<MonsterContext>
{
	public MonsterChaseState(MonsterContext context, StateMachine<MonsterContext> stateMachine) : base(context,
		stateMachine)
	{
	}

	public override void Enter()
	{
		_ctx.animHandler.PlayChaseAnim();
	}

	public override void Tick()
	{
		if (_ctx.playerDetection.TargetTransform == null)
		{
			_fsm.ChangeState(StateType.Idle);
			return;
		}

		float distance = Vector3.Distance(_ctx.transform.position, _ctx.playerDetection.TargetTransform.position);

		if (distance < _ctx.stat.AttackDistance)
		{
			_fsm.ChangeState(StateType.Attack);
		}

		Vector3 direction = (_ctx.playerDetection.TargetTransform.position - _ctx.transform.position).normalized;
		direction.y = 0;

		if (direction != Vector3.zero)
		{
			Quaternion targetRotation = Quaternion.LookRotation(direction);
			_ctx.transform.rotation = Quaternion.Slerp(_ctx.transform.rotation, targetRotation,
				Time.deltaTime * _ctx.stat.MoveSpeed);
		}

		_ctx.transform.Translate(Vector3.forward * (_ctx.stat.MoveSpeed * Time.deltaTime));
	}
}
