﻿using Rebus.Config;

Console.Title = "MyTelegram auth server";
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Async(c => c.Console(theme: AnsiConsoleTheme.Code))
    .WriteTo.Async(c => c.File("Logs/startup-log.txt"))
    .CreateLogger();

Log.Information(
    "{Info} {Version}",
    "MyTelegram Auth Server",
    typeof(Program).Assembly.GetName().Version
);
Log.Information(
    "{Description} {Url}",
    "For more information, please visit",
    MyTelegramServerDomainConsts.RepositoryUrl
);

Log.Information("MyTelegram authentication server starting...");

//Console.ReadLine();
var builder = Host.CreateDefaultBuilder(args);
builder.ConfigureAppConfiguration(options =>
{
    options.AddEnvironmentVariables();
    options.AddCommandLine(args);
});

builder.UseSerilog(
    (context, configuration) => { configuration.ReadFrom.Configuration(context.Configuration); }
);
builder.ConfigureServices(
    (context, services) =>
    {
        services.Configure<MyTelegramAuthServerOptions>(
            context.Configuration.GetRequiredSection("App")
        );
        services.Configure<EventBusRabbitMqOptions>(
            context.Configuration.GetRequiredSection("RabbitMQ:EventBus")
        );
        services.Configure<RabbitMqOptions>(
            context.Configuration.GetRequiredSection("RabbitMQ:Connections:Default")
        );
        services.AddHostedService<MyTelegramAuthServerBackgroundService>();
        services.AddAuthServer();
        services.AddMyTelegramStackExchangeRedisCache(options =>
        {
            options.Configuration = context.Configuration.GetValue<string>("Redis:Configuration");
        });
        services.AddCacheJsonSerializer(options =>
        {
            options.TypeInfoResolverChain.Add(MyJsonSerializeContext.Default);
        });

        services.AddRebusEventBus(options =>
        {
            var eventBusOptions = context
                .Configuration.GetRequiredSection("RabbitMQ:EventBus")
                .Get<EventBusRabbitMqOptions>();
            var rabbitMqOptions = context
                .Configuration.GetRequiredSection("RabbitMQ:Connections:Default")
                .Get<RabbitMqOptions>();

            options.Transport(t =>
            {
                t.UseRabbitMq(
                        $"amqp://{rabbitMqOptions!.UserName}:{rabbitMqOptions.Password}@{rabbitMqOptions.HostName}:{rabbitMqOptions.Port}",
                        eventBusOptions!.ClientName
                    )
                    .ExchangeNames(
                        eventBusOptions.ExchangeName,
                        eventBusOptions.TopicExchangeName ?? "RebusTopics"
                    );
            });
            options.AddSystemTextJson(jsonOptions =>
            {
                jsonOptions.TypeInfoResolverChain.Add(MyJsonSerializeContext.Default);
            });
        });
    }
);

var app = builder.Build();

var eventBus = app.Services.GetRequiredService<IEventBus>();
eventBus.ConfigureEventBus();

await app.RunAsync();