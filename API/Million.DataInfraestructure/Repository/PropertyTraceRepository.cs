using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using Million.Domain.Collections;
using Million.Domain.Repository;
using Million.Models.DTOs;
using MongoDB.Driver;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Million.DataInfraestructure.Repository
{
    public class PropertyTraceRepository : IBaseRepository<PropertyTraceDTO, string>
    {
        private readonly IMongoCollection<PropertyTrace> _propertyTrace;

        public PropertyTraceRepository(IOptions<MongoDBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _propertyTrace = database.GetCollection<PropertyTrace>("PropertyTrace");
        }

        public async Task<PropertyTraceDTO> AddAsync(PropertyTraceDTO entity)
        {

            var obj = new PropertyTrace()
            {
                Name = entity.Name,
                IdProperty = entity.IdProperty,
                DateSale = entity.DateSale,
                IdPropertyTrace = entity.IdPropertyTrace,
                Tax = entity.Tax,
                Value = entity.Value,
            };
            await _propertyTrace.InsertOneAsync(obj);
            return entity;
        }

        public async Task<string> DeleteAsync(string entity)
        {
            await _propertyTrace.DeleteOneAsync(o => o.IdProperty.Equals(entity));
            return entity;
        }

        public async Task<PropertyTraceDTO> EditAsync(PropertyTraceDTO entity)
        {
            var obj = new PropertyTrace()
            {
                Name = entity.Name,
                IdProperty = entity.IdProperty,
                DateSale = entity.DateSale,
                IdPropertyTrace = entity.IdPropertyTrace,
                Tax = entity.Tax,
                Value = entity.Value,
            };

            await _propertyTrace.ReplaceOneAsync(o => o.IdProperty.Equals(entity.IdProperty), obj);
            return entity;
        }

        public async Task<List<PropertyTraceDTO>> GetAsync()
        {
            var result = await _propertyTrace.Find(_ => true).ToListAsync();
            return result.Select(x => new PropertyTraceDTO()
            {
                Name = x.Name,
                IdProperty = x.IdProperty,
                DateSale = x.DateSale,
                IdPropertyTrace = x.IdPropertyTrace,
                Tax = x.Tax,
                Value = x.Value,
            }).ToList();
        }

        public async Task<PropertyTraceDTO> GetByIdAsync(string TId)
        {
            var result = await _propertyTrace.Find(o => o.IdProperty.Equals(TId)).FirstOrDefaultAsync();
            return new PropertyTraceDTO()
            {
                Name = result.Name,
                IdProperty = result.IdProperty,
                DateSale = result.DateSale,
                IdPropertyTrace = result.IdPropertyTrace,
                Tax = result.Tax,
                Value = result.Value,
            };
        }

        public void saveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
