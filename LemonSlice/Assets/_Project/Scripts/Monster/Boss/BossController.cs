using UnityEngine;

public class BossController : MonoBehaviour
{
	private BossAnimationHandler _animHandler;
	private BossContext _ctx;
	private StateMachine<BossContext> _machine;
	private BossStat _stat;

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
		_machine.Tick();
	}

	// --------------------------------

	private void CacheComponets()
	{
	}

	private void BindContext()
	{
		_ctx = new()
		{
			transform = transform,
			animHandler = _animHandler,
			stat = _stat,
		};
	}

	private void InitStateMachine()
	{
		_machine = new StateMachine<BossContext>();

		_machine.Add(StateType.Idle, new BossIdleState(_ctx, _machine));
	}

	public void OnAnimEvent(string animEvent)
	{
		_machine.OnAnimEvent(animEvent);
	}
}
