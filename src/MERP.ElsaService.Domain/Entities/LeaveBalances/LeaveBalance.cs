using MERP.ElsaService.Entities.Employees;
using MERP.ElsaService.Entities.Leaves;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace MERP.ElsaService.Entities.LeaveBalances;

public class LeaveBalance : FullAuditedAggregateRootWithUser<int, IdentityUser>
{
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }
    public LeaveType LeaveType { get; set; }
    public decimal Balance { get; set; }
}