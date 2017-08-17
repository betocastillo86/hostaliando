//-----------------------------------------------------------------------
// <copyright file="UserFilterModelValidator.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    using Beto.Core.Web.Api.Models;
    using FluentValidation;

    /// <summary>
    /// User Filter Model Validator
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{Hostaliando.Web.Models.UserFilterModel}" />
    public class UserFilterModelValidator : AbstractValidator<UserFilterModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserFilterModelValidator"/> class.
        /// </summary>
        public UserFilterModelValidator()
        {
            this.AddBaseFilterValidations();

            this.RuleFor(c => c.Keyword)
                .MinimumLength(2);
        }
    }
}