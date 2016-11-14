using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
 
using FluentValidation.Validators;
using System.Text.RegularExpressions;
namespace Ydb.BusinessResource.DomainModel
{
    public class ValidatorBusiness : AbstractValidator<Business>
    {
        public ValidatorBusiness()
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage("邮箱格式有误");
            RuleFor(x => x.Phone).SetValidator(new PhoneValidator());
            RuleFor(x => x.Name).Length(0, 20).WithMessage("店铺不能超过20个字符");
            RuleFor(x => x.Contact).Length(0, 20).WithMessage("联系人姓名不能超过20个字符");
            RuleFor(x => x.Description).Length(0, 200).WithMessage("地址不能超过200个字符");
            RuleFor(x => x.Address).Length(0, 200).WithMessage("地址不能超过200个字符");
        }
    }
}
