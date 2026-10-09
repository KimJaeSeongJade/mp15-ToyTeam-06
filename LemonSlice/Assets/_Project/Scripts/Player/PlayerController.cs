using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
	[SerializeField] private string paladinTag;

	private PlayerAnimHandler _animHandler;
	private PlayerContext _ctx;
	private PlayerInput _input;

	private StateMachine<PlayerContext> _machine;
	private PlayerDetection _playerDetection;
	private Rigidbody _rigidbody;
	private PlayerStat _stat;
	private AttackHitBox _hitBox;
	private LockOnController _lockOnController;
	private Transform _paladinTransform;

	// --------- 이벤트 함수 ------------
	private void Awake()
	{
		CacheComponets();
		BindContext();
		InitStateMachine();
	}

	private void Start() => _machine.ChangeState(StateType.Idle);

	private void Update()
	{
		if (Time.timeScale == 0) return;

		_input.Read();
		_machine.Tick();
	}

	private void FixedUpdate()
	{
		_machine.FixedTick();
	}

	// --------------------------------

	private void CacheComponets()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_input = GetComponent<PlayerInput>();
		_stat = GetComponent<PlayerStat>();
		_animHandler = GetComponent<PlayerAnimHandler>();
		_playerDetection = GetComponentInChildren<PlayerDetection>();
		_hitBox = GetComponentInChildren<AttackHitBox>();
		_lockOnController = GetComponentInChildren<LockOnController>();
		_paladinTransform = GetComponentInChildren<Animator>().transform;
	}

	public void TakeDamage(DamageInfo damageInfo)
	{

		if (_stat.IsInvincible)
		{
			return;
		}

		BindDamageInfo(damageInfo);
		TryChangeState();
	}

	private void BindContext()
	{
		_ctx = new()
		{
			animHandler = _animHandler,
			rigidbody = _rigidbody,
			transform = transform,
			input = _input,
			stat = _stat,
			playerDetection = _playerDetection,
			hitBox = _hitBox,
			lockOnController = _lockOnController,
			paladin =  _paladinTransform,
		};
	}

	private void InitStateMachine()
	{
		_machine = new StateMachine<PlayerContext>();

		_machine.Add(StateType.Idle, new PlayerIdleState(_ctx, _machine));
		_machine.Add(StateType.Move, new PlayerMoveState(_ctx, _machine));
		_machine.Add(StateType.Roll, new PlayerRollState(_ctx, _machine));
		_machine.Add(StateType.Attack, new PlayerAttackState(_ctx, _machine));
		_machine.Add(StateType.Die, new PlayerDieState(_ctx, _machine));
		_machine.Add(StateType.Hit, new PlayerHitState(_ctx, _machine));
		_machine.Add(StateType.KnockDown, new PlayerKnockDownState(_ctx, _machine));
	}

	private void BindDamageInfo(DamageInfo damageInfo)
	{
		_stat.CurrentHealth.Value -= damageInfo.Damage;
		_stat.DownPoint -= damageInfo.DownValue;
		_ctx.hitPoint = damageInfo.HitPoint;
		_ctx.hitDirection = damageInfo.HitDirection;
		_ctx.isUnderAttack = true;
		_ctx.knockDownDirection = damageInfo.KnockDownDirection;
	}

	private void TryChangeState()
	{
		if (_stat.CurrentHealth.Value <= 0)
		{
			_machine.ChangeState(StateType.Die);
		}
		else if (_stat.DownPoint <= 0)
		{
			_machine.ChangeState(StateType.KnockDown);
		}
		else
		{
			_machine.ChangeState(StateType.Hit);
		}
	}

	public void OnAnimEvent(string animEvent)
	{
		_machine.OnAnimEvent(animEvent);
	}
}
