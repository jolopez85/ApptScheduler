using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PetDeskAppt.Model;
using PetDeskAppt.Repository.Interfaces;
using PetDeskAppt.Repository.Providers;

namespace PetDeskAppt.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentsController : Controller
    {
        private readonly ILogger<AppointmentsController> _logger;
        readonly IHttpClientProvider _httpClient;

        public AppointmentsController(ILogger<AppointmentsController> logger, 
                                      IConfiguration configuration,
                                      IHttpClientProvider httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IEnumerable<Appointment>> Get()
        {
            var result = await _httpClient.GetAll<Appointment>("appointments");

            return result.OrderBy(t => t.RequestedDateTimeOffset);
        }


        [HttpPost]
        [Route("Confirm")]
        public BaseResponse Confirm()
        { 
        return new BaseResponse
            {
                Success = true,
                Message = $"The appointment has been confirmed",
                ConfirmationId = Guid.NewGuid().ToString()
            };
        }

        [HttpPost]
        [Route("Update")]
        public AppointmentUpdate UpdateAppointment(Appointment appointment)
        {
            return new AppointmentUpdate
            {
                PetName = appointment.Animal.FirstName,
                AppointmentDate = appointment.RequestedDateTimeOffset.AddDays(5)
            };
        }

    }
}
