//-----------------------------------------------------------------------
// <copyright file="AvailabilityFilterModelValidator.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    using Beto.Core.Web.Api.Models;
    using FluentValidation;

    /// <summary>
    /// Availability Filter Model Validator
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{Hostaliando.Web.Models.Booking.AvailabilityFilterModel}" />
    public class AvailabilityFilterModelValidator : AbstractValidator<AvailabilityFilterModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AvailabilityFilterModelValidator"/> class.
        /// </summary>
        public AvailabilityFilterModelValidator()
        {
            this.AddBaseFilterValidations();

            this.RuleFor(c => c.FromDate)
                .NotNull();

            this.RuleFor(c => c.People)
                .NotNull()
                .GreaterThan(0)
                .LessThan(15);

            this.RuleFor(c => c.ToDate)
                .NotNull()
                .GreaterThanOrEqualTo(c => c.FromDate);

            this.RuleFor(c => c.HostelId)
                .NotNull();
        }
    }
}