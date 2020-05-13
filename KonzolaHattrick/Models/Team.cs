using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonzolaHattrick
{
    public class Team
    {
        public int TeamID { get; set; }
        public string TeamName { get; set; }       
        public virtual Sport Sport { get; set; }
    }
}
