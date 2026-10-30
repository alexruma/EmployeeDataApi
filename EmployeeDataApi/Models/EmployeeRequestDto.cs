namespace EmployeeDataApi.Models;

public class EmployeeRequestDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string EmployeeType {  get; set; } = string.Empty;
}