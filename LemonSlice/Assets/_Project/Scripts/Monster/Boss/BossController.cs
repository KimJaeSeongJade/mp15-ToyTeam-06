using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour, IDamageable, ILockonable
{
	[SerializeField] private string _stateType;
	[SerializeField] private Image _lockOnUi;
	[SerializeField] private List<AttackHitBox> _hitBoxes;
	[SerializeField] private GameObject _coin;

	private BossAnimationHandler _animHandler;
	private BossContext _ctx;
	private StateMachine<BossContext> _machine;
	private MonsterDetection _monsterDetection;
	private BossStat _stat;
	private GameObject _hitEffect;
	private WaitForSeconds _hitEffectWait = new(1f);

	// --------- 이벤트 함수 ------------
	private void Awake()
	{
		CacheComponets();
		BindContext();
		InitStateMachine();
	}

	private void Start()
	{
		_machine.ChangeState(StateType.Idle);
	}

	private void Update()
	{
		_machine.Tick();
		RotateUI();
		_stateType = _machine.Current.GetType().ToString();
	}

	// --------------------------------

	private void CacheComponets()
	{
		_stat = GetComponent<BossStat>();
		_animHandler = GetComponent<BossAnimationHandler>();
		_monsterDetection = GetComponentInChildren<MonsterDetection>();
	}

	private void BindContext()
	{
		_ctx = new()
		{
			transform = transform,
			animHandler = _animHandler,
			stat = _stat,
			monsterDetection = _monsterDetection,
			hitBoxes = _hitBoxes,
			coin =  _coin,
		};
	}

	private void InitStateMachine()
	{
		_machine = new StateMachine<BossContext>();

		_machine.Add(StateType.Idle, new BossIdleState(_ctx, _machine));
		_machine.Add(StateType.Move, new BossChaseState(_ctx, _machine));
		_machine.Add(StateType.Attack, new BossAttackState(_ctx, _machine));
		_machine.Add(StateType.Knockback, new BossKnockbackState(_ctx, _machine));
		_machine.Add(StateType.Groggy, new BossGroggyState(_ctx, _machine));
		_machine.Add(StateType.Die, new BossDieState(_ctx, _machine));
		_machine.Add(StateType.Searching, new BossSearchingState(_ctx, _machine));
	}

	public void OnAnimEvent(string animEvent)
	{
		_machine.OnAnimEvent(animEvent);
	}

	public void TakeDamage(DamageInfo damageInfo)
	{
		_hitEffect = EffectManager.Instance.PlayMonsterHitEffect(damageInfo.HitPoint);
		_stat.CurrentHealth.Value -= damageInfo.Damage;
		_stat.CurrentGroggy.Value -= damageInfo.DownValue;

		if (_stat.CurrentHealth.Value > 0 && _stat.CurrentGroggy.Value <= 0)
		{
			_machine.ChangeState(StateType.Groggy);
		}

		if (_stat.CurrentHealth.Value <= 0)
		{
			_machine.ChangeState(StateType.Die);
		}
	}

	public void SetLockOnUi(bool lockOn)
	{
		_lockOnUi.gameObject.SetActive(lockOn);
	}

	private void RotateUI()
	{
		if (!_lockOnUi.gameObject.activeSelf) return;

		Transform cameraTransform = Camera.main.transform;

		_lockOnUi.rectTransform.LookAt(cameraTransform);
	}

	private IEnumerator hitEffectRoutine()
	{
		yield return _hitEffectWait;
		PoolManager.Instance.TryReturn(_hitEffect);
	}

}
