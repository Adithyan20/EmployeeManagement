using EmployeeManagement.API.Models;
using EmployeeManagement.Application.Contracts;
using EmployeeManagement.Application.Models;
using EmployeeManagement.DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.API.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeApiController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeApiController(IEmployeeService employeeService)
        {
            this._employeeService = employeeService;
        }

        [HttpGet]
        [Route("{employeeId}")]
        public IActionResult GetEmployeeById([FromRoute] int employeeId)
        {
            try
            {
                /// get employee by calling GetEmployeeById() in IEmployeeService and store it in a variable and Map that variable to EmployeeDetailedViewModel.              
                var employeeById = _employeeService.GetEmployeeById(employeeId);              
                return Ok(MapToGet(employeeById));
            }
            /*catch(ArgumentNullException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            catch(ArgumentException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }*/
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        private object MapToGet(EmployeeDto employeeById)
        {
            var employee = new EmployeeDetailedViewModel
            {
                Id = employeeById.Id,
                Name = employeeById.Name,
                Age = employeeById.Age,
                Address = employeeById.Address,
                Department = employeeById.Department
            };
            return employee;
        }

        [HttpPost]
        [Route("insertUsers")]
        public IActionResult InsertEmployeeData([FromBody] EmployeeDetailedViewModel employee )
        {
            try
            {
                var employeeInsert = _employeeService.InsertEmployeeData(MapInsertDto(employee));
                if (employeeInsert)
                {
                    return Ok (employeeInsert);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        private EmployeeDto MapInsertDto(EmployeeDetailedViewModel employee)
        {
            var employees = new EmployeeDto()
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                Address = employee.Address,
                Department = employee.Department
            };
            return employees;
        }

        [HttpGet]
        [Route("get-all")]
        public IActionResult GetEmployees()
        {
            /// get employees by calling GetEmployees() in IEmployeeService and store it in a variable and Map that variable to EmployeeDetailedViewModel. 
            /// 
            try
            {
                var employeeAll = _employeeService.GetEmployees();
                return Ok(MapForView(employeeAll));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

        private object MapForView(IEnumerable<EmployeeDto> employeeAll)
        {
            var employee = new List<EmployeeDetailedViewModel>();
            foreach (var item in employeeAll)
            {
                var employeeData = new EmployeeDetailedViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Age = item.Age,
                    Address = item.Address,
                    Department = item.Department
                };
                employee.Add(employeeData);

            }
            return employee;
        }

        [HttpPut]
        [Route("manageUsers")]
        public IActionResult UpdateEmployee([FromBody]EmployeeDetailedViewModel employee)
        {
            try
            {
                var employeeUpdated = _employeeService.UpdateEmployee(MapToUpdateDto(employee));
                if (employeeUpdated)
                {
                    return Ok(employeeUpdated);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        private EmployeeDto MapToUpdateDto(EmployeeDetailedViewModel employee)
        {
            var employeeUpdate= new EmployeeDto()
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                Address = employee.Address,
                Department = employee.Department
            };
            return employeeUpdate;
        }

        
        [HttpDelete]
        [Route("manageUsers/{id}")]
        public bool DeleteEmployee(int id)
        {
            var employeeDelete = _employeeService.DeleteEmployee(id);
            return (employeeDelete);
        }

    /*    public void ValidateEmployee(int employeeId)
        {
            if (employeeId < 0)
            {
                throw new ArgumentException("Enter a valid Id");
            }
        }
        public void ValidateEmployee(EmployeeDto employeeDto)
        {
            if (employeeDto == null)
            {
                throw new ArgumentNullException("Employee not found");
            }
        }*/


        //Create Employee Insert, Update and Delete Endpoint here
    }
}
