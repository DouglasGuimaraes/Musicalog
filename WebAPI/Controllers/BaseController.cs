using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.Interfaces.Service.Template;
using Models.Interfaces.ViewModel;

namespace WebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController<TEntity, TService> : ControllerBase
    where TEntity : class, IViewModel
    where TService : class, IService<TEntity>

    {
        private readonly IService<TEntity> _service;

        public BaseController(TService service)
        {
            this._service = service;
        }

        // GET: api/[controller]
        //[HttpGet]
        private async Task<ActionResult<IEnumerable<TEntity>>> Get()
        {
            SetResponseHeader();
            return Ok(await _service.GetAll());
        }

        // GET: api/[controller]/5
        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TEntity>> GetById(int id)
        {
            SetResponseHeader();

            try
            {
                var entity = await _service.Get(id);
                if (entity == null)
                    return NotFound($"Data not found for id: {id}.");

                return Ok(entity);
            }
            catch (System.Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: 500);
            }
        }

        // PUT: api/[controller]/5
        [HttpPut("{id}")]
        public virtual async Task<ActionResult<TEntity>> Put(int id, TEntity entity)
        {
            SetResponseHeader();

            try
            {
                if (id != entity.Id)
                    return UnprocessableEntity("Not expected information: Id.");

                var album = await _service.Update(entity);
                if (album == null)
                    return UnprocessableEntity("Not expected information.");

                return (album);
            }
            catch (System.Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: 500);
            }
        }

        //// POST: api/[controller]
        [HttpPost]
        public virtual async Task<ActionResult<TEntity>> Post(TEntity entity)
        {
            SetResponseHeader();

            try
            {
                var album = await _service.Add(entity);
                if (album == null)
                    return UnprocessableEntity("Not expected information.");

                return CreatedAtAction("Get", new { id = album.Id }, album);
            }
            catch (System.Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: 500);
            }
        }

        //// DELETE: api/[controller]/5
        [HttpDelete("{id}")]
        public virtual async Task<ActionResult<TEntity>> Delete(int id)
        {
            SetResponseHeader();

            try
            {
                var entity = await _service.Delete(id);
                if (entity == null)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (System.Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: 500);
            }
        }

        [NonAction]
        protected virtual void SetResponseHeader()
        {
            HttpContext.Response.Headers.Add("HealthCheckHeader_IsAlive", "true");
        }

    }

}
