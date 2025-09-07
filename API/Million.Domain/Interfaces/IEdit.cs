using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Million.Domain.Interfaces
{
    public interface IEdit<T>
    {
        Task<T> EditAsync(T entity);
    }
}
