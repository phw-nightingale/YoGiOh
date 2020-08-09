using UnityEngine;

namespace Utility.Singleton
{
    public class MonoSingletonAuto<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    _instance = obj.AddComponent<T>();
                }

                return _instance;
            }
        }
    }
}