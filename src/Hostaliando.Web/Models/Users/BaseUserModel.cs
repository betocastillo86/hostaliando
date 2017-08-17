//-----------------------------------------------------------------------
// <copyright file="BaseUserModel.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    using Hostaliando.Data;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Base user model
    /// </summary>
    public class BaseUserModel : BaseNamedModel
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public Role? Role { get; set; }

        /// <summary>
        /// Gets or sets the hostel.
        /// </summary>
        /// <value>
        /// The hostel.
        /// </value>
        public HostelModel Hostel { get; set; }

        /// <summary>
        /// Gets or sets the time zone.
        /// </summary>
        /// <value>
        /// The time zone.
        /// </value>
        public short TimeZone { get; set; }
    }
}