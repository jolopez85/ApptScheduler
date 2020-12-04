using Newtonsoft.Json;

namespace PetDeskAppt.Model
{
    public class User
    {
        [JsonProperty("userId")]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [JsonProperty("vetDataId")]
        public string VetId { get; set; }
    }
}

