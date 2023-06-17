using System;
using System.ComponentModel.DataAnnotations.Schema;
using MERP.ElsaService.Entities.Employees;
using Volo.Abp.Domain.Entities.Auditing;

namespace MERP.ElsaService.Entities.Absences;

public class Absence:FullAuditedAggregateRoot<int>
{
    public int EmployeeId { get; set; }
    [ForeignKey(nameof(EmployeeId))]
    public Employee Employee { get; set; }

    public DateTime Date { get; set; }
}