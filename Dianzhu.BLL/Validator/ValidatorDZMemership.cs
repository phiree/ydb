using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using Dianzhu.Model;
using FluentValidation.Validators;
using System.Text.RegularExpressions;
using Ydb.Membership.Application.Dto;
namespace Dianzhu.BLL.Validator
{
    public class ValidatorDZMembership : AbstractValidator<MemberDto>
    {
        public ValidatorDZMembership()
        {
            RuleFor(x => x.Email).EmailAddress().When(x => !string.IsNullOrEmpty(x.Email)).WithMessage("邮箱格式有误");//.EmailAddress()
            RuleFor(x => x.Phone).SetValidator(new PhoneValidator());
            //todo: 密码不能传给前端
            
            RuleFor(x => x.NickName).Length(0, 20).WithMessage("昵称不能超过20个字符");
            RuleFor(x => x.Address).Length(0, 200).WithMessage("地址不能超过200个字符");
        }
    }
    
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
            bool isMatch = Regex.IsMatch(value.ToString(),pattern);
            return isMatch;

        }
    }
}
