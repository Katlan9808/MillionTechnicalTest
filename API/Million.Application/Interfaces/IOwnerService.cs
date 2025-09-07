using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Million.Domain.Interfaces;

namespace Million.Application.Interfaces
{
    public interface IOwnerService<T, TId> : IAdd<T>, IEdit<T>, IDelete<TId>, IListE<T, TId>
    {

    }
}
