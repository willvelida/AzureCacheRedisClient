# Azure Cache for Redis Client

Azure Cache for Redis client for .NET 6.0.

## Getting started

> For a detailed example see [RedisCacheTests.cs](src\AzureCacheRedisClientTests\RedisCacheTests.cs).

```csharp
var cache = new RedisCache(connectionString);

var item = new TestItem { Name = "foo", Value = "bar" };

// Set cache item
await cache.Set("foobar1", item, TimeSpan.FromSeconds(1));

// Get cached item
var cachedItem = await cache.Get<TestItem>("foobar1");
```
