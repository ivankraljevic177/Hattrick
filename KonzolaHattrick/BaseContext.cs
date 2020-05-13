using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace KonzolaHattrick
{
    class MyDatabaseInitializer : DropCreateDatabaseIfModelChanges<BaseContext>
    {
        protected override void Seed(BaseContext db)
        {

            //sportovi
            Sport soccer = new Sport() { SportName = "Nogomet" };
            Sport basketball = new Sport() { SportName = "Košarka" };
            Sport handball = new Sport() { SportName = "Rukomet" };
            Sport tennis = new Sport() { SportName = "Tenis" };
            Sport soccer2 = new Sport() { SportName = "Mali Nogomet" };

            //timovi
            //nogomet
            Team barca = new Team() { TeamName = "FC Barcelona", Sport = soccer };
            Team real = new Team() { TeamName = "Real Madrid FC", Sport = soccer };
            Team milan = new Team() { TeamName = "AC Milan", Sport = soccer };
            Team juve = new Team() { TeamName = "Juventus", Sport = soccer };
            Team hajduk = new Team() { TeamName = "HNK Hajduk", Sport = soccer };
            Team dinamo = new Team() { TeamName = "GNK Dinamo", Sport = soccer };
            Team rijeka = new Team() { TeamName = "NK Rijeka", Sport = soccer };
            Team osijek = new Team() { TeamName = "NK Osijek", Sport = soccer };
            Team bayern = new Team() { TeamName = "FC Bayern Munich", Sport = soccer };
            Team borrusia = new Team() { TeamName = "Borrusia Dortmund", Sport = soccer };

            //kosarka
            Team barcakosarka = new Team() { TeamName = "BC Barcelona", Sport = basketball };
            Team realkosarka = new Team() { TeamName = "Real Madrid BC", Sport = basketball };
            //rukomet
            Team barcarukomet = new Team() { TeamName = "HC Barcelona", Sport = handball };
            Team realrukomet = new Team() { TeamName = "Real Madrid HC", Sport = handball };

            //tenis
            Team dokovic = new Team() { TeamName = "Novak Đoković", Sport = tennis };
            Team nadal = new Team() { TeamName = "Rafael Nadal", Sport = tennis };
            Team federer = new Team() { TeamName = "Roger Federrer", Sport = tennis };
            Team murray = new Team() { TeamName = "Andy Murray", Sport = tennis };
            //mali nogomet
            Team tommyst = new Team() { TeamName = "FC Tommy Split", Sport = soccer2 };
            Team olmisum = new Team() { TeamName = "MNK Olmissum", Sport = soccer2 };

            //utakmice
            Game juvevsmilan = new Game() { TeamAwayID = milan, TeamHomeID = juve, Sport = soccer };
            Game realvsbarca = new Game() { TeamAwayID = real, TeamHomeID = barca, Sport = soccer };
            Game hajdukvsdinamo = new Game() { TeamAwayID = dinamo, TeamHomeID = hajduk, Sport = soccer };
            Game bayernvsborrusia = new Game() { TeamAwayID = borrusia, TeamHomeID = bayern, Sport = soccer };

            Game rvsbrukomet = new Game() { TeamAwayID = realrukomet, TeamHomeID = barcarukomet, Sport = handball };

            Game rvsbkosarka = new Game() { TeamAwayID = realkosarka, TeamHomeID = barcakosarka, Sport = basketball };

            Game federervsmurray = new Game() { TeamAwayID = federer, TeamHomeID = murray, Sport = tennis };
            Game dokovicvsnadal = new Game() { TeamAwayID = dokovic, TeamHomeID = nadal, Sport = tennis };

            Game tommyvsolmissum = new Game() { TeamAwayID = tommyst, TeamHomeID = olmisum, Sport = soccer2 };



            //kvote za utakmice
            Quotas juvemilankvote = new Quotas() { QuotaAway = 2.35f, QuotaX = 2, QuotaHome = 1.40f, Game = juvevsmilan };
            Quotas realbarcakvote = new Quotas() { QuotaAway = 1.35f, QuotaX = 1.8f, QuotaHome = 2.40f, Game = realvsbarca };
            Quotas hajvsdinkv = new Quotas() { QuotaAway = 1.20f, QuotaX = 2.00f, QuotaHome = 3.20f, Game = hajdukvsdinamo };
            Quotas bayernvsborrkv = new Quotas() { QuotaAway = 1.65f, QuotaX = 2.40f, QuotaHome = 1.90f, Game = bayernvsborrusia };
            Quotas fedvsmurkv = new Quotas() { QuotaAway = 2.00f, QuotaX = 2.30f, QuotaHome = 1.75f, Game = federervsmurray };
            Quotas nadvsdokovickv = new Quotas() { QuotaAway = 1.75f, QuotaX = 2.40f, QuotaHome = 2.10f, Game = dokovicvsnadal };
            Quotas tomvsolmkv = new Quotas() { QuotaAway = 1.65f, QuotaX = 2.00f, QuotaHome = 2.40f, Game = tommyvsolmissum };



            Quotas realbarcaruk = new Quotas() { QuotaAway = 1.25f, QuotaX = 2, QuotaHome = 3.20f, Game = rvsbrukomet };
            Quotas realbarcakos = new Quotas() { QuotaAway = 3.20f, QuotaX = 1.8f, QuotaHome = 1.10f, Game = rvsbkosarka };






            //dodavanje sportova u bazu
            db.Sports.Add(soccer);
            db.Sports.Add(basketball);
            db.Sports.Add(handball);
            db.Sports.Add(tennis);
            db.Sports.Add(soccer2);

            //dodavanje timova u bazu
            db.Teams.Add(barca);
            db.Teams.Add(real);
            db.Teams.Add(milan);
            db.Teams.Add(juve);
            db.Teams.Add(barcakosarka);
            db.Teams.Add(barcarukomet);
            db.Teams.Add(realkosarka);
            db.Teams.Add(realrukomet);
            db.Teams.Add(dinamo);
            db.Teams.Add(hajduk);
            db.Teams.Add(rijeka);
            db.Teams.Add(osijek);
            db.Teams.Add(bayern);
            db.Teams.Add(borrusia);
            db.Teams.Add(dokovic);
            db.Teams.Add(nadal);
            db.Teams.Add(federer);
            db.Teams.Add(murray);
            db.Teams.Add(tommyst);
            db.Teams.Add(olmisum);






            //dodavanje utakmica u bazu
            db.Games.Add(juvevsmilan);
            db.Games.Add(realvsbarca);
            db.Games.Add(rvsbrukomet);
            db.Games.Add(rvsbkosarka);
            db.Games.Add(hajdukvsdinamo);
            db.Games.Add(bayernvsborrusia);
            db.Games.Add(federervsmurray);
            db.Games.Add(dokovicvsnadal);
            db.Games.Add(tommyvsolmissum);

            //dodavanje kvota za utakmice u bazu
            db.Quotas.Add(juvemilankvote);
            db.Quotas.Add(realbarcakvote);
            db.Quotas.Add(realbarcakos);
            db.Quotas.Add(realbarcaruk);
            db.Quotas.Add(hajvsdinkv);
            db.Quotas.Add(bayernvsborrkv);
            db.Quotas.Add(nadvsdokovickv);
            db.Quotas.Add(fedvsmurkv);
            db.Quotas.Add(tomvsolmkv);

            db.SaveChanges();
            base.Seed(db);
        }
    }

    class BaseContext : DbContext
    {
        public BaseContext() : base("BaseBettingApp")
        {
            System.Data.Entity.Database.SetInitializer(new MyDatabaseInitializer());
        }

        public DbSet<Sport> Sports { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Quotas> Quotas { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<TicketContent> TicketContents { get; set; }



    }
}
