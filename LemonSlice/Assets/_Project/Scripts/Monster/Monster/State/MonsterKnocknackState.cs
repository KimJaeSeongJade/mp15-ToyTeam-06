using UnityEngine;

public class MonsterKnockbackState : StateBase<MonsterContext>
{
	private float _knockbackDuration = 0.5f;
	private float _knockbackSpeed = 2f;
	private float _timer;

	public MonsterKnockbackState(MonsterContext context, StateMachine<MonsterContext> stateMachine) : base(context, stateMachine)
	{
	}

	public override void Enter()
	{
		_timer = 0f;

		//  애니메이션을 실행합니다.
		Debug.Log("몬스터 넉백 시작!");
	}

	public override void Tick()
	{
		_timer += Time.deltaTime;


		if (_timer >= _knockbackDuration)
		{
			_fsm.ChangeState(StateType.Idle);
			return;
		}

		if (_ctx.targetTransform != null)
		{
			Vector3 direction = (_ctx.transform.position - _ctx.targetTransform.position).normalized;
			direction.y = 0;

			_ctx.transform.Translate(direction * (_knockbackSpeed * Time.deltaTime), Space.World);
		}
	}
}
