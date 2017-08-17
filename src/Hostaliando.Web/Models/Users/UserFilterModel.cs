//-----------------------------------------------------------------------
// <copyright file="UserFilterModel.cs" company="Gabriel Castillo">
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
    /// User Filter Model
    /// </summary>
    /// <seealso cref="Beto.Core.Web.Api.BaseFilterModel" />
    public class UserFilterModel : BaseFilterModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserFilterModel"/> class.
        /// </summary>
        public UserFilterModel()
        {
            this.MaxPageSize = 20;
            this.ValidOrdersBy = Enum.GetNames(typeof(SortUserBy));
        }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public Role? Role { get; set; }

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
        /// Gets the order by enum.
        /// </summary>
        /// <value>
        /// The order by enum.
        /// </value>
        public SortUserBy OrderByEnum
        {
            get
            {
                return !string.IsNullOrEmpty(this.OrderBy) ? (SortUserBy)Enum.Parse(typeof(SortUserBy), this.OrderBy) : SortUserBy.Recent;
            }
        }
    }
}