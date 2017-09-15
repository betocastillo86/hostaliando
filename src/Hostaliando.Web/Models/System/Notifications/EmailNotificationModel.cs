//-----------------------------------------------------------------------
// <copyright file="EmailNotificationModel.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    using System;

    /// <summary>
    /// Email Notification Model
    /// </summary>
    /// <seealso cref="Hostaliando.Web.Models.BaseModel" />
    public class EmailNotificationModel : BaseModel
    {
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
        /// Gets or sets the body.
        /// </summary>
        /// <value>
        /// The body.
        /// </value>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the sent date.
        /// </summary>
        /// <value>
        /// The sent date.
        /// </value>
        public DateTime? SentDate { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the sent tries.
        /// </summary>
        /// <value>
        /// The sent tries.
        /// </value>
        public short SentTries { get; set; }
    }
}