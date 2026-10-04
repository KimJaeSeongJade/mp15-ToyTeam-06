using UnityEngine;

public class MonsterAttackState : StateBase<MonsterContext>
{
	private float attackTime;
	private bool isAttacking;


	public MonsterAttackState(MonsterContext context, StateMachine<MonsterContext> stateMachine) : base(context,
		stateMachine)
	{
	}

	private bool CanAttack => attackTime >= _ctx.stat.AttackDelay;

	public override void Enter()
	{
		attackTime = 0f;
		_ctx.animHandler.PlayAttackAnim();
		isAttacking = true;
	}

	public override void Tick()
	{
		if (_ctx.playerDetection.TargetTransform == null)
		{
			if (isAttacking)
			{
				return;
			}
			else
			{
				_fsm.ChangeState(StateType.Idle);
			}
		}

		if (!isAttacking)
		{
			attackTime += Time.deltaTime;
		}

		if (CanAttack)
		{
			float distance = Vector3.Distance(_ctx.transform.position, _ctx.playerDetection.TargetTransform.position);

			if (distance > _ctx.stat.AttackDistance)
			{
				_fsm.ChangeState(StateType.Idle);
			}

			_ctx.animHandler.PlayAttackAnim();
			isAttacking = true;

			attackTime = 0;
		}
		else if (!isAttacking)
		{
			_ctx.animHandler.PlayIdleAnim();
		}
	}

	public override void OnAnimEvent(string animEvent)
	{
		if (animEvent == _ctx.animHandler.EndAttackAnim)
		{
			isAttacking = false;
		}
	}
}
