using HomeAccounting.Application.Interfaces.Identity;
using HomeAccounting.Application.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HomeAccounting.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;

        public UserController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Регистрация новго пользователя
        /// </summary>
        /// <remarks>
        /// На вход передаём логин, ник и пароль.
        ///  Возвращаем ID созданного пользователя
        ///  
        ///     Post/
        ///         {
        ///             "userId": 1
        ///         }
        ///     
        /// </remarks>
        /// <returns>
        ///</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="500">Иная ошибка</response>
        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
        {
            try
            {
                var response = await _authService.Register(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// Автороизация пользователя
        /// </summary>
        /// <remarks>
        /// На вход передаём логин и пароль.
        ///  Возвращаем имя пользователя и токен
        ///  
        ///     Post/
        ///         {
        ///             "userName": "goker111",
        ///             "token": "dkawdjahwudyhawyuidtwayjgey12g4hk12gh4uh12gh4jk12h4uj124.124k124j12i412uyuaydhuawdawodpawoiAWDHAWjdhawj"
        ///         }
        ///     
        /// </remarks>
        /// <returns>
        ///</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="500">Иная ошибка</response>
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(AuthRequest request)
        {
            try
            {
                var response = await _authService.Login(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// Обновление токена пользователя
        /// </summary>
        /// <remarks>
        /// Доступно только авторизованому пользователю
        /// Никакие параметры не передаём. Из headers тянем Bearer токен и потом меняем его.
        /// Возвращаем имя пользователя и новый токен
        /// 
        ///     Post/
        ///         {
        ///             "userName": "goker111",
        ///             token": "dkawdjahwudyhawyuidtwayjgey12g4hk12gh4uh12gh4jk12h4uj124.124k124j12i412uyuaydhuawdawodpawoiAWDHAWjdhawj"
        ///         }
        ///         
        /// </remarks>
        /// <returns>
        ///</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Токен не найден</response>
        [Authorize]
        [HttpPost("refresh")]
        public async Task<ActionResult<AuthResponse>> RefreshToken()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Token is missing.");
            }

            var response = await _authService.UserTokenRefrest(new RefreshTokenRequest { Token = token });

            return Ok(response);
        }
    }
}
