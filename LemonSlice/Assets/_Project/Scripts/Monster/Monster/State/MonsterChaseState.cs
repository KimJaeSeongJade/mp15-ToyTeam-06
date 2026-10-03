using UnityEngine;

public class MonsterChaseState : StateBase<MonsterContext>
{
	public MonsterChaseState(MonsterContext context, StateMachine<MonsterContext> stateMachine) : base(context,
		stateMachine)
	{
	}

	public override void Tick()
	{
		if (_ctx.targetTransform == null)
		{
			_ctx.isPlayerEnter = false;
			_fsm.ChangeState(StateType.Idle);
			return;
		}

		float distance = Vector3.Distance(_ctx.transform.position, _ctx.targetTransform.position);

		if (distance < _ctx.stat.AttackDistance)
		{
			_fsm.ChangeState(StateType.Attack);
		}

		Vector3 direction = (_ctx.targetTransform.position - _ctx.transform.position).normalized;
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
