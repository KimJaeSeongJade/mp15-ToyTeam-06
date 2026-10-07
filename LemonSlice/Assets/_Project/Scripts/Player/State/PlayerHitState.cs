using UnityEngine;

public class PlayerHitState : StateBase<PlayerContext>
{
	private GameObject _bloodEffect;
	public PlayerHitState(PlayerContext context, StateMachine<PlayerContext> stateMachine) : base(context, stateMachine)
	{
	}

	public override void Enter()
	{
		PlayHitAnim();
		PlayHitEffect();
	}

	public override void Tick()
	{
		if (_ctx.input.IsRollPressed)
		{
			_fsm.ChangeState(StateType.Roll);
			return;
		}

		if (_ctx.isUnderAttack)
		{
			PlayHitAnim();
			PlayHitEffect();
		}
	}

	public override void Exit()
	{
		StopHitEffect();
	}

	public override void OnAnimEvent(string animEvent)
	{
		if (animEvent == AnimEvents.EndHitAnim)
		{
			_fsm.ChangeState(StateType.Idle);
			StopHitEffect();
		}
	}

	private void PlayHitAnim()
	{
		_ctx.animHandler.PlayHitAnim();
		_ctx.isUnderAttack = false;
	}

	private void PlayHitEffect()
	{
		// TODO 잘 보이지 않음
		Vector3 pos = _ctx.hitPoint;
		Vector3 dir = _ctx.hitDirection;
		_bloodEffect = EffectManager.Instance.PlayBloodEffect(pos, dir);
	}

	private void StopHitEffect()
	{
		EffectManager.Instance.StopBloodEffect(_bloodEffect);
	}
}
