using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using WebAPI.Data.Enum;
using WebAPI.Extensions;

namespace WebAPI.Attributes.CustomAttributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
    AllowMultiple = false)]
public class PhoneNumberAttribute : ValidationAttribute
{
    protected override ValidationResult?
        IsValid(object value, ValidationContext validationContext)
    {
        return Regex.IsMatch(value.ToString()!, "^(\\d{10})$")
            ? ValidationResult.Success
            : new ValidationResult(LangKeys.PhoneNumberValidation.Localize());
    }
}