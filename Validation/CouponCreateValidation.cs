using FluentValidation;
using MinimalVilla.Models;

namespace MinimalVilla.Validation
{
    public class CouponCreateValidation : AbstractValidator<couponcreateddto>
    {

        public CouponCreateValidation()
        {
            RuleFor(model => model.Name).NotEmpty();
            RuleFor(model=>model.Persent).InclusiveBetween(1, 500);
        }
    }
}
