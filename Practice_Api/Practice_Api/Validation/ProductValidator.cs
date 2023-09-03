using FluentValidation;
using Practice_Api.Models;

namespace Practice_Api.Validation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(50);
            RuleFor(p => p.Price).GreaterThan(10);
        }
    }
}