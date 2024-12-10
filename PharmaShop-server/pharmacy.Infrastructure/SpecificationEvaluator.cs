using Microsoft.EntityFrameworkCore;
using pharmacy.Core.Entities;
using pharmacy.Core.Specifications;
namespace pharmacy.Infrastructure;
public static class SpecificationEvaluator<T> where T : BaseEntity
{
    public static IQueryable<T> GetQuary(IQueryable<T> Dbset, ISpecifications<T> specifications)
    {

        var Quary = Dbset;


        if (specifications.Criteria is not null)
            Quary = Quary.Where(specifications.Criteria);

        if (specifications.OrderBy is not null)
            Quary = Quary.OrderBy(specifications.OrderBy);
        else if (specifications.OrderByDesc is not null)
            Quary = Quary.OrderByDescending(specifications.OrderByDesc);

        if (specifications.ispagenation)
            Quary = Quary.Skip(specifications.skip).Take(specifications.Take);



        Quary = specifications.Includs.Aggregate(Quary, (CurrentQuary, IncludExprssion) => CurrentQuary.Include(IncludExprssion));


        return Quary;
    }
}