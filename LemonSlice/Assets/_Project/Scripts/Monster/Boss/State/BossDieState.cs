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
			GameFlowManager.Instance.IsBossDead = true;
		}
	}
}
