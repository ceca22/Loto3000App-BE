using Loto3000App.Domain.Models;
using Loto3000App.Models.Session;
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

namespace Loto3000App.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {

        private ISessionService _sessionService;
        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }



        // GET: api/<UserController>
        [Authorize(Roles = "1")]
        [HttpGet]
        public ActionResult<IEnumerable<SessionModel>> Get()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _sessionService.GetAllEntities());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/<UserController>/5
        [Authorize(Roles = "1")]
        [HttpGet("{id}")]
        public ActionResult<SessionModel> Get(int id)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _sessionService.GetEntityById(id));
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
            }
        }

        [Authorize(Roles ="2")]
        [HttpGet("info")]
        public ActionResult<SessionModel> Info()
        {
            try
            {
                //var requestSession = Url.ActionLink();
                return StatusCode(StatusCodes.Status200OK, _sessionService.Info());
            }
            catch (SessionException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "2")]
        [HttpGet("status")]
        public ActionResult<bool> SessionStatus()
        {
            try
            {

                return StatusCode(StatusCodes.Status200OK, _sessionService.SessionStatus());
            }
            catch (SessionException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [Authorize(Roles = "1")]
        [HttpPost("start")]
        public IActionResult StartSession()
        {
            try
            {

                Claim nameIdentifier = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                List<Claim> roleClaim = HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.Role).ToList();
                
                string id = nameIdentifier.Value;
                List<string> roles = new List<string>();
                foreach (Claim role in roleClaim)
                {
                    roles.Add(role.Value);
                }
                

                _sessionService.AddEntity(roles, id);
                return StatusCode(StatusCodes.Status201Created, $"Session  successfully started at {DateTime.Now}");
                
            }
            catch (SessionException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [Authorize(Roles = "1")]
        [HttpPost("end")]
        public IActionResult EndSession()
        {
            try
            {
                Claim nameIdentifier = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                string id = nameIdentifier.Value;

                _sessionService.UpdateEntity(id);
                return StatusCode(StatusCodes.Status201Created, $"Session successfully ended at {DateTime.Now}");
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);

            }
            catch (SessionException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }




        // DELETE api/<SessionController>/5
        [Authorize(Roles = "1")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _sessionService.DeleteEntity(id);
                return StatusCode(StatusCodes.Status204NoContent);
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
