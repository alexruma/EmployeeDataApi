using EmployeeDataApi.DataLayer;
using EmployeeDataApi.Models;
using EmployeeDataApi.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeDataApi.Controllers;

[Route("api/Employees")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeQueries _queries;
    private readonly ILogger<EmployeesController> _logger;
    private readonly IEmployeeDataValidator _employeeDataValidator;

    public EmployeesController(
        IEmployeeQueries queries,
        ILogger<EmployeesController> logger,
        IEmployeeDataValidator validateEmployeeInsertData)
    {
        _queries = queries;
        _logger = logger;
        _employeeDataValidator = validateEmployeeInsertData;
    }


    // GET: api/<Employee>
    [HttpGet]
    
    public async Task<List<Employee>> Get()
    {
        var employees = await _queries.GetAllEmployeesAsync();
        return employees;
    }

    // GET api/<Employee>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<Employee>
    [HttpPost]
    public async Task<string> InsertEmployee([FromBody] EmployeeRequestDto requestBody)
    {
        if (_employeeDataValidator.ValidateEmployeeData(requestBody))
        {
            int? employeeId = await _queries.InsertEmployee(requestBody);
            if (employeeId == null)
            {
                return $"Employee inserted. New employee id: {employeeId}";
            }
        }
        return "Request Body failed validation: helpful error messages";
    }

    // PUT api/<Employee>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<Employee>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
