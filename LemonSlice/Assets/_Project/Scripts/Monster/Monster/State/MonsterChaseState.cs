using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterChaseState : StateBase<MonsterContext>
{
	public MonsterChaseState(MonsterContext context, StateMachine<MonsterContext> stateMachine) : base(context,
		stateMachine)
	{
	}

	public override void Tick()
	{
		if (_ctx.targetTransform == null) // 기존 엔터를 target transform으로 변경
		{
			_ctx.isPlayerEnter = false;
			_fsm.ChangeState(StateType.Idle);
			return;
		}

		Vector3 direction = (_ctx.targetTransform.position - _ctx.transform.position).normalized; // 거리계산
		direction.y = 0;

		if (direction != Vector3.zero)
		{
			Quaternion targetRotation = Quaternion.LookRotation(direction); // target으로 위치 변경
			_ctx.transform.rotation = Quaternion.Slerp(_ctx.transform.rotation, targetRotation, Time.deltaTime * 5f); //돌아가는 속도 조정
		}

		_ctx.transform.Translate(Vector3.forward * (_ctx.stat.MoveSpeed * Time.deltaTime));
	}


}
