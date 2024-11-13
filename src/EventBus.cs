using Godot;
using System;
using System.Collections.Generic;

public partial class EventBus : Node
{
	private static Dictionary<string, Action<object>> _eventDictionary = new Dictionary<string, Action<object>>();

	public static void Subscribe(string eventName, Action<object> listener)
	{
		if (_eventDictionary.ContainsKey(eventName))
		{
			_eventDictionary[eventName] += listener;
		}
		else
		{
			 _eventDictionary[eventName] = listener;
		}
	}

	public static void Unsubscribe(string eventName, Action<object> listener)
	{
		if (_eventDictionary.ContainsKey(eventName))
		{
			_eventDictionary[eventName] -= listener;

			if (_eventDictionary[eventName] == null)
			{
				_eventDictionary.Remove(eventName);
			}
		}
	}

	public static void Publish(string eventName, object eventData = null)
	{
		if (_eventDictionary.ContainsKey(eventName))
		{
			_eventDictionary[eventName]?.Invoke(eventData);
		}
	}
}
