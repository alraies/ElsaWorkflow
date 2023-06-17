using System.Linq;
using System.Threading.Tasks;
using MERP.ElsaService.Entities.Employees;
using MERP.ElsaService.Entities.Employees.Dtos;
using MERP.ElsaService.Entities.LeaveBalances.Dtos;
using MERP.ElsaService.Entities.Leaves;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace MERP.ElsaService.Entities.LeaveBalances;

public class LeaveBalanceAppService : CrudAppService<LeaveBalance, LeaveBalanceDto, int, LeaveBalanceGetListInput,
    LeaveBalanceCreateUpdateDto,
    EmployeeCreateUpdateDto>
{
    public LeaveBalanceAppService(IRepository<LeaveBalance, int> repository) : base(repository)
    {
    }


    public async Task<LeaveBalanceDto> GetEmployeeBalance(EmployeeBalanceInput input)
    {
        var entity = (await Repository.GetQueryableAsync()).FirstOrDefault(x =>
            x.EmployeeId == input.EmployeeId && x.LeaveType == input.LeaveType);
        if (entity == null)
            throw new EntityNotFoundException(typeof(Employee), input.EmployeeId);
        return await MapToGetOutputDtoAsync(entity);
    }

    protected override async Task<IQueryable<LeaveBalance>> CreateFilteredQueryAsync(LeaveBalanceGetListInput input)
    {
        return (await Repository.GetQueryableAsync())
            .WhereIf(input.EmployeeId != null, x => x.EmployeeId == input.EmployeeId)
            .WhereIf(input.LeaveType != null, x => x.LeaveType == input.LeaveType)
            ;
    }
}