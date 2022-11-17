using Bookzilla.API.Mapper;
using Bookzilla.API.Services.Implémentation;
using Bookzilla.API.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookzilla.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SerieController : ControllerBase
    {
        private readonly ISerieService _serieService;
        public SerieController(ISerieService serieService)
        {
            _serieService = serieService;
        }
        // GET: api/<CollectionController>
        [HttpGet]
        public ActionResult<IEnumerable<SerieDTO>> Get()
        {
            return Ok(_serieService.Get());
        }
        // GET: api/<CollectionController>
        [HttpGet("SetDefaultCover")]
        public ActionResult SetDefaultCover()
        {
            _serieService.SetDefaultCoverForSeries();
            return Ok();
        }

        // GET api/<CollectionController>/5
        [HttpGet("{id}")]
        public ActionResult<SerieDTO> Get(int id)
        {
            return Ok(_serieService.GetById(id));
        }

        // POST api/<CollectionController>
        [HttpPost]
        public ActionResult Post([FromBody] SerieDTO value)
        {
            if (ModelState.IsValid)
            {
                _serieService.Add(value);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpPost("PostWithFile/{name}"), DisableRequestSizeLimit]
        public async Task<ActionResult<SerieDTO>> PostFile(string name, IFormFile fileData)
        {
            if (CheckIfImageFile(fileData.FileName))
            {
                SerieDTO collec = new SerieDTO() { Name = name };
                collec = _serieService.Add(collec);
                return Ok(collec);
            }
            else
            {
                return BadRequest(new { message = "Invalid File" });
            }
        }
        [HttpPost("UpdateFile/{id}"), DisableRequestSizeLimit]
        public async Task<ActionResult<SerieDTO>> UpdateFile(int id, IFormFile fileData)
        {
            if (CheckIfImageFile(fileData.FileName))
            {
                var collec = await _serieService.AddFile(id, fileData.FileName, fileData.OpenReadStream());
                return Ok(collec);
            }
            else
            {
                return BadRequest(new { message = "Invalid File" });
            }
        }
        // PUT api/<CollectionController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] SerieDTO todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }
            try
            {
                _serieService.Update(todoItem);
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
            var todoItem = _serieService.GetById(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            _serieService.Remove(todoItem);
            return NoContent();
        }
        private bool CheckIfImageFile(string FileName)
        {
            var extension = "." + FileName.Split('.')[FileName.Split('.').Length - 1];
            return (extension == ".jpg" || extension == ".jpeg" || extension == ".png"); // Change the extension based on your need
        }
    }
}
