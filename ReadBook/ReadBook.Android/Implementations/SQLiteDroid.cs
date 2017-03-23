using System;
using System.IO;
using Xamarin.Forms;
using ReadBook.Interfaces;
using ReadBook.Android;

[assembly: Dependency(typeof(FileHelper))]
namespace ReadBook.Android
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}