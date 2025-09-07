using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Million.Domain.Interfaces
{
    public interface IListE<T, Tid>
    {
        Task<List<T>> GetAsync();

        Task<T> GetByIdAsync(string TId);
    }
}
