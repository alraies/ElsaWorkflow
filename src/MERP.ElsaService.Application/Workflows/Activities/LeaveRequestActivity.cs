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
using MERP.ElsaService.Entities.Employees;
using MERP.ElsaService.Entities.LeaveBalances;
using MERP.ElsaService.Entities.Leaves;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace MERP.ElsaService.Workflows.Activities;

[Activity(Outcomes = new[] { "Success", "Failed" })]
public class LeaveRequestActivity : Activity
{
    private readonly IRepository<LeaveBalance, int> _leaveBalanceRepository;
    private readonly IJavaScriptService _scriptService;

    public LeaveRequestActivity(IRepository<LeaveBalance, int> leaveBalanceRepository, IJavaScriptService scriptService)
    {
        _leaveBalanceRepository = leaveBalanceRepository;
        _scriptService = scriptService;
    }

    [ActivityInput(Hint = "Employee Id",SupportedSyntaxes = new []{"JavaScript"})]
    public string EmployeeId { get; set; }

    [ActivityInput(Hint = "Leave Type")]
    public string LeaveType { get; set; }

    [ActivityInput]
    public string Duration { get; set; }

    protected override async ValueTask<IActivityExecutionResult> OnExecuteAsync(ActivityExecutionContext context)
    {
        var employeeIdValue =await _scriptService.EvaluateAsync(EmployeeId, typeof(int), context);
        var durationValue = await _scriptService.EvaluateAsync(Duration, typeof(int), context);
        var leaveTypeValue = await _scriptService.EvaluateAsync(LeaveType, typeof(int), context);
        if (employeeIdValue is int employeeId && durationValue is int duration&&leaveTypeValue is int)
        {
            // Check Balance
            var entity = (await _leaveBalanceRepository.GetQueryableAsync()).FirstOrDefault(x =>
                x.EmployeeId == employeeId && x.LeaveType == (LeaveType)leaveTypeValue);
            if (entity is null)
                throw new EntityNotFoundException(typeof(Employee), EmployeeId);
            if (duration <= entity.Balance)
            {
                return Outcomes("Success");
            }
        }

        return Outcomes("Failed");
    }
}