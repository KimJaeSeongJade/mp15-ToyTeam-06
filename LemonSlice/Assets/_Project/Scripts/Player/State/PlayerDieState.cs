using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieState : StateBase<PlayerContext>
{
	public PlayerDieState(PlayerContext context, StateMachine<PlayerContext> stateMachine) : base(context, stateMachine)
	{
	}

	public override void Enter()
	{
		_ctx.animHandler.PlayDieAnim();
	}

	public override void OnAnimEvent(string animEvent)
	{
		if (animEvent == AnimEvents.EndDieAnim)
		{
			GameFlowManager.Instance.IsPlayerDead = true;
		}
	}
}
