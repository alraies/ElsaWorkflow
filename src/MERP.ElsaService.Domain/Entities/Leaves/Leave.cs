using System.ComponentModel.DataAnnotations.Schema;
using MERP.ElsaService.Entities.Employees;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace MERP.ElsaService.Entities.Leaves;

public class Leave : FullAuditedAggregateRootWithUser<int, IdentityUser>
{
    public int EmployeeId { get; set; }
    [ForeignKey(nameof(EmployeeId))] 
    public Employee Employee { get; set; }
    public LeaveType LeaveType { get; set; }
}