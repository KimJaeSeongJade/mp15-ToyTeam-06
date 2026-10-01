public abstract class StateBase<T> where T : IContext
{
	public T _ctx;
	public StateMachine _fsm;

	public StateBase()
	{
	}

	public StateBase(T context, StateMachine stateMachine)
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
}
