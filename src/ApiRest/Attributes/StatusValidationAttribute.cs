using ApiRest.Support;
using System;
using System.ComponentModel.DataAnnotations;

namespace ApiRest.Attributes
{
    public class StatusValidationAttribute : ValidationAttribute
    {
        private readonly StatusHelper statusHelper;

        public StatusValidationAttribute() : this(new StatusHelper()) { }
        public StatusValidationAttribute(StatusHelper statusHelper) : base(Constants.ValidationMessages.Status)
        {
            this.statusHelper = statusHelper ?? throw new ArgumentNullException();
        }


        public override bool IsValid(object value)
        {
            var valueStr = value?.ToString();
            return valueStr is null || statusHelper.IsValid(valueStr);
        }
    }
}
