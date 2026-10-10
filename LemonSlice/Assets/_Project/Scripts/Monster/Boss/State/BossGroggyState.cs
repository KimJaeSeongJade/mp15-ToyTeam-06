using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGroggyState : StateBase<BossContext>
{
	private float groggyTime;

	public BossGroggyState(BossContext context, StateMachine<BossContext> stateMachine) : base(context,
		stateMachine)
	{
	}

	public override void Enter()
	{
		_ctx.animHandler.PlayGroggyAnim();
		groggyTime = 0f;
	}

	public override void Tick()
	{
		groggyTime += Time.deltaTime;

		if (groggyTime >= _ctx.stat.GroggyTime)
		{
			_fsm.ChangeState(StateType.Move);
		}
	}
}
