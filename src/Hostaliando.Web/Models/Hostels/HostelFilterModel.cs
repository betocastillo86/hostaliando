//-----------------------------------------------------------------------
// <copyright file="HostelFilterModel.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    using Beto.Core.Web.Api;

    /// <summary>
    /// Hostel Filter Model
    /// </summary>
    /// <seealso cref="Beto.Core.Web.Api.BaseFilterModel" />
    public class HostelFilterModel : BaseFilterModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HostelFilterModel"/> class.
        /// </summary>
        public HostelFilterModel()
        {
            this.MaxPageSize = 20;
        }

        /// <summary>
        /// Gets or sets the keyword.
        /// </summary>
        /// <value>
        /// The keyword.
        /// </value>
        public string Keyword { get; set; }

        /// <summary>
        /// Gets or sets the location identifier.
        /// </summary>
        /// <value>
        /// The location identifier.
        /// </value>
        public int? LocationId { get; set; }
    }
}