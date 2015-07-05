using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using Dianzhu.Model;
using FluentValidation.Validators;
namespace Dianzhu.BLL.Validator
{
    public class ValidatorDZService : AbstractValidator<DZService>
    {
        public ValidatorDZService()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("名称不能为空");
            RuleFor(x => x.Business).NotNull().WithMessage("所属商家未指定");
            RuleFor(x => x.BusinessAreaCode).NotEmpty().WithMessage("服务商圈未指定");
            RuleFor(x => x.ChargeUnit).NotEmpty().WithMessage("计费单位不能为空");
            RuleFor(x => x.Description).NotEmpty().WithMessage("服务描述不能为空");
            RuleFor(x => x.FixedPrice).SetValidator(new DataTypeValidator()).WithMessage("一口价格式有误,请输入数字");

        }
    }
    public class DataTypeValidator : PropertyValidator
    {
        public DataTypeValidator()
            : base("输入格式有误")
        {
        }
        protected override bool IsValid(PropertyValidatorContext context)
        {
            object value = context.PropertyValue;
            decimal result;
            return decimal.TryParse(value.ToString(), out result);

        }
    }
}
