using UnityEngine;

public class MonsterController : MonoBehaviour
{
	public LayerMask TargetLayer;
	private MonsterAnimationHandler _animHandler;
	private MonsterContext _ctx;
	private StateMachine<MonsterContext> _machine;
	private SphereCollider _sphereCollider; // Trigger Collider Player가 들어왔는지 판별용
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

	private void OnTriggerEnter(Collider other)
	{
		int layer = (1 << other.gameObject.layer);

		if ((TargetLayer.value & layer) != 0)
		{
			_ctx.isPlayerEnter = true;
			_ctx.targetTransform = other.transform;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		int layer = (1 << other.gameObject.layer);
		if ((TargetLayer.value & layer) !=0)
		{
			_ctx.isPlayerEnter = false;
		}
	}

	// --------------------------------

	private void CacheComponents()
	{
		_sphereCollider = GetComponent<SphereCollider>();
		_stat = GetComponent<MonsterStat>();
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
		_machine = new StateMachine<MonsterContext>();

		_machine.Add(StateType.Idle, new MonsterIdleState(_ctx, _machine));
		_machine.Add(StateType.Move, new MonsterChaseState(_ctx, _machine));
	}
}
