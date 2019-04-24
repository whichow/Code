using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Event
{
    public class EventSystem
    {
        public EventSystem()
        {

        }

        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="listener">事件监听者</param>
        public void RegisterEvent(IEventListener listener)
        {

        }

        /// <summary>
        /// 发送事件，并移除已销毁的监听者
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="eventArgs">事件数据</param>
        public void SendEvent(string eventType, params object[] eventArgs)
        {

        }
    }
}
