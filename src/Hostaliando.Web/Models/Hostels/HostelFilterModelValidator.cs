//-----------------------------------------------------------------------
// <copyright file="HostelFilterModelValidator.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    using Beto.Core.Web.Api.Models;
    using FluentValidation;

    /// <summary>
    /// Hostel Filter Model Validator
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{Hostaliando.Web.Models.HostelFilterModel}" />
    public class HostelFilterModelValidator : AbstractValidator<HostelFilterModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HostelFilterModelValidator"/> class.
        /// </summary>
        public HostelFilterModelValidator()
        {
            this.AddBaseFilterValidations();

            this.RuleFor(c => c.Keyword)
                .MinimumLength(2);
        }
    }
}