//-----------------------------------------------------------------------
// <copyright file="AvailabilityFilterModel.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    using System;
    using Beto.Core.Web.Api;

    /// <summary>
    /// Availability Filter Model
    /// </summary>
    /// <seealso cref="Beto.Core.Web.Api.BaseFilterModel" />
    public class AvailabilityFilterModel : BaseFilterModel
    {
        /// <summary>
        /// Gets or sets the hostel identifier.
        /// </summary>
        /// <value>
        /// The hostel identifier.
        /// </value>
        public int? HostelId { get; set; }

        /// <summary>
        /// Gets or sets the room identifier.
        /// </summary>
        /// <value>
        /// The room identifier.
        /// </value>
        public int? RoomId { get; set; }

        /// <summary>
        /// Gets or sets the only private.
        /// </summary>
        /// <value>
        /// The only private.
        /// </value>
        public bool? OnlyPrivated { get; set; }

        /// <summary>
        /// Gets or sets from date.
        /// </summary>
        /// <value>
        /// From date.
        /// </value>
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// Gets or sets to date.
        /// </summary>
        /// <value>
        /// To date.
        /// </value>
        public DateTime? ToDate { get; set; }

        /// <summary>
        /// Gets or sets the people.
        /// </summary>
        /// <value>
        /// The people.
        /// </value>
        public int? People { get; set; }
    }
}