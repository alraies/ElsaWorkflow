using System;

namespace MERP.ElsaService.Entities.Absences.Dtos;

[Serializable]
public class AbsenceCreateDto
{
    public int EmployeeId { get; set; }

    public DateTime Date { get; set; }
}