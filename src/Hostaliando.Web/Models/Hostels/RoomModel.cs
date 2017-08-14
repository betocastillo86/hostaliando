//-----------------------------------------------------------------------
// <copyright file="RoomModel.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    using System;
    using Hostaliando.Data;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// The room model
    /// </summary>
    /// <seealso cref="Hostaliando.Web.Models.BaseModel" />
    public class RoomModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the beds.
        /// </summary>
        /// <value>
        /// The beds.
        /// </value>
        public byte Beds { get; set; }

        /// <summary>
        /// Gets or sets the type of the room.
        /// </summary>
        /// <value>
        /// The type of the room.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public RoomType? RoomType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is privated.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is privated; otherwise, <c>false</c>.
        /// </value>
        public bool IsPrivated { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="RoomModel"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the creation date UTC.
        /// </summary>
        /// <value>
        /// The creation date UTC.
        /// </value>
        public DateTime CreationDateUtc { get; set; }

        /// <summary>
        /// Gets or sets the hostel.
        /// </summary>
        /// <value>
        /// The hostel.
        /// </value>
        public BaseNamedModel Hostel { get; set; }
    }
}