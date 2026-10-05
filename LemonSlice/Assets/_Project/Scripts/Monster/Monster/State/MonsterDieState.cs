using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterDieState : StateBase<MonsterContext>
{
	public MonsterDieState(MonsterContext context, StateMachine<MonsterContext> stateMachine) : base(context,
		stateMachine)
	{
	}
	private Coin _coin;

	public override void Enter()
	{
		_ctx.animHandler.PlayDieAnim();
		// TODO
		// 몬스터를 풀로 반환
		DropCoin();
	}

	private void DropCoin()
	{

		int count = Random.Range(1, 5);

		for (int i = 0; i < count; i++)
		{
			Vector3 coinPosition = new Vector3(
				_ctx.transform.position.x,
				_ctx.transform.position.y+3,
				_ctx.transform.position.z
			);

			GameObject go = PoolManager.Instance.Take(_ctx.coin).SetPosition(coinPosition
			).Build();
			/*Rigidbody rigidbody = go.GetComponent<Rigidbody>();
			rigidbody.AddForce(coinPosition, ForceMode.VelocityChange);*/
		}
	}
}
