//-----------------------------------------------------------------------
// <copyright file="NotificationFilterModel.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    using Beto.Core.Web.Api;

    /// <summary>
    /// Base Filter Model
    /// </summary>
    /// <seealso cref="Beto.Core.Web.Api.BaseFilterModel" />
    public class NotificationFilterModel : BaseFilterModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationFilterModel"/> class.
        /// </summary>
        public NotificationFilterModel()
        {
            this.MaxPageSize = 30;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
    }
}