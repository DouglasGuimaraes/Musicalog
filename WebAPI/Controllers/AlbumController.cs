using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using Models.Interfaces.Service;
using Models.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : BaseController<AlbumViewModel, IAlbumService>
    {
        private readonly IAlbumService _service;
        public AlbumController(IAlbumService service) : base(service)
        {
            _service = service;
        }

        // GET: api/[controller]
        [HttpGet]
        public async Task<ActionResult<List<AlbumViewModel>>> GetByFilter(string title, string artistName)
        {
            base.SetResponseHeader();

            try
            {
                var list = await _service.GetByFilter(title, artistName);
                return Ok(list);
            }
            catch (System.Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: 500);
            }
        }

    }
}
