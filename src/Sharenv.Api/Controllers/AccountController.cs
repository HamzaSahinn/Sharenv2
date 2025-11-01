using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sharenv.Api.Models.Auth;
using Sharenv.Application.Interfaces;
using Sharenv.Application.Interfaces.AuthService;
using Sharenv.Application.Models;
using Sharenv.Application.Models.Token;
using Sharenv.Domain.Entities;

namespace Sharenv.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : SharenvBaseController
    {
        private IAuthService _authService;

        public AccountController(IAuthService authService, ICurrentUserService svc) : base(svc)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("login")]
        public ApiResponse LoginWithCookie(LoginRequestModel model)
        {
            var result = _authService.LoginWithAuthCookie(model.Username, model.Password);

            return new ApiResponse<LoginResult>()
            {
                Data = result.Value,
                Message = result.FormatErrors(),
                Success = result.IsSucceded
            };
        }

        [HttpPost]
        [Route("request-token")]
        public ApiResponse RequestAccessToken(LoginRequestModel model)
        {
            var result = _authService.LoginWithJwt(model.Username, model.Password);

            return new ApiResponse<LoginResult<GeneratedTokenResponse>>()
            {
                Data = result.Value,
                Message = result.FormatErrors(),
                Success = result.IsSucceded
            };
        }

        [Authorize]
        [HttpPost]
        public ApiResponse Logout()
        {
            _authService.Logout();

            return ApiResponse.SuccessResponse();
        }

        [Authorize]
        [HttpGet]
        public ApiResponse WhoAmI()
        {
            return ApiResponse<User>.SuccessResponse(_currentUserService.CurrentUser);
        }
    }
}
