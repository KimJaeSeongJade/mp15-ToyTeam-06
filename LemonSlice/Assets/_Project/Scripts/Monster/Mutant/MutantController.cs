using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MutantController : MonoBehaviour, IDamageable, ILockonable
{
	[SerializeField] private string _stateType;
	[SerializeField] private Image _lockOnUi;
	[SerializeField] private List<AttackHitBox> _hitBoxes;

	public int CurrentPhase { get; private set; } = 1;

	private MutantAnimationHandler _animHandler;
    private MutantContext _ctx;
    private StateMachine<MutantContext> _machine;
	private MonsterDetection _monsterDetection;
    private BossStat _stat;

    private void Awake()
    {
        CacheComponents();
        BindContext();
        InitStateMachine();
    }

    private void Start()
    {
	    _machine.ChangeState(StateType.Idle);
    }

    private void OnEnable()
    {
        CurrentPhase = 1;
    }

    private void Update()
    {
        _machine?.Tick();
        RotateUI();
        _stateType = _machine.Current.GetType().ToString();
    }

    private void CacheComponents()
    {
	    _stat = GetComponent<BossStat>();
        _monsterDetection = GetComponentInChildren<MonsterDetection>();
        _animHandler = GetComponentInChildren<MutantAnimationHandler>();
    }

    private void BindContext()
    {
        List<AttackHitBox> hitBoxes = new List<AttackHitBox>(GetComponentsInChildren<AttackHitBox>());

        _ctx = new ()
        {
	        transform = transform,
	        animHandler = _animHandler,
	        monsterDetection = _monsterDetection,
	        stat = _stat,
	        hitBoxes = _hitBoxes,
	        attackIndex = 0,
	        isInAttackRange = false
        };
    }

    private void InitStateMachine()
    {
        _machine = new StateMachine<MutantContext>();

        _machine.Add(StateType.Idle, new MutantIdleState(_ctx, _machine));
        _machine.Add(StateType.Move, new MutantChaseState(_ctx, _machine));
        _machine.Add(StateType.Attack, new MutantAttackState(_ctx, _machine));
        _machine.Add(StateType.PhaseChange, new MutantPhaseChangeState(_ctx, _machine));
        _machine.Add(StateType.Die, new MutantDieState(_ctx, _machine));
    }

    public void TakeDamage(DamageInfo damageInfo)
    {
	    if (_stat == null) return;

	    if (_stat.IsInvincible) return;

	    _stat.CurrentHealth.Value -= damageInfo.Damage;
	    _stat.CurrentGroggy.Value -= damageInfo.DownValue;

	    if (_stat.CurrentHealth.Value <= 0)
	    {
		    if (CurrentPhase == 1)
		    {
			    _machine.ChangeState(StateType.PhaseChange);
		    }
		    else if (CurrentPhase == 2)
		    {
			    _machine.ChangeState(StateType.Die);
		    }
		    return;
	    }

	    if (_stat.CurrentGroggy.Value <= 0)
	    {
		    _stat.SetFullGroggy();
		    _machine.ChangeState(StateType.Groggy);
	    }
    }



    public void SetPhase(int phase)
    {
        CurrentPhase = phase;
    }


    public void SetLockOnUi(bool lockOn)
    {
        if (_lockOnUi != null) _lockOnUi.gameObject.SetActive(lockOn);
    }

    private void RotateUI()
    {
        if (_lockOnUi != null && _lockOnUi.gameObject.activeSelf)
        {
            _lockOnUi.rectTransform.LookAt(Camera.main.transform);
        }
    }
    public void OnAnimEvent(string animEvent) => _machine?.OnAnimEvent(animEvent);

}
