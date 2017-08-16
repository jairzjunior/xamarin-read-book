using Newtonsoft.Json;

namespace ReadBook.Models
{
    public class UserBook : BaseDataObject
    {				
		[JsonProperty("userId")]
		public string UserId { get; set; }

		[JsonProperty("bookId")]
		public string BookId { get; set; }

		[JsonIgnore]        
		public Book Book { get; set; }

		public UserBook()
		{			
			UserId = "";
			BookId = "";            
        }
    }
}
