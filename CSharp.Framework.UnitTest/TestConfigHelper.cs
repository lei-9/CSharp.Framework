using System;
using System.IO;
using CSharp.Framework.Helper;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace CSharp.Framework.UnitTest
{
    public class TestConfigHelper
    {
        [Fact]
        public void TestGet()
        {
            var basePath = Directory.GetCurrentDirectory();
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();

            var result = new UserModel();
            builder.Bind("Users", result);
            var value1 = builder["Value1"];

            value1 = ConfigHelper.Get("Value1");
            result = ConfigHelper.Get<UserModel>("Users");
        }
    }

    public class UserModel
    {
        public int Status { get; set; }

        public string Name { get; set; }
    }
}