using UnityEngine;

public class PlayerMoveState : StateBase<PlayerContext>
{
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
		if (TryChangeState())
		{
			return;
		}

		_ctx.animHandler.SetMoveParam(_ctx.input.MoveAxisRaw);

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
			_ctx.lockOnController.ClearLockOn();
		}

		if (_ctx.isLockOn)
		{
			if (_ctx.input.TargetChangePressed)
			{
				_ctx.lockOnController.ChangeLockOn();
			}

			_ctx.transform.LookAt(_ctx.lockOnController.LockOn());
		}
		else
		{
			Rotate();
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

	private bool TryChangeState()
	{
		if (_ctx.input.MoveAxisRaw == Vector3.zero)
		{
			_fsm.ChangeState(StateType.Idle);
			return true;
		}

		if (_ctx.input.IsRollPressed)
		{
			_fsm.ChangeState(StateType.Roll);
			return true;
		}

		if (_ctx.input.IsAttackPressed)
		{
			_fsm.ChangeState(StateType.Attack);
			return true;
		}

		return false;
	}

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
}
