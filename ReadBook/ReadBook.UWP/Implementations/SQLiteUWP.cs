using System.IO;
using Xamarin.Forms;
using Windows.Storage;
using ReadBook.Interfaces;
using ReadBook.UWP;

[assembly: Dependency(typeof(FileHelper))]
namespace ReadBook.UWP
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, filename);
        }
    }
}