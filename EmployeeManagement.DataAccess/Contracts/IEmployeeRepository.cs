using EmployeeManagement.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.DataAccess.Contracts
{
    public interface IEmployeeRepository
    {
        IEnumerable<EmployeeData> GetEmployees();
        EmployeeData GetEmployeeById(int id);
        bool InsertEmployeeData(EmployeeData employeeData);
        bool DeleteEmployee(int id);
        bool UpdateEmployee(EmployeeData employeeData);

    }
}
