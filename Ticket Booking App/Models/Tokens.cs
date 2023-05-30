namespace Ticket_Booking_App.Models
{
    public class Tokens
    {

        public int token_id { get; set; } 
        public string Token { get; set; }

        public string login_name { get; set; }

        public string expiration_date { get; set; }
        public string RefreshToken { get; set; }
    }
}
