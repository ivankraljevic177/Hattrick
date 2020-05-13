using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace KonzolaHattrick
{
    class Program
    {



        static void Main(string[] args)
        {



            double wallet = 0;
            bool programstart = true;
            while (programstart == true)
            {
                
                Console.WriteLine("===================================" + "\n" + " " +
                           "\n" + "Dobrodošli u aplikaciju za klađenje" + "\n" + " " +
                           "\n" + "===================================" );
                Console.WriteLine("Izaberite opcije koje nudimo:");
                Console.WriteLine("1. Novčanik" + "\n" +
                                  "2. Kreiranje novog listića" + "\n" +
                                  "3. Pregled odigranog listića" + "\n" +
                                  "4. Izlazak iz aplkacije");
                Console.WriteLine("-----------------------------------");

                int menu = int.Parse(Console.ReadLine());


                while (menu < 1 || menu > 4)
                {
                    Console.WriteLine("Opcija netočna! Unesite ponovo:");
                    menu = int.Parse(Console.ReadLine());

                }
                
                if (menu == 1)
                {
                    Console.WriteLine("-----------------------------------");
                    Console.WriteLine("Raspoloživi iznos računa:" + wallet);
                    Console.WriteLine("Upišite iznos koji želite uplatiti na račun:");


                    wallet += double.Parse(Console.ReadLine());
                    System.Threading.Thread.Sleep(1000);


                    Console.WriteLine("Uspješno ste dodali " + wallet + " u novčanik");
                    Console.WriteLine("Raspoloživi iznos računa:" + wallet);
                    Console.WriteLine("-----------------------------------");
                    System.Threading.Thread.Sleep(1000);


                }

                else if (menu == 2)
                {


                    string finish;
                    float totalTicketQuota = 1;
                    var ticketContentList = new List<TicketContent>();
                    do
                    {
                        PlayOneTicketGame(ticketContentList);
                        Console.WriteLine("Pritisnite 1 za zaključenje tiketa, a za nastavak odabira ponude pritisnite bilo koju drugu tipku");
                        finish = Console.ReadLine();
                    } while (finish != "1");

                    foreach (var item in ticketContentList.Select(x => x.SelectedQuotaValue))
                    {
                        totalTicketQuota *= item;
                    }
                    //Bonus za 3 utakmice istog sporta
                    var bonus3SamesPORT = ticketContentList.GroupBy(x => x.Game.Sport.SportID).Any(x => x.Count() > 3);
                    if(bonus3SamesPORT == true)
                    {
                        totalTicketQuota += 5;
                    }
                    //Bonus za sve odigrane sportove
                    var allSports = new Database().GetAllSports().Select(x=>x.SportID);
                    var playedSportsId = ticketContentList.Select(x => x.Game.Sport.SportID);
                    if (allSports.Intersect(playedSportsId).Count() == allSports.Count()) {
                        totalTicketQuota += 10;
                    }



                    while (wallet <= 0)
                    {
                        Console.WriteLine("Vaš novčanik je prazan. Uplatite iznos na novčanik:");
                        wallet = double.Parse(Console.ReadLine());
                    };
                    double amountForTicket = Payment(wallet);
                    wallet -= amountForTicket;
                    new Database().SaveTicket(ticketContentList, amountForTicket, totalTicketQuota);

                }
                else if (menu == 3)
                {
                    using (var dbTicketList = new BaseContext())
                    {

                        var listTickets = from c in dbTicketList.Tickets
                                          select c;
                        int count = 0;
                        
                        foreach (var ticket in listTickets)
                        {
                            count++;
                            Console.WriteLine("-----------------------------------");
                            Console.WriteLine("Listić" + count);
                            Console.WriteLine("-----------------------------------");
                            foreach (var ticketContent in ticket.TicketContents)
                            {
                                Console.WriteLine(ticketContent.Game.TeamAwayID.TeamName + " : " + ticketContent.Game.TeamHomeID.TeamName + " " +ticketContent.SelectedQuotaValue);
                            }
                            Console.WriteLine("-----------------------------------");
                            Console.WriteLine("Ukupna kvota: " + ticket.TotalQuota + " \n " + "Uplaćeni iznos: " + ticket.PaymentAmount);
                            Console.WriteLine("Eventualni dobitak:" + ticket.TotalQuota * ticket.PaymentAmount);
                        }
                    }
                }
                else if (menu == 4)
                {
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Neispravan unos");
                }
            }

            Console.ReadKey();

        }

        private static double Payment(double wallet)
        {
            double amountForTicket = 0;


            Console.WriteLine("Raspoloživi iznos:" + wallet);
            Console.WriteLine("Unesite svotu koju ćete uplatiti na tiket:");

            var finalAmount = 0.0d;
            do
            {
                amountForTicket = Math.Abs(double.Parse(Console.ReadLine()));

                finalAmount = wallet - amountForTicket;
            } while (finalAmount < 0);


            return amountForTicket;
        }


        private static void PlayOneTicketGame(List<TicketContent> ticketContentList)
        {
            int menuSport = SelectSport();
            var sports = new Database().GetAllSports();


            Console.WriteLine("Ponuda  utakmica za klađenje iz sporta : " + sports.Where(x => x.SportID == menuSport).FirstOrDefault().SportName + "\n" +
                "Izaberite redni broj utakmice na koju se želite kladiti:");
            int selectedQuotaId;


            var quotas = new Database().GetQuotasBySportId(menuSport);
            quotas = PreventPlayingDuplicateGames(ticketContentList, quotas);
            PrintOffer(quotas);

            selectedQuotaId = SelectGameFromOffer(quotas);


            //Ispis kvota iz tablice Quotas
            var quotaForSelectedGame = (from a in quotas
                                        where a.QuotasID == selectedQuotaId
                                        select a).FirstOrDefault();


            Console.WriteLine("Kvota na tip 1: " + quotaForSelectedGame.QuotaHome + "\n" + " Kvota na tip X: " + quotaForSelectedGame.QuotaX + "\n" + " Kvota na tip 2 " + quotaForSelectedGame.QuotaAway);
            float selectedQuotaValue = GetQuotaValue(quotaForSelectedGame);

            TicketContent ticketContent = new TicketContent() { Game = quotaForSelectedGame.Game, SelectedQuotaValue = selectedQuotaValue };
            ticketContentList.Add(ticketContent);

        }

        private static List<Quotas> PreventPlayingDuplicateGames(List<TicketContent> ticketContentList, List<Quotas> quotas)
        {
            var playedGames = ticketContentList.Select(x => x.Game.GameID);

            quotas = quotas.Where(x => !playedGames.Contains(x.Game.GameID)).ToList();
            return quotas;
        }

        private static float GetQuotaValue(Quotas quotaForSelectedGame)
        {
            string selectedQuota = Console.ReadLine();
            float selectedQuotaValue = 0;
            if (selectedQuota == "1")
            {
                selectedQuotaValue = quotaForSelectedGame.QuotaHome;
            }
            else if (selectedQuota == "x")
            {
                selectedQuotaValue = quotaForSelectedGame.QuotaX;
            }
            else if (selectedQuota == "2")
            {
                selectedQuotaValue = quotaForSelectedGame.QuotaAway;
            }
            else
            {
                Console.WriteLine("Neispravan unos!");
            }

            return selectedQuotaValue;
        }

        private static int SelectSport()
        {
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Izaberite sport:");
            Console.WriteLine("1. Nogomet" + "\n" +
                              "2. Košarka" + "\n" +
                              "3. Rukomet" + "\n" +
                              "4. Tenis" + "\n" +
                              "5. Mali Nogomet");
            Console.WriteLine("-----------------------------------");
            int menuSport = int.Parse(Console.ReadLine());

            while (menuSport < 1 || menuSport > 5)
            {
                Console.WriteLine("Opcija netočna! Unesite ponovo:");
                menuSport = int.Parse(Console.ReadLine());
            }

            return menuSport;
        }

        private static int SelectGameFromOffer(List<Quotas> quotas)
        {
            int numberOFSelectedGame;
            do
            {
                numberOFSelectedGame = int.Parse(Console.ReadLine());

            } while (!quotas.Select(x => x.QuotasID).ToList().Contains(numberOFSelectedGame));
            return numberOFSelectedGame;
        }

        private static void PrintOffer(List<Quotas> quotas)
        {
            foreach (var kv in quotas)
            {
                Console.WriteLine(kv.QuotasID + " " + kv.Game.TeamHomeID.TeamName + " vs " + kv.Game.TeamAwayID.TeamName +
                " Kvota na tip 1: " + kv.QuotaHome + " Kvota na tip X: " + kv.QuotaX + " Kvota na tip 2: " + kv.QuotaAway);
            }
        }


    }
}
