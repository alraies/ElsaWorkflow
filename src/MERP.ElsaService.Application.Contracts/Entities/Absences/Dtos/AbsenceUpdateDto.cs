using System;

namespace MERP.ElsaService.Entities.Absences.Dtos;

[Serializable]
public class AbsenceUpdateDto
{
    public int EmployeeId { get; set; }

    public DateTime Date { get; set; }
}