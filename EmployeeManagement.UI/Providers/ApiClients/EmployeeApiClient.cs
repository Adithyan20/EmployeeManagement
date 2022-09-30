using EmployeeManagement.UI.Models;
using EmployeeManagement.UI.Models.Provider;
using EmployeeManagement.UI.Providers.Contracts;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace EmployeeManagement.UI.Providers.ApiClients
{
    public class EmployeeApiClient : IEmployeeApiClient
    {
        private readonly HttpClient _httpClient;

        public EmployeeApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IEnumerable<EmployeeViewModel> GetAllEmployee()
        {
            //Consume /employee endpoint in the EmployeeManagementApi using _httpClient
            using (var response = _httpClient.GetAsync("https://localhost:5001/api/employee/get-all").Result)
            {
                var allEmployees = JsonConvert.DeserializeObject<IEnumerable<EmployeeViewModel>>(response.Content.ReadAsStringAsync().Result);
                return  allEmployees;
            }
        }

        public EmployeeData GetEmployeeById(int employeeId)
        {
            //Consume /{employeeId} endpoint in the EmployeeManagementApi using _httpClient
            using (var response = _httpClient.GetAsync("https://localhost:5001/api/employee/" + employeeId).Result)
            {
                var employeeById = JsonConvert.DeserializeObject<EmployeeData>(response.Content.ReadAsStringAsync().Result);
                return employeeById;
            }
        }
        /*  public EmployeeData InsertEmployeeData(EmployeeData employeeData)
          {
              using (var response = _httpClient.PostAsync("https://localhost:5001/api/employee/manageUsers").Result)
          }*/
    }
}
