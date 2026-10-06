using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHitState : StateBase<MonsterContext>
{
	public MonsterHitState(MonsterContext monsterContext, StateMachine<MonsterContext> stateMachine) : base(
		monsterContext, stateMachine)
	{
	}

	public override void Enter()
	{
		_ctx.animHandler.PlayHitAnim();
		_ctx.stat.IsInvincible = true;
	}

	public override void OnAnimEvent(string animEvent)
	{
		if (animEvent == AnimEvents.EndHitAnim)
		{
			_fsm.ChangeState(StateType.Idle);
			_ctx.stat.IsInvincible = false;
		}
	}
}
