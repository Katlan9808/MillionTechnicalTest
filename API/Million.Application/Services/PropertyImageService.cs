using Million.Application.Interfaces;
using Million.Models.DTOs;
using Million.Domain.Repository;

namespace Task.Application.Services
{
    public class PropertyImageService : IBaseService<PropertyImageDTO, string>
    {
        private readonly IBaseRepository<PropertyImageDTO, string> _propertyImageRepository;

        public PropertyImageService(IBaseRepository<PropertyImageDTO, string> taskRepository)
        {
            _propertyImageRepository = taskRepository;
        }

        public Task<PropertyImageDTO> AddAsync(PropertyImageDTO entity)
        {
            var result = _propertyImageRepository.AddAsync(entity); ;
            return result;
        }

        public Task<PropertyImageDTO> EditAsync(PropertyImageDTO entity)
        {
            var result = _propertyImageRepository.EditAsync(entity); ;
            return result;
        }

        public Task<List<PropertyImageDTO>> GetAsync()
        {
            return _propertyImageRepository.GetAsync(); ;
        }

        public async Task<string> DeleteAsync(string id)
        {
            var result = await _propertyImageRepository.DeleteAsync(id); ;
            return id;
        }

        public async Task<PropertyImageDTO> GetByIdAsync(string TId)
        {
            return await _propertyImageRepository.GetByIdAsync(TId); ;
        }

    }
}
