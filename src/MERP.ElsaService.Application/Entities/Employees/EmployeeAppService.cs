using System.Linq;
using System.Threading.Tasks;
using MERP.ElsaService.Entities.Employees.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace MERP.ElsaService.Entities.Employees;

public class EmployeeAppService : CrudAppService<Employee, EmployeeDto, int, EmployeeGetListInput,
    EmployeeCreateUpdateDto,
    EmployeeCreateUpdateDto>
{
    public EmployeeAppService(IRepository<Employee, int> repository) : base(repository)
    {
    }

    public override async Task<EmployeeDto> GetAsync(int id)
    {
        var entity =
            (await Repository.WithDetailsAsync(x => x.Creator, x => x.Deleter, x => x.LastModifier)).FirstOrDefault(x =>
                x.Id == id);
        if (entity is null)
            throw new EntityNotFoundException(typeof(Employee), id);
        return await MapToGetOutputDtoAsync(entity);
    }
}