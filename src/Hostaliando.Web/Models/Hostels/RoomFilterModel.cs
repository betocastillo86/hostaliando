//-----------------------------------------------------------------------
// <copyright file="RoomFilterModel.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    using System;
    using Beto.Core.Web.Api;
    using Hostaliando.Data;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Room Filter Model
    /// </summary>
    /// <seealso cref="Beto.Core.Web.Api.BaseFilterModel" />
    public class RoomFilterModel : BaseFilterModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoomFilterModel"/> class.
        /// </summary>
        public RoomFilterModel()
        {
            this.MaxPageSize = 50;
            this.ValidOrdersBy = Enum.GetNames(typeof(SortRoomBy));
        }

        /// <summary>
        /// Gets or sets the keyword.
        /// </summary>
        /// <value>
        /// The keyword.
        /// </value>
        public string Keyword { get; set; }

        /// <summary>
        /// Gets or sets the hostel identifier.
        /// </summary>
        /// <value>
        /// The hostel identifier.
        /// </value>
        public int? HostelId { get; set; }

        /// <summary>
        /// Gets or sets the type of the room.
        /// </summary>
        /// <value>
        /// The type of the room.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public RoomType? RoomType { get; set; }

        /// <summary>
        /// Gets or sets the only private.
        /// </summary>
        /// <value>
        /// The only private.
        /// </value>
        public bool? OnlyPrivated { get; set; }

        /// <summary>
        /// Gets the order by enumerator.
        /// </summary>
        /// <value>
        /// The order by enumerator.
        /// </value>
        public SortRoomBy OrderByEnum => !string.IsNullOrEmpty(this.OrderBy) ? (SortRoomBy)Enum.Parse(typeof(SortRoomBy), this.OrderBy) : SortRoomBy.Beds;
    }
}