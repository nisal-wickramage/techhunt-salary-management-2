using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Threading.Tasks;
using Techhunt.SalaryManagement.Api;
using Techhunt.SalaryManagement.Infrastructure.Persistance;
using Xunit;

namespace Techhunt.SalaryManagement.Tests
{
    public class UserDashboardTests : IClassFixture<SalaryManagementWebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public UserDashboardTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData(100.0, 6000.00,30,30,"+name")]
        public async Task ValidParametersShouldReturn200(
            decimal minSalary,
            decimal maxSalary,
            long offset,
            long limit,
            string sort)
        {
            var responseHttpStatus = await GetUserDashboardResultCode(minSalary, maxSalary, offset, limit, sort);
            Assert.Equal(HttpStatusCode.OK, responseHttpStatus);
        }

        [Theory]
        [InlineData(100.0, 6000.00, 30, 30, "+namew")]
        public async Task InvalidParametersShouldReturn400(
            decimal minSalary,
            decimal maxSalary,
            long offset,
            long limit,
            string sort)
        {
            var responseHttpStatus = await GetUserDashboardResultCode(minSalary, maxSalary, offset,limit, sort);
            Assert.Equal(HttpStatusCode.BadRequest, responseHttpStatus);
        }

        private async Task<HttpStatusCode> GetUserDashboardResultCode(
            decimal minSalary,
            decimal maxSalary,
            long offset,
            long limit,
            string sort)
        {
            var url = $"users?minSalary={minSalary}&maxSalary={maxSalary}&offset{offset}&limit{limit}&sort{sort}";
            var client = _factory.CreateClient();
            var service = _factory.Server.Host.Services.GetService(typeof(SalaryManagementDbContext));
            var response = await client.GetAsync(url);
            var responseHttpStatus = response.StatusCode;
            return responseHttpStatus;
        }
    }
}
