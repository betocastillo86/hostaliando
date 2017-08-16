//-----------------------------------------------------------------------
// <copyright file="UserModel.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    using Hostaliando.Data;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// User Model
    /// </summary>
    /// <seealso cref="Hostaliando.Web.Models.BaseNamedModel" />
    public class UserModel : BaseNamedModel
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
        public Role Role { get; set; }

        /// <summary>
        /// Gets the hostel.
        /// </summary>
        /// <value>
        /// The hostel.
        /// </value>
        public HostelModel Hostel { get; internal set; }
    }
}