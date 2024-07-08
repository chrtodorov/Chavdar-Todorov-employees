using Chavdar_Todorov_employees.Models;
using Chavdar_Todorov_employees.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chavdar_Todorov_employees.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FileRecordsController : ControllerBase
{
    private readonly UploadRecordService _uploadRecordService = new();

    [HttpPut]
    public IActionResult FileRecordUpload([FromBody] List<EmployeeProject> employees)
    {
        try
        {
            _uploadRecordService.UploadEmployees(employees);

            return Ok(employees);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Issue uploading employees - {e.Message}");
        }
    }

}