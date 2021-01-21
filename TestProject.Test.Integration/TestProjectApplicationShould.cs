using System.Threading.Tasks;
using Xunit;

namespace TestProject.Test.Integration
{
    public class TestProjectApplicationShould : IClassFixture<TestServerFixture>
    {
        private readonly TestServerFixture _fixture;

        public TestProjectApplicationShould(TestServerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetUsers()
        {
            
            var response = await _fixture.Client.GetAsync("api/user");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("{\"result\":[],\"totalCount\":0,\"pagingInfo\":{\"pageSize\":10,\"pageNumber\":0,\"sortDir\":null,\"sortKey\":null}}", responseString);
        }
    }
}