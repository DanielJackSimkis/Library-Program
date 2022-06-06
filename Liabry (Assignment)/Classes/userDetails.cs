using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Liabry__Assignment_
{

    public enum title
    {
        Mr,
        Mrs,
        Miss,
        Ms,
        Mx,
        Master,
        Professor,
        Sir,
        Doctor,
        Madam,
        Dame,
        Lady,
        Lord, 
        None  
    }

    public class userDetails
    {
        public int userID { get; set; }
        public title title { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string firstLineAddress {get; set;}
        public string secondLineAddress { get; set; }
        public string postCode { get; set; }
        public DateTime DOB { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public Image userPhoto { get; set; }
    }
}
