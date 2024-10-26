using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FiltersAttributesAndMiddleware
{
    public class SanitizeInput : ValidationAttribute
    {
        private string _propertyName = "";
        public SanitizeInput(string propertyName)
        {
            _propertyName = propertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string stringValue = value as string;

            // Get the property info of the nested property
            PropertyInfo property = validationContext.ObjectType.GetProperty(_propertyName);
            if (property == null)
            {
                return new ValidationResult($"Property '{_propertyName}' not found.");
            }

            // Modify the property value
            property.SetValue(validationContext.ObjectInstance, HttpUtility.HtmlEncode(stringValue), null);


            // validation is successful
            return ValidationResult.Success;
        }

    }
}
