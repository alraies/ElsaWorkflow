using System.ComponentModel.DataAnnotations;
using MERP.ElsaService.Entities.Employees.Dtos;
using MERP.ElsaService.Entities.Leaves;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace MERP.ElsaService.Entities.LeaveBalances.Dtos;

public class EmployeeBalanceInput
{
    [Required] public int EmployeeId { get; set; }
    [Required] public LeaveType LeaveType { get; set; }
}