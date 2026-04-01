using System;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class InterfaceRef<T> where T : class
{
	[SerializeField] private Object target;

	public T Value
	{
		get
		{
			if (_value == null)
			{
				_value = target as T;
			}

			return _value;
		}
	}
	
	private T _value;

	public Object Raw => target;
}
