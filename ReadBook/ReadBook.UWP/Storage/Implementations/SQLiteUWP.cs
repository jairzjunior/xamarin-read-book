using SQLite;
using System.IO;
using Xamarin.Forms;
using ReadBook.Interfaces;
using ReadBook.UWP.Storage.Implementations;
using Windows.Storage;

[assembly: Dependency(typeof(SQLiteUWP))]
namespace ReadBook.UWP.Storage.Implementations
{
    public class SQLiteUWP : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            SQLitePCL.Batteries.Init();
            var sqliteFilename = "db.db3";
            string documentsPath = ApplicationData.Current.LocalFolder.Path;
            var path = Path.Combine(documentsPath, sqliteFilename);

            var conn = new SQLiteConnection(path);

            return conn;
        }
    }
}