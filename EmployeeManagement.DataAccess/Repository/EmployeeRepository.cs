using EmployeeManagement.DataAccess.Contracts;
using EmployeeManagement.DataAccess.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.DataAccess.Repository
{
    /// <summary>
    /// Connect To Database and Perforum CRUD operations
    /// </summary>
    
    public class EmployeeRepository : IEmployeeRepository
    {
        private SqlConnection _sqlConnection;
        public EmployeeRepository()
        {
            _sqlConnection = new SqlConnection("data source = (localdb)\\mssqllocaldb; database= EmployeeManagement;");
        }
        public EmployeeData GetEmployeeById(int id)
        {
            //Take data from Table By Id
            //return null;
            try
            {
                _sqlConnection.Open();
                var sqlCommand = new SqlCommand(cmdText: "exec spGetById @id", _sqlConnection);
                sqlCommand.Parameters.AddWithValue("Id", id);
                var sqlReader = sqlCommand.ExecuteReader();
                var employee = new EmployeeData();
                while (sqlReader.Read())
                {
                    employee.Id = (int)sqlReader["Id"];
                    employee.Name = (string)sqlReader["Name"];
                    employee.Age = (int)sqlReader["Age"];
                    employee.Address = (string)sqlReader["Address"];
                    employee.Department = (string)sqlReader["Department"];
                }
                return employee;
            }
            catch (System.Exception ex)
            {

                System.Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }

        public IEnumerable<EmployeeData> GetEmployees()
        {
            //Take data from Table
            //return null;
            try
            {
                _sqlConnection.Open();
                var sqlCommand = new SqlCommand(cmdText: "exec spGetAll", _sqlConnection);
                var sqlReader = sqlCommand.ExecuteReader();
                var employee = new List<EmployeeData>();
                while (sqlReader.Read())
                {
                    employee.Add(new EmployeeData()
                    {
                        Id = (int)sqlReader["Id"],
                        Name = (string)sqlReader["Name"],
                        Age = (int)sqlReader["Age"],
                        Address = (string)sqlReader["Address"],
                        Department = (string)sqlReader["Department"]
                    });
                }
                return employee;
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                return null;
                
            }
            finally
            {
                _sqlConnection.Close();
            }
        }
        //Create Methods For Table insert, update and Delete Here
        public bool InsertEmployeeData(EmployeeData employeeData)
        {
            try
            {
                _sqlConnection.Open();
                var sqlCommand = new SqlCommand(cmdText: "exec spInsertEmployee @name,@age,@address,@department", _sqlConnection);
                sqlCommand.Parameters.AddWithValue("Name ", employeeData.Name);
                sqlCommand.Parameters.AddWithValue("Age", employeeData.Age);
                sqlCommand.Parameters.AddWithValue("Address", employeeData.Address);
                sqlCommand.Parameters.AddWithValue("Department", employeeData.Department);
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (System.Exception ex)
            {

                System.Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }
        public bool DeleteEmployee(int id)
        {
            try
            {
                _sqlConnection.Open();
                var sqlCommand = new SqlCommand(cmdText: "exec spDeleteEmployee @id", _sqlConnection);
                sqlCommand.Parameters.AddWithValue("Id", id);
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (System.Exception ex)
            {

                System.Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }
        public bool UpdateEmployee(EmployeeData employeeData)
        {
            try
            {
                _sqlConnection.Open();
                var sqlCommand = new SqlCommand(cmdText: "Update EmployeeManagement  set Name = @name,Age = @Age, Address= @Address , Department = @department where Id = @id", _sqlConnection);
                sqlCommand.Parameters.AddWithValue("id", employeeData.Id);
                sqlCommand.Parameters.AddWithValue("name", employeeData.Name);
                sqlCommand.Parameters.AddWithValue("age", employeeData.Age);
                sqlCommand.Parameters.AddWithValue("address", employeeData.Address);
                sqlCommand.Parameters.AddWithValue("department", employeeData.Department);
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (System.Exception ex)
            {

                System.Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }
    }
}
