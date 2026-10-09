using UnityEngine;

public class PlayerHitState : StateBase<PlayerContext>
{
	private GameObject _hitEffect;

	private int _hitIndex;
	private const int HIT_COUNT = 2;

	public PlayerHitState(PlayerContext context, StateMachine<PlayerContext> stateMachine) : base(context, stateMachine)
	{
	}

	public override void Enter()
	{
		_hitIndex = 1;
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
			_hitIndex = (_hitIndex % HIT_COUNT) + 1;
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
		_ctx.animHandler.PlayHitAnim(_hitIndex);
		_ctx.isUnderAttack = false;
	}

	private void PlayHitEffect()
	{
		// TODO 이펙트 변경 필요함
		// 플레이어 앞으로 표시되서 잘 안보임

		if (_hitEffect != null)
		{
			StopHitEffect();
		}

		Vector3 pos = _ctx.hitPoint;
		Debug.Log($"PlayerHitPoint: {pos.x},{pos.y},{pos.z}");
		_hitEffect = EffectManager.Instance.PlayHitEffect(pos);
	}

	private void StopHitEffect()
	{
		EffectManager.Instance.StopEffect(_hitEffect);
	}
}
