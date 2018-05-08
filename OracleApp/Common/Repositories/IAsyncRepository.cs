using System.Threading.Tasks;

namespace OracleApp.Common.Repositories
{
    public interface IAsyncRepository<T>
    {
        Task<int> UpdateAsync(T oldItem, T newItem);
        //Task<T> UpdateAsync(decimal id);
        //Task DeleteAsync(T item);
        Task DeleteAsync(decimal id);
    }
}
