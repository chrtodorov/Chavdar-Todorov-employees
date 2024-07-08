using Chavdar_Todorov_employees.Models;

namespace Chavdar_Todorov_employees.Services.Interfaces;

public interface IEmployeeProjectService
{
    List<EmployeeProject> ReadEmployeeProjects(string filePath);
    
    EmployeePair? FindLongestWorkingPair(List<EmployeeProject> employeeProjects);

    int CalculateOverlap(EmployeeProject employeeProject1, EmployeeProject employeeProject2);
}