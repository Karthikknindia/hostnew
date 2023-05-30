using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ticket_Booking_App.Models
{
    
    public class Login
    {
        [Key]
        public int login_id { get; set; }
        public string login_name { get; set; }   
        public string login_email { get; set; }
        [Required]
        public string login_password { get; set; }
        public string login_status{ get; set; }
        public DateTime login_createdate { get; set; }


        public string login_usertype{ get; set; }








    }
}

