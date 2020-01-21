using Autofac;

namespace Mmm.Platform.IoT.Common.Services
{
    public class Factory : IFactory
    {
        private static IContainer container;

        public static void RegisterContainer(IContainer c)
        {
            container = c;
        }

        public T Resolve<T>()
        {
            return container.Resolve<T>();
        }
    }
}