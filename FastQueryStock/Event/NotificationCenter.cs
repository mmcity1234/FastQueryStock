using FastQueryStock.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastQueryStock.Event
{
    /// <summary>
    /// 事件通知中心
    /// </summary>
    public class NotificationCenter
    {
        private static readonly NotificationCenter _singletonNotificationCenter = new NotificationCenter();
        private Dictionary<EventType, List<object>> eventDict = new Dictionary<EventType, List<object>>();

        static NotificationCenter()
        {           
        }

        public static NotificationCenter Instance
        {
            get
            {
                return _singletonNotificationCenter;
            }
        }

        /// <summary>
        /// Register the event and start to listen notification
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventType"></param>
        /// <param name="eventHandler"></param>
        public void RegisterEvent<T>(EventType eventType, Action<T> eventHandler) where T : new()
        {
            List<object> eventList;
            if (!eventDict.TryGetValue(eventType, out eventList))
            {
                eventList = new List<object>();
                eventDict.Add(eventType, eventList);
            }
          
            eventList.Add(eventHandler);
        }

        /// <summary>
        /// Send the notification to the event handler
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventType"></param>
        /// <param name="model"></param>
        public void NotifyEvent<T>(EventType eventType, T model) where T : class
        {
            List<object> eventList;
            if (eventDict.TryGetValue(eventType, out eventList))
            {
                foreach (Action<T> action in eventList)
                {
                    action(model);
                }
            }
        }

        public void RemoveEvent<T>(EventType eventType, Action<T> removeAction)
        {
            List<object> eventList;
            if (eventDict.TryGetValue(eventType, out eventList))
            {
                eventList.Remove(removeAction);
            }
        }
    }
}
