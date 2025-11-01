using Microsoft.AspNetCore.Mvc;
using Sharenv.Application.Interfaces;
using Sharenv.Application.Models;

namespace Sharenv.Api.Controllers
{
    public class SharenvBaseController : ControllerBase
    {
        protected ICurrentUserService _currentUserService;

        public SharenvBaseController(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        /// <summary>
        /// Execute given action in an exception handiling context
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        protected Result Execute(Action<Result> action)
        {
            var result = new Result();

            try
            {
                action(result);
            }
            catch (Exception ex)
            {
                Core.ExceptionManager.HandleException(ex);
                result.AddException(ex);
            }

            return result;
        }

        /// <summary>
        /// Execute given action in an exception handiling context
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        protected Result<T> Execute<T>(Action<Result<T>> action)
        {
            var result = new Result<T>();

            try
            {
                action(result);
            }
            catch (Exception ex)
            {
                Core.ExceptionManager.HandleException(ex);
                result.AddException(ex);
            }

            return result;
        }
    }
}
