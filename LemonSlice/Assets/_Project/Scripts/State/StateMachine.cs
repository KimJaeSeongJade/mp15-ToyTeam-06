using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T> where T : IContext
{
	private StateBase<T> _current;
	private Dictionary<StateType, StateBase<T>> _stateDict;

	public StateMachine()
	{
		_stateDict = new();
	}

	public StateBase<T> Current => _current;

	public void ChangeState(StateType stateType)
	{
		StateBase<T> next = _stateDict[stateType];

		if (_current == next)
		{
			return;
		}

		if (_current != null)
		{
			// TODO 현재상태 로그 비활성화
			// Debug.Log($"{_current.GetType().Name} => {next.GetType().Name}");
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
