//-----------------------------------------------------------------------
// <copyright file="UsersController.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Controllers.Users
{
    using System.Threading.Tasks;
    using Beto.Core.Exceptions;
    using Beto.Core.Helpers;
    using Beto.Core.Web.Api.Controllers;
    using Beto.Core.Web.Api.Filters;
    using Hostaliando.Business.Exceptions;
    using Hostaliando.Business.Security;
    using Hostaliando.Business.Services;
    using Hostaliando.Data;
    using Hostaliando.Web.Infraestructure.Filters;
    using Hostaliando.Web.Models;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Users Controller
    /// </summary>
    /// <seealso cref="Beto.Core.Web.Api.Controllers.BaseApiController" />
    [Route("api/v1/users")]
    public class UsersController : BaseApiController
    {
        /// <summary>
        /// The user service
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// The work context
        /// </summary>
        private readonly IWorkContext workContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="messageExceptionFinder">The message exception finder.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="workContext">The work context.</param>
        public UsersController(
            IMessageExceptionFinder messageExceptionFinder,
            IUserService userService,
            IWorkContext workContext) : base(messageExceptionFinder)
        {
            this.userService = userService;
            this.workContext = workContext;
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>the action</returns>
        [HttpDelete]
        [Route("{id:int}")]
        [ServiceFilter(typeof(AuthorizeAdminAttribute))]
        public async Task<IActionResult> Delete(int id)
        {
            var user = this.userService.GetById(id);

            if (user == null)
            {
                return this.NotFound();
            }

            await this.userService.Delete(user);

            return this.Ok();
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>the user</returns>
        [HttpGet]
        [Route("{id:int}")]
        [ServiceFilter(typeof(AuthorizeAdminAttribute))]
        public async Task<IActionResult> Get(int id)
        {
            var user = this.userService.GetById(id, true);

            if (user == null)
            {
                return this.NotFound();
            }

            await Task.FromResult(0);

            return this.Ok(user.ToBaseModel());
        }

        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>the list</returns>
        [HttpGet]
        [ServiceFilter(typeof(AuthorizeAdminAttribute))]
        public async Task<IActionResult> Get([FromQuery] UserFilterModel filter)
        {
            filter = filter ?? new UserFilterModel();

            var users = await this.userService.GetAll(
                filter.Keyword,
                null,
                filter.Role,
                filter.HostelId,
                filter.OrderByEnum,
                filter.Page,
                filter.PageSize);

            var models = users.ToBaseModels();
            return this.Ok(models, users.HasNextPage, users.TotalCount);
        }

        /// <summary>
        /// Posts the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>the model</returns>
        [HttpPost]
        [ServiceFilter(typeof(AuthorizeAdminAttribute))]
        [RequiredModel]
        public async Task<IActionResult> Post([FromBody] UserModel model)
        {
            if (string.IsNullOrEmpty(model.Password))
            {
                this.ModelState.AddModelError("Password", "La clave es obligatoria");
                return this.BadRequest(this.ModelState);
            }

            var salt = StringHelpers.GetRandomString();

            var user = new User()
            {
                Name = model.Name,
                Email = model.Email,
                HostelId = model.Hostel?.Id,
                Password = StringHelpers.ToSha1(model.Password, salt),
                Role = model.Role.Value,
                Salt = salt,
                TimeZone = model.TimeZone
            };

            try
            {
                await this.userService.Insert(user);

                return this.Ok(new BaseModel { Id = user.Id });
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
        /// <returns>the task</returns>
        [HttpPut]
        [RequiredModel]
        [Route("{id:int}")]
        [ServiceFilter(typeof(AuthorizeAdminAttribute))]
        public async Task<IActionResult> Put(int id, [FromBody] UserModel model)
        {
            var user = this.userService.GetById(id);

            if (user == null)
            {
                return this.NotFound();
            }

            if (!this.workContext.CurrentUser.IsAdmin() && this.workContext.CurrentUserId != user.Id)
            {
                return this.Forbid();
            }

            user.Name = model.Name;
            user.Email = model.Email;
            user.TimeZone = model.TimeZone;
            user.Role = this.workContext.CurrentUser.IsAdmin() ? model.Role.Value : user.Role;
            user.HostelId = user.IsAdmin() ? model.Hostel?.Id : user.HostelId;

            if (!string.IsNullOrEmpty(model.Password))
            {
                user.Password = StringHelpers.ToSha1(model.Password, user.Salt);
            }

            try
            {
                await this.userService.Update(user);

                return this.Ok();
            }
            catch (HostaliandoException e)
            {
                return this.BadRequest(e);
            }
        }
    }
}