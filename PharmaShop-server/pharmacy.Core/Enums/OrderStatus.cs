
using System.Runtime.Serialization;
namespace pharmacy.Core.Enums;
public enum OrderStatus
{
    [EnumMember(Value = "Pending")]  
    Pending,
    [EnumMember(Value = "Payment Failed")]  
    PaymentFailed,
    [EnumMember(Value = "payment Succeded")]
    paymentSucceded
}