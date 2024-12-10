using pharmacy.Core.Entities.OrderAggregate;


namespace pharmacy.Core.Specifications.OrderSpecifications;

public class OrderSpecificationsToGetOrderByPaymentintentId : BaseSpecifications<Order>
{
    public OrderSpecificationsToGetOrderByPaymentintentId(string PaymentIntentId)
        : base(o => o.PaymentIntentId == PaymentIntentId)
    {

    }
}
