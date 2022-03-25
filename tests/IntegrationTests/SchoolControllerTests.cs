using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace NetCoreManualDI.IntegrationTests
{
    public class SchoolControllerTests
    {
        [Fact]
        public async Task When_Enroll_Student_Then_Success()
        {
            using var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:5130");

            var request = new HttpRequestMessage(HttpMethod.Post, "School?studentName=Otto&courseName=Math");
            var response = await httpClient.SendAsync(request);
            Assert.True(response.IsSuccessStatusCode);
        }
    }
}