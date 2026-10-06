using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterKnockDownState : StateBase<MonsterContext>
{
	public MonsterKnockDownState(MonsterContext context, StateMachine<MonsterContext> stateMachine) : base(context,
		stateMachine)
	{
	}

	public override void Enter()
	{
		_ctx.stat.DownPoint = _ctx.stat.MaxDownPoint;
		_ctx.stat.IsInvincible = true;
		_ctx.animHandler.PlayKnockDownAnim();

		_ctx.rigidbody.AddForce(_ctx.hitDirection * 3.0f, ForceMode.Impulse);
	}

	public override void OnAnimEvent(string animEvent)
	{
		switch (animEvent)
		{
			case AnimEvents.EndKnockDownAnim:
				_ctx.animHandler.PlayStandUpAnim();
				break;
			case AnimEvents.EndStandUpAnim:
				_fsm.ChangeState(StateType.Idle);
				_ctx.stat.IsInvincible = false;
				break;
		}
	}
}
