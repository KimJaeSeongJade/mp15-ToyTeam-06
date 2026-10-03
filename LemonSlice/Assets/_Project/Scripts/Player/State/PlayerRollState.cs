using UnityEngine;

public class PlayerRollState : StateBase<PlayerContext>
{
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
			moveDirection.x * _ctx.stat.MoveSpeed * 1.5f,
			_ctx.rigidbody.velocity.y,
			moveDirection.z * _ctx.stat.MoveSpeed * 1.5f);
	}
}
