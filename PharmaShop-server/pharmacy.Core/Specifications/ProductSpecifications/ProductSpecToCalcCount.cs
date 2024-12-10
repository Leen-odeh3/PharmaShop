

using pharmacy.Core.Entities;

namespace pharmacy.Core.Specifications.ProductSpecifications;
public class ProductSpecToCalcCount : BaseSpecifications<Product>
{

    public ProductSpecToCalcCount(ProductSpecPramas productSpecPramas) :
        base(p =>
                   (!productSpecPramas.brandid.HasValue || p.BrandId == productSpecPramas.brandid) &&
                   (!productSpecPramas.catogaryid.HasValue || p.CategoryId == productSpecPramas.catogaryid) &&
                   (string.IsNullOrEmpty(productSpecPramas.Name) || p.ProductName.ToUpper().Contains(productSpecPramas.Name))
             )
    {

    }
}