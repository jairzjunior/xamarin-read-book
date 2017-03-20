using SQLite;
using System.IO;
using Xamarin.Forms;
using ReadBook.Interfaces;
using ReadBook.Droid.Storage.Implementations;

[assembly: Dependency(typeof(SQLiteDroid))]
namespace ReadBook.Droid.Storage.Implementations
{
    public class SQLiteDroid : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            SQLitePCL.Batteries.Init();
            var sqliteFilename = "db.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile);
            var path = Path.Combine(documentsPath, sqliteFilename);

            var conn = new SQLiteConnection(path);

            return conn;
        }
    }
}