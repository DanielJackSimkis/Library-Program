using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liabry__Assignment_
{
    public class Rentals
    {

        public int rental_ID { get; set; }
        public int bookReference_FK { get; set; }
        public string customer_ID_FK { get; set; }
        public DateTime rental_date { get; set; }
        public DateTime return_date { get; set; }
        public DateTime date_returned { get; set; }

    }
}
