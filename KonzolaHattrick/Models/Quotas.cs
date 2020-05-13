using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonzolaHattrick
{
    public class Quotas
    {
        public int QuotasID { get; set; }
        public float QuotaHome { get; set; }
        public float QuotaAway { get; set; }
        public float QuotaX { get; set; }
        public virtual Game Game { get; set; }


    }
}
