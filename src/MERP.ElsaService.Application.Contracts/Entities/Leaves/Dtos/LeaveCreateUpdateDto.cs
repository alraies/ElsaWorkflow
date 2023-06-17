using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace MERP.ElsaService.Entities.Leaves.Dtos;

public class LeaveCreateUpdateDto
{
    public int EmployeeId { get; set; }
    public LeaveType LeaveType { get; set; }
}