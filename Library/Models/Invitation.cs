using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Invitation
    {
        public String Title { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public DateOnly Date { get; set; }
        public String Location { get; set; }

        public String Mail { get; set; }
        public override string ToString()
        {
            return $"{Mail}\nDear {Title}{FirstName} {LastName}\nIt gives us great joy to invite such an accomplished personality like you to our little business event held at {Location}.\nWe will be expecting your presence on {Date}.";

        }
    }
}
