using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GenericPubSub.PubSub
{
    public static class PubSub
    {
        
        
        private static Dictionary<Type, List<Action<object>>> listeners = new Dictionary<Type, List<Action<object>>>();
        private static Dictionary<string, List<Action<Dictionary<string, dynamic>>>> namedListeners = new Dictionary<string, List<Action<Dictionary<string, dynamic>>>>();

        public static void RegisterListener<T>(Action<object> listener) where T : class
        {
            if (listeners.ContainsKey(typeof(T)) == false)
                listeners.Add(typeof(T), new List<Action<object>>());
            listeners[typeof(T)].Add(listener);
        }
        
        public static void RegisterListener(Action<Dictionary<string, dynamic>> listener, string eventTypeName)
        {
            if (namedListeners.ContainsKey(eventTypeName) == false)
                namedListeners.Add(eventTypeName, new List<Action<Dictionary<string, dynamic>>>());
            namedListeners[eventTypeName].Add(listener);
        }

        public static void Publish<T>(T publishedEvent) where T : class
        {
            if (listeners.ContainsKey(typeof(T)))
            {
                foreach (var action in listeners[typeof(T)])
                {
                    action.Invoke(publishedEvent);
                }
            }
            
        }
    }
}