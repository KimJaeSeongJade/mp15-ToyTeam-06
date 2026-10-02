using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private Animator _animator;

	private PlayerContext _ctx;
	private PlayerInput _input;

	private StateMachine<PlayerContext> _machine;
	private Rigidbody _rigidbody;
	private PlayerStat _stat;

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
	}

	private void BindContext()
	{
		_ctx = new()
		{
			animator = _animator,
			rigidbody = _rigidbody,
			transform = transform,
			input = _input,
			stat = _stat
		};
	}

	private void InitStateMachine()
	{
		_machine = new StateMachine<PlayerContext>();

		_machine.Add(StateType.Idle, new PlayerIdleState(_ctx, _machine));
		_machine.Add(StateType.Move, new PlayerMoveState(_ctx, _machine));
	}
}
