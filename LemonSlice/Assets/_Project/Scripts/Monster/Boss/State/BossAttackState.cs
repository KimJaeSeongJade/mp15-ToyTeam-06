using UnityEngine;

public class BossAttackState : StateBase<BossContext>
{
	private float attackTime;

	public BossAttackState(BossContext context, StateMachine<BossContext> stateMachine) : base(context,
		stateMachine)
	{
	}

	public override void Enter()
	{
		attackTime = 0f;
		// Animation 실행
	}

	public override void Tick()
	{
		// TODO 추후에 애니메이션 작업 후에 Attack중이라면 return
		// TODO Attack 애니메이션 끝나면 시간초 세는 구조로 변경 필요

		attackTime += Time.deltaTime;

		float distance = Vector3.Distance(_ctx.transform.position, _ctx.playerDetection.TargetTransform.position);

		if (attackTime >= _ctx.stat.AttackDelay)
		{
			if (distance > _ctx.stat.AttackDistance)
			{
				_fsm.ChangeState(StateType.Move);
			}

			Debug.Log("공격시작");
			// 공격 애니 재생
			// 공격 쿨타임 초기화

			attackTime = 0;
		}
		else
		{
			Debug.Log("보스 공격중");
		}
	}
}
