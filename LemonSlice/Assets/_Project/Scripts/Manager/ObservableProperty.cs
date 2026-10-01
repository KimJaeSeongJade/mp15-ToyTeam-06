using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObservableProperty<T>
{
	// 반환값이 없는 제네릭 메서드를 담는다
	private Action<T> onValueChanged;

	private T value; // 제네릭 필드

	public T Value // 제네릭 프로퍼티
	{
		get => value;

		set { value = Value; }
	}

	public ObservableProperty(T initialValue) // 생성자
	{
		value = initialValue;
	}

	
}
