using UnityEngine;
using UnityEngine.UI;

public class MonsterController : MonoBehaviour, IPoolable, IDamageable, ILockonable
{
	[SerializeField] private string _stateType;
	[SerializeField] private GameObject _coin;
	[SerializeField] private Image _lockOnUi;
	[SerializeField] private string _leftHitBoxTag;
	[SerializeField] private string _rightHitBoxTag;
	[SerializeField] private PoolType _poolType;

	private MonsterAnimHandler _animHandler;
	private MonsterContext _ctx;
	private StateMachine<MonsterContext> _machine;
	private MonsterDetection _monsterDetection;
	private Rigidbody _rigidbody;
	private MonsterStat _stat;
	private AttackHitBox _leftHitBox;
	private AttackHitBox _rightHitBox;
	private AttackHitBox[] _hitBoxes;

	// --------- 이벤트 함수 ------------
	private void Awake()
	{
		CacheComponents();
		BindContext();
		InitStateMachine();
	}

	private void OnEnable()
	{
		if(_machine != null)
		{
			_machine.ChangeState(StateType.Idle);
		}

		_ctx.stat.currentHealth.Value = _ctx.stat.MaxHealth;
	}
	private void Update()
	{
		_machine.Tick();
		RotateUI();
	}

	// --------------------------------

	private void CacheComponents()
	{
		_stat = GetComponent<MonsterStat>();
		_rigidbody = GetComponent<Rigidbody>();
		_animHandler = GetComponent<MonsterAnimHandler>();
		_monsterDetection = GetComponentInChildren<MonsterDetection>();
		GetComponentHitBoxes();
	}

	private void BindContext()
	{
		_ctx = new()
		{
			transform = transform,
			animHandler = _animHandler,
			stat = _stat,
			rigidbody = _rigidbody,
			monsterDetection = _monsterDetection,
			coin = _coin,
			leftHitBox = _leftHitBox,
			rightHitBox = _rightHitBox,
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
		_machine.Add(StateType.Hit, new MonsterHitState(_ctx, _machine));
		_machine.Add(StateType.KnockDown, new MonsterKnockDownState(_ctx, _machine));
	}

	public void OnAnimEvent(string animEvent)
	{
		_machine.OnAnimEvent(animEvent);
	}

	public void TakeDamage(DamageInfo damageInfo)
	{
		if (_ctx.stat.IsInvincible)
		{
			return;
		}
		BindDamageInfo(damageInfo);
		TryChangeState();
	}

	public void SetLockOnUi(bool lockOn)
	{
		_lockOnUi.gameObject.SetActive(lockOn);
	}

	private void RotateUI()
	{
		if (!_lockOnUi.gameObject.activeSelf)
		{
			return;
		}

		Transform cameraTransform = Camera.main.transform;

		_lockOnUi.rectTransform.LookAt(cameraTransform);
	}

	private void GetComponentHitBoxes()
	{
		AttackHitBox[] hitBoxes = GetComponentsInChildren<AttackHitBox>();
		for (int i = 0; i < hitBoxes.Length; i++)
		{
			if (hitBoxes[i].gameObject.tag.Equals(_leftHitBoxTag))
			{
				_leftHitBox = hitBoxes[i];
			}
			else
			{
				_rightHitBox = hitBoxes[i];
			}
		}
	}

	private void BindDamageInfo(DamageInfo damageInfo)
	{
		_stat.currentHealth.Value -= damageInfo.Damage;
		_stat.DownPoint -= damageInfo.DownValue;
		_ctx.hitPoint = damageInfo.HitPoint;
		_ctx.hitDirection = damageInfo.HitDirection;
		_ctx.knockDownDirection = damageInfo.KnockDownDirection;
		_ctx.isUnderAttack = true;
	}

	private void TryChangeState()
	{
		if (_stat.currentHealth.Value <= 0)
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

	public PoolType PoolId => _poolType;
}
