using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Million.Application.Interfaces;
using Million.DataInfraestructure;
using Million.DataInfraestructure.Repository;
using Million.Domain.Repository;
using Million.Models.DTOs;
using Million.Models.Response;
using MongoDB.Bson;
using SharpCompress.Common;
using System.Reflection;
using Task.Application.Services;

namespace ApiMillion.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PropertyTraceController : ControllerBase
    {
        PropertyTraceService createService()
        {
            var settings = Options.Create(new MongoDBSettings
            {
                ConnectionString = "mongodb://localhost:27017",
                DatabaseName = "MillionDatabase"
            });

            PropertyTraceRepository repository = new PropertyTraceRepository(settings);
            PropertyTraceService service = new PropertyTraceService(repository);
            return service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            RequestResultModel<IEnumerable<PropertyTraceDTO>> response = new RequestResultModel<IEnumerable<PropertyTraceDTO>>();
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
            RequestResultModel<PropertyTraceDTO> response = new RequestResultModel<PropertyTraceDTO>();
            try
            {
                var service = createService();
                var propertyTrace = await service.GetByIdAsync(id);
                if (propertyTrace == null)
                    return NotFound();

                response.isSuccessful = true;
                response.result = propertyTrace;
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
        public async Task<IActionResult> Create([FromBody] PropertyTraceDTO propertyTrace)
        {
            RequestResultModel<PropertyTraceDTO> response = new RequestResultModel<PropertyTraceDTO>();
            try
            {
                var service = createService();
                var created = await service.AddAsync(propertyTrace);

                response.isSuccessful = true;
                response.result = created;

                return CreatedAtAction(nameof(GetById), new { id = created.IdPropertyTrace }, response);
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
        public async Task<IActionResult> Update(string id, [FromBody] PropertyTraceDTO propertyTrace)
        {
            RequestResultModel<PropertyTraceDTO> response = new RequestResultModel<PropertyTraceDTO>();
            try
            {
                if (id != propertyTrace.IdPropertyTrace)
                    return BadRequest("ID mismatch");

                var service = createService();
                var updated = await service.EditAsync(propertyTrace);

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
