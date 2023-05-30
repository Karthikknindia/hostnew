namespace Ticket_Booking_App.Models
{
    public class Booking
    {
        public int booking_id { get; set; }
        public string booking_name { get; set; }
        public string booking_email { get; set; }
        public string booking_seats { get; set; }
        public string booking_movie { get; set; }

        public string booking_date { get; set; }

        public string booking_theater { get; set; }
        public string booking_showtime { get; set; }
        public string booking_status { get; set; }
        public DateTime booking_createdate { get; set; }
        public DateTime booking_updatedate { get; set; }
        public string booking_poster { get; set; }
        public string booking_amount { get; set; }

    }
}
