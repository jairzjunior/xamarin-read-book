using System.Collections.Generic;
using System.Threading.Tasks;
using ReadBook.Models;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Xamarin.Forms;
using ReadBook.Interfaces;
using ReadBook.Helpers;
using System.Diagnostics;
using System;
using System.Collections.ObjectModel;
using System.Linq;

[assembly: Dependency(typeof(ReadBook.Services.AzureService))]
namespace ReadBook.Services
{    
    public class AzureService : ICloudService
    {
        private string AppUrl = "https://mbl-readbook.azurewebsites.net/";
        public MobileServiceClient Client { get; set; } = null;
        public static bool UseAuth { get; set; } = false;                
        public void Initialize()
        {
            Client = new MobileServiceClient(AppUrl);            
            if (!string.IsNullOrWhiteSpace(Settings.AuthToken) && !string.IsNullOrWhiteSpace(Settings.UserId))
            {
                Client.CurrentUser = new MobileServiceUser(Settings.UserId)
                {
                    MobileServiceAuthenticationToken = Settings.AuthToken
                };                
            }
        }

        public AzureService()
        {
            Initialize();
            if (!Client.SyncContext.IsInitialized)
            {
                var store = new MobileServiceSQLiteStore("local.db");
                store.DefineTable<User>();
                store.DefineTable<UserBook>();
                store.DefineTable<Book>();
                store.DefineTable<Gamification>();
                Client.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());
            }
        }

        public async Task<bool> LoginAsync()
        {
            var auth = DependencyService.Get<IAuthentication>();
            var user = await auth.LoginAsync(Client, MobileServiceAuthenticationProvider.Facebook);           

            if (user == null)
            {
                Settings.AuthToken = string.Empty;
                Settings.UserId = string.Empty;

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await App.Current.MainPage.DisplayAlert("Ops!", "Não conseguimos efetuar o seu login, tente novamente!", "OK");
                });

                return false;
            }
            else
            {
                Settings.AuthToken = user.MobileServiceAuthenticationToken;
                Settings.UserId = user.UserId;

                await SyncAsync<User>(true);
                var userIdentity = (await GetAllAsync<User>()).FirstOrDefault(u => u.Id == Settings.UserId);
                if (userIdentity == null)
                {
                    await SyncAsync<User>(true);
                    var identity = (await Client.InvokeApiAsync<List<AppServiceIdentity>>("/.auth/me?")).FirstOrDefault();
                    var urlClaims = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/";
                    userIdentity = new User()
                    {
                        Id = Settings.UserId,                        
                        NameIdentifier = identity.UserClaims.FirstOrDefault(i => i.Type == $"{urlClaims}nameidentifier")?.Value,
                        FirstName = identity.UserClaims.FirstOrDefault(i => i.Type == $"{urlClaims}givenname")?.Value,
                        LastName = identity.UserClaims.FirstOrDefault(i => i.Type == $"{urlClaims}surname")?.Value,
                        Email = identity.UserClaims.FirstOrDefault(i => i.Type == $"{urlClaims}emailaddress")?.Value
                    };
                    await InsertAsync(userIdentity);
                }                                
            }

            return true;
        }    

		public async Task<IEnumerable<T>> GetTable<T>()
		{
			IMobileServiceTable<T> table = Client.GetTable<T>();
			return await table.ToEnumerableAsync();
		}

		public async Task InsertAsync<T>(T item)
		{
			IMobileServiceTable<T> table = Client.GetTable<T>();
			await table.InsertAsync(item);
		}

		public async Task UpdateAsync<T>(T item)
		{
			IMobileServiceTable<T> table = Client.GetTable<T>();
			await table.UpdateAsync(item);
		}

		public async Task DeleteAsync<T>(T item)
		{
			IMobileServiceTable<T> table = Client.GetTable<T>();
			await table.DeleteAsync(item);
		}

		public async Task<T> GetAsync<T>(string id)
		{            
            var table = Client.GetTable<T>();            
            var theEntity = await table.LookupAsync(id);
            return theEntity;                        
        }

        public async Task<ObservableCollection<T>> GetAllAsync<T>()
        {
            ObservableCollection<T> theCollection = new ObservableCollection<T>();            
            try
            {
                var theTable = Client.GetSyncTable<T>();
                theCollection = await theTable.ToCollectionAsync<T>();
            }
            catch (Exception ex)
            {
                theCollection = null;
                ReportError(ex);
            }

            return theCollection;
        }        

        public async Task SyncAsync<T>(bool pullData = false)
        {
            try
            {
                var theTable = Client.GetSyncTable<T>();
                await Client.SyncContext.PushAsync();
                if (pullData)
                {
                    await theTable.PullAsync(null, theTable.CreateQuery());
                }
            }
            catch (Exception ex)
            {
                ReportError(ex);
            }
        }

        private void ReportError(Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
}