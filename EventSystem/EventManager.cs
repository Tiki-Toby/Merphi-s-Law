using System;
using UnityEngine;
using System.Collections.Generic;

namespace HairyEngine.EventSystem
{
	public static class EventManager
	{
		private static Dictionary<Type, List<IEventListener>> _listnersList;

		static EventManager()
		{
			_listnersList = new Dictionary<Type, List<IEventListener>>();
		}
		public static void AddListener<EventData>(IEventListener<EventData> listener) where EventData : struct
		{
			Type eventType = typeof(EventData);

			if (!_listnersList.ContainsKey(eventType))
				_listnersList.Add(eventType, new List<IEventListener>());

			if (SubscriptionPosition(eventType, listener) == -1)
				_listnersList[eventType].Add(listener);
		}

		public static void RemoveListener<EventData>(IEventListener<EventData> listener) where EventData : struct
		{
			Type eventType = typeof(EventData);

			int position = SubscriptionPosition(eventType, listener);

			if(position > -1)
            {
				_listnersList[eventType].RemoveAt(position);
				if (_listnersList[eventType].Count == 0)
					_listnersList.Remove(eventType);
			}
		}

		public static void TriggerEvent<EventData>(EventData newEvent) where EventData : struct
		{
			Type eventType = typeof(EventData);
			if (!_listnersList.ContainsKey(eventType))
				return;

			List<IEventListener<EventData>> executers = _listnersList[eventType] as List<IEventListener<EventData>>;
			foreach(IEventListener<EventData> executer in executers)
				executer.ExecuteEvent(newEvent);
		}

		private static int SubscriptionPosition(Type type, IEventListener checkListner)
		{
			List<IEventListener> listeners;

			if (!_listnersList.TryGetValue(type, out listeners))
				return -1;

			for(int i = 0; i < _listnersList.Count; i++)
				if (checkListner == listeners[i])
					return i;
			return -1;
		}
	}
}