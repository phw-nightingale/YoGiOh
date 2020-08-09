using UnityEngine;

namespace Utility.Singleton
{
    public abstract class MonoSingletonManual<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static T instance;

        protected abstract void Awake();

        public static T Instance => instance;
    }
}