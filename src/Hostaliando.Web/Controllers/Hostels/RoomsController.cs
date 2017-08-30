//-----------------------------------------------------------------------
// <copyright file="RoomsController.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Controllers.Hostels
{
    using System.Threading.Tasks;
    using Beto.Core.Exceptions;
    using Beto.Core.Web.Api.Controllers;
    using Beto.Core.Web.Api.Filters;
    using Hostaliando.Business.Exceptions;
    using Hostaliando.Business.Security;
    using Hostaliando.Business.Services;
    using Hostaliando.Data;
    using Hostaliando.Web.Models;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Rooms Controller
    /// </summary>
    /// <seealso cref="Beto.Core.Web.Api.Controllers.BaseApiController" />
    [Route("api/v1/rooms")]
    public class RoomsController : BaseApiController
    {
        /// <summary>
        /// The room service
        /// </summary>
        private readonly IRoomService roomService;

        /// <summary>
        /// The work context
        /// </summary>
        private readonly IWorkContext workContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoomsController"/> class.
        /// </summary>
        /// <param name="messageExceptionFinder">The message exception finder.</param>
        /// <param name="workContext">The work context.</param>
        /// <param name="roomService">The room service.</param>
        public RoomsController(
            IMessageExceptionFinder messageExceptionFinder,
            IWorkContext workContext,
            IRoomService roomService) : base(messageExceptionFinder)
        {
            this.workContext = workContext;
            this.roomService = roomService;
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>the action</returns>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var room = await this.roomService.GetById(id);

            if (room == null)
            {
                return this.NotFound();
            }

            if (!this.workContext.CurrentUser.IsAdmin() && this.workContext.CurrentUser.HostelId != room.HostelId)
            {
                return this.Forbid();
            }

            return this.Ok(room.ToModel());
        }

        /// <summary>
        /// Gets the rooms by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>the rooms</returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] RoomFilterModel filter)
        {
            //// A non admin user can't filter by hostel that is not owner
            if (!this.workContext.CurrentUser.IsAdmin() && (!filter.HostelId.HasValue || this.workContext.CurrentUser.HostelId != filter.HostelId.Value))
            {
                return this.Forbid();
            }

            var rooms = await this.roomService.GetAll(
                filter.Keyword,
                filter.HostelId,
                filter.OnlyPrivated,
                filter.RoomType,
                filter.OrderByEnum,
                filter.Page,
                filter.PageSize);

            var models = rooms.ToModels();

            return this.Ok(models, rooms.HasNextPage, rooms.TotalCount);
        }

        /// <summary>
        /// Posts the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>the action</returns>
        [HttpPost]
        [RequiredModel]
        public async Task<IActionResult> Post([FromBody] RoomModel model)
        {
            if (!this.workContext.CurrentUser.IsAdmin() && this.workContext.CurrentUser.HostelId != model.Hostel.Id)
            {
                return this.Forbid();
            }

            var room = new Room
            {
                Name = model.Name,
                Active = model.Active,
                Beds = model.Beds,
                IsPrivated = model.IsPrivated,
                RoomType = model.RoomType.Value,
                HostelId = model.Hostel.Id,
                UserId = this.workContext.CurrentUserId
            };

            try
            {
                await this.roomService.Insert(room);

                return this.Ok(new BaseModel() { Id = room.Id });
            }
            catch (HostaliandoException e)
            {
                return this.BadRequest(e);
            }
        }

        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns>the result</returns>
        [HttpPut]
        [RequiredModel]
        [Route("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] RoomModel model)
        {
            var room = await this.roomService.GetById(id);

            if (room == null)
            {
                return this.NotFound();
            }

            if (!this.workContext.CurrentUser.IsAdmin() && this.workContext.CurrentUser.HostelId != room.HostelId)
            {
                return this.Forbid();
            }

            room.Name = model.Name;
            room.Active = model.Active;
            room.Beds = model.Beds;
            room.IsPrivated = model.IsPrivated;
            room.RoomType = model.RoomType.Value;

            await this.roomService.Update(room);

            return this.Ok();
        }
    }
}