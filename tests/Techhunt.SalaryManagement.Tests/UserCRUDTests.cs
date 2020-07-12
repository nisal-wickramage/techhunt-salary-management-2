using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Techhunt.SalaryManagement.Api;
using Techhunt.SalaryManagement.Domain;
using Xunit;

namespace Techhunt.SalaryManagement.Tests
{
    public class UserCRUDTests : IClassFixture<SalaryManagementWebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public UserCRUDTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ReadUserValidUserIdShouldReturn200()
        {
            var responseHttpStatus = await ReadUserById("e0001");
            Assert.Equal(HttpStatusCode.OK, responseHttpStatus);
        }

        [Fact]
        public async Task ReadUserInvalidUserIdShouldReturn400()
        {
            var responseHttpStatus = await ReadUserById("e1001");
            Assert.Equal(HttpStatusCode.BadRequest, responseHttpStatus);
        }

        private async Task<HttpStatusCode> ReadUserById(string id)
        {
            var url = $"users/{id}";
            var client = _factory.CreateClient();
            var response = await client.GetAsync(url);
            var responseHttpStatus = response.StatusCode;
            return responseHttpStatus;
        }

        [Fact]
        public async Task DeleteUserValidUserIdShouldReturn200()
        {
            var responseHttpStatus = await DeleteUserById("e0001");
            Assert.Equal(HttpStatusCode.OK, responseHttpStatus);
        }

        [Fact]
        public async Task DeleteUserInvalidUserIdShouldReturn400()
        {
            var responseHttpStatus = await DeleteUserById("e1001");
            Assert.Equal(HttpStatusCode.BadRequest, responseHttpStatus);
        }

        private async Task<HttpStatusCode> DeleteUserById(string id)
        {
            var url = $"users/{id}";
            var client = _factory.CreateClient();
            var response = await client.DeleteAsync(url);
            var responseHttpStatus = response.StatusCode;
            return responseHttpStatus;
        }

        [Theory]
        [InlineData("e0001","john", "john Doe", 2000.00)]
        public async Task UpdateUserValidUserDataShouldReturn200(
            string id, 
            string login, 
            string name, 
            decimal salary)
        {
            var responseHttpStatus = await UpdateUserById(id, login, name, salary);
            Assert.Equal(HttpStatusCode.OK, responseHttpStatus);
        }

        [Theory]
        [InlineData("e0001", "john", "john Doe", 2000.00)]
        public async Task UpdateUserInvalidUserDataShouldReturn400(
            string id,
            string login,
            string name,
            decimal salary)
        {
            var responseHttpStatus = await UpdateUserById(id, login, name, salary);
            Assert.Equal(HttpStatusCode.BadRequest, responseHttpStatus);
        }

        private async Task<HttpStatusCode> UpdateUserById(
            string id, 
            string login, 
            string name, 
            decimal salary)
        {
            var url = $"users/{id}";
            var client = _factory.CreateClient();
            var employee = new Employee { Id = id, Login = login, Name = name, Salary = salary };
            var response = await client.PatchAsync(url, GetStringContent(employee));
            var responseHttpStatus = response.StatusCode;
            return responseHttpStatus;
        }

        [Theory]
        [InlineData("e0001", "john", "john Doe", 2000.00)]
        public async Task CreateUserValidUserDataShouldReturn200(
            string id,
            string login,
            string name,
            decimal salary)
        {
            var responseHttpStatus = await CreateUserById(id, login, name, salary);
            Assert.Equal(HttpStatusCode.OK, responseHttpStatus);
        }

        [Theory]
        [InlineData("e0001", "john", "john Doe", 2000.00)]
        public async Task CreateUserInvalidUserDataShouldReturn400(
            string id,
            string login,
            string name,
            decimal salary)
        {
            var responseHttpStatus = await CreateUserById(id, login, name, salary);
            Assert.Equal(HttpStatusCode.BadRequest, responseHttpStatus);
        }

        private async Task<HttpStatusCode> CreateUserById(
            string id, 
            string login, 
            string name, 
            decimal salary)
        {
            var url = $"users/{id}";
            var client = _factory.CreateClient();
            var employee = new Employee { Id = id, Login = login, Name = name, Salary = salary };
            var response = await client.PostAsync(url, GetStringContent(employee));
            var responseHttpStatus = response.StatusCode;
            return responseHttpStatus;
        }

        private StringContent GetStringContent(Employee employee)
        {
            return new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json");
        }
    }
}
