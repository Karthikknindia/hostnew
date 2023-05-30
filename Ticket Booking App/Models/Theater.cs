using System.ComponentModel.DataAnnotations;

namespace Ticket_Booking_App.Models
{
    public class Theater
    {
        [Key]
        public int theater_id { get; set; }
        public string theater_name { get; set; }

        public int theater_capacity { get; set; }

        public string theater_location { get; set; }


        public int theater_screen { get; set; }

        public string theater_status { get; set; }

        

        public DateTime theater_datetime { get; set; }

        public DateTime theater_createdate { get; set; }


        public DateTime theater_updatedate { get; set; }

       
    }
}
