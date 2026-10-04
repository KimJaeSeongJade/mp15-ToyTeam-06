using UnityEngine;

public class BossController : MonoBehaviour, ILockonable
{
	[SerializeField] private string _stateType;

	private BossAnimationHandler _animHandler;
	private BossContext _ctx;
	private StateMachine<BossContext> _machine;
	private PlayerDetection _playerDetection;
	private BossStat _stat;

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

	public GameObject GameObject { get; }

	// --------------------------------

	private void CacheComponets()
	{
		_stat = GetComponent<BossStat>();
		_animHandler = GetComponent<BossAnimationHandler>();
		_playerDetection = GetComponentInChildren<PlayerDetection>();
	}

	private void BindContext()
	{
		_ctx = new()
		{
			transform = transform,
			animHandler = _animHandler,
			stat = _stat,
			playerDetection = _playerDetection
		};
	}

	private void InitStateMachine()
	{
		_machine = new StateMachine<BossContext>();

		_machine.Add(StateType.Idle, new BossIdleState(_ctx, _machine));
		_machine.Add(StateType.Move, new BossChaseState(_ctx, _machine));
		_machine.Add(StateType.Attack, new BossAttackState(_ctx, _machine));
		_machine.Add(StateType.Die, new BossDieState(_ctx, _machine));
		_machine.Add(StateType.Knockback, new BossKnockbackState(_ctx, _machine));
	}

	public void OnAnimEvent(string animEvent)
	{
		_machine.OnAnimEvent(animEvent);
	}
}
