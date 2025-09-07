using Microsoft.Extensions.Options;
using Million.Domain.Collections;
using Million.Domain.Repository;
using Million.Models.DTOs;
using MongoDB.Driver;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Million.DataInfraestructure.Repository
{
    public class PropertyRepository : IBaseRepository<PropertyDTO, string>
    {
        private readonly IMongoCollection<Property> _property;

        public PropertyRepository(IOptions<MongoDBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _property = database.GetCollection<Property>("Property");
        }

        public async Task<PropertyDTO> AddAsync(PropertyDTO entity)
        {
            var obj = new Property()
            {
               IdProperty = entity.IdProperty,
               Address = entity.Address,
               CodeInternal = entity.CodeInternal,
               IdOwner = entity.IdOwner,
               Name = entity.Name,
               Price = entity.Price,
               Year = entity.Year
            };
            await _property.InsertOneAsync(obj);
            return entity;
        }

        public async Task<string> DeleteAsync(string entity)
        {
            await _property.DeleteOneAsync(o => o.IdProperty.Equals(entity));
            return entity;
        }

        public async Task<PropertyDTO> EditAsync(PropertyDTO entity)
        {
            var result = await _property.Find(o => o.IdProperty.Equals(entity.IdProperty)).FirstOrDefaultAsync();
            var obj = new Property()
            {
                _id = result._id,
                IdProperty = entity.IdProperty,
                Address = entity.Address,
                CodeInternal = entity.CodeInternal,
                IdOwner = entity.IdOwner,
                Name = entity.Name,
                Price = entity.Price,
                Year = entity.Year
            };
            await _property.ReplaceOneAsync(o => o.IdProperty.Equals(entity.IdProperty), obj);
            return entity;
        }

        public async Task<List<PropertyDTO>> GetAsync()
        {
            var result = await _property.Find(_ => true).ToListAsync();
            return result.Select(x => new PropertyDTO()
            {
                IdProperty = x.IdProperty,
                Address = x.Address,
                CodeInternal = x.CodeInternal,
                IdOwner = x.IdOwner,
                Name = x.Name,
                Price = x.Price,
                Year = x.Year
            }).ToList();
        }

        public async Task<PropertyDTO> GetByIdAsync(string TId)
        {
            var result = await _property.Find(o => o.IdProperty.Equals(TId)).FirstOrDefaultAsync();
            return new PropertyDTO()
            {
                IdProperty = result.IdProperty,
                Address = result.Address,
                CodeInternal = result.CodeInternal,
                IdOwner = result.IdOwner,
                Name = result.Name,
                Price = result.Price,
                Year = result.Year
            };
        }

        public void saveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
