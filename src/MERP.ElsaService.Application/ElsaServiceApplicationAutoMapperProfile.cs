using AutoMapper;
using MERP.ElsaService.Entities.Employees;
using MERP.ElsaService.Entities.Employees.Dtos;
using MERP.ElsaService.Entities.LeaveBalances;
using MERP.ElsaService.Entities.LeaveBalances.Dtos;
using MERP.ElsaService.Entities.Leaves;
using MERP.ElsaService.Entities.Absences;
using MERP.ElsaService.Entities.Absences.Dtos;
using MERP.ElsaService.Entities.Leaves.Dtos;

namespace MERP.ElsaService;

public class ElsaServiceApplicationAutoMapperProfile : Profile
{
    public ElsaServiceApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Employee, EmployeeDto>();
        CreateMap<EmployeeCreateUpdateDto, Employee>(MemberList.Source);  
        
        CreateMap<Leave, LeaveDto>();
        CreateMap<LeaveCreateUpdateDto, Leave>(MemberList.Source);
  
        CreateMap<LeaveBalance, LeaveBalanceDto>();
        CreateMap<LeaveBalanceCreateUpdateDto, LeaveBalance>(MemberList.Source);
        CreateMap<Absence, AbsenceDto>();
        CreateMap<AbsenceCreateDto, Absence>(MemberList.Source);
        CreateMap<AbsenceUpdateDto, Absence>(MemberList.Source);
    }
}
