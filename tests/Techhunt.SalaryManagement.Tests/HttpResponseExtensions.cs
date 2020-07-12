using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Techhunt.SalaryManagement.Domain;

namespace Techhunt.SalaryManagement.Tests
{
    public static class HttpResponseExtensions
    {
        public static async Task<int> GetResponseItemCount(this HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var employees = JsonConvert.DeserializeObject<List<Employee>>(responseContent);
                return employees.Count;
            }
            return 0;
        }
    }
}
