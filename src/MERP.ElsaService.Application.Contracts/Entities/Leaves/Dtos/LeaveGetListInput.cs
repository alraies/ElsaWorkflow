using MERP.ElsaService.Entities.Employees.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace MERP.ElsaService.Entities.Leaves.Dtos;

public class LeaveGetListInput : FullAuditedEntityDto<int>
{
    public int EmployeeId { get; set; }
    public LeaveType LeaveType { get; set; }
}