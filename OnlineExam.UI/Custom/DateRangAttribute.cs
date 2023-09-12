using System.ComponentModel.DataAnnotations;

namespace OnlineExam.UI.Custom
{
    public class DateRangeAttribute : ValidationAttribute
    {
        private readonly string startDatePropertyName;
        private readonly string endDatePropertyName;

        public DateRangeAttribute(string startDatePropertyName, string endDatePropertyName)
        {
            this.startDatePropertyName = startDatePropertyName;
            this.endDatePropertyName = endDatePropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var startDateProperty = validationContext.ObjectType.GetProperty(startDatePropertyName);
            var endDateProperty = validationContext.ObjectType.GetProperty(endDatePropertyName);

            if (startDateProperty == null || endDateProperty == null)
            {
                throw new ArgumentException("Invalid property names.");
            }

            var startDate = (DateTime)startDateProperty.GetValue(validationContext.ObjectInstance);
            var endDate = (DateTime)endDateProperty.GetValue(validationContext.ObjectInstance);

            if (startDate > endDate)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
