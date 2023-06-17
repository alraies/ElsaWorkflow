using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace MERP.ElsaService.Entities.Absences.Dtos;

[Serializable]
public class AbsenceGetListInput : PagedAndSortedResultRequestDto
{
    public int? EmployeeId { get; set; }

    public DateTime? Date { get; set; }
}