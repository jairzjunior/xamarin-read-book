using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using ReadBook.Interfaces;
using ReadBook.UWP.Implementations;
using Xamarin.Forms;
using System;

[assembly: Dependency(typeof(SocialAuthentication))]
namespace ReadBook.UWP.Implementations
{
    public class SocialAuthentication : IAuthentication
    {        
        public async Task<MobileServiceUser> LoginAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provider, IDictionary<string, string> parameters = null)
        {            
            try
            {
                return await client.LoginAsync(provider);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}