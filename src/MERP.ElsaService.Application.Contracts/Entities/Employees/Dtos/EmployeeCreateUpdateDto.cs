﻿using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace MERP.ElsaService.Entities.Employees.Dtos;

public class EmployeeCreateUpdateDto
{
    public string Name { get; set; }
    public string Phone { get; set; }
    public string EmployeeNo { get; set; }
}