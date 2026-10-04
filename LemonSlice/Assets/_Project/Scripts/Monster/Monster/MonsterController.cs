using UnityEngine;

public class MonsterController : MonoBehaviour, IPoolable, IDamageable
{
	[SerializeField] private string _stateType;

	private MonsterAnimHandler _animHandler;
	private MonsterContext _ctx;
	private StateMachine<MonsterContext> _machine;
	private PlayerDetection _playerDetection;
	private Rigidbody _rigidbody;
	private MonsterStat _stat;

	// --------- 이벤트 함수 ------------
	private void Awake()
	{
		CacheComponents();
		BindContext();
		InitStateMachine();
	}

	private void Start() => _machine.ChangeState(StateType.Idle);

	private void Update()
	{
		_machine.Tick();
	}

	// --------------------------------

	private void CacheComponents()
	{
		_stat = GetComponent<MonsterStat>();
		_rigidbody = GetComponent<Rigidbody>();
		_animHandler = GetComponent<MonsterAnimHandler>();
		_playerDetection = GetComponentInChildren<PlayerDetection>();
	}

	private void BindContext()
	{
		_ctx = new()
		{
			transform = transform,
			animHandler = _animHandler,
			stat = _stat,
			rigidbody = _rigidbody,
			playerDetection = _playerDetection
		};
	}

	private void InitStateMachine()
	{
		_machine = new StateMachine<MonsterContext>();

		_machine.Add(StateType.Idle, new MonsterIdleState(_ctx, _machine));
		_machine.Add(StateType.Move, new MonsterChaseState(_ctx, _machine));
		_machine.Add(StateType.Attack, new MonsterAttackState(_ctx, _machine));
		_machine.Add(StateType.Die, new MonsterDieState(_ctx, _machine));
		_machine.Add(StateType.Knockback, new MonsterKnockbackState(_ctx, _machine));
	}

	public void OnAnimEvent(string animEvent)
	{
		_machine.OnAnimEvent(animEvent);
	}

	public void TakeDamage(DamageInfo damageInfo)
	{
		_stat.currentHealth.Value -= damageInfo.Damage;

		if (_stat.currentHealth.Value <= 0)
		{
			_machine.ChangeState(StateType.Die);
		}
	}

	public PoolType PoolId => PoolType.Monster;
}
