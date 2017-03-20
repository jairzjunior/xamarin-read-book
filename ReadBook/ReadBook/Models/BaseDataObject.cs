using System;
using ReadBook.Helpers;
using SQLite;

namespace ReadBook.Models
{
    public class BaseDataObject : ObservableObject
    {
        public BaseDataObject()
        {
            Id = Guid.NewGuid().ToString();
        }
        
        [PrimaryKey]        
        public string Id { get; set; }        
    }
}
