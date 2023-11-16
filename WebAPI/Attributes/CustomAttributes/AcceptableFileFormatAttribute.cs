using System.ComponentModel.DataAnnotations;
using WebAPI.Data.Enum;
using WebAPI.Extensions;

namespace WebAPI.Attributes.CustomAttributes;

public class AcceptableFileFormatAttribute : ValidationAttribute
{
    private List<FileFormat> Format { get; set; }

    public AcceptableFileFormatAttribute(params FileFormat[] format)
    {
        Format = format.ToList();
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
        {
            return ValidationResult.Success;
        }

        if (value is IFormFile file)
        {
            var ext = Path.GetExtension(file.FileName).TrimStart('.').ToLower();
            if (Format.All(x => x.ToString().ToLower() != ext))
            {
                return new ValidationResult(LangKeys.InvalidFileFormat.Localize());
            }
        }

        return ValidationResult.Success;
    }
}