using Newtonsoft.Json;

namespace PetDeskAppt.Model
{
    public class Animal
    {
        [JsonProperty("animalId")]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Species { get; set; }
        public string Breed { get; set; }
    }
}