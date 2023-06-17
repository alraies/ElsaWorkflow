using MERP.ElsaService.Entities.Employees.Dtos;
using MERP.ElsaService.Entities.Leaves.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace MERP.ElsaService.Entities.Leaves;

public class LeaveAppService : CrudAppService<Leave, LeaveDto, int, LeaveGetListInput, LeaveCreateUpdateDto,
    EmployeeCreateUpdateDto>
{
    public LeaveAppService(IRepository<Leave, int> repository) : base(repository)
    {
    }
}