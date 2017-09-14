//-----------------------------------------------------------------------
// <copyright file="HostelEarningsController.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Controllers.Hostels
{
    using System;
    using System.Threading.Tasks;
    using Beto.Core.Exceptions;
    using Beto.Core.Web.Api.Controllers;
    using Hostaliando.Business.Security;
    using Hostaliando.Business.Services;
    using Hostaliando.Web.Models;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Hostel Earnings Controller
    /// </summary>
    /// <seealso cref="Beto.Core.Web.Api.Controllers.BaseApiController" />
    [Route("api/v1/hostels/{id:int}/earnings")]
    public class HostelEarningsController : BaseApiController
    {
        /// <summary>
        /// The booking service
        /// </summary>
        private readonly IBookingService bookingService;

        /// <summary>
        /// The hostel service
        /// </summary>
        private readonly IHostelService hostelService;

        /// <summary>
        /// The work context
        /// </summary>
        private readonly IWorkContext workContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="HostelEarningsController"/> class.
        /// </summary>
        /// <param name="messageExceptionFinder">The message exception finder.</param>
        /// <param name="bookingService">The booking service.</param>
        /// <param name="workContext">The work context.</param>
        /// <param name="hostelService">The hostel service.</param>
        public HostelEarningsController(
            IMessageExceptionFinder messageExceptionFinder,
            IBookingService bookingService,
            IWorkContext workContext,
            IHostelService hostelService) : base(messageExceptionFinder)
        {
            this.bookingService = bookingService;
            this.workContext = workContext;
            this.hostelService = hostelService;
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>the action</returns>
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var hostel = await this.hostelService.GetById(id);

            if (hostel == null)
            {
                return this.NotFound();
            }

            if (!this.workContext.CurrentUser.IsAdmin() && this.workContext.CurrentUser.HostelId != hostel.Id)
            {
                return this.Forbid();
            }

            var bookings = await this.bookingService.GetAll(
                hostelId: hostel.Id,
                fromDate: DateTime.Today.AddDays(-60),
                toDate: DateTime.Today,
                notStatus: Data.BookingStatus.Canceled);

            var model = new HostelEarningsModel();

            foreach (var booking in bookings)
            {
                if (booking.FromDate == DateTime.Today)
                {
                    model.Today += booking.TotalPrice;
                    model.TwoDays += booking.TotalPrice;
                    model.Month += booking.TotalPrice;
                    model.TwoWeeks += booking.TotalPrice;
                    model.Week += booking.TotalPrice;
                }
                else if (booking.FromDate >= DateTime.Today.AddDays(-1))
                {
                    model.Month += booking.TotalPrice;
                    model.TwoDays += booking.TotalPrice;
                    model.TwoWeeks += booking.TotalPrice;
                    model.Week += booking.TotalPrice;
                }
                else if (booking.FromDate >= DateTime.Today.AddDays(-7))
                {
                    model.Month += booking.TotalPrice;
                    model.TwoWeeks += booking.TotalPrice;
                    model.Week += booking.TotalPrice;
                }
                else if (booking.FromDate >= DateTime.Today.AddDays(-15))
                {
                    model.Month += booking.TotalPrice;
                    model.TwoWeeks += booking.TotalPrice;
                }
                else if (booking.FromDate >= DateTime.Today.AddDays(-30))
                {
                    model.Month += booking.TotalPrice;
                }

                model.TwoMonths += booking.TotalPrice;
            }

            return this.Ok(model);
        }
    }
}