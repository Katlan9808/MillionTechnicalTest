using Million.Domain.Interfaces;

namespace Million.Application.Interfaces
{
    public interface IBaseService<T, TId> : IAdd<T>, IEdit<T>, IDelete<TId>, IListE<T, TId>
    {

    }
}
