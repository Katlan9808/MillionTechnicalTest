using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Million.Domain.Interfaces;

namespace Million.Domain.Repository
{
    public interface IOwnerRepository<T, TId> : IAdd<T>, IEdit<T>, IDelete<TId>, IListE<T, TId>, ITransaction
    {
        
    }
}
