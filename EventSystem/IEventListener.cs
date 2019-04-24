using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Event
{
    public interface IEventListener
    {
        /// <summary>
        /// 监听者的优先级
        /// </summary>
        int EventPriority { get; }
        /// <summary>
        /// 是否阻止事件传递
        /// </summary>
        bool BlockEvent { get; }
        /// <summary>
        /// 要监听的事件
        /// </summary>
        string[] ListenEvents { get; }
        /// <summary>
        /// 处理事件
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="eventArgs">事件参数</param>
        void HandEvent(string eventType, params object[] eventArgs);
    }
}
