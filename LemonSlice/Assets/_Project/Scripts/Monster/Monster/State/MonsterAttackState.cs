using UnityEngine;

public class MonsterAttackState : StateBase<MonsterContext>
{
	private float attackTime;
	private bool isAttacking;
	private int downValue = 10;
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
		_ctx.hitBox.Open(_ctx.stat.AttackPower, downValue);
	}

	public override void Tick()
	{
		if (_ctx.monsterDetection.TargetTransform == null)
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
			float distance = Vector3.Distance(_ctx.transform.position, _ctx.monsterDetection.TargetTransform.position);

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

	public override void Exit()
	{
		_ctx.hitBox.Close();
	}

	public override void OnAnimEvent(string animEvent)
	{
		if (animEvent == _ctx.animHandler.EndAttackAnim)
		{
			isAttacking = false;
		}
	}
}
