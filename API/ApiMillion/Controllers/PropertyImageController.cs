using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Million.Application.Interfaces;
using Million.DataInfraestructure.Repository;
using Million.DataInfraestructure;
using Million.Models.DTOs;
using Task.Application.Services;
using Million.Models.Response;
using Million.Domain.Collections;

namespace ApiMillion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyImageController : ControllerBase
    {

        PropertyImageService createService()
        {
            var settings = Options.Create(new MongoDBSettings
            {
                ConnectionString = "mongodb://localhost:27017",
                DatabaseName = "MillionDatabase"
            });

            PropertyImageRepository repository = new PropertyImageRepository(settings);
            PropertyImageService service = new PropertyImageService(repository);
            return service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            RequestResultModel<IEnumerable<PropertyImageDTO>> response = new RequestResultModel<IEnumerable<PropertyImageDTO>>();
            try
            {
                var service = createService();
                var propertiesTrace = await service.GetAsync();

                response.isSuccessful = true;
                response.result = propertiesTrace;
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
            RequestResultModel<PropertyImageDTO> response = new RequestResultModel<PropertyImageDTO>();
            try
            {
                var service = createService();
                var propertyImage = await service.GetByIdAsync(id);
                if (propertyImage == null)
                    return NotFound();

                response.isSuccessful = true;
                response.result = propertyImage;
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PropertyImageDTO propertyImage)
        {
            RequestResultModel<PropertyImageDTO> response = new RequestResultModel<PropertyImageDTO>();
            try
            {
                var service = createService();
                var created = await service.AddAsync(propertyImage);

                response.isSuccessful = true;
                response.result = created;

                return CreatedAtAction(nameof(GetById), new { id = created.IdPropertyImage }, response);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] PropertyImageDTO propertyImage)
        {
            RequestResultModel<PropertyImageDTO> response = new RequestResultModel<PropertyImageDTO>();
            try
            {
                if (id != propertyImage.IdPropertyImage)
                    return BadRequest("ID mismatch");

                var service = createService();
                var updated = await service.EditAsync(propertyImage);

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
