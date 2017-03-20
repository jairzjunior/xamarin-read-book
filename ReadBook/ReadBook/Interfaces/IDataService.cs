using SQLite;
using System.Threading.Tasks;

namespace ReadBook.Interfaces
{
    public interface IDataService
    {
        SQLiteConnection Connection { get; }
        Task<bool> InsertAsync<T>(T entity);
        Task<bool> UpdateAsync<T>(T entity);
        Task<bool> DeleteAsync<T>(T entity);        
    }
}