using Entities.DTOs.AuthDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.FluentValidation.AuthValidations
{
    public class RegisterValidation : AbstractValidator<RegisterDTO>
    {
        public RegisterValidation()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress();
            RuleFor(x => x.Firstname).NotEmpty().NotNull();
            RuleFor(x => x.Lastname).NotEmpty().NotNull();
            RuleFor(x => x.Password).NotNull().NotEmpty().Equal(x => x.ConfirmPassword);
            RuleFor(x => x.ConfirmPassword).NotEmpty().NotNull().Equal(x => x.Password);
        }
    }
}
