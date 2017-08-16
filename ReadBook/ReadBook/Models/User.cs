using Newtonsoft.Json;

namespace ReadBook.Models
{
    public class User : BaseDataObject
    {		
		[JsonIgnore]        
        public string Name { get { return (FirstName + " " + LastName).Trim(); } }

		[JsonProperty("firstName")]
		public string FirstName { get; set; }

		[JsonProperty("lastName")]
		public string LastName { get; set; }

		[JsonProperty("email")]
		public string Email { get; set; }

        [JsonProperty("nameIdentifier")]
        public string NameIdentifier { get; set; }        

		public User()
		{
			FirstName = "";
			LastName = "";
			Email = "";			
		}
    }
}
