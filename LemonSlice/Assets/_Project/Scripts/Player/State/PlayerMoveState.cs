using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : StateBase<PlayerContext>
{
	private List<Transform> enemyList = new List<Transform>();

	private int maxIndex;

	public PlayerMoveState(PlayerContext context, StateMachine<PlayerContext> stateMachine) : base(context,
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
		if (_ctx.input.MoveAxisRaw == Vector3.zero)
		{
			_fsm.ChangeState(StateType.Idle);
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

		_ctx.animHandler.SetMoveParam(_ctx.input.MoveAxisRaw);


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

	public override void FixedTick()
	{
		Move();
	}

	public override void Exit()
	{
		_ctx.rigidbody.velocity = Vector3.zero;
		_ctx.animHandler.SetMoveParam(Vector3.zero);
	}

	//----------------State Method------------------

	private void Rotate()
	{
		_ctx.transform.Rotate(0, _ctx.input.MouseDelta.y, 0, Space.Self);
	}

	private void Move()
	{
		Vector3 moveDirection = _ctx.transform.TransformDirection(_ctx.input.MoveAxisRaw).normalized;

		_ctx.rigidbody.velocity = new Vector3(
			moveDirection.x * _ctx.stat.MoveSpeed,
			_ctx.rigidbody.velocity.y,
			moveDirection.z * _ctx.stat.MoveSpeed);
	}

	private void LockOn()
	{
		enemyList = _ctx.MonsterDetection.GetEnemyList();

		// 락온을 했지만 락온거리에 적이 없을 때
		if (enemyList.Count == 0)
		{
			_ctx.isLockOn = false;
			return;
		}

		maxIndex = enemyList.Count - 1;

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

		// 락온 중에 몬스터가 감지거리 밖으로 나가졌을때 예외처리
		// TODO 제가 10/3에 고쳐보겠습니다...(강성현)
		if (enemyList[_ctx.lockOnIndex] == null)
		{
			return;
		}

		_ctx.transform.LookAt(enemyList[_ctx.lockOnIndex]);
	}
}
