using System.Threading.Tasks;

namespace OracleApp.Common.Repositories
{
    public interface IAsyncRepository<T>
    {
        Task UpdateAsync(T oldItem, T newItem);
        Task AddAsync(T newItem);
        Task DeleteAsync(decimal id);
    }
}
