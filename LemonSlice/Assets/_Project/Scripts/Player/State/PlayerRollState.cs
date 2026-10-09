using System.Collections.Generic;
using UnityEngine;

public class PlayerRollState : StateBase<PlayerContext>
{
	private bool isRolled;
	private bool isRotated => (_ctx.paladin.rotation == _ctx.transform.rotation);
	private float rotateSpeed = 1200f;

	public PlayerRollState(PlayerContext context, StateMachine<PlayerContext> stateMachine) : base(context,
		stateMachine)
	{
	}

	public override void Enter()
	{
		_ctx.stat.IsInvincible = true;
		_ctx.animHandler.PlayRollAnim();
		SetRollVelocity();
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

		if (isRolled)
		{
			_ctx.paladin.rotation = Quaternion.RotateTowards(
				_ctx.paladin.rotation,
				_ctx.transform.rotation,
				rotateSpeed * Time.deltaTime);
		}

		if (isRolled && isRotated)
		{
			isRolled = false;
			EndRollState();
		}
	}

	public override void Exit()
	{
		_ctx.stat.IsInvincible = false;
		_ctx.animHandler.PlayIdleAndMoveAnim();
		_ctx.rigidbody.velocity = Vector3.zero;
		_ctx.animHandler.SetMoveParam(Vector3.zero);

		_ctx.paladin.rotation = Quaternion.LookRotation(_ctx.transform.forward);
	}

	public override void OnAnimEvent(string animEvent)
	{
		switch (animEvent)
		{
			case AnimEvents.EndRollAnim:
				isRolled = true;
				break;
		}
	}

	//----------------State Method------------------

	private void SetRollVelocity()
	{
		Vector3 moveDirection;
		if (_ctx.input.MoveAxisRaw != Vector3.zero)
		{
			moveDirection = _ctx.transform.TransformDirection(_ctx.input.MoveAxisRaw).normalized;
		}
		else
		{
			moveDirection = _ctx.transform.forward;
		}

		_ctx.rigidbody.velocity = new Vector3(
			moveDirection.x * _ctx.stat.MoveSpeed * _ctx.stat.RollForce,
			_ctx.rigidbody.velocity.y,
			moveDirection.z * _ctx.stat.MoveSpeed * _ctx.stat.RollForce);

		_ctx.paladin.rotation = Quaternion.LookRotation(moveDirection);
	}

	private void EndRollState()
	{
		_fsm.ChangeState(StateType.Idle);
	}
}
