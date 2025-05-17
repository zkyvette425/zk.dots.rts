using UnityEngine;

namespace RTS.Runtime.Manager
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T _instance;

        public static T Instance => _instance;

        protected virtual void Awake()
        {
            _instance = this as T;
        }
    }
}