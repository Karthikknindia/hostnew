namespace Ticket_Booking_App.Models
{
    public class Movies


    {
        public int movie_id { get; set; }
        public string movie_name { get; set; }
        public string movie_categories { get; set; }
        public string movie_theater { get; set; }
        public string movie_poster { get; set; }

        public string movie_status { get; set; }

        public string movie_showtiming { get; set; }
        public string movie_timeduration { get; set; }
        public string movie_director { get; set; }
        public string movie_cast { get; set; }
        public string movie_thumbnail { get; set; }
        public string movie_ytlink { get; set; }
        public DateTime movie_createdate { get; set; }
        public DateTime movie_updatedate { get; set; }
    }
}
