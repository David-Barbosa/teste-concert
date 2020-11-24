namespace Concert.Shared.Singletons
{
    public abstract class Singleton<T> where T : Singleton<T>, new()
    {
        private static readonly T _instance;
        public static T Instance => _instance ?? new T();
    }
}
