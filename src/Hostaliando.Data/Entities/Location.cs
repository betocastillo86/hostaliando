//-----------------------------------------------------------------------
// <copyright file="Location.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Data
{
    using System.Collections.Generic;

    /// <summary>
    /// Location entity
    /// </summary>
    public partial class Location
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Location"/> class.
        /// </summary>
        public Location()
        {
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the parent location identifier.
        /// </summary>
        /// <value>
        /// The parent location identifier.
        /// </value>
        public int? ParentLocationId { get; set; }

        /// <summary>
        /// Gets or sets the parent location.
        /// </summary>
        /// <value>
        /// The parent location.
        /// </value>
        public virtual Location ParentLocation { get; set; }

        /// <summary>
        /// Gets or sets the inverse parent location.
        /// </summary>
        /// <value>
        /// The inverse parent location.
        /// </value>
        public virtual ICollection<Location> Children { get; set; }
    }
}