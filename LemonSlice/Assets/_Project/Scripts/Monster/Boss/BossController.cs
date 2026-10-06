using UnityEngine;

public class BossController : MonoBehaviour, IDamageable
{
	[SerializeField] private string _stateType;

	private BossAnimationHandler _animHandler;
	private BossContext _ctx;
	private StateMachine<BossContext> _machine;
	private MonsterDetection _monsterDetection;
	private BossStat _stat;
	private AttackHitBox _hitBox;

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
		_stateType = _machine.Current.GetType().ToString();
	}

	// --------------------------------

	private void CacheComponets()
	{
		_stat = GetComponent<BossStat>();
		_animHandler = GetComponent<BossAnimationHandler>();
		_monsterDetection = GetComponentInChildren<MonsterDetection>();
		_hitBox = GetComponentInChildren<AttackHitBox>();
	}

	private void BindContext()
	{
		_ctx = new()
		{
			transform = transform,
			animHandler = _animHandler,
			stat = _stat,
			monsterDetection = _monsterDetection,
			hitBox = _hitBox
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
	}

	public void OnAnimEvent(string animEvent)
	{
		_machine.OnAnimEvent(animEvent);
	}

	public void TakeDamage(DamageInfo damageInfo)
	{
		_stat.CurrentHealth.Value -= damageInfo.Damage;
		_stat.CurrentGroggy.Value -= damageInfo.DownValue;

		if (_stat.CurrentHealth.Value > 0 && _stat.CurrentGroggy.Value <= 0)
		{
			_stat.SetFullGroggy();
			_machine.ChangeState(StateType.Groggy);
		}

		if (_stat.CurrentHealth.Value <= 0)
		{
			_machine.ChangeState(StateType.Die);
		}
	}
}
