using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantGroggyState : StateBase<MutantContext>
{
	private float groggyTime;

	public MutantGroggyState(MutantContext context, StateMachine<MutantContext> stateMachine) : base(context,
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
