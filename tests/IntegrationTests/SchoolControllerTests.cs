using Microsoft.EntityFrameworkCore;
using NetCoreManualDI.Application.School.Dtos;
using NetCoreManualDI.Domain.Core.Students;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace NetCoreManualDI.IntegrationTests
{
    public class SchoolControllerTests: IClassFixture<DbAndHttpFixture>
    {
        private readonly HttpClient httpClient;
        private readonly DbSet<Student> students;

        public SchoolControllerTests(DbAndHttpFixture fixture)
        {
            httpClient = fixture.HttpClient;
            students = fixture.DbContext.Students;
        }

        [Fact]
        public async Task When_Enroll_Student_Then_Success()
        {
            var dto = new RegisterStudentDto("Jose", "Math");

            var request = new HttpRequestMessage(HttpMethod.Post, "school/students");
            request.Content = JsonContent.Create(dto);
            var response = await httpClient.SendAsync(request);

            Assert.True(response.IsSuccessStatusCode);

            var studentFound = await students.SingleOrDefaultAsync(s => s.Name == dto.Name.ToStudentName());
            Assert.NotNull(studentFound);
        }
    }
}