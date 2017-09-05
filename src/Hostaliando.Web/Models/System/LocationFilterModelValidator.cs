//-----------------------------------------------------------------------
// <copyright file="LocationFilterModelValidator.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    using Beto.Core.Web.Api.Models;
    using FluentValidation;

    /// <summary>
    /// Location Filter Model Validator
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{Hostaliando.Web.Models.LocationFilterModel}" />
    public class LocationFilterModelValidator : AbstractValidator<LocationFilterModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocationFilterModelValidator"/> class.
        /// </summary>
        public LocationFilterModelValidator()
        {
            this.AddBaseFilterValidations();
        }
    }
}