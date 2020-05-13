using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonzolaHattrick
{
    public class Game
    {
        public int GameID { get; set; }
        public virtual Team TeamHomeID { get; set; }
        public virtual Team TeamAwayID { get; set; }
        
        public virtual Sport Sport { get; set; }
        


    }
}
