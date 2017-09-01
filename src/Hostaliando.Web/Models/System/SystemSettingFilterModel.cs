//-----------------------------------------------------------------------
// <copyright file="SystemSettingFilterModel.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    using Beto.Core.Web.Api;

    /// <summary>
    /// System Setting Filter Model
    /// </summary>
    /// <seealso cref="Beto.Core.Web.Api.BaseFilterModel" />
    public class SystemSettingFilterModel : BaseFilterModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SystemSettingFilterModel"/> class.
        /// </summary>
        public SystemSettingFilterModel()
        {
            this.MaxPageSize = 50;
        }

        /// <summary>
        /// Gets or sets the keyword.
        /// </summary>
        /// <value>
        /// The keyword.
        /// </value>
        public string Keyword { get; set; }
    }
}