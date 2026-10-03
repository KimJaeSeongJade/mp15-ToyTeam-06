using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDieState : StateBase<MonsterContext>
{
	public MonsterDieState(MonsterContext context, StateMachine<MonsterContext> stateMachine) : base(context,
		stateMachine)
	{

	}

	public override void Enter()
	{
		// 사망 애니메이션 재생
		_ctx.animHandler.PlayMonsterDieAnim();

		// 몬스터를 풀로 반환
	}
}
