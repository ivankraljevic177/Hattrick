using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonzolaHattrick
{
    public class Sport
    {
        public int SportID { get; set; }
        public string SportName { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
    }
}
