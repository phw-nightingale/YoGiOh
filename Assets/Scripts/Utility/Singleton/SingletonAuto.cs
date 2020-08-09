namespace Utility.Singleton
{
    public class SingletonAuto<T> where T : new()
    {
        private static T _instance;

        public static T Instance => _instance == null ? new T() : _instance;
    }
}