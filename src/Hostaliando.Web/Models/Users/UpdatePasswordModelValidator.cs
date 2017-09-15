//-----------------------------------------------------------------------
// <copyright file="UpdatePasswordModelValidator.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    using FluentValidation;

    /// <summary>
    /// Update Password Model Validator
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{Hostaliando.Web.Models.UpdatePasswordModel}" />
    public class UpdatePasswordModelValidator : AbstractValidator<UpdatePasswordModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatePasswordModelValidator"/> class.
        /// </summary>
        public UpdatePasswordModelValidator()
        {
            this.RuleFor(c => c.Password)
                .NotNull()
                .MinimumLength(6);
        }
    }
}