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
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _albumService;
        public AlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
        }        // GET: api/<CollectionController>
        [HttpGet]
        public ActionResult<IEnumerable<AlbumDTO>> Get()
        {
            return Ok(_albumService.Get());
        }

        // GET api/<CollectionController>/5
        [HttpGet("{id}")]
        public ActionResult<AlbumDTO> Get(int id)
        {
            return Ok(_albumService.GetById(id));
        }
        [HttpPost("PostWithFile"), DisableRequestSizeLimit]
        public async Task<ActionResult<AlbumDTO>> PostFile(int order,int serieId, IFormFile fileData)
        {
            if (CheckIfComicsFile(fileData.FileName))
            {
                AlbumDTO collec = new AlbumDTO() { SerieId= serieId,Order = order,Name = Path.GetFileNameWithoutExtension(fileData.FileName),CurrentPage=0,CoverArtPath=String.Empty, Path = String.Empty };
                collec = await _albumService.AddWithFile(collec, fileData.FileName, fileData.OpenReadStream());
                return Ok(collec);
            }
            else
            {
                return BadRequest(new { message = "Invalid File" });
            }
        }
        // POST api/<CollectionController>
        [HttpPost]
        public ActionResult Post([FromBody] AlbumDTO value)
        {
            if (ModelState.IsValid)
            {
                _albumService.Add(value);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // PUT api/<CollectionController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] AlbumDTO todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }
            try
            {
                _albumService.Update(todoItem);
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
            var todoItem = _albumService.GetById(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            _albumService.Remove(todoItem);
            return NoContent();
        }
        private bool CheckIfComicsFile(string FileName)
        {
            var extension = "." + FileName.Split('.')[FileName.Split('.').Length - 1];
            return (extension == ".cbz" || extension == ".cbr"); // Change the extension based on your need
        }

    }
}
