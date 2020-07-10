using Microsoft.AspNetCore.Mvc.Testing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Techhunt.SalaryManagement.Api;
using Xunit;

namespace Techhunt.SalaryManagement.Tests
{
    public class UsersUploadTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public UsersUploadTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ValidCsvFilesShouldReturn200()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act             
            using (var csvFile = File.OpenRead(@"CsvFiles\ValidWithOneRecord.csv"))
            using (var fileContent = new StreamContent(csvFile))
            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(fileContent, "file", "ValidWithOneRecord.csv");
                var response = await client.PostAsync("users/upload", formData);
                response.EnsureSuccessStatusCode();
            }
        }
    }
}