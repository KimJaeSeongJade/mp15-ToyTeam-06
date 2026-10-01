using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private PlayerContext _ctx;
	private PlayerInput _input;

	private StateMachine<PlayerContext> _machine;
	private PlayerBase _playerBase;
	private Rigidbody _rigidbody;

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
		_playerBase = GetComponent<PlayerBase>();
	}

	private void BindContext()
	{
		_ctx = new()
		{
			rigidbody = _rigidbody,
			transform = transform,
			input = _input,
			stat = _playerBase
		};
	}

	private void InitStateMachine()
	{
		_machine = new StateMachine<PlayerContext>();

		_machine.Add(StateType.Idle, new PlayerIdleState(_ctx, _machine));
		_machine.Add(StateType.Move, new PlayerMoveState(_ctx, _machine));
	}
}
