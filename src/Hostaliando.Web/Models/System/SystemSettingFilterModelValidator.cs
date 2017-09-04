//-----------------------------------------------------------------------
// <copyright file="SystemSettingFilterModelValidator.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    using Beto.Core.Web.Api.Models;
    using FluentValidation;

    /// <summary>
    /// System Setting Filter Model Validator
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{Hostaliando.Web.Models.SystemSettingFilterModel}" />
    public class SystemSettingFilterModelValidator : AbstractValidator<SystemSettingFilterModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SystemSettingFilterModelValidator"/> class.
        /// </summary>
        public SystemSettingFilterModelValidator()
        {
            this.AddBaseFilterValidations();
        }
    }
}