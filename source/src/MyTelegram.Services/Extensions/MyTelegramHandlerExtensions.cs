using Microsoft.Extensions.DependencyInjection;
using MyTelegram.Services.Services;
using System.Reflection;

namespace MyTelegram.Services.Extensions
{
    public static class MyTelegramHandlerExtensions
    {
        public static IServiceCollection AddMyTelegramHandlerServices(this IServiceCollection services)
        {
            services.RegisterServices();

            services.AddSingleton(typeof(IInMemoryRepository<,>), typeof(InMemoryRepository<,>));
            services.AddTransient(typeof(IDataProcessor<>), typeof(DefaultDataProcessor<>));
            services.AddSingleton(typeof(IMessageQueueProcessor<>), typeof(MessageQueueProcessor2<>));
            services.AddTransient<IDataProcessor<ISessionMessage>, SessionMessageDataProcessor>();

            services.AddSystemTextJson();

            services.AddSingleton(typeof(ICacheHelper<,>),typeof(CacheHelper<,>));
            services.AddTransient<IHashCalculator, HashCalculator>();
            services.AddSingleton(typeof(IQueuedCommandExecutor<,,>), typeof(QueuedCommandExecutor<,,>));

            return services;
        }

        public static IServiceCollection RegisterHandlers(this IServiceCollection services, Assembly handlerImplTypeInThisAssembly)
        {
            var baseType = typeof(IObjectHandler);
            //var baseInterface = typeof(IProcessedHandler);
            var types = handlerImplTypeInThisAssembly.DefinedTypes
                .Where(p => baseType.IsAssignableFrom(p) /*&& baseInterface.IsAssignableFrom(p)*/ && !p.IsAbstract)
                .ToList();
            foreach (var typeInfo in types)
            {
                services.AddTransient(typeInfo);
            }

            return services;
        }
    }
}
