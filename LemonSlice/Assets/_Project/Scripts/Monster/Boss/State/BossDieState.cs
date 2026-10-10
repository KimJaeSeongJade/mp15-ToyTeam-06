using UnityEngine;

public class BossDieState : StateBase<BossContext>
{
	public BossDieState(BossContext context, StateMachine<BossContext> stateMachine) : base(context,
		stateMachine)
	{
	}

	public override void Enter()
	{
		_ctx.animHandler.PlayDieAnim();

		_ctx.transform.GetComponent<CapsuleCollider>().enabled = false;

		MainUIManager.Instance.SetBossUI(false);
	}

	public override void OnAnimEvent(string animEvent)
	{
		if (animEvent == AnimEvents.EndDieAnim)
		{
			DropCoin();
		}
	}
	private void DropCoin()
	{
		int count = 20;

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
					Random.Range(-2f, 2f),
					Random.Range(1.5f, 2.5f),
					Random.Range(-2f, 2f)
				).normalized;

				float dropForce = Random.Range(3f, 4f);
				rigidbody.AddForce(randomDirection * dropForce, ForceMode.Impulse);
			}
		}
	}
}
