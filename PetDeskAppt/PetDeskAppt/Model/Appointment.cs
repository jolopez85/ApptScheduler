using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PetDeskAppt.Model
{
    public class Appointment
    {
        [JsonProperty("appointmentId")]
        public int ID { get; set; }

        [JsonProperty("appointmentType")]
        public string Type { get; set; }

        public DateTime CreateDateTime { get; set; }

        public DateTime RequestedDateTimeOffset { get; set; }

        public User User { get; set; }

        public Animal Animal { get; set; }
    }
}
