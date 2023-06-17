﻿using MERP.ElsaService.Entities.Employees.Dtos;
using MERP.ElsaService.Entities.Leaves;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace MERP.ElsaService.Entities.LeaveBalances.Dtos;

public class LeaveBalanceCreateUpdateDto
{
    public int EmployeeId { get; set; }
    public LeaveType LeaveType { get; set; }
    public decimal Balance { get; set; }
}