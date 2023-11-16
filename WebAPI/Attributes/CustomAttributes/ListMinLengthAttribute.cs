using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Attributes.CustomAttributes;

public class ListMinLengthAttribute : ValidationAttribute
{
    private readonly int minLength;

    public ListMinLengthAttribute(int minLength)
    {
        this.minLength = minLength;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is IList list)
        {
            if (list.Count >= minLength)
                return ValidationResult.Success;
        }


        return new ValidationResult($"{minLength}' ten büyük olmalıdır");
    }
}