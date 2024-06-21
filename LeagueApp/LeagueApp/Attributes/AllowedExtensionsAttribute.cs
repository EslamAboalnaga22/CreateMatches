using System.ComponentModel.DataAnnotations;

namespace LeagueApp.Attributes
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string allowedExtensions;
        public AllowedExtensionsAttribute(string _allowedExtensions)
        {
            allowedExtensions = _allowedExtensions;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file is not null)
            {
                var extenison = Path.GetExtension(file.FileName);

                var isAllowed = allowedExtensions
                    .Split(',')
                    .Contains(extenison, StringComparer.OrdinalIgnoreCase);

                if (!isAllowed)
                {
                    return new ValidationResult($"Only {allowedExtensions} are allowed");
                }
            }

            return ValidationResult.Success;
        }
    }
}

