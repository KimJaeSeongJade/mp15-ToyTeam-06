using UnityEngine;

public class PlayerIdleState : StateBase<PlayerContext>
{
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
		else
		{
			Rotate();
		}
	}
	//----------------State Method------------------

	private void Rotate()
	{
		_ctx.transform.Rotate(0, _ctx.input.MouseDelta.y, 0, Space.Self);
	}
}
