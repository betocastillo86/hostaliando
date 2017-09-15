//-----------------------------------------------------------------------
// <copyright file="LogFilterModelValidator.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    using Beto.Core.Web.Api.Models;
    using FluentValidation;

    /// <summary>
    /// Log Filter Model Validator
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{Hostaliando.Web.Models.LogFilterModel}" />
    public class LogFilterModelValidator : AbstractValidator<LogFilterModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogFilterModelValidator"/> class.
        /// </summary>
        public LogFilterModelValidator()
        {
            this.AddBaseFilterValidations();
        }
    }
}