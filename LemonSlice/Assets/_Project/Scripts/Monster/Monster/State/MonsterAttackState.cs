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
		_ctx.animHandler.PlayAttackAnim();
	}

	public override void Tick()
	{
		// TODO 추후에 애니메이션 작업 후에 Attack중이라면 return
		// TODO Attack 애니메이션 끝나면 시간초 세는 구조로 변경 필요\

		if (_ctx.playerDetection.TargetTransform == null)
		{
			// TODO 애니메이션 연결 후에 애니메이션 이벤트에 따라 if 필요
			// if(애니메이션이 끝났다면)
			// { _fsm.ChangeState(StateType.Idle); }
			_fsm.ChangeState(StateType.Idle);
			return;
		}

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
			Debug.Log("몬스터 공격중");
		}
	}
}
