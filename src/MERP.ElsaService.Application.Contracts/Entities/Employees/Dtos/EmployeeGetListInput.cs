using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace MERP.ElsaService.Entities.Employees.Dtos;

public class EmployeeGetListInput : FullAuditedEntityDto<int>
{
    public string? Name { get; set; }
    public string? Phone { get; set; }
    public string? EmployeeNo { get; set; }
}