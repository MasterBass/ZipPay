using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
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
        
          
        [Fact]
        public async Task PostUser()
        {
            HttpRequestMessage postRequest = 
                new HttpRequestMessage(HttpMethod.Post, "api/user");

            var obj = new {Email = "abc@2mail.com", MonthlySalary = 1211.50, MonthlyExpenses = 25};
        
            var json = JsonConvert.SerializeObject(obj);
            
            postRequest.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _fixture.Client.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("{\"id\":\"1\",\"email\":\"abc@2mail.com\",\"monthlySalary\":1211.5,\"monthlyExpenses\":25}", responseString);


        }
        
    }
}