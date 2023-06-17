using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MERP.ElsaService.Entities.Absences;

public static class AbsenceEfCoreQueryableExtensions
{
    public static IQueryable<Absence> IncludeDetails(this IQueryable<Absence> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            // .Include(x => x.xxx) // TODO: AbpHelper generated
            ;
    }
}
