using System;
using MERP.ElsaService.Entities.Absences.Dtos;
using Volo.Abp.Application.Services;

namespace MERP.ElsaService.Entities.Absences;

public interface IAbsenceAppService :
    ICrudAppService< 
        AbsenceDto, 
        int, 
        AbsenceGetListInput,
        AbsenceCreateDto,
        AbsenceUpdateDto>
{

}