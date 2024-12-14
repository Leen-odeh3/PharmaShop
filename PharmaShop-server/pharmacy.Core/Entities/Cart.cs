
using pharmacy.Api.CustomAttribute;

namespace pharmacy.Core.Entities;
public class Cart
{
    public int ProductId { get; set; }

    [NotZero(ErrorMessage = "Quantity cannot be zero.")]
    public int Quantity { get; set; }
}
