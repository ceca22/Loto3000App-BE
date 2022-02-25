using Loto3000App.Domain.Models;
using Loto3000App.Models.Winning;
using Loto3000App.Services.Interfaces;
using Loto3000App.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Loto3000App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WinningController : ControllerBase
    {
        private IWinningService _winningService;
        public WinningController(IWinningService winningService)
        {
            _winningService = winningService;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Winning>> Get()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _winningService.GetAllEntities());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/<WinnerController>/5
        [HttpGet("{id}")]
        public ActionResult<WinningModel> Get(int id)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _winningService.GetEntityById(id));
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

        // POST api/<WinnerController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT api/<WinnerController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<WinnerController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _winningService.DeleteEntity(id);
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

        [HttpGet("board")]
        public ActionResult<IEnumerable<WinningModel>> WinnersBoard()
         {
            try
            {

                return StatusCode(StatusCodes.Status200OK, _winningService.WinnersBoard());
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

        [HttpGet("findwinners")]
        public ActionResult<bool> FindWinners()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _winningService.FindWinners());
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

        [HttpGet("50-gift-card")]
        public ActionResult<int> GetGiftCardFifty()
        {
            try
            {
                
                return StatusCode(StatusCodes.Status200OK, _winningService.GetGiftCardFifty());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("100-gift-card")]
        public ActionResult<int> GetGiftCardHundred()
        {
            try
            {

                return StatusCode(StatusCodes.Status200OK, _winningService.GetGiftCardHundred());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("vacation")]
        public ActionResult<int> GetVacation()
        {
            try
            {

                return StatusCode(StatusCodes.Status200OK, _winningService.GetVacation());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("car")]
        public ActionResult<int> GetCar()
        {
            try
            {

                return StatusCode(StatusCodes.Status200OK, _winningService.GetCar());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("tv")]
        public ActionResult<int> GetTv()
        {
            try
            {

                return StatusCode(StatusCodes.Status200OK, _winningService.GetTv());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
