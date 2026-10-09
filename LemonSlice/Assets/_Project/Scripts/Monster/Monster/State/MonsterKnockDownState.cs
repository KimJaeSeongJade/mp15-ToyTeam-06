using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterKnockDownState : StateBase<MonsterContext>
{
	private GameObject _hitEffect;
	public MonsterKnockDownState(MonsterContext context, StateMachine<MonsterContext> stateMachine) : base(context,
		stateMachine)
	{
	}

	public override void Enter()
	{
		_ctx.stat.DownPoint = _ctx.stat.MaxDownPoint;
		_ctx.stat.IsInvincible = true;
		_ctx.animHandler.PlayKnockDownAnim();

		_ctx.rigidbody.AddForce(_ctx.knockDownDirection * _ctx.stat.KnockDownForce, ForceMode.Impulse);

		PlayHitEffect();
	}

	public override void OnAnimEvent(string animEvent)
	{
		switch (animEvent)
		{
			case AnimEvents.EndKnockDownAnim:
				_ctx.animHandler.PlayStandUpAnim();
				StopHitEffect();
				break;
			case AnimEvents.EndStandUpAnim:
				_fsm.ChangeState(StateType.Idle);
				_ctx.stat.IsInvincible = false;
				break;
		}
	}

	private void PlayHitEffect()
	{
		if (_hitEffect != null)
		{
			StopHitEffect();
		}

		Vector3 pos = _ctx.hitPoint;
		Vector3 dir = _ctx.hitDirection;
		_hitEffect = EffectManager.Instance.PlayMonsterHitEffect(pos);
	}

	private void StopHitEffect()
	{
		EffectManager.Instance.StopEffect(_hitEffect);
	}
}
