using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using WebAPI.Data.Enum;
using WebAPI.Extensions;

namespace WebAPI.Attributes;

public class PasswordAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (!(value is string password))
            return new ValidationResult(LangKeys.PasswordError.Localize());

        if (string.IsNullOrWhiteSpace(password))
            return new ValidationResult(LangKeys.PasswordNull.Localize());

        if (password.Length < 8 ||
            password.Length > 12)
            return new ValidationResult(LangKeys.PasswordLength.Localize());

        //^(?=.*[A-Za-z])(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,12}$
        if (!Regex.IsMatch(password, ""))
        {
            return new ValidationResult("Şifre geçersiz format.");
        }

        return ValidationResult.Success!;
    }
}