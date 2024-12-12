using System.ComponentModel.DataAnnotations;
namespace pharmacy.Api.CustomAttribute;
public class NotZeroAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is int intvalue && intvalue != 0)
            return true;

        return false;
    }


}