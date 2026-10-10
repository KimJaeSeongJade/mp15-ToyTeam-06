using UnityEngine;

public class MutantAttackState : StateBase<MutantContext>
{
    private int randIndex;
    private int hitBoxIndex;
    private float attackTime;
    private bool isAttacking;
    private bool isJumping;
    private Vector3 jumpPos;
    private GameObject jumpEffect;


    private int[] extraDamage = { 0, 5, 10 };
    private int[] groggyDamage = { 20, 30, 100 };

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

	    if (isJumping)
	    {
		    _ctx.transform.position = Vector3.MoveTowards(_ctx.transform.position, jumpPos, _ctx.jumpSpeed * Time.deltaTime);
		    _ctx.transform.LookAt(jumpPos);
	    }
    }

    public override void Exit()
    {
	    _ctx.hitBoxes[hitBoxIndex].Close();

	    if (jumpEffect != null)
	    {
		    EffectManager.Instance.StopEffect(jumpEffect);
	    }
    }

    public override void OnAnimEvent(string animEvent)
    {
	    if (animEvent == _ctx.animHandler.StartAttack + randIndex)
	    {
		    _ctx.hitBoxes[hitBoxIndex].Open(_ctx.stat.AttackPower + extraDamage[randIndex], groggyDamage[randIndex]);

		    if (randIndex == 2)
		    {
			    jumpEffect = EffectManager.Instance.PlayMutantJumpEffect(jumpPos);
		    }
	    }
	    else if (animEvent == _ctx.animHandler.EndHitBox + hitBoxIndex)
	    {
		    _ctx.hitBoxes[hitBoxIndex].Close();
		    isJumping = false;
	    }
	    else if (animEvent == _ctx.animHandler.EndAttack + randIndex)
	    {
		    isAttacking = false;
	    }
	    else if (animEvent == _ctx.animHandler.StartJump)
	    {
		    isJumping = true;
	    }
    }

    private void Attack()
    {
	    if (!_ctx.isPhase2)
	    {
		    randIndex = Random.Range(0, 2);
	    }
	    else
	    {
            randIndex = Random.Range(0, 3);
        }

        _ctx.animHandler.PlayAttackAnim(randIndex);

        if (randIndex == 2)
        {
	        jumpPos = _ctx.monsterDetection.TargetTransform.position;
	        hitBoxIndex = 1;
        }
        else
        {
	        hitBoxIndex = 0;
        }
    }
}
