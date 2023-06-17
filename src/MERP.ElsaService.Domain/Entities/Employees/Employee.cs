using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace MERP.ElsaService.Entities.Employees;

public class Employee:FullAuditedAggregateRootWithUser<int,IdentityUser>
{
    public string Name { get; set; }
    public string Phone { get; set; }
    public string EmployeeNo { get; set; }
}

