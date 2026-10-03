using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : StateBase<PlayerContext>
{
	private List<Transform> enemyList = new List<Transform>();

	private int maxIndex;

	public PlayerIdleState(PlayerContext context, StateMachine<PlayerContext> stateMachine) : base(context,
		stateMachine)
	{
	}

	//----------------State Method------------------
	public override void Tick()
	{
		if (_ctx.input.MoveAxisRaw != Vector3.zero)
		{
			_fsm.ChangeState(StateType.Move);
			return;
		}

		if (_ctx.input.IsRollPressed)
		{
			_fsm.ChangeState(StateType.Roll);
			return;
		}

		if (_ctx.input.IsAttackPressed)
		{
			_fsm.ChangeState(StateType.Attack);
			return;
		}

		if (_ctx.input.LockOnPressed)
		{
			_ctx.isLockOn = !_ctx.isLockOn;
			_ctx.lockOnIndex = 0;
		}

		if (!_ctx.isLockOn)
		{
			Rotate();
		}
		else
		{
			LockOn();
		}
	}
	//----------------State Method------------------

	private void Rotate()
	{
		_ctx.transform.Rotate(0, _ctx.input.MouseDelta.y, 0, Space.Self);
	}

	private void LockOn()
	{
		enemyList = _ctx.MonsterDetection.GetEnemyList();
		maxIndex = enemyList.Count - 1;

		// 락온을 했지만 락온거리에 적이 없을 때
		if (enemyList.Count == 0)
		{
			_ctx.isLockOn = false;
			return;
		}

		if (Input.GetKeyDown(KeyCode.Tab))
		{
			if (_ctx.lockOnIndex >= maxIndex)
			{
				_ctx.lockOnIndex = 0;
			}
			else
			{
				_ctx.lockOnIndex++;
			}
		}

		// TODO 추후에 몬스터 스크립트 생기면 변경 필요
		TempMonster target = enemyList[_ctx.lockOnIndex].GetComponent<TempMonster>();

		_ctx.transform.LookAt(target.transform);
	}
}
