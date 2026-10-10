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
	private int[] weaponDamage = { 5, 10, 15 };
	private int[] weaponDownValue = { 10, 20, 40 };
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
		CloseHitBox();
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
			case AnimEvents.OpenHitBox:
				OpenHitBox();
				break;
			case AnimEvents.CloseHitBox:
				CloseHitBox();
				break;
			case AnimEvents.PlaySlashEffect:
				PlaySlashEffect(comboIndex);
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

	private void EndAttackAnim()
	{
		if (canNextAttack)
		{
			canNextAttack = false;
			comboIndex++;

			_ctx.animHandler.PlayAttackAnim(comboIndex);
			AddAttackForce(comboIndex);
		}
		else
		{
			_fsm.ChangeState(StateType.Idle);
		}
	}

	public void OpenHitBox()
	{
		int baseDamage = _ctx.stat.AttackPower;
		int extraDamage = weaponDamage[comboIndex - 1];
		_ctx.hitBox.Open(baseDamage + extraDamage, weaponDownValue[comboIndex - 1]);
	}

	public void CloseHitBox() => _ctx.hitBox.Close();
	private void OpenCombo() => canCombo = true;
	private void CloseCombo() => canCombo = false;

	private void PlaySlashEffect(int comboIndex)
	{
		Vector3 pos = _ctx.transform.position;
		pos.y += 1;
		Quaternion rot = GetSlashDir(comboIndex);

		EffectManager.Instance.PlaySlashEffect(pos, rot);
	}

	private Quaternion GetSlashDir(int comboIndex)
	{
		Quaternion baseRot = Quaternion.LookRotation(_ctx.transform.forward, Vector3.up);
		Quaternion resultRot = new();
		switch (comboIndex)
		{
			case 1:
				resultRot = baseRot *
				            Quaternion.AngleAxis(180, Vector3.forward) *
				            Quaternion.AngleAxis(-90, Vector3.up) *
				            Quaternion.AngleAxis(45, Vector3.right);
				break;
			case 2:
				resultRot = baseRot *
				            Quaternion.AngleAxis(75, Vector3.forward);
				break;
			case 3:
				resultRot = baseRot;
				break;
		}
		return resultRot;
	}
}
