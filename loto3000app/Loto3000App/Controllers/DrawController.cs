using Loto3000App.Models.Draw;
using Loto3000App.Models.Winning;
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
    public class DrawController : ControllerBase
    {
        private IDrawService _drawService;
        public DrawController(IDrawService drawService)
        {
            _drawService = drawService;
        }
        // GET: api/<DrawController>
        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<DrawModel>> Get()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _drawService.GetAllEntities());
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/<DrawController>/5
        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<DrawCombinationModel> Get(int id)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _drawService.GetEntityById(id));
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


        [Authorize]
        [HttpGet("drawStatus")]
        public ActionResult<bool> GetDrawStatus()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _drawService.DrawIsMade());
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

        // POST api/<DrawController>
        [Authorize]
        [HttpPost("start")]
        public IActionResult Post()
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


                string drawCombo = _drawService.AddEntity(id, roles);
                return StatusCode(StatusCodes.Status200OK, $"Draw Combination:{drawCombo}");
            }
            catch (DrawException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
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



        // PUT api/<DrawController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<DrawController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _drawService.DeleteEntity(id);
                return StatusCode(StatusCodes.Status204NoContent);
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



        








    }
}
