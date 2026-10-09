using UnityEngine;

public class MutantAttackState : StateBase<MutantContext>
{
    private float attackTime;
    private bool isAttacking;
    private int randIndex;

    private int current;
    private int[] extraDamage = { 0, 5, 15 };
    private int[] groggyDamage = { 30, 50, 100 };

    public MutantAttackState(MutantContext context, StateMachine<MutantContext> stateMachine) : base(context, stateMachine)
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

          if (_ctx.isInAttackRange && !_ctx.monsterDetection.IsInSight)
          {
             _fsm.ChangeState(StateType.Searching);
          }
       }
       if (CanAttack)
       {
          _fsm.ChangeState(StateType.Move);
       }
    }

    public override void Exit()
    {
	    _ctx.hitBoxes.Close();
    }

    public override void OnAnimEvent(string animEvent)
    {
       if (animEvent == _ctx.animHandler.StartAttack + randIndex)
       {
	       _ctx.hitBoxes.Open(_ctx.stat.AttackPower + extraDamage[randIndex], groggyDamage[randIndex]);
       }
       else if (animEvent == _ctx.animHandler.EndHitBox + randIndex)
       {
	       _ctx.hitBoxes.Close();
	       if (_ctx.animHandler.a == randIndex)
	       {
		       Vector3 pos = new Vector3(_ctx.transform.position.x + 0.5f, _ctx.transform.position.y,
			       _ctx.transform.position.z);
		       EffectManager.Instance.PlayMutantJumpEffect(pos);
	       }
       }
       else if (animEvent == _ctx.animHandler.EndAttack + randIndex)
       {
          isAttacking = false;
       }
    }

    private void Attack()
    {
       randIndex = Random.Range(0, extraDamage.Length);
       _ctx.animHandler.PlayAttackAnim(randIndex);
    }
}
