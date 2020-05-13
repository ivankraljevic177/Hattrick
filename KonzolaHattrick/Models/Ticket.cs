using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonzolaHattrick
{
    public class Ticket
    {
        public int TicketID { get; set; }
        public double PaymentAmount { get; set; }
        public float TotalQuota { get; set; }
        public virtual List<TicketContent> TicketContents { get; set; }



    }
}
