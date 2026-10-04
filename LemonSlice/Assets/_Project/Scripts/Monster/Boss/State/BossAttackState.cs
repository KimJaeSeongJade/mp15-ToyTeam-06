using UnityEngine;

public class BossAttackState : StateBase<BossContext>
{
	private float attackTime;
	private bool isAttacking;

	public BossAttackState(BossContext context, StateMachine<BossContext> stateMachine) : base(context,
		stateMachine)
	{
	}

	private bool CanAttack => attackTime >= _ctx.stat.AttackDelay;

	public override void Enter()
	{
		attackTime = 0f;
		_ctx.attackIndex = 1;
		_ctx.animHandler.PlayAttackAnim(_ctx.attackIndex);
		isAttacking = true;
	}

	public override void Tick()
	{
		if (!isAttacking)
		{
			attackTime += Time.deltaTime;
		}

		if (CanAttack)
		{
			float distance = Vector3.Distance(_ctx.transform.position, _ctx.playerDetection.TargetTransform.position);

			if (distance > _ctx.stat.AttackDistance)
			{
				_fsm.ChangeState(StateType.Move);
			}

			_ctx.animHandler.PlayAttackAnim(_ctx.attackIndex);
			attackTime = 0;
		}
	}

	public override void OnAnimEvent(string animEvent)
	{
		if (animEvent == _ctx.animHandler.EndMinoAttack)
		{
			Debug.Log("미노 공격 모션 끝");
			isAttacking = false;
		}
	}
}
