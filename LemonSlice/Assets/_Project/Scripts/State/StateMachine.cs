using System.Collections.Generic;

public class StateMachine<T> where T : IContext
{
	private StateBase<T> _current;
	private Dictionary<StateType, StateBase<T>> _stateDict;

	public StateMachine()
	{
		_stateDict = new();
	}

	public void ChangeState(StateType stateType)
	{
		StateBase<T> next = _stateDict[stateType];

		if (_current == next)
		{
			return;
		}

		_current?.Exit();
		_current = next;
		_current?.Enter();
	}

	public bool Add(StateType stateType, StateBase<T> state)
	{
		return _stateDict.TryAdd(stateType, state);
	}

	public void Tick() => _current?.Tick();
	public void FixedTick() => _current?.FixedTick();
	public void OnAnimEvent(string animEvent) => _current?.OnAnimEvent(animEvent);
}
