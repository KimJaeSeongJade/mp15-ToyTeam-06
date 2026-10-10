using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MutantController : MonoBehaviour, IDamageable, ILockonable
{
	[SerializeField] private string _stateType;
	[SerializeField] private Image _lockOnUi;
	[SerializeField] private List<AttackHitBox> _hitBoxes;
	[SerializeField] private GameObject _changeEffectPrefab;

	private MutantAnimationHandler _animHandler;
    private MutantContext _ctx;
    private StateMachine<MutantContext> _machine;
	private MonsterDetection _monsterDetection;
    private MutantStat _stat;

    private void Awake()
    {
        CacheComponents();
        BindContext();
        InitStateMachine();
    }

    private void Start() => _machine.ChangeState(StateType.Idle);

    private void OnEnable()
    {
	    _ctx.isPhase2 = false;
    }

    private void Update()
    {
        _machine?.Tick();
	    RotateUI();
       _stateType = _machine.Current.GetType().ToString();
    }

    private void CacheComponents()
    {
	    _stat = GetComponent<MutantStat>();
        _monsterDetection = GetComponentInChildren<MonsterDetection>();
        _animHandler = GetComponentInChildren<MutantAnimationHandler>();
    }

    private void BindContext()
    {
        _ctx = new MutantContext()
        {
	        transform = transform,
	        animHandler = _animHandler,
	        monsterDetection = _monsterDetection,
	        stat = _stat,
	        hitBoxes = _hitBoxes,
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
        _machine.Add(StateType.Groggy, new MutantGroggyState(_ctx, _machine));
        _machine.Add(StateType.Searching, new MutantSearchingState(_ctx, _machine));
        _machine.Add(StateType.JumpAttack, new MutantJumpAttackState(_ctx, _machine));
    }

    public void TakeDamage(DamageInfo damageInfo)
    {
	    if (_ctx.stat.IsInvincible == true) return;

	    _stat.CurrentHealth.Value -= damageInfo.Damage;
	    _stat.CurrentGroggy.Value -= damageInfo.DownValue;

	    if (_stat.CurrentGroggy.Value <= 0)
	    {
		    _stat.SetFullGroggy();
		    _machine.ChangeState(StateType.Groggy);
	    }
	    if (_stat.CurrentHealth.Value <= 0)
	    {
		    if (!_ctx.isPhase2)
		    {
			    _machine.ChangeState(StateType.PhaseChange);
			    Vector3 position = new Vector3(_ctx.transform.position.x, _ctx.transform.position.y, _ctx.transform.position.z);
			    Instantiate(_changeEffectPrefab, position, Quaternion.identity);

		    }
		    else
		    {
			    _machine.ChangeState(StateType.Die);
		    }
	    }
    }

    public void SetLockOnUi(bool lockOn)
    {
        if (_lockOnUi != null) _lockOnUi.gameObject.SetActive(lockOn);
    }

    private void RotateUI()
    {
        _lockOnUi.rectTransform.LookAt(Camera.main.transform);
    }

    public void OnAnimEvent(string animEvent) => _machine?.OnAnimEvent(animEvent);
}
