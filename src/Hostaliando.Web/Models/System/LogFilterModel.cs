//-----------------------------------------------------------------------
// <copyright file="LogFilterModel.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    using Beto.Core.Web.Api;

    /// <summary>
    /// Log Filter Model
    /// </summary>
    /// <seealso cref="Beto.Core.Web.Api.BaseFilterModel" />
    public class LogFilterModel : BaseFilterModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogFilterModel"/> class.
        /// </summary>
        public LogFilterModel()
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