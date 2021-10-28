using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Soaps.Dto;

namespace Soaps.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [Obsolete]
        public LoginController()
        {
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UserAdminDto> Post([FromBody] UserAdminDto userAdminDto)
        {
            try
            {
                UserAdminDto newUserAdminDto = new UserAdminDto
                {
                    User = userAdminDto.User.Trim(),
                    Password = userAdminDto.Password.Trim(),
                    IsLogin = (userAdminDto.User.Trim() == "VidaSana" && userAdminDto.Password.Trim() == "VidaSana5.")
                };

                if (!newUserAdminDto.IsLogin)
                {
                    if (userAdminDto.User.Trim() != "VidaSana")
                    {
                        newUserAdminDto.Message = "User is not valid";
                    }
                    else if (userAdminDto.Password.Trim() != "VidaSana5.")
                    {
                        newUserAdminDto.Message = "Password is not valid";
                    }
                }
                else
                {
                    newUserAdminDto.Message = "Login was successed";
                }
                
                return Ok(newUserAdminDto);
            }
            catch (ArgumentException)
            {
                return this.ValidationProblem();
            }
            catch
            {
                return this.StatusCode(500, "Internal Server error");
            }
        }
    }
}
