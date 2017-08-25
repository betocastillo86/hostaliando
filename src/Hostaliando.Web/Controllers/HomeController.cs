//-----------------------------------------------------------------------
// <copyright file="HomeController.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Home Controller
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// The main method
        /// </summary>
        /// <returns>the view</returns>
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return this.View();
        }
    }
}