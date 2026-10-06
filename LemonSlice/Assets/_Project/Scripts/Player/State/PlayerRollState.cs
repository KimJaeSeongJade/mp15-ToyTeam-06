using System.Collections.Generic;
using UnityEngine;

public class PlayerRollState : StateBase<PlayerContext>
{
	public PlayerRollState(PlayerContext context, StateMachine<PlayerContext> stateMachine) : base(context,
		stateMachine)
	{
	}

	public override void Enter()
	{
		_ctx.stat.SetInvincible(true);
		_ctx.animHandler.PlayRollAnim();
		SetRollVelocity();
		// PlayerLayerMask 변경으로 무적처리
	}

	public override void Tick()
	{
		if (_ctx.input.LockOnPressed)
		{
			if (_ctx.isLockOn)
			{
				_ctx.lockOnController.ClearLockOn();
				_ctx.isLockOn = false;
			}
			else
			{
				_ctx.isLockOn = _ctx.lockOnController.TryLockOn();
			}
		}

		if (_ctx.isLockOn && !_ctx.lockOnController.HasTarget())
		{
			_ctx.isLockOn = false;
		}

		if(_ctx.isLockOn)
		{
			if (_ctx.input.TargetChangePressed)
			{
				_ctx.lockOnController.ChangeLockOn();
			}

			_ctx.transform.LookAt(_ctx.lockOnController.LockOn());
		}
	}

	public override void Exit()
	{
		_ctx.stat.SetInvincible(false);
		_ctx.animHandler.PlayIdleAndMoveAnim();
		_ctx.rigidbody.velocity = Vector3.zero;
		_ctx.animHandler.SetMoveParam(Vector3.zero);
	}

	public override void OnAnimEvent(string animEvent)
	{
		switch (animEvent)
		{
			case AnimEvents.EndRollAnim:
				EndRollState();
				break;
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

	private void EndRollState()
	{
		_fsm.ChangeState(StateType.Idle);
	}
}
