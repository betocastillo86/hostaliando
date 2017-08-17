//-----------------------------------------------------------------------
// <copyright file="UserModelValidator.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    using FluentValidation;

    /// <summary>
    /// User Model Validator
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{Hostaliando.Web.Models.UserModel}" />
    public class UserModelValidator : AbstractValidator<UserModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserModelValidator"/> class.
        /// </summary>
        public UserModelValidator()
        {
            this.RuleFor(c => c.Name)
                .NotNull()
                .MinimumLength(2)
                .MaximumLength(150);

            this.RuleFor(c => c.Email)
                .NotNull()
                .EmailAddress()
                .MaximumLength(150);

            this.RuleFor(c => c.Role)
                .NotNull();

            this.RuleFor(c => c.Password)
                .MinimumLength(3);

            this.RuleFor(c => c.TimeZone)
                .GreaterThanOrEqualTo((short)-11)
                .LessThanOrEqualTo((short)12);
        }
    }
}