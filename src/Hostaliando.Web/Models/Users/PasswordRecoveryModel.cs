//-----------------------------------------------------------------------
// <copyright file="PasswordRecoveryModel.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    /// <summary>
    /// Password Recovery Model
    /// </summary>
    public class PasswordRecoveryModel
    {
        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        public string Email { get; set; }
    }
}