using Million.Application.Interfaces;
using Million.Models.DTOs;
using Million.Domain.Repository;

namespace Task.Application.Services
{
    public class PropertyTraceService : IBaseService<PropertyTraceDTO, string>
    {
        private readonly IBaseRepository<PropertyTraceDTO, string> _propertyRepository;

        public PropertyTraceService(IBaseRepository<PropertyTraceDTO, string> taskRepository)
        {
            _propertyRepository = taskRepository;
        }

        public Task<PropertyTraceDTO> AddAsync(PropertyTraceDTO entity)
        {
            var result = _propertyRepository.AddAsync(entity); ;
            return result;
        }

        public Task<PropertyTraceDTO> EditAsync(PropertyTraceDTO entity)
        {
            var result = _propertyRepository.EditAsync(entity); ;
            return result;
        }

        public Task<List<PropertyTraceDTO>> GetAsync()
        {
            return _propertyRepository.GetAsync(); ;
        }

        public async Task<string> DeleteAsync(string id)
        {
            var result = await _propertyRepository.DeleteAsync(id); ;
            return id;
        }

        public async Task<PropertyTraceDTO> GetByIdAsync(string TId)
        {
            return await _propertyRepository.GetByIdAsync(TId); ;
        }

    }
}
