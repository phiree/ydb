using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using Dianzhu.Model;
namespace Dianzhu.BLL.Validator
{
 public class ValidatorDZService:AbstractValidator<DZService>
    {
     public ValidatorDZService()
     {
         RuleFor(x => x.Name).NotEmpty().WithMessage("不能为空");
         
     }
    }
}
