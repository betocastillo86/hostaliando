//-----------------------------------------------------------------------
// <copyright file="SystemSettingModel.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    /// <summary>
    /// System Setting Model
    /// </summary>
    /// <seealso cref="Hostaliando.Web.Models.BaseNamedModel" />
    public class SystemSettingModel : BaseNamedModel
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; set; }
    }
}