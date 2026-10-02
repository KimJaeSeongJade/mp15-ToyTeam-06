using Unity.VisualScripting;
using UnityEngine;

public class MonsterIdleState : StateBase<MonsterContext>
{
	public MonsterIdleState(MonsterContext context, StateMachine<MonsterContext> stateMachine) : base(context,
		stateMachine)
	{
	}
	public override void Tick()
	{
		if (_ctx.isPlayerEnter)
		{
			_fsm.ChangeState(StateType.Move);
		}
	}

}
