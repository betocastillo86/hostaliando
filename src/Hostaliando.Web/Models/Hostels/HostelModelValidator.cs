//-----------------------------------------------------------------------
// <copyright file="HostelModelValidator.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models.Hostels
{
    using FluentValidation;

    /// <summary>
    /// Hostel Model Validator
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{Hostaliando.Web.Models.HostelModel}" />
    public class HostelModelValidator : AbstractValidator<HostelModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HostelModelValidator"/> class.
        /// </summary>
        public HostelModelValidator()
        {
            this.RuleFor(c => c.Name)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(150);

            this.RuleFor(c => c.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(150);

            this.RuleFor(c => c.Location)
                .NotNull();

            this.RuleFor(c => c.Currency)
                .NotNull();

            this.RuleFor(c => c.Address)
                .MaximumLength(150);

            this.RuleFor(c => c.PhoneNumber)
                .MaximumLength(15);
        }
    }
}