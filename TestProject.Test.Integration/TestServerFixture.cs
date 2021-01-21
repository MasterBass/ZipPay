using System;
using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.PlatformAbstractions;

namespace TestProject.Test.Integration
{
    public class TestServerFixture : IDisposable
    {
        private readonly TestServer _testServer;
        public HttpClient Client { get; }

        public TestServerFixture()
        {
            var builder = new WebHostBuilder()
                .UseContentRoot(GetContentRootPath())
                .UseEnvironment("Development")
                .UseStartup<API.Startup>();
            
            _testServer = new TestServer(builder);

            Client = _testServer.CreateClient();
        }

        private string GetContentRootPath()
        {
            string testProjectPath = PlatformServices.Default.Application.ApplicationBasePath;
            var directory = new DirectoryInfo(testProjectPath);
            directory = directory.Parent;
            directory = directory?.Parent;
            directory = directory?.Parent;
            directory = directory?.Parent;

            var path = Path.Combine(directory?.FullName, "TestProject.API");
            return path;
        }
        
        public void Dispose()
        {
            Client.Dispose();
            _testServer.Dispose();
        }
    }
}