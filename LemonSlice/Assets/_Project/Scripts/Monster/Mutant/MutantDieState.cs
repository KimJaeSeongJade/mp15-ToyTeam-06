using UnityEngine;

public class MutantDieState : StateBase<MutantContext>
{
	public MutantDieState(MutantContext context, StateMachine<MutantContext> stateMachine) : base(context, stateMachine)
	{
	}

	public override void Enter()
	{
		
		_ctx.animHandler.PlayDieAnim();
		
		CapsuleCollider collider = _ctx.transform.GetComponent<CapsuleCollider>();
		if (collider != null)
		{
			collider.enabled = false;
		}
	}

	public override void OnAnimEvent(string animEvent)
	{
		
		if (animEvent == "EndDieAnim")
		{
			
		}
	}
}
