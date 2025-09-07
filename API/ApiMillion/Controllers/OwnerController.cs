using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Million.Application.Interfaces;
using Million.Application.Services;
using Million.DataInfraestructure;
using Million.DataInfraestructure.Repository;
using Million.Domain.Collections;
using Million.Models.DTOs;
using Million.Models.Response;
using Task.Application.Services;

namespace ApiMillion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnerController : ControllerBase
    {
        OwnerService createService()
        {
            var settings = Options.Create(new MongoDBSettings
            {
                ConnectionString = "mongodb://localhost:27017",
                DatabaseName = "MillionDatabase"
            });

            OwnerRepository repository = new OwnerRepository(settings);
            OwnerService service = new OwnerService(repository);
            return service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            RequestResultModel<IEnumerable<OwnerDTO>> response = new RequestResultModel<IEnumerable<OwnerDTO>>();
            try
            {
                var service = createService();
                var owners = await service.GetAsync();

                response.isSuccessful = true;
                response.result = owners;
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
            RequestResultModel<OwnerDTO> response = new RequestResultModel<OwnerDTO>();
            try
            {
                var service = createService();
                var owner = await service.GetByIdAsync(id);
                if (owner == null)
                    return NotFound();


                response.isSuccessful = true;
                response.result = owner;
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
        public async Task<IActionResult> Create([FromBody] OwnerDTO owner)
        {
            RequestResultModel<OwnerDTO> response = new RequestResultModel<OwnerDTO>();
            try
            {
                var service = createService();
                var created = await service.AddAsync(owner);
                response.isSuccessful = true;
                response.result = created;

                return CreatedAtAction(nameof(GetById), new { id = created.IdOwner }, response);
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
        public async Task<IActionResult> Update(string id, [FromBody] OwnerDTO owner)
        {
            RequestResultModel<OwnerDTO> response = new RequestResultModel<OwnerDTO>();
            try
            {
                if (id != owner.IdOwner)
                    return BadRequest("ID mismatch");

                var service = createService();
                var updated = await service.EditAsync(owner);

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

                return Ok(id);
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
