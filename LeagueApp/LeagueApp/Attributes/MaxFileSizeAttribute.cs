using System.ComponentModel.DataAnnotations;

namespace LeagueApp.Attributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int maxFileSize;
        public MaxFileSizeAttribute(int _maxFileSize)
        {
            maxFileSize = _maxFileSize;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file is not null)
            {
                if (file.Length > maxFileSize)
                {
                    return new ValidationResult($"Maximmum allowed size is {maxFileSize} bytes");
                }
            }

            return ValidationResult.Success;
        }
    }
}
