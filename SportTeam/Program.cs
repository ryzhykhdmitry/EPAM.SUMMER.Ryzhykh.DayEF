using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportTeam
{
    class Program
    {
        public class Team
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public ICollection<Player> Players { get; set; }
            public Team()
            {
                Players = new List<Player>();
            }
        }
        public class Player
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Position { get; set; }
            public int Age { get; set; }

            public ICollection<Team> Teams { get; set; }
            public Player()
            {
                Teams = new List<Team>();
            }
        }
        public class SportTeamContext : DbContext
        {
            public DbSet<Player> Players { get; set; }
            public DbSet<Team> Teams { get; set; }
        }

        static void Main(string[] args)
        {
            using (SportTeamContext db = new SportTeamContext())
            {
                
                Player pl1 = new Player { Name = "Роналду", Age = 31, Position = "Нападающий" };
                Player pl2 = new Player { Name = "Месси", Age = 28, Position = "Нападающий" };
                Player pl3 = new Player { Name = "Хави", Age = 34, Position = "Полузащитник" };
                db.Players.AddRange(new List<Player> { pl1, pl2, pl3 });
                db.SaveChanges();

                Team t1 = new Team { Name = "Барселона" };
                t1.Players.Add(pl2);
                t1.Players.Add(pl3);
                Team t2 = new Team { Name = "Реал Мадрид" };
                t2.Players.Add(pl1);
                db.Teams.Add(t1);
                db.Teams.Add(t2);
                db.SaveChanges();

                
                foreach (Team t in db.Teams.Include(t => t.Players))
                {
                    Console.WriteLine("Команда: {0}", t.Name);
                    foreach (Player pl in t.Players)
                    {
                        Console.WriteLine("{0} - {1}", pl.Name, pl.Position);
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
