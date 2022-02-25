
using Loto3000App.Models.UserEntity;
using Loto3000App.Services.Interfaces;
using Loto3000App.Shared.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Loto3000App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        
        private IService<UserRegisterModel> _userService;

        public UserController(IService<UserRegisterModel> userService)
        {
            _userService = userService;
        }

        

        // GET: api/<UserController>
        
        [HttpGet]
        public ActionResult<IEnumerable<UserRegisterModel>> Get()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _userService.GetAllEntities());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/<UserController>/5
        
        [HttpGet("{id}")]
        public ActionResult<UserRegisterModel> Get(int id)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _userService.GetEntityById(id));
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("role")]
        public IActionResult GetUserRole()
        {
            try
            {
                
                Claim roleClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);
                string role = roleClaim.Value;
                return StatusCode(StatusCodes.Status200OK, role);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/<UserController>/
        [HttpPut]
        public IActionResult Put([FromBody] UserRegisterModel userRegister)
        {
            try
            {
                _userService.UpdateEntity(userRegister);
                return StatusCode(StatusCodes.Status200OK, "User Updated!");
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            catch (UserException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE api/<UserController>/5
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _userService.DeleteEntity(id);
                return StatusCode(StatusCodes.Status200OK, "User Deleted!");
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            catch (Exception ex)
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
