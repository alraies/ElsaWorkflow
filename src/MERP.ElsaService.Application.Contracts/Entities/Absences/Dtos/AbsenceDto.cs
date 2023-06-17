using System;
using MERP.ElsaService.Entities.Employees.Dtos;
using Volo.Abp.Application.Dtos;

namespace MERP.ElsaService.Entities.Absences.Dtos;

[Serializable]
public class AbsenceDto : FullAuditedEntityDto<int>
{
    public int EmployeeId { get; set; }

    public EmployeeDto Employee { get; set; }

    public DateTime Date { get; set; }
}