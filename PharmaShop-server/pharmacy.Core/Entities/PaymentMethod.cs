namespace pharmacy.Core.Entities;
public class PaymentMethod
{
    public Guid PaymentMethodId { get; set; }= Guid.NewGuid();  
    public string PaymentMethodName { get; set;}
}
