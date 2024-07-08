using Chavdar_Todorov_employees.Models;

namespace Chavdar_Todorov_employees.Services.Interfaces;

public interface IUploadRecordService
{
    void UploadEmployees(List<EmployeeProject> employees);
}