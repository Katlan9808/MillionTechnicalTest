using Million.Application.Interfaces;
using Million.Models.DTOs;
using Million.Domain.Repository;

namespace Million.Application.Services
{
    public class OwnerService : IBaseRepository<OwnerDTO, string>
    {
        private readonly IBaseRepository<OwnerDTO, string> _ownerRepository;

        public OwnerService(IBaseRepository<OwnerDTO, string> taskRepository)
        {
            _ownerRepository = taskRepository;
        }

        public Task<OwnerDTO> AddAsync(OwnerDTO entity)
        {
            var result = _ownerRepository.AddAsync(entity); ;
            return result;
        }

        public Task<OwnerDTO> EditAsync(OwnerDTO entity)
        {
            var result = _ownerRepository.EditAsync(entity); ;
            return result;
        }

        public Task<List<OwnerDTO>> GetAsync()
        {
            return _ownerRepository.GetAsync(); ;
        }

        public async Task<string> DeleteAsync(string id)
        {
            var result = await _ownerRepository.DeleteAsync(id); ;
            return id;
        }

        public async Task<OwnerDTO> GetByIdAsync(string TId)
        {
            return await _ownerRepository.GetByIdAsync(TId); ;
        }

        public void saveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
