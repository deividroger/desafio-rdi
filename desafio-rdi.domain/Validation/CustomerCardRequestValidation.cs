using desafio_rdi.domain.Models;
using FluentValidation;

namespace desafio_rdi.domain.Validation
{
    public class CustomerCardRequestValidation : AbstractValidator<CustomerCardRequest>
    {
        public CustomerCardRequestValidation()
        {
            RuleFor(x => x.CVV).GreaterThan(0).WithMessage("invalid CVV");
            RuleFor(x => x.CustomerId).GreaterThan(0).WithMessage("invalid customerId");
            RuleFor(x => x.CardNumber).GreaterThan(0).WithMessage("invalid cardNumber");
        }
    }
}
