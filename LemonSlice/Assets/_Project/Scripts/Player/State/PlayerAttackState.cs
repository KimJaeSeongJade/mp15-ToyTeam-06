public class PlayerAttackState : StateBase<PlayerContext>
{
	private const int MAX_COMBO = 3;
	private bool canCombo;
	private bool canNextAttack;
	private int comboIndex;

	public PlayerAttackState(PlayerContext context, StateMachine<PlayerContext> stateMachine) : base(context,
		stateMachine)
	{
	}

	//----------------State Method------------------
	public override void Enter()
	{
		canCombo = false;
		comboIndex = 1;
		_ctx.animHandler.PlayAttackAnim(comboIndex);
	}

	public override void Tick()
	{
		if (_ctx.input.IsRollPressed && comboIndex < MAX_COMBO)
		{
			_fsm.ChangeState(StateType.Roll);
			return;
		}

		if (_ctx.input.IsAttackPressed && canCombo && comboIndex < MAX_COMBO)
		{
			canNextAttack = true;
		}
	}

	public override void Exit()
	{
	}

	public override void OnAnimEvent(string animEvent)
	{
		if (animEvent == _ctx.animHandler.EndAttackAnim)
		{
			if (canNextAttack)
			{
				canNextAttack = false;
				if (comboIndex < MAX_COMBO)
				{
					comboIndex++;
				}

				_ctx.animHandler.PlayAttackAnim(comboIndex);
			}
			else
			{
				_fsm.ChangeState(StateType.Idle);
			}
		}

		if (animEvent == _ctx.animHandler.OpenCombo)
		{
			canCombo = true;
		}

		if (animEvent == _ctx.animHandler.CloseCombo)
		{
			canCombo = false;
		}
	}
	//----------------State Method------------------
}
