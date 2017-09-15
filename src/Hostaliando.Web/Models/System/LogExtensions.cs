//-----------------------------------------------------------------------
// <copyright file="LogExtensions.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Hostaliando.Data;

    /// <summary>
    /// Log Extensions
    /// </summary>
    public static class LogExtensions
    {
        /// <summary>
        /// To the model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>the model</returns>
        public static LogModel ToModel(this Log entity)
        {
            return new LogModel
            {
                CreationDate = entity.CreationDate,
                FullMessage = entity.FullMessage,
                Id = entity.Id,
                IpAddress = entity.IpAddress,
                PageUrl = entity.PageUrl,
                ShortMessage = entity.ShortMessage,
                UserModel = entity.User != null ? entity.User.ToBaseModel() : null
            };
        }

        /// <summary>
        /// To the models.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>the list</returns>
        public static IList<LogModel> ToModels(this IEnumerable<Log> entities)
        {
            return entities.Select(ToModel).ToList();
        }
    }
}