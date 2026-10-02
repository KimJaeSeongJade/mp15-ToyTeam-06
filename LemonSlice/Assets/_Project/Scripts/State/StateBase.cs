public abstract class StateBase<T> where T : IContext
{
	public T _ctx;
	public StateMachine<T> _fsm;

	public StateBase()
	{
	}

	public StateBase(T context, StateMachine<T> stateMachine)
	{
		_ctx = context;
		_fsm = stateMachine;
	}

	public virtual void Enter()
	{
	}

	public virtual void Tick()
	{
	}

	public virtual void FixedTick()
	{
	}

	public virtual void Exit()
	{
	}

	public virtual void OnAnimEvent(string animEvent)
	{
	}
}
