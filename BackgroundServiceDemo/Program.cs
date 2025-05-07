using BackgroundServiceDemo;
using Medallion.Threading;

var builder = Host.CreateApplicationBuilder(args);

var redisConnectionString = builder.Configuration.GetConnectionString("Redis");

if (string.IsNullOrWhiteSpace(redisConnectionString)) throw new Exception("Redis connection string is required");

Console.WriteLine($"Using Redis connection string: {redisConnectionString}");

builder.Services.AddSingleton<IDistributedLockProvider>(sp => new RedisDistributedLockProvider(redisConnectionString));

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
await host.RunAsync();
return 0;