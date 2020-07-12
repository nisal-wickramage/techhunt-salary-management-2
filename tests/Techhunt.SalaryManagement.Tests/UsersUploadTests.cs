using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Techhunt.SalaryManagement.Api;
using Xunit;

namespace Techhunt.SalaryManagement.Tests
{
    public class UsersUploadTests : IClassFixture<SalaryManagementWebApplicationFactory<Startup>>
    {
        private readonly SalaryManagementWebApplicationFactory<Startup> _factory;

        public UsersUploadTests(SalaryManagementWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("valid-small.csv")]
        [InlineData("valid-with-comments.csv")]
        [InlineData("valid-with-non-english.csv")]
        public async Task ValidCsvFilesShouldReturn200(string fileName)
        {
            var responseHttpStatus = await GetUploadUsersResultCode(fileName);

            Assert.Equal(HttpStatusCode.OK, responseHttpStatus);
        }

        [Theory]
        [InlineData("invalid-duplicate-ids.csv")]
        [InlineData("invalid-duplicate-logins.csv")]
        [InlineData("invalid-missing-column.csv")]
        [InlineData("invalid-missing-column-header.csv")]
        [InlineData("invalid-no-headers.csv")]
        [InlineData("invalid-empty.csv")]
        [InlineData("invalid-headers-only.csv")]
        public async Task InvalidCsvFilesShouldReturn400(string fileName)
        {
            var responseHttpStatus = await GetUploadUsersResultCode(fileName);
            Assert.Equal(HttpStatusCode.BadRequest, responseHttpStatus);
        }

        private async Task<HttpStatusCode> GetUploadUsersResultCode(string fileName)
        {
            var client = _factory.CreateClient();

            var path = Path.Combine("CsvFiles", fileName);
            using (var csvFile = File.OpenRead(path))
            using (var fileContent = new StreamContent(csvFile))
            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(fileContent, "file", "ValidWithOneRecord.csv");
                var response = await client.PostAsync("users/upload", formData);
                var responseHttpStatus = response.StatusCode;
                return responseHttpStatus;
            }
        }

        [Fact]
        public async Task MultipleCsvFilesShouldReturn400()
        {
            var client = _factory.CreateClient();
            var path = Path.Combine("CsvFiles", "valid-small.csv");
            using (var csvFile = File.OpenRead(path))
            using (var fileContent = new StreamContent(csvFile))
            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(fileContent, "file", "ValidWithOneRecord.csv");
                formData.Add(fileContent, "file2", "ValidWithOneRecord2.csv");
                var response = await client.PostAsync("users/upload", formData);
                var responseHttpStatus = response.StatusCode;
                Assert.Equal(HttpStatusCode.BadRequest, responseHttpStatus);
            }
        }
    }
}