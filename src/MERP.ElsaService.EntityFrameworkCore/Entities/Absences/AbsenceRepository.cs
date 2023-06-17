using System;
using System.Linq;
using System.Threading.Tasks;
using MERP.ElsaService.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace MERP.ElsaService.Entities.Absences;

public class AbsenceRepository : EfCoreRepository<ElsaServiceDbContext, Absence, int>, IAbsenceRepository
{
    public AbsenceRepository(IDbContextProvider<ElsaServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<Absence>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}