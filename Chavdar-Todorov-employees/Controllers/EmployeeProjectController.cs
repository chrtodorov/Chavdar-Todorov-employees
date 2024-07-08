using Chavdar_Todorov_employees.Models;
using Chavdar_Todorov_employees.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chavdar_Todorov_employees.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeProjectController : ControllerBase
{
    private readonly EmployeeProjectService _employeeProjectService = new();
    
    [HttpGet]
    public ActionResult<EmployeePair> GetLongestWorkingPair()
    {
        var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "test.csv");
        var employeeProjects = _employeeProjectService.ReadEmployeeProjects(filePath);
        var result = _employeeProjectService.FindLongestWorkingPair(employeeProjects);
        
        if (result != null)
        {
            return Ok(result);
        }
        
        return NotFound("No common project found for the employees");
    }
}