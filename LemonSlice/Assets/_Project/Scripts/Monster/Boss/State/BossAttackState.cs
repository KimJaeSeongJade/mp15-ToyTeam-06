using UnityEngine;

public class BossAttackState : StateBase<BossContext>
{
	private float attackTime;
	private bool isAttacking;
	private int randIndex;
	private int[] extraDamage = { 0, 5 };
	private int[] groggyDamage = { 30, 100 };

	public BossAttackState(BossContext context, StateMachine<BossContext> stateMachine) : base(context,
		stateMachine)
	{
	}

	private bool CanAttack => attackTime >= _ctx.stat.AttackDelay;

	public override void Enter()
	{
		attackTime = 0f;
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
		_ctx.hitBoxes[randIndex].Close();
	}

	public override void OnAnimEvent(string animEvent)
	{
		if (animEvent == _ctx.animHandler.StartAttack + randIndex)
		{
			_ctx.hitBoxes[randIndex].Open(_ctx.stat.AttackPower + extraDamage[randIndex], groggyDamage[randIndex]);
		}

		if (animEvent == _ctx.animHandler.EndAttack+randIndex)
		{
			isAttacking = false;
			_ctx.hitBoxes[randIndex].Close();
		}
	}

	private void Attack()
	{
		randIndex = Random.Range(0, extraDamage.Length);

		_ctx.animHandler.PlayAttackAnim(randIndex);
	}
}
