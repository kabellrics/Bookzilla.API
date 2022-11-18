using Bookzilla.API.Mapper;
using Bookzilla.API.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bookzilla.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionController : ControllerBase
    {
        private readonly ICollectionService _collectionService;
        public CollectionController(ICollectionService collectionService)
        {
            _collectionService = collectionService;
        }
        // GET: api/<CollectionController>
        [HttpGet]
        public ActionResult<IEnumerable<CollectionDTO>> Get()
        {
            return Ok(_collectionService.Get());
        }

        // GET api/<CollectionController>/5
        [HttpGet("{id}")]
        public ActionResult<CollectionDTO> Get(int id)
        {
            return Ok(_collectionService.GetById(id));
        }

        // POST api/<CollectionController>
        [HttpPost]
        public ActionResult Post([FromBody] CollectionDTO value)
        {
            if (ModelState.IsValid)
            {
                _collectionService.Add(value);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // PUT api/<CollectionController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] CollectionDTO todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }
            try
            {
                _collectionService.Update(todoItem);
            }
            catch (DbUpdateConcurrencyException)
            {
                     throw;
            }

            return NoContent();
        }

        // DELETE api/<CollectionController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var todoItem = _collectionService.GetById(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            _collectionService.Remove(todoItem);
            return NoContent();
        }
    }
}
