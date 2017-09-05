//-----------------------------------------------------------------------
// <copyright file="LocationFilterModel.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    using Beto.Core.Web.Api;

    /// <summary>
    /// Location Filter Model
    /// </summary>
    /// <seealso cref="Beto.Core.Web.Api.BaseFilterModel" />
    public class LocationFilterModel : BaseFilterModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocationFilterModel"/> class.
        /// </summary>
        public LocationFilterModel()
        {
            this.MaxPageSize = 200;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the parent identifier.
        /// </summary>
        /// <value>
        /// The parent identifier.
        /// </value>
        public int? ParentId { get; set; }
    }
}