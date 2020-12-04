namespace PetDeskAppt.Model
{
    public class BaseResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public string ConfirmationId { get; set; }
    }
}
