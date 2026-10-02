using UnityEngine;

public class MonsterController : MonoBehaviour
{
	private MonsterAnimationHandler _animHandler;
	private MonsterContext _ctx;
	private StateMachine<MonsterContext> _machine;
	private MonsterStat _stat;
	private bool _isPlayerEnter;
	private SphereCollider _sphereCollider;
	public LayerMask TileLayer;

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
		_sphereCollider =  GetComponent<SphereCollider>();
	}

	private void BindContext()
	{
		_ctx = new()
		{
			transform = transform,
			animHandler = _animHandler,
			stat = _stat,
			isPlayerEnter = _isPlayerEnter
		};
	}
	void OnTriggerEnter(Collider other)
	{
		if (!(other.gameObject.layer == LayerMask.NameToLayer("Player"))) return;

		_isPlayerEnter = true;
	}

	private void InitStateMachine()
	{
		_machine = new StateMachine<MonsterContext>();

		_machine.Add(StateType.Idle, new MonsterIdleState(_ctx, _machine));
	}
}
