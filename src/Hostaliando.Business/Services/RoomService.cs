//-----------------------------------------------------------------------
// <copyright file="RoomService.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Business.Services
{
    using System;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading.Tasks;
    using Beto.Core.Data;
    using Beto.Core.EventPublisher;
    using Hostaliando.Business.Exceptions;
    using Hostaliando.Data;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Room Service
    /// </summary>
    /// <seealso cref="Hostaliando.Business.Services.IRoomService" />
    public class RoomService : IRoomService
    {
        /// <summary>
        /// The publisher
        /// </summary>
        private readonly IPublisher publisher;

        /// <summary>
        /// The room repository
        /// </summary>
        private readonly IRepository<Room> roomRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoomService"/> class.
        /// </summary>
        /// <param name="roomRepository">The room repository.</param>
        /// <param name="publisher">The publisher.</param>
        public RoomService(
            IRepository<Room> roomRepository,
            IPublisher publisher)
        {
            this.roomRepository = roomRepository;
            this.publisher = publisher;
        }

        /// <summary>
        /// Deletes the specified room.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <returns>the task</returns>
        public async Task Delete(Room room)
        {
            room.Deleted = true;

            await this.roomRepository.UpdateAsync(room);

            await this.publisher.EntityDeleted(room);
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        /// <param name="hostelId">The hostel identifier.</param>
        /// <param name="onlyPrivated">only private rooms</param>
        /// <param name="roomType">the room type</param>
        /// <param name="sortRoomBy">sort rooms by</param>
        /// <param name="minimumBeds">the minimum beds needed</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// the list of rooms
        /// </returns>
        public async Task<IPagedList<Room>> GetAll(
            string keyword = null,
            int? hostelId = null,
            bool? onlyPrivated = null,
            RoomType? roomType = null,
            SortRoomBy sortRoomBy = SortRoomBy.Name,
            int? minimumBeds = null,
            int page = 0,
            int pageSize = int.MaxValue)
        {
            var query = this.roomRepository.Table
                .Include(c => c.Hostel)
                .Where(c => !c.Deleted);

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(c => c.Name.Contains(keyword));
            }

            if (hostelId.HasValue)
            {
                query = query.Where(c => c.HostelId == hostelId.Value);
            }

            if (onlyPrivated.HasValue)
            {
                query = query.Where(c => c.IsPrivated.Equals(onlyPrivated.Value));
            }

            if (roomType.HasValue)
            {
                var roomTypeId = Convert.ToInt16(roomType);
                query = query.Where(c => c.RoomTypeId == roomTypeId);
            }

            if (minimumBeds.HasValue)
            {
                query = query.Where(c => c.Beds >= minimumBeds.Value);
            }

            switch (sortRoomBy)
            {
                case SortRoomBy.Name:
                    query = query.OrderBy(c => c.Name);
                    break;
                case SortRoomBy.Beds:
                    query = query.OrderBy(c => c.Beds);
                    break;
                case SortRoomBy.RoomType:
                    query = query.OrderBy(c => c.RoomTypeId);
                    break;
            }

            return await new PagedList<Room>().Async(query, page, pageSize);
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="track">if set to <c>true</c> [track].</param>
        /// <returns>
        /// the room
        /// </returns>
        public async Task<Room> GetById(int id, bool track = true)
        {
            var query = this.roomRepository.Table;

            if (!track)
            {
                query = query.AsNoTracking();
            }

            return await query.Include(c => c.Hostel)
                    .FirstOrDefaultAsync(c => c.Id == id && !c.Deleted);
        }

        /// <summary>
        /// Inserts the specified room.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task Insert(Room room)
        {
            room.CreationDateUtc = DateTime.UtcNow;

            try
            {
                await this.roomRepository.InsertAsync(room);

                await this.publisher.EntityInserted(room);
            }
            catch (DbUpdateException e)
            {
                var inner = (SqlException)e.InnerException;

                if (inner.Number == 547)
                {
                    this.Throw547Exception(inner);
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Updates the specified room.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task Update(Room room)
        {
            try
            {
                await this.roomRepository.UpdateAsync(room);

                await this.publisher.EntityUpdated(room);
            }
            catch (DbUpdateException e)
            {
                var inner = (SqlException)e.InnerException;

                if (inner.Number == 547)
                {
                    this.Throw547Exception(inner);
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Throw a foreign key exception
        /// </summary>
        /// <param name="ex">The exception.</param>
        /// <exception cref="Hostaliando.Business.Exceptions.HostaliandoException">the target</exception>
        private void Throw547Exception(SqlException ex)
        {
            var target = "Unknown";

            if (ex.Message.IndexOf("FK_Rooms_Hostels") != -1)
            {
                target = "Hostel";
            }
            else if (ex.Message.IndexOf("FK_Rooms_Users") != -1)
            {
                target = "User";
            }

            throw new HostaliandoException(target, HostaliandoExceptionCode.InvalidForeignKey);
        }
    }
}