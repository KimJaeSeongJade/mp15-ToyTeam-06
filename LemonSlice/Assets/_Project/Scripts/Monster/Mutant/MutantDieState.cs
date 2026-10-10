using UnityEngine;

public class MutantDieState : StateBase<MutantContext>
{
	public MutantDieState(MutantContext context, StateMachine<MutantContext> stateMachine) : base(context, stateMachine)
	{
	}

	public override void Enter()
	{
		_ctx.animHandler.PlayDieAnim();

		_ctx.monsterDetection.TargetTransform.GetComponentInChildren<LockOnController>()
			.RemoveEnemy(_ctx.transform.gameObject);

		BoxCollider collider = _ctx.transform.GetComponent<BoxCollider>();
		if (collider != null)
		{
			collider.enabled = false;
		}
	}

	public override void OnAnimEvent(string animEvent)
	{

		if (animEvent == AnimEvents.EndDieAnim)
		{
			GameFlowManager.Instance.IsBossDead = true;
		}
	}
}
