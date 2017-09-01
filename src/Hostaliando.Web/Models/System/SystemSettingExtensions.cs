//-----------------------------------------------------------------------
// <copyright file="SystemSettingExtensions.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Hostaliando.Data;

    /// <summary>
    /// System Setting Extensions
    /// </summary>
    public static class SystemSettingExtensions
    {
        /// <summary>
        /// To the model.
        /// </summary>
        /// <param name="setting">The setting.</param>
        /// <returns>the model</returns>
        public static SystemSettingModel ToModel(this SystemSetting setting)
        {
            return new SystemSettingModel
            {
                Id = setting.Id,
                Name = setting.Name,
                Value = setting.Value
            };
        }

        /// <summary>
        /// To the models.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>the models</returns>
        public static IList<SystemSettingModel> ToModels(this ICollection<SystemSetting> settings)
        {
            return settings.Select(ToModel).ToList();
        }
    }
}