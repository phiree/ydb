using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
 
using FluentValidation.Validators;
using System.Text.RegularExpressions;
namespace Ydb.BusinessResource.DomainModel
{
    public class ValidatorServiceOpenTimeForDay : AbstractValidator<ServiceOpenTimeForDay>
    {
        public ValidatorServiceOpenTimeForDay()
        {
            RuleFor(x => x.TimePeriod).NotNull().WithMessage("开始时间不能为空");
          
            RuleFor(x => x.MaxOrderForOpenTime).NotEmpty().WithMessage("最大接单量不能为空");
        }
    }
}
