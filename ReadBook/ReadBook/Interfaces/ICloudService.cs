using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ReadBook.Interfaces
{
	public interface ICloudService
	{
		Task InsertAsync<T>(T item);
		Task UpdateAsync<T>(T item);
		Task DeleteAsync<T>(T item);
		Task<T> GetAsync<T>(string id);
		Task<ObservableCollection<T>> GetAllAsync<T>();
        Task SyncAsync<T>(bool pullData = false);
        Task<bool> LoginAsync();
    }    
}
