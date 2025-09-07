using Million.Application.Interfaces;
using Million.Models.DTOs;
using Million.Domain.Repository;

namespace Task.Application.Services
{
    public class PropertyService : IBaseService<PropertyDTO, string>
    {
        private readonly IBaseRepository<PropertyDTO, string> _propertyRepository;

        public PropertyService(IBaseRepository<PropertyDTO, string> taskRepository)
        {
            _propertyRepository = taskRepository;
        }

        public Task<PropertyDTO> AddAsync(PropertyDTO entity)
        {
            var result = _propertyRepository.AddAsync(entity); ;
            return result;
        }

        public Task<PropertyDTO> EditAsync(PropertyDTO entity)
        {
            var result = _propertyRepository.EditAsync(entity); ;
            return result;
        }

        public Task<List<PropertyDTO>> GetAsync()
        {
            return _propertyRepository.GetAsync(); ;
        }

        public async Task<string> DeleteAsync(string id)
        {
            var result = await _propertyRepository.DeleteAsync(id); ;
            return id;
        }

        public async Task<PropertyDTO> GetByIdAsync(string TId)
        {
            return await _propertyRepository.GetByIdAsync(TId); ;
        }

    }
}
