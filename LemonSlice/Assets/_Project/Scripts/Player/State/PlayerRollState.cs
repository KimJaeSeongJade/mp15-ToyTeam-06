using System.Collections.Generic;
using UnityEngine;

public class PlayerRollState : StateBase<PlayerContext>
{
	private List<Transform> enemyList = new List<Transform>();

	private int maxIndex;

	public PlayerRollState(PlayerContext context, StateMachine<PlayerContext> stateMachine) : base(context,
		stateMachine)
	{
	}

	public override void Enter()
	{
		_ctx.animHandler.PlayRollAnim();
		SetRollVelocity();
		// PlayerLayerMask 변경으로 무적처리
	}

	public override void Tick()
	{
		if (_ctx.input.LockOnPressed)
		{
			_ctx.isLockOn = !_ctx.isLockOn;
			_ctx.lockOnIndex = 0;
		}

		if (_ctx.isLockOn)
		{
			LockOn();
		}
	}

	public override void Exit()
	{
		// PlayerLayerMask 원복
		_ctx.animHandler.PlayIdleAndMoveAnim();
		_ctx.rigidbody.velocity = Vector3.zero;
		_ctx.animHandler.SetMoveParam(Vector3.zero);
	}

	public override void OnAnimEvent(string animEvent)
	{
		if (animEvent == _ctx.animHandler.EndRollAnim)
		{
			_fsm.ChangeState(StateType.Idle);
		}
	}

	//----------------State Method------------------


	private void SetRollVelocity()
	{
		Vector3 moveDirection = _ctx.transform.TransformDirection(_ctx.input.MoveAxisRaw).normalized;

		_ctx.rigidbody.velocity = new Vector3(
			moveDirection.x * _ctx.stat.MoveSpeed * _ctx.stat.RollSpeed,
			_ctx.rigidbody.velocity.y,
			moveDirection.z * _ctx.stat.MoveSpeed * _ctx.stat.RollSpeed);
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
			if (_ctx.lockOnIndex >= maxIndex)
			{
				_ctx.lockOnIndex = 0;
			}
			else
			{
				_ctx.lockOnIndex++;
			}
		}

		_ctx.transform.LookAt(enemyList[_ctx.lockOnIndex]);
	}
}
