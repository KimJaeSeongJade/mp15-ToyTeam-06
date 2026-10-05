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
	public override void Enter()
	{
		_ctx.animHandler.PlayIdleAndMoveAnim();
	}

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
		enemyList = _ctx.playerDetection.GetEnemyList();

		if (enemyList.Count == 0)
		{
			_ctx.isLockOn = false;
			return;
		}

		maxIndex = enemyList.Count - 1;

		if (Input.GetKeyDown(KeyCode.Tab))
		{
			enemyList[_ctx.lockOnIndex].GetComponent<MonsterController>().SetLockOnUi(false);

			if (_ctx.lockOnIndex >= maxIndex)
			{
				_ctx.lockOnIndex = 0;
			}
			else
			{
				_ctx.lockOnIndex++;
			}
		}

		if (enemyList.Count - 1 < _ctx.lockOnIndex)
		{
			return;
		}

		_ctx.transform.LookAt(enemyList[_ctx.lockOnIndex]);
		enemyList[_ctx.lockOnIndex].GetComponent<MonsterController>().SetLockOnUi(true);
	}
}
