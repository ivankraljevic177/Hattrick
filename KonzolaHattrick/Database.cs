using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace KonzolaHattrick
{
    public class Database
    {

        public List<Quotas> GetQuotasBySportId(int menuSport)
        {
            using (var dbSocc = new BaseContext())
            {

                var quotas = dbSocc.Quotas.Include(x => x.Game)
                    .Include(x => x.Game.TeamAwayID)
                    .Include(x => x.Game.TeamHomeID)
                    .Include(x => x.Game.Sport).Where(p => p.Game.Sport.SportID == menuSport).ToList();
                return quotas;
            }

        }
        public List<Sport> GetAllSports()
        {
            using (var db = new BaseContext())
            {

                var sports = db.Sports.ToList();
                return sports;
            }
        }
        public void SaveTicket(List<TicketContent> ticketContentList, double amountForTicket, float totalTicketQuota)
        {
            using (var db = new BaseContext())
            {
                Ticket t = db.Tickets.Add(new Ticket
                {
                    PaymentAmount = amountForTicket,
                    TotalQuota = totalTicketQuota,

                });
                foreach (var item in ticketContentList)
                {
                    db.TicketContents.Add(new TicketContent
                    {
                        Game = item.Game,
                        Ticket = t,
                        SelectedQuotaValue = item.SelectedQuotaValue,
                    });
                }
                db.SaveChanges();
            }
        }


    }
}
