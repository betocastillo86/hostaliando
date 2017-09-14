//-----------------------------------------------------------------------
// <copyright file="LogService.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Business.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Beto.Core.Data;
    using Beto.Core.Exceptions;
    using Beto.Core.Helpers;
    using Hostaliando.Business.Security;
    using Hostaliando.Data;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// The logger Service
    /// </summary>
    /// <seealso cref="Beto.Core.Exceptions.ILoggerService" />
    public class LogService : ILoggerService, ILogService
    {
        /// <summary>
        /// The HTTP context helper
        /// </summary>
        private readonly IHttpContextHelper httpContextHelper;

        /// <summary>
        /// The log repository
        /// </summary>
        private readonly IRepository<Log> logRepository;

        /// <summary>
        /// The work context
        /// </summary>
        private readonly IWorkContext workContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogService"/> class.
        /// </summary>
        /// <param name="logRepository">The log repository.</param>
        /// <param name="workContext">The work context.</param>
        /// <param name="httpContextHelper">The HTTP context helper.</param>
        public LogService(
            IRepository<Log> logRepository,
            IWorkContext workContext,
            IHttpContextHelper httpContextHelper)
        {
            this.logRepository = logRepository;
            this.workContext = workContext;
            this.httpContextHelper = httpContextHelper;
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        /// <returns>
        /// the task
        /// </returns>
        public async Task Clear()
        {
            var logs = this.logRepository.Table.ToList();
            foreach (var log in logs)
            {
                await this.logRepository.DeleteAsync(log);
            }
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// the logs
        /// </returns>
        public async Task<IPagedList<Log>> GetAll(string keyword = null, int page = 0, int pageSize = int.MaxValue)
        {
            var query = this.logRepository.Table
                .Include(c => c.User)
                .AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(c => c.FullMessage.Contains(keyword) || c.ShortMessage.Contains(keyword));
            }

            query = query.OrderByDescending(c => c.CreationDate);

            return await new PagedList<Log>().Async(query, page, pageSize);
        }

        /// <summary>
        /// Inserts the specified short message.
        /// </summary>
        /// <param name="shortMessage">The short message.</param>
        /// <param name="fullMessage">The full message.</param>
        public void Insert(string shortMessage, string fullMessage = "")
        {
            var log = this.GetLog(shortMessage, LogLevel.Error, fullMessage);
            this.logRepository.Insert(log);
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="shortMessage">The short message.</param>
        /// <param name="fullMessage">The full message.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task InsertAsync(string shortMessage, string fullMessage = "")
        {
            var log = this.GetLog(shortMessage, LogLevel.Error, fullMessage);
            await this.logRepository.InsertAsync(log);
        }

        /// <summary>
        /// Gets the log.
        /// </summary>
        /// <param name="shortMessage">The short message.</param>
        /// <param name="logLevel">The log level.</param>
        /// <param name="fullMessage">The full message.</param>
        /// <returns>the log</returns>
        private Log GetLog(string shortMessage, LogLevel logLevel, string fullMessage = "")
        {
            return new Log
            {
                ShortMessage = shortMessage,
                FullMessage = fullMessage,
                CreationDate = DateTime.UtcNow,
                LogLevel = logLevel,
                IpAddress = this.httpContextHelper.GetCurrentIpAddress(),
                UserId = this.workContext.CurrentUser?.Id,
                PageUrl = this.httpContextHelper.GetThisPageUrl(true)
            };
        }
    }
}