using Chavdar_Todorov_employees.Models;
using Chavdar_Todorov_employees.Services.Interfaces;

namespace Chavdar_Todorov_employees.Services;

public class UploadRecordService : IUploadRecordService
{
    public void UploadEmployees(List<EmployeeProject> employees)
    {
        var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "test.csv");
        
        using var streamWriter = new StreamWriter(filePath);
        streamWriter.WriteLine("EmpID,ProjectID,DateFrom,DateTo");
        foreach (var employee in employees)
        {
            
            streamWriter.WriteLine($"{employee.EmployeeId},{employee.ProjectId},{employee.DateFrom.HasValue},{(employee.DateTo.HasValue ? employee.DateTo : "NULL")}");
        }
    }
}