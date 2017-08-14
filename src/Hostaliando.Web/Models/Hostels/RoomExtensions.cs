//-----------------------------------------------------------------------
// <copyright file="RoomExtensions.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Hostaliando.Data;

    /// <summary>
    /// Room extensions
    /// </summary>
    public static class RoomExtensions
    {
        /// <summary>
        /// To the model.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <returns>the model</returns>
        public static RoomModel ToModel(this Room room)
        {
            return new RoomModel
            {
                Id = room.Id,
                Name = room.Name,
                Active = room.Active,
                Beds = room.Beds,
                CreationDateUtc = room.CreationDateUtc,
                Hostel = new BaseNamedModel { Id = room.HostelId, Name = room.Hostel?.Name },
                IsPrivated = room.IsPrivated,
                RoomType = room.RoomType
            };
        }

        /// <summary>
        /// To the models.
        /// </summary>
        /// <param name="rooms">The rooms.</param>
        /// <returns>the models</returns>
        public static IList<RoomModel> ToModels(this ICollection<Room> rooms)
        {
            return rooms.Select(ToModel).ToList();
        }
    }
}