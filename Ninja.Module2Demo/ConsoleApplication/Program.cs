using System;
using NinjaDomain.Classes;
using NinjaDomain.DataModel;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    class Program
    {
        private static NinjaContext context = new NinjaContext();

        static void Main(string[] args)
        {
            Database.SetInitializer(new NullDatabaseInitializer<NinjaContext>()); //Look this up
            //InsertNinja();
            SimpleNinjaQueries();
        }

        private static void SimpleNinjaQueries()
        {
            //Look up this use of using -> appears to be creating a block element vs using a global context variable
            var ninjas = context.Ninjas.Where(n => n.Name == "Leonardo");
            var singleNinja = context.Ninjas.Where(n => n.DateOfBirth >= new DateTime(1985, 1, 1)).FirstOrDefault();
            foreach (var ninja in ninjas)
            {
                Console.WriteLine(ninja.Name);
            }
            Console.WriteLine(singleNinja.Name);
        }

        private static void InsertNinja()
        {
            var ninja1 = new Ninja()
            {
                Name = "Raphael",
                ServedInOniwaban = false,
                DateOfBirth = new DateTime(1985, 1, 2),
                ClanId = 1
            };

            var ninja2 = new Ninja()
            {
                Name = "Leonardo",
                ServedInOniwaban = false,
                DateOfBirth = new DateTime(1984, 2, 23),
                ClanId = 1
            };
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine; //Logging feature in Entity Framework
                context.Ninjas.AddRange(new List<Ninja> { ninja1, ninja2 });
                context.SaveChanges();
            }
        }
    }
}
