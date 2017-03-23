using System.Threading.Tasks;
using ReadBook.Models;
using Xamarin.Forms;
using SQLite;
using System;
using ReadBook.Interfaces;

[assembly: Dependency(typeof(ReadBook.Services.SQLiteService))]
namespace ReadBook.Services
{   
    public class SQLiteService : IDataService
	{
        private readonly SQLiteAsyncConnection connection;

        public SQLiteAsyncConnection Connection
        {
            get { return connection; }
        }

        public SQLiteService()
		{
            connection = new SQLiteAsyncConnection(DependencyService.Get<IFileHelper>().GetLocalFilePath("db.db3"));            

            connection.CreateTableAsync<User>().Wait();
            connection.CreateTableAsync<Book>().Wait();
            connection.CreateTableAsync<UserBook>().Wait();
            connection.CreateTableAsync<Gamification>().Wait();
        }        

        public async Task<bool> InsertAsync<T>(T item)
		{
            await connection.InsertAsync(item);
            return true;
        }        

        public async Task<bool> UpdateAsync<T>(T item)
		{            
            await connection.UpdateAsync(item);

            return true;
        }

		public async Task<bool> DeleteAsync<T>(T item)
		{            
            await connection.DeleteAsync(item);

            return true;
        }        

		public bool PullLatest()
		{									
			return true;
		}		        

        public void Sync()
        {
            throw new NotImplementedException();
        }        
    }	
}