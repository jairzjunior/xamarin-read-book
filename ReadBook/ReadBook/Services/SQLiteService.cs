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
        private readonly SQLiteConnection connection;

        public SQLiteConnection Connection
        {
            get { return connection; }
        }

        public SQLiteService()
		{            
            connection = DependencyService.Get<ISQLite>().GetConnection();
            connection.CreateTable<User>();
            connection.CreateTable<Book>();
            connection.CreateTable<UserBook>();            
            connection.CreateTable<Gamification>();			
		}        

        public Task<bool> InsertAsync<T>(T item)
		{            
            connection.Insert(item);

            return Task.FromResult(true);
        }        

        public Task<bool> UpdateAsync<T>(T item)
		{            
            connection.Update(item);

            return Task.FromResult(true);
        }

		public Task<bool> DeleteAsync<T>(T item)
		{            
            connection.Delete(item);

            return Task.FromResult(true);
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