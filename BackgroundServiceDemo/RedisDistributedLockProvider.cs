using Medallion.Threading;
using Medallion.Threading.Redis;
using StackExchange.Redis;

namespace BackgroundServiceDemo;

internal class RedisDistributedLockProvider(string connectionString) : IDistributedLockProvider
{
    public IDistributedLock CreateLock(string name)
    {
        var connection = ConnectionMultiplexer.Connect(connectionString);
        return new RedisDistributedLock(name, connection.GetDatabase());
    }
}