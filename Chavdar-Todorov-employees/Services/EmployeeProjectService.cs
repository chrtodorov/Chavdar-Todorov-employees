using System.Globalization;
using Chavdar_Todorov_employees.Models;
using Chavdar_Todorov_employees.Services.Interfaces;

namespace Chavdar_Todorov_employees.Services;

public class EmployeeProjectService : IEmployeeProjectService
{
    private readonly string[] _dateFormats = { "yyyy-MM-dd", "dd-MM-yyyy", "dd-MMM-yyyy", "dd-MMM-yy h:mm:ss tt" };
    
    public List<EmployeeProject> ReadEmployeeProjects(string filePath)
    {
        var employeeProjects = new List<EmployeeProject>();
        var lines = File.ReadAllLines(filePath);

        foreach (var line in lines.Skip(1)) 
        {
            var columns = line.Split(',');

            var employeeId = int.Parse(columns[0]);
            var projectId = int.Parse(columns[1]);
            var dateFrom = ParseDate(columns[2]);
            var dateTo = columns[3] == "NULL" ? (DateTime?)null : ParseDate(columns[3]);

            employeeProjects.Add(new EmployeeProject
            {
                EmployeeId = employeeId,
                ProjectId = projectId,
                DateFrom = dateFrom,
                DateTo = dateTo
            });
        }

        return employeeProjects;
    }

    public EmployeePair? FindLongestWorkingPair(List<EmployeeProject> employeeProjects)
    {
        var pairsWorkDuration = new Dictionary<Tuple<int, int, int>, int>();

        for (var i = 0; i < employeeProjects.Count; i++)
        {
            for (var j = i + 1; j < employeeProjects.Count; j++)
            {
                var empProj1 = employeeProjects[i];
                var empProj2 = employeeProjects[j];

                if (empProj1.ProjectId == empProj2.ProjectId && empProj1.EmployeeId != empProj2.EmployeeId)
                {
                    var overlap = CalculateOverlap(empProj1, empProj2);
                    if (overlap > 0)
                    {
                        var pair = new Tuple<int, int, int>(Math.Min(empProj1.EmployeeId, empProj2.EmployeeId), Math.Max(empProj1.EmployeeId, empProj2.EmployeeId), empProj1.ProjectId);
                        pairsWorkDuration.TryAdd(pair, 0);
                        pairsWorkDuration[pair] += overlap;
                    }
                }
            }
        }

        var longestWorkingPair = pairsWorkDuration.OrderByDescending(kv => kv.Value).FirstOrDefault();
        if (longestWorkingPair.Key != null)
        {
            return new EmployeePair
            {
                FirstEmployeeId = longestWorkingPair.Key.Item1,
                SecondEmployeeId = longestWorkingPair.Key.Item2,
                ProjectId = longestWorkingPair.Key.Item3,
                DaysWorkedTogether = longestWorkingPair.Value
            };
        }

        return null;
    }

    public int CalculateOverlap(EmployeeProject employeeProject1, EmployeeProject employeeProject2)
    {
        var start = employeeProject1.DateFrom > employeeProject2.DateFrom ? employeeProject1.DateFrom : employeeProject2.DateFrom;
        var end1 = employeeProject1.DateTo ?? DateTime.Now;
        var end2 = employeeProject2.DateTo ?? DateTime.Now;
        var end = end1 < end2 ? end1 : end2;

        return start < end ? (end - start).Value.Days : 0;
    }
    
    private DateTime ParseDate(string dateString)
    {
        if (DateTime.TryParseExact(dateString, _dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
        {
            return result;
        }

        throw new FormatException($"Unable to parse date: {dateString}");
    }
}