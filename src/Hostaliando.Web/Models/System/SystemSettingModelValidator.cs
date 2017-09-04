//-----------------------------------------------------------------------
// <copyright file="SystemSettingModelValidator.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    using FluentValidation;

    /// <summary>
    /// System Setting Model Validator
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{Hostaliando.Data.SystemSetting}" />
    public class SystemSettingModelValidator : AbstractValidator<SystemSettingModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SystemSettingModelValidator"/> class.
        /// </summary>
        public SystemSettingModelValidator()
        {
            this.RuleFor(c => c.Name)
                .MaximumLength(50)
                .NotNull()
                .NotEmpty();

            this.RuleFor(c => c.Value)
                .NotNull();
        }
    }
}