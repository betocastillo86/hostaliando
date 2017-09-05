//-----------------------------------------------------------------------
// <copyright file="LocationModel.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    /// <summary>
    /// Location Model
    /// </summary>
    /// <seealso cref="Hostaliando.Web.Models.Common.BaseModel" />
    public class LocationModel : BaseNamedModel
    {
        /// <summary>
        /// Gets or sets the parent location.
        /// </summary>
        /// <value>
        /// The parent location.
        /// </value>
        public LocationModel ParentLocation { get; set; }
    }
}