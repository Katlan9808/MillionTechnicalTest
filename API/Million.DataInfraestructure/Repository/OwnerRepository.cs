using Microsoft.Extensions.Options;
using Million.Domain.Collections;
using Million.Domain.Repository;
using Million.Models.DTOs;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Million.DataInfraestructure.Repository
{
    public class OwnerRepository : IBaseRepository<OwnerDTO, string>
    {
        private readonly IMongoCollection<Owner> _owner;

        public OwnerRepository(IOptions<MongoDBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _owner = database.GetCollection<Owner>("Owner");
        }

        public async Task<OwnerDTO> AddAsync(OwnerDTO entity)
        {
            
            var obj = new Owner()
            {
                IdOwner = entity.IdOwner,
                Address = entity.Address,
                Birthday = entity.Birthday,
                Name = entity.Name,
                Photo = entity.Photo
            };
            await _owner.InsertOneAsync(obj);
            return entity;
        }

        public async Task<string> DeleteAsync(string entity)
        {
            await _owner.DeleteOneAsync(o => o.IdOwner.Equals(entity));
            return entity;
        }

        public async Task<OwnerDTO> EditAsync(OwnerDTO entity)
        {
            var obj = new Owner()
            {
                IdOwner = entity.IdOwner,
                Address = entity.Address,
                Birthday = entity.Birthday,
                Name = entity.Name,
                Photo = entity.Photo
            };
            await _owner.ReplaceOneAsync(o => o.IdOwner.Equals(entity.IdOwner), obj);
            return entity;
        }

        public async Task<List<OwnerDTO>> GetAsync()
        {
            var result = await _owner.Find(_ => true).ToListAsync();
            return result.Select(x => new OwnerDTO
            {
                IdOwner = x.IdOwner,
                Address = x.Address,
                Birthday = x.Birthday,
                Name = x.Name,
                Photo = x.Photo
            }).ToList();

        }

        public async Task<OwnerDTO> GetByIdAsync(string TId)
        {
            var result =  await _owner.Find(o => o.IdOwner.Equals(TId)).FirstOrDefaultAsync();
            return new OwnerDTO {
                IdOwner = result.IdOwner,
                Address = result.Address,
                Birthday = result.Birthday,
                Name = result.Name,
                Photo = result.Photo
            };
        }

        public void saveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
