using Newtonsoft.Json;
using SQLite;

namespace ReadBook.Models
{
    public class User : BaseDataObject
    {		
		[JsonIgnore]
        [Ignore]
        public string Name { get { return (FirstName + " " + LastName).Trim(); } }

		[JsonProperty("firstName")]
		public string FirstName { get; set; }

		[JsonProperty("lastName")]
		public string LastName { get; set; }

		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("username")]
		public string Username { get; set; }

		[JsonProperty("password")]
		public string Password { get; set; }

		public User()
		{
			FirstName = "";
			LastName = "";
			Email = "";
			Username = "";
			Password = "";
		}
    }
}
