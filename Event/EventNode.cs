using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventNode : MonoBehaviour, IEventListener
{
    /// <summary>
    /// 处理接收的事件
    /// </summary>
    /// <param name="type">事件类型</param>
    /// <param name="data">接收到的数据</param>
    public virtual void HandleEvent(EventType type, params object[] data)
    {

    }

    /// <summary>
    /// 事件的优先级
    /// </summary>
    /// <returns>优先级，越大优先级越高</returns>
    public virtual int EventPriority()
    {
        return 0;
    }

    /// <summary>
    /// 是否阻止消息传递
    /// </summary>
    /// <returns></returns>
    public virtual bool BlockEvent()
    {
        return false;
    }

    /// <summary>
    /// 要监听的事件
    /// </summary>
    /// <returns>事件数组</returns>
    protected virtual EventType[] ListenEvents()
    {
        return null;
    }

    protected virtual void Awake()
    {
        var types = ListenEvents();
        foreach (var type in types)
        {
            EventCenter.Instance.RegisterEvent(type, this);
        }
    }

    protected virtual void OnDestroy()
    {
		if(EventCenter.Instance == null)
		{
			return;
		}
        var types = ListenEvents();
        foreach (var type in types)
        {
            EventCenter.Instance.RemoveEvent(type, this);
        }
    }
}
