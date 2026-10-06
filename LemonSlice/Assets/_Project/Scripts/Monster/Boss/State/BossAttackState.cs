using UnityEngine;

public class BossAttackState : StateBase<BossContext>
{
	private float attackTime;
	private bool isAttacking;
	private int groggyDamage = 30;

	public BossAttackState(BossContext context, StateMachine<BossContext> stateMachine) : base(context,
		stateMachine)
	{
	}

	private bool CanAttack => attackTime >= _ctx.stat.AttackDelay;

	public override void Enter()
	{
		attackTime = 0f;
		_ctx.attackIndex = 1;
		isAttacking = true;
		Attack();
	}

	public override void Tick()
	{
		if (!isAttacking)
		{
			attackTime += Time.deltaTime;
		}

		if (CanAttack)
		{
			_fsm.ChangeState(StateType.Move);
		}
	}

	public override void Exit()
	{
		_ctx.hitBox.Close();
	}

	public override void OnAnimEvent(string animEvent)
	{
		if (animEvent == _ctx.animHandler.EndMinoAttack)
		{
			isAttacking = false;
			_ctx.hitBox.Close();
		}
	}

	private void Attack()
	{
		_ctx.hitBox.Open(_ctx.stat.AttackPower, groggyDamage);
		_ctx.animHandler.PlayAttackAnim(_ctx.attackIndex);
	}
}
