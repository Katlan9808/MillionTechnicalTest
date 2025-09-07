using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Million.Domain.Interfaces
{
    public  interface IAdd<T>
    {
        Task<T> AddAsync(T entity);
    }
}
