namespace Chavdar_Todorov_employees.Models;

public class EmployeeProject
{
    public int EmployeeId { get; set; }
    
    public int ProjectId { get; set; }
    
    public DateTime? DateFrom { get; set; }
    
    public DateTime? DateTo { get; set; }
}