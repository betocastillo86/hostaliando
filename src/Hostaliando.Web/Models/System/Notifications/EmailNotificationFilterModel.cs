//-----------------------------------------------------------------------
// <copyright file="EmailNotificationFilterModel.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    using Beto.Core.Web.Api;

    /// <summary>
    /// Email Notification Filter Model
    /// </summary>
    /// <seealso cref="Beto.Core.Web.Api.BaseFilterModel" />
    public class EmailNotificationFilterModel : BaseFilterModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailNotificationFilterModel"/> class.
        /// </summary>
        public EmailNotificationFilterModel()
        {
            this.MaxPageSize = 50;
        }

        /// <summary>
        /// Gets or sets to.
        /// </summary>
        /// <value>
        /// To email.
        /// </value>
        public string To { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the sent.
        /// </summary>
        /// <value>
        /// The sent.
        /// </value>
        public bool? Sent { get; set; }
    }
}