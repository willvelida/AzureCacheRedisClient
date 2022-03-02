using AzureCacheRedisClient;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AzureCacheRedisClientTests
{
    [TestClass]
    public class RedisCacheTests
    {
        private readonly IConfiguration _configuration;

        public RedisCacheTests() => _configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile(@"appsettings.json", false, false)
               .AddJsonFile(@"appsettings.Development.json", true, false)
               .Build();

        [TestMethod]
        public async Task UsageTest()
        {
            var cache = new RedisCache(_configuration["AzureCacheRedisConnectionString"]);

            var item = new TestItem { Name = "foo", Value = "bar" };

            await cache.Set("foobar1", item, TimeSpan.FromSeconds(1));
            var cachedItem = await cache.Get<TestItem>("foobar1");

            Assert.AreEqual(item, cachedItem);
        }
    }

    public class TestItem
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public override int GetHashCode() => HashCode.Combine(Name, Value);

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            return obj.GetHashCode() == GetHashCode();
        }
    }
}