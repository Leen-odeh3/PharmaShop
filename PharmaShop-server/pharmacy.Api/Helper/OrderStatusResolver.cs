using AutoMapper;
using pharmacy.Core.DTOs.Order;
using pharmacy.Core.Entities.OrderAggregate;

namespace pharmacy.Api.Helper;
public class OrderStatusResolver : IValueResolver<Order, OrderToReturnDto, string>
{
    public string Resolve(Order source, OrderToReturnDto destination, string destMember, ResolutionContext context)
    {
        return destination.status = source.status.ToString();
    }
}