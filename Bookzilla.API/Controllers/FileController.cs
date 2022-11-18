using Bookzilla.API.Mapper;
using Bookzilla.API.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookzilla.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IAlbumService _albumService;
        private readonly ICollectionService _collectionService;
        private readonly ISerieService _serieService;
        public FileController(ISerieService serieService, ICollectionService collectionService, IAlbumService albumService)
        {
            _albumService = albumService;
            _collectionService = collectionService;
            _serieService = serieService;
        }
        private bool CheckIfComicsFile(string FileName)
        {
            var extension = "." + FileName.Split('.')[FileName.Split('.').Length - 1];
            return (extension == ".cbz" || extension == ".cbr"); // Change the extension based on your need
        }
        private bool CheckIfImageFile(string FileName)
        {
            var extension = "." + FileName.Split('.')[FileName.Split('.').Length - 1];
            return (extension == ".jpg" || extension == ".jpeg" || extension == ".png"); // Change the extension based on your need
        }
        [HttpPost("CreateSerie/{name}"), DisableRequestSizeLimit]
        public async Task<ActionResult<SerieDTO>> CreateSerie(string name, IFormFile fileData)
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
        [HttpPost("UpdateSerieFile/{id}"), DisableRequestSizeLimit]
        public async Task<ActionResult<SerieDTO>> UpdateSerieFile(int id, IFormFile fileData)
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
        [HttpPost("CreateCollection/{name}"), DisableRequestSizeLimit]
        public async Task<ActionResult<CollectionDTO>> CreateCollection(string name, IFormFile fileData)
        {
            if (CheckIfImageFile(fileData.FileName))
            {
                CollectionDTO collec = new CollectionDTO() { Name = name };
                collec = await _collectionService.Add(collec, fileData.FileName, fileData.OpenReadStream());
                return Ok(collec);
            }
            else
            {
                return BadRequest(new { message = "Invalid File" });
            }
        }
        [HttpPost("UpdateCollectionFile/{id}"), DisableRequestSizeLimit]
        public async Task<ActionResult<CollectionDTO>> UpdateCollectionFile(int id, IFormFile fileData)
        {
            if (CheckIfImageFile(fileData.FileName))
            {
                var collec = await _collectionService.AddFile(id, fileData.FileName, fileData.OpenReadStream());
                return Ok(collec);
            }
            else
            {
                return BadRequest(new { message = "Invalid File" });
            }
        }
        [HttpPost("CreateAlbum"), DisableRequestSizeLimit]
        public async Task<ActionResult<AlbumDTO>> CreateAlbum(int order, int serieId, IFormFile fileData)
        {
            if (CheckIfComicsFile(fileData.FileName))
            {
                AlbumDTO collec = new AlbumDTO() { SerieId = serieId, Order = order, Name = Path.GetFileNameWithoutExtension(fileData.FileName), CurrentPage = 0, CoverArtPath = String.Empty, Path = String.Empty };
                collec = await _albumService.AddWithFile(collec, fileData.FileName, fileData.OpenReadStream());
                return Ok(collec);
            }
            else
            {
                return BadRequest(new { message = "Invalid File" });
            }
        }
    }
}
