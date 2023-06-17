using System;
using System.Linq;
using System.Threading.Tasks;
using MERP.ElsaService.Permissions;
using MERP.ElsaService.Entities.Absences.Dtos;
using Volo.Abp.Application.Services;

namespace MERP.ElsaService.Entities.Absences;


public class AbsenceAppService : CrudAppService<Absence, AbsenceDto, int, AbsenceGetListInput, AbsenceCreateDto, AbsenceUpdateDto>,
    IAbsenceAppService
{
  
    private readonly IAbsenceRepository _repository;

    public AbsenceAppService(IAbsenceRepository repository) : base(repository)
    {
        _repository = repository;
    }

    protected override async Task<IQueryable<Absence>> CreateFilteredQueryAsync(AbsenceGetListInput input)
    {
        // TODO: AbpHelper generated
        return (await base.CreateFilteredQueryAsync(input))
            .WhereIf(input.EmployeeId != null, x => x.EmployeeId == input.EmployeeId)
            .WhereIf(input.Date != null, x => x.Date == input.Date)
            ;
    }
}
