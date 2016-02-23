using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using Dianzhu.Model;
using FluentValidation.Validators;
using System.Text.RegularExpressions;
namespace Dianzhu.BLL.Validator
{
    public class ValidatorStaff : AbstractValidator<Staff>
    {
        public ValidatorStaff()
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage("邮箱格式有误");
            RuleFor(x => x.Phone).SetValidator(new PhoneValidator());
            RuleFor(x => x.NickName).Length(0, 20).WithMessage("昵称不能超过20个字符");
            RuleFor(x => x.Address).Length(0, 200).WithMessage("地址不能超过200个字符");
        }
    }
}
