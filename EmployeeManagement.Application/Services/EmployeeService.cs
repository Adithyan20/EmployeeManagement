using EmployeeManagement.Application.Contracts;
using EmployeeManagement.Application.Models;
using EmployeeManagement.DataAccess.Contracts;
using EmployeeManagement.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public EmployeeDto GetEmployeeById(int id)
        {
            var employeeById = _employeeRepository.GetEmployeeById(id);
            return MapToEmployee(employeeById);
        }
        public IEnumerable<EmployeeDto> GetEmployees()
        {
            //Get data from Repository
            //return null;
            var employee = _employeeRepository.GetEmployees();
            return MapToEmployeeDto(employee);
        }

        public bool InsertEmployeeData(EmployeeDto employee)
        {
            var employees = _employeeRepository.InsertEmployeeData(MapInsertToData(employee));
            return (employees);
        }
        public bool DeleteEmployee(int id)
        {
            var employee = _employeeRepository.DeleteEmployee(id);
            return (employee);
        }
        public bool UpdateEmployee(EmployeeDto employeeDto)
        {
            var employee = _employeeRepository.UpdateEmployee(MapUpdateToData(employeeDto));
            return (employee);
        }

        private EmployeeData MapUpdateToData(EmployeeDto employeeDto)
        {
            var updateDto = new EmployeeData
            {
                Id = employeeDto.Id,
                Name = employeeDto.Name,
                Age = employeeDto.Age,
                Department = employeeDto.Department
            };
            return updateDto;
        }
        private IEnumerable<EmployeeDto> MapToEmployeeDto(IEnumerable<EmployeeData> employee)
        {
            var employeeDtoList = new List<EmployeeDto>();
            foreach (var item in employee)
            {
                var employeeDto = new EmployeeDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Age = item.Age,
                    Address = item.Address,
                    Department = item.Department
                };
                employeeDtoList.Add(employeeDto);
            }
            return employeeDtoList;

        }
        private EmployeeData MapInsertToData(EmployeeDto employee)
        {
            var employeeData = new EmployeeData()
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                Address = employee.Address,
                Department = employee.Department
            };
            return employeeData;
        }
        private EmployeeDto MapToEmployee(EmployeeData employeeById)
        {
            return new EmployeeDto
            {
                Id = employeeById.Id,
                Name = employeeById.Name,
                Age = employeeById.Age,
                Address = employeeById.Address,
                Department = employeeById.Department
            };
        }
       
    }
}
