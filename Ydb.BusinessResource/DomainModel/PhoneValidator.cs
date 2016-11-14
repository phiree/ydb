using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ydb.BusinessResource.DomainModel
{
    public class PhoneValidator : PropertyValidator
    {
        public PhoneValidator()
            : base("电话格式有误")
        {
        }
        protected override bool IsValid(PropertyValidatorContext context)
        {
            object value = context.PropertyValue;
            if (value == null) return true;
            string pattern = @"^((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})))$";
            bool isMatch = Regex.IsMatch(value.ToString(), pattern);
            return isMatch;

        }
    }
}
