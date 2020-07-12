using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Web;
using Techhunt.SalaryManagement.Api;
using Techhunt.SalaryManagement.Domain;
using Techhunt.SalaryManagement.Infrastructure.Persistance;
using Xunit;

namespace Techhunt.SalaryManagement.Tests
{
    public class UserDashboardTests : IClassFixture<SalaryManagementWebApplicationFactory<Startup>>
    {
        private readonly SalaryManagementWebApplicationFactory<Startup> _factory;

        public UserDashboardTests(SalaryManagementWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData(100.0, 6000.00,0,30,"+name")]
        public async Task ValidParametersShouldReturn200(
            decimal minSalary,
            decimal maxSalary,
            int offset,
            int limit,
            string sort)
        {
            var response = await GetUserDashboardResultCode(minSalary, maxSalary, offset, limit, sort);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(16, await response.GetResponseItemCount());
        }

        [Theory]
        [InlineData(100.0, 6000.00, 0, 30, "+namew")]
        public async Task InvalidParametersShouldReturn400(
            decimal minSalary,
            decimal maxSalary,
            int offset,
            int limit,
            string sort)
        {
            var response = await GetUserDashboardResultCode(minSalary, maxSalary, offset,limit, sort);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal(0, await response.GetResponseItemCount());
        }

        private async Task<HttpResponseMessage> GetUserDashboardResultCode(
            decimal minSalary,
            decimal maxSalary,
            long offset,
            long limit,
            string sort)
        {
            var url = $"users?minSalary={minSalary}&maxSalary={maxSalary}&offset={offset}&limit={limit}&sort={sort}";
            var client = _factory.CreateClient();
            var response = await client.GetAsync(url);

            return response;
        }
    }
}
