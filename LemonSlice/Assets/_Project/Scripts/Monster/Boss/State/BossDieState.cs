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

		_ctx.monsterDetection.TargetTransform.GetComponentInChildren<LockOnController>()
			.RemoveEnemy(_ctx.transform.gameObject);

		MainUIManager.Instance.SetBossUI(false);
	}

	public override void OnAnimEvent(string animEvent)
	{
		if (animEvent == AnimEvents.EndDieAnim)
		{
			DropCoin();
			_ctx.transform.gameObject.SetActive(false);
		}
	}
	private void DropCoin()
	{
		int count = 10;

		for (int i = 0; i < count; i++)
		{
			Vector3 coinPosition = new Vector3(
				_ctx.transform.position.x + Random.Range(-0.5f, 0.5f),
				_ctx.transform.position.y + Random.Range(1f, 1.5f),
				_ctx.transform.position.z + Random.Range(-0.5f, 0.5f)
			);

			GameObject go = PoolManager.Instance.Take(_ctx.coin).SetPosition(coinPosition
			).Build();
			Rigidbody rigidbody = go.GetComponent<Rigidbody>();
			if (rigidbody != null)
			{
				rigidbody.velocity = Vector3.zero;
				rigidbody.angularVelocity = Vector3.zero;

				Vector3 randomDirection = new Vector3(
					Random.Range(-0.5f, 0.5f),
					Random.Range(0.5f, 1.5f),
					Random.Range(-0.5f, 0.5f)
				).normalized;

				float dropForce = Random.Range(1f, 2f);
				rigidbody.AddForce(randomDirection * dropForce, ForceMode.Impulse);
			}
		}
	}
}
