using pharmacy.Core.Entities.Identity;

namespace pharmacy.Core.Entities;
public class Pharmacist :User
{
    public string LicenseNumber { get; set; }
}
