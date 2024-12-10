using pharmacy.Core.Entities;
namespace pharmacy.Core.Specifications.ProductSpecifications;
public class ProductSpecificationToGenerateQuary : BaseSpecifications<Product>
{
    public ProductSpecificationToGenerateQuary(ProductSpecPramas productSpecPramas)
        : base(p =>
                   (!productSpecPramas.brandid.HasValue || p.BrandId == productSpecPramas.brandid) &&
                   (!productSpecPramas.catogaryid.HasValue || p.CategoryId == productSpecPramas.catogaryid) &&
                   (string.IsNullOrEmpty(productSpecPramas.Name) || p.ProductName.ToUpper().Contains(productSpecPramas.Name))
             )
    {
        Includs();

        if (!string.IsNullOrEmpty(productSpecPramas.sorting))
        {
            switch (productSpecPramas.sorting)
            {
                case "priceAsc":
                    AddOrderBy(OrderBy = p => p.Price);
                    break;

                case "priceDesc":
                    AddOrderByDesc(OrderByDesc = p => p.Price);
                    break;

                default:
                    AddOrderBy(OrderBy = p => p.ProductName);
                    break;
            }
        }
        else
        {
            AddOrderBy(OrderBy = p => p.ProductName);
        }

        AplayPagination(productSpecPramas.PageSize, productSpecPramas.pageindex);

    }

    public ProductSpecificationToGenerateQuary(int id) :
        base(p => p.Id== id)
    {
        Includs();
    }
    private void Includs()
    {
        base.Includs.Add(p => p.Brand);
        base.Includs.Add(p => p.Category);
    }

}