using Loto3000App.Models.Prize;
using Loto3000App.Services.Interfaces;
using Loto3000App.Shared.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Loto3000App.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PrizeController : ControllerBase
    {
        private IService<PrizeModel> _prizeService;
        public PrizeController(IService<PrizeModel> prizeService)
        {
            _prizeService = prizeService;
        }
        // GET: api/<PrizeController>
        [HttpGet]
        public ActionResult<IEnumerable<PrizeModel>> Get()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _prizeService.GetAllEntities());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/<PrizeController>/5
        [HttpGet("{id}")]
        public ActionResult<PrizeModel> Get(int id)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _prizeService.GetEntityById(id));
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

        // POST api/<PrizeController>
        [HttpPost]
        public IActionResult Post([FromBody] PrizeModel prize)
        {
            try
            {
                _prizeService.AddEntity(prize);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (PrizeException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);

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

        // PUT api/<PrizeController>/5
        [HttpPut]
        public IActionResult Put([FromBody] PrizeModel entity)
        {
            try
            {
                _prizeService.UpdateEntity(entity);
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);

            }
            catch (PrizeException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE api/<PrizeController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _prizeService.DeleteEntity(id);
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
