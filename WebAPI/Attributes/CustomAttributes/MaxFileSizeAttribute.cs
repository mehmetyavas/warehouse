using System.ComponentModel.DataAnnotations;

namespace WebAPI.Attributes.CustomAttributes;

public class MaxFileSizeAttribute : ValidationAttribute
{
    private readonly long _maxFileSize;

    public MaxFileSizeAttribute(long maxFileSize)
    {
        _maxFileSize = maxFileSize;
    }

    public override bool IsValid(object? value)
    {
        if (value is List<IFormFile> files)
        {
            var arraySize = files.Select(x => x.Length).Sum();
            if (arraySize > _maxFileSize)
            {
                ErrorMessage = $"Dosyaların toplam boyutu {_maxFileSize / (1024 * 1024)} MB'dan küçük olmalıdır.";
                return false;
            }

            foreach (var file in files)
            {
                if (file.Length > _maxFileSize)
                {
                    ErrorMessage = $"Dosya boyutu {_maxFileSize / (1024 * 1024)} MB'dan küçük olmalıdır.";
                    return false;
                }
            }
        }
        else if (value is IFormFile file)
        {
            if (file.Length > _maxFileSize)
            {
                ErrorMessage = $"Dosya boyutu {_maxFileSize / (1024 * 1024)} MB'dan küçük olmalıdır.";
                return false;
            }
        }

        return true;
    }
}