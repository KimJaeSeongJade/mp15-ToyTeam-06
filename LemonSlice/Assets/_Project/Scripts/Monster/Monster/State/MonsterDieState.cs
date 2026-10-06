using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterDieState : StateBase<MonsterContext>
{

	public MonsterDieState(MonsterContext context, StateMachine<MonsterContext> stateMachine) : base(context,
		stateMachine)
	{
	}

	private GameObject gameObject => _ctx.rigidbody.gameObject;

	public override void Enter()
	{
		_ctx.animHandler.PlayDieAnim();
	}


	public override void OnAnimEvent(string animEvent)
	{
		if (animEvent == _ctx.animHandler.EndDieAnim)
		{
			PoolManager.Instance.TryReturn(gameObject);
			DropCoin();

			_ctx.monsterDetection.TargetTransform.GetComponentInChildren<LockOnController>()
			.RemoveEnemy(_ctx.transform.gameObject);
		}
	}

	private void DropCoin()
	{
		int count = Random.Range(1, 5);

		for (int i = 0; i < count; i++)
		{
			Vector3 coinPosition = new Vector3(
				_ctx.transform.position.x,
				_ctx.transform.position.y + 1f,
				_ctx.transform.position.z
			);

			GameObject go = PoolManager.Instance.Take(_ctx.coin).SetPosition(coinPosition
			).Build();
			Rigidbody rigidbody = go.GetComponent<Rigidbody>();
			if (rigidbody != null)
			{
				rigidbody.velocity = Vector3.zero;
				rigidbody.angularVelocity = Vector3.zero;

				Vector3 randomDirection = new Vector3(
					Random.Range(-1f, 1f),
					Random.Range(1.5f, 2.5f),
					Random.Range(-1f, 1f)
				).normalized;

				float dropForce = Random.Range(3f, 5f);
				rigidbody.AddForce(randomDirection * dropForce, ForceMode.Impulse);
			}
		}
	}
}

