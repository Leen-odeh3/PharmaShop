﻿using pharmacy.Core.Entities;

namespace pharmacy.Core.Contracts;
public interface ICartRepository:IGenericRepository<Cart>
{
    Task<Cart> GetCartByCustomerIdAsync(string customerId);
}
