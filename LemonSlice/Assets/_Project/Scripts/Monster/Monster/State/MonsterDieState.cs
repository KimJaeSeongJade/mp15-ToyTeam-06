using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterDieState : StateBase<MonsterContext>
{
	public MonsterDieState(MonsterContext context, StateMachine<MonsterContext> stateMachine) : base(context,
		stateMachine)
	{
	}

	public override void Enter()
	{
		_ctx.animHandler.PlayDieAnim();
		// PoolManager.Instance.Take().SetPosition()
		// TODO
		// 몬스터를 풀로 반환
		DropCoin();
	}

	private void DropCoin()
	{
		int count = Random.Range(1, 5);

		for (int i = 0; i < count; i++)
		{
			 Vector3 coinPosition = _ctx.transform.position;
			PoolManager.Instance.Take(_ctx.coin).SetPosition(coinPosition
			);
		}
	}
}
