using UnityEngine;

public class MonsterAttackState : StateBase<MonsterContext>
{
	private float attackTime;

	public MonsterAttackState(MonsterContext context, StateMachine<MonsterContext> stateMachine) : base(context,
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
		attackTime += Time.deltaTime;

		if (attackTime >= _ctx.stat.AttackDelay)
		{
			Debug.Log("공격시작");
			// 공격 애니 재생
			// 공격 쿨타임 초기화
			attackTime = 0;
		}
	}
}
