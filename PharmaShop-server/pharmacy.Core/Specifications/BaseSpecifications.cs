using pharmacy.Core.Entities;
using System.Linq.Expressions;

namespace pharmacy.Core.Specifications;
public class BaseSpecifications<T> : ISpecifications<T> where T : BaseEntity
{
    public List<Expression<Func<T, object>>> Includs { get; set; } = new List<Expression<Func<T, object>>>();
    public Expression<Func<T, bool>> Criteria { get; set; } = null;
    public Expression<Func<T, object>> OrderBy { get; set; } = null;
    public Expression<Func<T, object>> OrderByDesc { get; set; } = null;

    public bool ispagenation { get; set; }
    public int Take { get; set; }
    public int skip { get; set; }
    public BaseSpecifications()
    {

    }
   public BaseSpecifications(Expression<Func<T, bool>> expression)
    {

        Criteria = expression;
    }

    public void AddOrderBy(Expression<Func<T, object>> expression)
    {
        OrderBy = expression;
    }

    public void AddOrderByDesc(Expression<Func<T, object>> expression)
    {
        OrderByDesc = expression;
    }

    public void AplayPagination(int _pagesize, int _pageindex)
    {
        ispagenation = true;

        Take = _pagesize;

        skip = ((_pageindex - 1) * _pagesize);
    }



}