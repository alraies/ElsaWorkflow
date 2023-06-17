using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elsa.ActivityResults;
using Elsa.Attributes;
using Elsa.Scripting.JavaScript.Messages;
using Elsa.Scripting.JavaScript.Services;
using Elsa.Services;
using Elsa.Services.Models;
using MERP.ElsaService.Entities.Absences;
using MERP.ElsaService.Entities.Employees;
using MERP.ElsaService.Entities.LeaveBalances;
using MERP.ElsaService.Entities.Leaves;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace MERP.ElsaService.Workflows.Activities;

[Activity(Outcomes = new[] { "Success", "Failed" })]
public class CancelAbsenceActivity : Activity
{
    private readonly IAbsenceRepository _absenceRepository;
    private readonly IJavaScriptService _scriptService;

    public CancelAbsenceActivity(IAbsenceRepository absenceRepository, IJavaScriptService scriptService)
    {
        _absenceRepository = absenceRepository;
        _scriptService = scriptService;
    }

    [ActivityInput(Hint = "Employee Id", DefaultSyntax = "JavaScript", SupportedSyntaxes = new[] { "JavaScript" })]
    public string EmployeeId { get; set; }


    protected override async ValueTask<IActivityExecutionResult> OnExecuteAsync(ActivityExecutionContext context)
    {
        var employeeIdValue = await _scriptService.EvaluateAsync(EmployeeId, typeof(int), context);
        if (employeeIdValue is int employeeId )
        {
            // Check Balance
            var entity = (await _absenceRepository.GetQueryableAsync()).FirstOrDefault(x =>
                x.EmployeeId == employeeId);
            if (entity is null)
                throw new EntityNotFoundException(typeof(Employee), EmployeeId);

            await _absenceRepository.DeleteAsync(entity);
            return Outcomes("Success");
        }

        return Outcomes("Failed");
    }
}