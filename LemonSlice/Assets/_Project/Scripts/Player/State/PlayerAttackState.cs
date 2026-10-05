using UnityEngine;

public class PlayerAttackState : StateBase<PlayerContext>
{
	private bool canCombo;
	private bool canNextAttack;
	private int comboIndex;

	// TODO 콤보마다 데미지와 다운게이지를 다르게 주기
	// 무기 변경 시 세트로 바뀌도록?
	// Inspector에서 리스트로 더하더록?
	private const int MAX_COMBO = 3;
	private int[] weaponDamage = { 20, 30, 50 };
	private int[] weaponDownValue = { 10, 20, 50 };
	private float[] moveForce = { 0, 3.5f, 5f };


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
		if (comboIndex < MAX_COMBO)
		{
			Rotate();
		}

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
		switch (animEvent)
		{
			case AnimEvents.EndAttackAnim:
				EndAttackAnim();
				break;
			case AnimEvents.OpenCombo:
				OpenCombo();
				break;
			case AnimEvents.CloseCombo:
				CloseCombo();
				break;
		}
	}
	//----------------State Method------------------

	private void Rotate()
	{
		_ctx.transform.Rotate(0, _ctx.input.MouseDelta.y, 0, Space.Self);
	}

	private void AddAttackForce(int comboIndex)
	{
		Vector3 direction = _ctx.transform.forward.normalized;
		_ctx.rigidbody.AddForce(direction * moveForce[comboIndex - 1], ForceMode.Impulse);
	}

	private void OpenCombo()
	{
		int baseDamage = _ctx.stat.AttackPower;
		int extraDamage = weaponDamage[comboIndex - 1];
		canCombo = true;
		_ctx.hitBox.Open(baseDamage + extraDamage, weaponDownValue[comboIndex - 1]);
	}

	private void CloseCombo()
	{
		canCombo = false;
		_ctx.hitBox.Close();
	}

	private void EndAttackAnim()
	{
		if (canNextAttack)
		{
			canNextAttack = false;
			if (comboIndex < MAX_COMBO)
			{
				comboIndex++;
			}

			_ctx.animHandler.PlayAttackAnim(comboIndex);
			AddAttackForce(comboIndex);
		}
		else
		{
			_fsm.ChangeState(StateType.Idle);
		}
	}
}
