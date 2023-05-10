using System.ComponentModel.DataAnnotations.Schema;

namespace WEBAPIForUserAuthorization.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        
        public DateTime Created_Date { get; set; }
        public int User_Group_Id { get; set; }
        public int User_State_Id { get; set; }

    }
}
