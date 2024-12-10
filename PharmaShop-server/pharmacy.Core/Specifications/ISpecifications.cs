using pharmacy.Core.Entities;
using pharmacy.Core.Entities.Helpers;
using System.Linq.Expressions;

namespace pharmacy.Core.Specifications;
public interface ISpecifications<T> where T : BaseEntity
{
    List<Expression<Func<T, object>>> Includs { get; set; }
    Expression<Func<T, bool>> Criteria { get; set; }
    Expression<Func<T, object>> OrderBy { get; set; }
    Expression<Func<T, object>> OrderByDesc { get; set; }
    bool ispagenation { get; set; }
    int Take { get; set; }
    int skip { get; set; }
}