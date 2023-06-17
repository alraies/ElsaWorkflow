using System;
using Volo.Abp.Domain.Repositories;

namespace MERP.ElsaService.Entities.Absences;

public interface IAbsenceRepository : IRepository<Absence, int>
{
}
