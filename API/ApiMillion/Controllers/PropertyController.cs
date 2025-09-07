using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Million.Application.Interfaces;
using Million.Application.Services;
using Million.DataInfraestructure.Repository;
using Million.DataInfraestructure;
using Million.Models.DTOs;
using Task.Application.Services;
using Million.Models.Response;

namespace ApiMillion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyController : ControllerBase
    {
        PropertyService createService()
        {
            var settings = Options.Create(new MongoDBSettings
            {
                ConnectionString = "mongodb://localhost:27017",
                DatabaseName = "MillionDatabase"
            });

            PropertyRepository repository = new PropertyRepository(settings);
            PropertyService service = new PropertyService(repository);
            return service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            RequestResultModel<IEnumerable<PropertyDTO>> response = new RequestResultModel<IEnumerable<PropertyDTO>>();
            try
            {
                var service = createService();
                var properties = await service.GetAsync();
                response.isSuccessful = true;

                response.result = properties;
                return Ok(response);
            }
            catch (Exception e)
            {
                response.isSuccessful = false;
                response.result = [];
                response.errorMessage = e.StackTrace + " " + e.Message;
                return BadRequest(response);
            }

           
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var service = createService();
            var propertyTrace = await service.GetByIdAsync(id);
            if (propertyTrace == null)
                return NotFound();
            return Ok(propertyTrace);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PropertyDTO property)
        {
            RequestResultModel<PropertyDTO> response = new RequestResultModel<PropertyDTO>();
            try
            {
                var service = createService();
                var created = await service.AddAsync(property);

                response.isSuccessful = true;
                response.result = created;

                return CreatedAtAction(nameof(GetById), new { id = created.IdProperty }, response);
            }
            catch (Exception e)
            {
                response.isSuccessful = false;
                response.result = null;
                response.errorMessage = e.StackTrace + " " + e.Message;
                return BadRequest(response);
            }
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] PropertyDTO property)
        {
            RequestResultModel<PropertyDTO> response = new RequestResultModel<PropertyDTO>();

            try
            {
                if (id != property.IdProperty)
                    return BadRequest("ID mismatch");

                var service = createService();
                var updated = await service.EditAsync(property);

                response.isSuccessful = true;
                response.result = updated;

                return Ok(response);
            }
            catch (Exception e)
            {
                response.isSuccessful = false;
                response.result = null;
                response.errorMessage = e.StackTrace + " " + e.Message;
                return BadRequest(response);
            }
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            RequestResultModel<string> response = new RequestResultModel<string>();
            try
            {
                var service = createService();
                await service.DeleteAsync(id);

                response.isSuccessful = true;
                response.result = id;

                return Ok(response);
            }
            catch (Exception e)
            {
                response.isSuccessful = false;
                response.result = string.Empty;
                response.errorMessage = e.StackTrace + " " + e.Message;
                return BadRequest(response);
            }
        }
    }
}
