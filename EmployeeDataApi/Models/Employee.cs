namespace EmployeeDataApi.Models
{
    public class Employee
    {
        public required int EmployeeId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? Address1 { get; set; }
        public required int WageId { get; set; } 
        public required char EmployeeType { get; set; }
        public int? ExpenseId {  get; set; }
    }
}
