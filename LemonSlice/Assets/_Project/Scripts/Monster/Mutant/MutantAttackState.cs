using UnityEngine;

public class MutantAttackState : StateBase<MutantContext>
{
    private float attackTime;
    private bool isAttacking;
    private int randIndex;

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

          if (_ctx.isInAttackRange && _ctx.monsterDetection != null && !_ctx.monsterDetection.IsInSight)
          {
             _fsm.ChangeState(StateType.Move);
          }
       }
       if (CanAttack && !isAttacking)
       {
          _fsm.ChangeState(StateType.Move);
       }
    }

    public override void Exit()
    {}

    public override void OnAnimEvent(string animEvent)
    {
       if (animEvent == _ctx.animHandler.StartAttack + randIndex)
       {
       }
       else if (animEvent == _ctx.animHandler.EndHitBox + randIndex)
       {
       }
       else if (animEvent == _ctx.animHandler.EndAttack + randIndex)
       {
          isAttacking = false;
          _ctx.animHandler.PlayIdleAnim();
       }
    }

    private void Attack()
    {
       randIndex = Random.Range(0, extraDamage.Length);
       _ctx.attackIndex = randIndex;
       _ctx.animHandler.PlayAttackAnim(randIndex);
    }
}
