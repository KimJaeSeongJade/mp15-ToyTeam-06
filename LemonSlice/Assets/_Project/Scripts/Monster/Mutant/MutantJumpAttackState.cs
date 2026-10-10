using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantJumpAttackState : StateBase<MutantContext>
{
	private bool isAttacking;
	private bool isJumping;
	private Vector3 jumpPos;
	private GameObject jumpEffect;


	private int extraDamage = 15 ;
	private int groggyDamage = 100 ;

	public MutantJumpAttackState(MutantContext context, StateMachine<MutantContext> stateMachine) : base(context, stateMachine)
	{
	}

	public override void Enter()
	{
		isAttacking = true;
		Attack();
	}

	public override void Tick()
	{
		if (!isAttacking)
		{
			if (_ctx.isInAttackRange && !_ctx.monsterDetection.IsInSight)
			{
				_fsm.ChangeState(StateType.Searching);
			}
			else
			{
				_fsm.ChangeState(StateType.Move);
			}
		}

		if (isJumping)
		{
			_ctx.transform.position = Vector3.MoveTowards(_ctx.transform.position, jumpPos, _ctx.jumpSpeed * Time.deltaTime);
		}

	}

	public override void Exit()
	{
		_ctx.hitBoxes[1].Close();

		if (jumpEffect != null)
		{
			EffectManager.Instance.StopEffect(jumpEffect);
		}
	}

	public override void OnAnimEvent(string animEvent)
	{
		if (animEvent == _ctx.animHandler.StartAttack + 2)
		{
			_ctx.hitBoxes[1].Open(_ctx.stat.AttackPower + extraDamage, groggyDamage);
			jumpEffect = EffectManager.Instance.PlayMutantJumpEffect(jumpPos);
		}
		else if (animEvent == _ctx.animHandler.EndHitBox + 1)
		{
			_ctx.hitBoxes[1].Close();
			isJumping = false;
		}
		else if (animEvent == _ctx.animHandler.EndAttack + 2)
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

		_ctx.animHandler.PlayAttackAnim(2);

		jumpPos = _ctx.monsterDetection.TargetTransform.position;
		_ctx.transform.LookAt(jumpPos);
	}
}
