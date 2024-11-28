using pharmacy.Core.Contracts;

namespace pharmacy.Core;
public interface IUnitOfWork
{
    ICategoryRepository categoryRepository { get; }
    IProductRepository productRepository { get; }
    IBrandRepository brandRepository { get; }
    int Complete();
}
