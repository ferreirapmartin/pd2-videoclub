using ApiRest.Support;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ApiRest.Attributes
{
    public class GuidFormatValidationAttribute : ValidationAttribute
    {
        private Regex rgx = new Regex(@"^[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}$");
        
        public GuidFormatValidationAttribute() : base(Constants.ValidationMessages.FormatUUID) { }
        
        public override bool IsValid(object value)
        {
            var valueStr = value?.ToString();
            return valueStr is null || (valueStr != Guid.Empty.ToString() && rgx.IsMatch(valueStr));
        }
    }
}
