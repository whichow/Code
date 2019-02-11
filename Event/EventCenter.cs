using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCenter : MonoSingleton<EventCenter>
{
    private Dictionary<EventType, List<IEventListener>> _listeners;

    void Awake()
    {
        _Init();
    }

    private void _Init()
    {
        _listeners = new Dictionary<EventType, List<IEventListener>>();
    }

    /// <summary>
    /// 注册事件监听
    /// </summary>
    /// <param name="type">事件类型</param>
    /// <param name="listener">监听者</param>
    public void RegisterEvent(EventType type, IEventListener listener)
    {
        if (_listeners.ContainsKey(type))
        {
            var listeners = _listeners[type];
			if(listeners.Count == 0)
			{
				listeners.Add(listener);
			}
			else
			{
				for (int i = 0; i < listeners.Count; i++)
				{
					if (listener.EventPriority() > listeners[i].EventPriority())
					{
						listeners.Insert(i, listener);
						break;
					}
					else
					{
						if (!listeners.Contains(listener))
							listeners.Add(listener);
					}
				}
			}
        }
        else
        {
            _listeners.Add(type, new List<IEventListener>() { listener });
        }
    }

    /// <summary>
    /// 移除事件监听
    /// </summary>
    /// <param name="type">事件类型</param>
    /// <param name="listener">监听者</param>
    public void RemoveEvent(EventType type, IEventListener listener)
    {
        if (_listeners.ContainsKey(type))
        {
            var listeners = _listeners[type];
            for (int i = 0; i < listeners.Count; i++)
            {
                if (listener == listeners[i])
                {
                    listeners.RemoveAt(i);
                    break;
                }
            }
        }
    }

    /// <summary>
    /// 发送事件
    /// </summary>
    /// <param name="type">事件类型</param>
    /// <param name="data">要发送的数据</param>
    public void SendEvent(EventType type, params object[] data)
    {
        if (_listeners.ContainsKey(type))
        {
            var listeners = _listeners[type];
            for (int i = 0; i < listeners.Count; i++)
            {
                listeners[i].HandleEvent(type, data);
				if(listeners[i].BlockEvent())
				{
					break;
				}
            }
        }
    }
}
