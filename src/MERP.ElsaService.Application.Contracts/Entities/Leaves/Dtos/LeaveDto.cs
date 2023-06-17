using MERP.ElsaService.Entities.Employees.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace MERP.ElsaService.Entities.Leaves.Dtos;

public class LeaveDto : FullAuditedEntityDto<int>
{
    public int EmployeeId { get; set; }
    public EmployeeDto Employee { get; set; }
    public LeaveType LeaveType { get; set; }
}