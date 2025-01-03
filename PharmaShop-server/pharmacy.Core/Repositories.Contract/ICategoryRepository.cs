﻿using pharmacy.Core.Entities;

namespace pharmacy.Core.Repositories.Contract;
public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId);

}
