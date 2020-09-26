using ApiRest.Support;
using System.ComponentModel.DataAnnotations;

namespace ApiRest.Attributes
{
    public class DateFormatValidationAttribute : ValidationAttribute
    {
        private readonly DateHelper dateHelper;

        public DateFormatValidationAttribute() : this(new DateHelper()) { }
        
        public DateFormatValidationAttribute(DateHelper dateHelper)
            : base(Constants.ValidationMessages.FormatDate)
        {
            this.dateHelper = dateHelper;
        }

        public override bool IsValid(object value) 
            => value is null || dateHelper.IsValid(value.ToString());

    }
}