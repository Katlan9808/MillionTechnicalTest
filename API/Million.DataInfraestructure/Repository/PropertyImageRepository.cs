using Microsoft.Extensions.Options;
using Million.Domain.Collections;
using Million.Domain.Repository;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Million.Domain.Repository;
using Million.Models.DTOs;
using SharpCompress.Common;

namespace Million.DataInfraestructure.Repository
{
    public class PropertyImageRepository : IBaseRepository<PropertyImageDTO, string>
    {
        private readonly IMongoCollection<PropertyImage> _propertyImage;

        public PropertyImageRepository(IOptions<MongoDBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _propertyImage = database.GetCollection<PropertyImage>("PropertyImage");
        }

        public async Task<PropertyImageDTO> AddAsync(PropertyImageDTO entity)
        {
            var obj = new PropertyImage()
            {
              IdPropertyImage = entity.IdPropertyImage,
              Enabled = entity.Enabled,
              File = entity.File,
              IdProperty = entity.IdProperty,              
            };
            await _propertyImage.InsertOneAsync(obj);
            return entity;
        }

        public async Task<string> DeleteAsync(string entity)
        {
            await _propertyImage.DeleteOneAsync(o => o.IdPropertyImage.Equals(entity));
            return entity;
        }

        public async Task<PropertyImageDTO> EditAsync(PropertyImageDTO entity)
        {
            var obj = new PropertyImage()
            {
                IdPropertyImage = entity.IdPropertyImage,
                Enabled = entity.Enabled,
                File = entity.File,
                IdProperty = entity.IdProperty,
            };
            await _propertyImage.ReplaceOneAsync(o => o.IdPropertyImage.Equals(entity.IdPropertyImage), obj);
            return entity;
        }

        public async Task<List<PropertyImageDTO>> GetAsync()
        {
            var result = await _propertyImage.Find(_ => true).ToListAsync();
            return result.Select(x => new PropertyImageDTO()
            {
                IdPropertyImage = x.IdPropertyImage,
                Enabled = x.Enabled,
                File = x.File,
                IdProperty = x.IdProperty,
            }).ToList();
        }

        public async Task<PropertyImageDTO> GetByIdAsync(string TId)
        {
            var result = await _propertyImage.Find(o => o.IdPropertyImage.Equals(TId)).FirstOrDefaultAsync();
            return new PropertyImageDTO()
            {
                IdPropertyImage = result.IdPropertyImage,
                Enabled = result.Enabled,
                File = result.File,
                IdProperty = result.IdProperty,
            };
        }

        public void saveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
