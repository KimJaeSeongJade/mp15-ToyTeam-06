using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterChaseState : StateBase<MonsterContext>
{
	public MonsterChaseState(MonsterContext context, StateMachine<MonsterContext> stateMachine) : base(context,
		stateMachine)
	{
	}

	public override void Tick()
	{
		if (_ctx.isPlayerEnter)
		{
			_fsm.ChangeState(StateType.Idle);
		}
	}

	public override void Exit()
	{
		_fsm.ChangeState(StateType.Idle);
	}
}
