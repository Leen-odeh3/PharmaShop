using pharmacy.Core.Entities.OrderAggregate;

namespace pharmacy.Core.Specifications.OrderSpecifications;
public class OrderSpecifications : BaseSpecifications<Order>
{

    public OrderSpecifications(string email)
        : base(o => o.BuyerEmail == email)
    {

        Includes();

        AddOrderByDesc(o => o.OrderTime);
    }

    public OrderSpecifications(string email, int orderid)
        : base(o => o.BuyerEmail == email && o.Id == orderid)
    {
        Includes();
    }

    private void Includes()
    {
        Includs.Add(o => o.deliveryMethod);
        Includs.Add(o => o.Items);
    }
}