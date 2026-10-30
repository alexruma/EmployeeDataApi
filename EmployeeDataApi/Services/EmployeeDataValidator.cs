using EmployeeDataApi.Models;

namespace EmployeeDataApi.Services;

public interface IEmployeeDataValidator
{
    bool ValidateEmployeeData(EmployeeRequestDto );
}

public class EmployeeDataValidator : IEmployeeDataValidator
{
    public bool ValidateEmployeeData(EmployeeRequestDto employeeDto)
    {
        // Validate data provided in request before inserting into DB.
        throw new NotImplementedException();
    }
}
