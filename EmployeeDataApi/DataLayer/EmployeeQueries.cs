using Microsoft.AspNetCore.Http.Connections;
using Dapper;
using EmployeeDataApi.Models;
using EmployeeDataApi.Models.Options;
using Microsoft.Data.SqlClient;



namespace EmployeeDataApi.DataLayer;

public interface IEmployeeQueries
{
    Task<List<Employee>> GetAllEmployeesAsync();
    Task<int?> InsertEmployee(EmployeeRequestDto employeeRequestDto);
}

public class EmployeeQueries : IEmployeeQueries
{
    private readonly ILogger<EmployeeQueries> _logger;
    private readonly EmployeeConnectionOptions _connectionOptions;

    public EmployeeQueries(ILogger<EmployeeQueries> logger, EmployeeConnectionOptions connectionOptions)
    {
        _logger = logger;
        _connectionOptions = connectionOptions;
    }

    public async Task<List<Employee>> GetAllEmployeesAsync()
    {
        string query = @"
            SELECT * --normal would specify columns but dapper will only map columns that match model class properties, so skipping for time
            FROM Employees";
        using (SqlConnection conn = new SqlConnection(_connectionOptions.DbConnectionString))
        {
            var gridReader = await conn.QueryMultipleAsync(query);
            //var result = new List<Employee>();
            var result = gridReader.Read<Employee>().ToList();
            return result;

        }
    }

    public async Task<int?> InsertEmployee(EmployeeRequestDto employeeRequestDto)
    {
        string query = @"
        INSERT INTO Employees (FirstName, LastName, Address1, EmployeeType)
        OUTPUT inserted.EmployeeId
        VALUES (@FirstName, @LastName, @Address, @Type)";

        using(SqlConnection conn = new SqlConnection(_connectionOptions.DbConnectionString))
        {

            int newEmployeeId = await conn.QuerySingleAsync<int>(query, new
            {
                "FirstName" = employeeRequestDto.FirstName,
                "LastName" = employeeRequestDto.LastName,
                "Address" = employeeRequestDto.Address,
                "Type" = employeeRequestDto.Type

            });
            return newEmployeeId;
         
        }
    }
}
