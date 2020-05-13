using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonzolaHattrick
{
    public class TicketContent
    {
        public int TicketContentID { get; set; }
        public virtual Ticket Ticket { get; set; }
        public virtual Game Game { get; set; }
        public float SelectedQuotaValue { get; set; }
        

    }
}
