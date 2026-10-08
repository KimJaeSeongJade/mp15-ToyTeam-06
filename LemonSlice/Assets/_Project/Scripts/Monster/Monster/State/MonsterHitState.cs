using UnityEngine;

public class MonsterHitState : StateBase<MonsterContext>
{
	private GameObject _bloodEffect;
	private int _hitIndex;
	private const int HIT_COUNT = 2;

	public MonsterHitState(MonsterContext monsterContext, StateMachine<MonsterContext> stateMachine) : base(
		monsterContext, stateMachine)
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
		if (_bloodEffect != null)
		{
			StopHitEffect();
		}

		Vector3 pos = _ctx.hitPoint;
		Vector3 dir = _ctx.hitDirection;
		_bloodEffect = EffectManager.Instance.PlayBloodEffect(pos, dir);
	}

	private void StopHitEffect()
	{
		EffectManager.Instance.StopBloodEffect(_bloodEffect);
	}
}
