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

		// _ctx.transform.LookAt(_ctx.playerDetection.TargetTransform);

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
