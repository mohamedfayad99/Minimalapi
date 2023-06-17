using FluentValidation;
using MinimalVilla.Models;

namespace MinimalVilla.Validation
{
    public class CouponUPdateValidation : AbstractValidator<couponupdatedto>
    {

        public CouponUPdateValidation()
        {
            RuleFor(model => model.Id).NotEmpty().GreaterThan(0);
            RuleFor(model => model.Name).NotEmpty();
            RuleFor(model=>model.Persent).InclusiveBetween(1, 500);
        }
    }
}
