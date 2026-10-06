using UnityEngine;

public class BossChaseState : StateBase<BossContext>
{
	public BossChaseState(BossContext context, StateMachine<BossContext> stateMachine) : base(context,
		stateMachine)
	{
	}

	public override void Enter()
	{
		MainUIManager.Instance.SetBossUI(true);
		_ctx.animHandler.PlayChaseAnim();
	}

	public override void Tick()
	{
		// TODO Die 테스트용 추후에 지워야 함
		if (Input.GetKeyDown(KeyCode.P))
		{
			_fsm.ChangeState(StateType.Die);
		}

		float distance = Vector3.Distance(
			_ctx.transform.position,
			_ctx.monsterDetection.TargetTransform.position
		);

		if (distance < _ctx.stat.AttackDistance)
		{
			_fsm.ChangeState(StateType.Attack);
		}

		Vector3 direction = (_ctx.monsterDetection.TargetTransform.position - _ctx.transform.position).normalized;
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
