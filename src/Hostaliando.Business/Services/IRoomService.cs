//-----------------------------------------------------------------------
// <copyright file="IRoomService.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Business.Services
{
    using System.Threading.Tasks;
    using Beto.Core.Data;
    using Hostaliando.Data;

    /// <summary>
    /// Interface of room service
    /// </summary>
    public interface IRoomService
    {
        /// <summary>
        /// Deletes the specified room.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <returns>the task</returns>
        Task Delete(Room room);

        /// <summary>
        /// Gets all rooms
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        /// <param name="hostelId">The hostel identifier.</param>
        /// <param name="onlyPrivated">only private rooms</param>
        /// <param name="roomType">the room type</param>
        /// <param name="sortRoomBy">sort rooms by</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>the list of rooms</returns>
        Task<IPagedList<Room>> GetAll(
            string keyword = null, 
            int? hostelId = null, 
            bool? onlyPrivated = null,
            RoomType? roomType = null,
            SortRoomBy sortRoomBy = SortRoomBy.Name, 
            int page = 0, 
            int pageSize = int.MaxValue);

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="track">if set to <c>true</c> [track].</param>
        /// <returns>the room</returns>
        Task<Room> GetById(int id, bool track = true);

        /// <summary>
        /// Inserts the specified room.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <returns>the task</returns>
        Task Insert(Room room);

        /// <summary>
        /// Updates the specified room.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <returns>the task</returns>
        Task Update(Room room);
    }
}