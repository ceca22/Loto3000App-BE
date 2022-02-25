using Loto3000App.Models.Ticket;
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
    public class TicketController : ControllerBase
    {

        private ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        // GET: api/<TicketController>
        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<TicketModel>> Get()
        {
            try
            {
                return _ticketService.GetAllEntities().ToList();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            
        }

        // GET api/<TicketController>/5
        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<TicketModel> Get(int id)
        {
            try
            {

                return StatusCode(StatusCodes.Status200OK, _ticketService.GetEntityById(id));
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

        //user can check his own tickets 
        [HttpGet("myTickets")]
        public ActionResult<IEnumerable<TicketCombinationModel>> MyTickets()
        {
            try
            {
                Claim nameIdentifier = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                string id = nameIdentifier.Value;

                return StatusCode(StatusCodes.Status200OK, _ticketService.CheckMyTickets(id));
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            catch (TicketException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }


        }



        // POST api/<TicketController>
        [HttpPost]
        public IActionResult Post([FromBody] TicketModel ticketModel)
        {
            try
            {
                Claim nameIdentifier = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                Claim roleClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);

                string id = nameIdentifier.Value;
                string role = roleClaim.Value;

                _ticketService.AddEntity(ticketModel, id, role);
                return StatusCode(StatusCodes.Status200OK, "Ticket successfully created! \r\n Go to draw/board and check the Winners board. Your name will be displayed if you have won any prizes!");
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            catch (TicketException ex)
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

        [HttpPost("automatic")]
        public IActionResult GenerateATicket()
        {
            try
            {
                Claim nameIdentifier = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                Claim roleClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);
                

                string id = nameIdentifier.Value;
                string role = roleClaim.Value;

                string newTicket = _ticketService.GenerateTicket(id, role);
                return StatusCode(StatusCodes.Status200OK, $"Ticket successfully created! \r\n {newTicket} \r\n Go to draw / board and check the Winners board.Your name will be displayed if you have won any prizes!");

            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            catch (TicketException ex)
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

        

        // PUT api/<TicketController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<TicketController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _ticketService.DeleteEntity(id);
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

        [Authorize]
        [HttpGet("tickets")]
        public ActionResult<IEnumerable<TicketCombinationModel>> GetTicketForCurrentSession()
        {
            try
            {
                
                return StatusCode(StatusCodes.Status200OK, _ticketService.GetTickets());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }


        }

        [Authorize]
        [HttpGet("userCount")]
        public ActionResult<int> GetUserCount()
        {
            try
            {

                return StatusCode(StatusCodes.Status200OK, _ticketService.GetUsersEnrolled());
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            catch (TicketException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }


        }








    }
}
