using System.Reflection;
using Autofac;
using Mmm.Platform.IoT.Common.Services;
using Mmm.Platform.IoT.StorageAdapter.Services;

namespace Mmm.Platform.IoT.StorageAdapter.WebService
{
    public class DependencyResolution : DependencyResolutionBase
    {
        protected override void SetupCustomRules(ContainerBuilder builder)
        {
            var assembly = typeof(StatusService).GetTypeInfo().Assembly;
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces();
        }
    }
}