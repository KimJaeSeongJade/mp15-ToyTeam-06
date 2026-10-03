using System;

public class ObservableProperty<T>
{
	// 반환값이 없는 제네릭 메서드를 담는다
	private Action<T> onValueChanged;

	private T value; // 제네릭 필드

	public ObservableProperty(T initialValue) // 생성자
	{
		value = initialValue;
	}

	public T Value // 제네릭 프로퍼티
	{
		get => value;

		set
		{
			value = Value;
			Notify();
		}
	}

	public void Notify() // 프로퍼티 안으로 들어가 Action을 동작시킨다
	{
		onValueChanged?.Invoke(value);
	}

	public void AddListener(Action<T> onValue) // 이벤트를 Action에 구독
	{
		onValueChanged += onValue;
	}

	public void RemoveListener(Action<T> onValue) // 이벤트를 Action에 구독 해제
	{
		onValueChanged -= onValue;
	}

	public void RemoveAllListeners(Action<T> onValue) // 모든 이벤트를 Action에서 구독 해제
	{
		onValueChanged -= onValueChanged;
	}
}
