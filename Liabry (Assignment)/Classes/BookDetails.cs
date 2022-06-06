using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Liabry__Assignment_
{
    public class BookDetails
    {
        public int bookReference { get; set; }
        public long ISBN { get; set; }
        public int stockNumber { get; set; }
        public string bookTitle { get; set; }
        public string author { get; set; }
        public string genre { get; set; }
        public string publisher { get; set; }
        public int rentalDurationInWeeks { get; set; }
        public int isleNumber { get; set; }
        public int rowNumber { get; set; }
        public Image bookCover { get; set; }
        public string bookDirectory { get; set; }
      
    }
}
