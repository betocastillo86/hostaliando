//-----------------------------------------------------------------------
// <copyright file="BookingModelValidator.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    using FluentValidation;

    /// <summary>
    /// Booking Model Validator
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{Hostaliando.Web.Models.BookingModel}" />
    public class BookingModelValidator : AbstractValidator<BookingModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookingModelValidator"/> class.
        /// </summary>
        public BookingModelValidator()
        {
            this.RuleFor(c => c.Room)
                .NotNull();

            this.RuleFor(c => c.FromDate)
                .NotNull()
                .LessThanOrEqualTo(c => c.ToDate);

            this.RuleFor(c => c.ToDate)
                .NotNull()
                .GreaterThanOrEqualTo(c => c.FromDate);

            this.RuleFor(c => c.GuestName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(150);

            this.RuleFor(c => c.GuestEmail)
                .EmailAddress()
                .MaximumLength(150);

            this.RuleFor(c => c.Comments)
                .MaximumLength(2500);

            this.RuleFor(c => c.TotalPrice)
                .NotNull()
                .GreaterThan(0);

            this.RuleFor(c => c.Source)
                .NotNull();
        }
    }
}