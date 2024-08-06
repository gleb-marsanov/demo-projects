namespace Services
{
    public class DiContainer
    {
        private static DiContainer _instance;
        public static DiContainer Container => _instance ??= new DiContainer();

        public void Bind<TService>(TService implementation) where TService : IService =>
            Implementation<TService>.ServiceInstance = implementation;

        public TService Resolve<TService>() where TService : IService =>
            Implementation<TService>.ServiceInstance;

        private class Implementation<TService> where TService : IService
        {
            public static TService ServiceInstance;
        }
    }
}