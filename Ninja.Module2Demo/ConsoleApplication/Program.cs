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
            //SimpleNinjaQueries();
            //QueryAndUpdateNinja();
            //InsertNinjaWithEquipment();
            //SimpleNinjaGraph();
            //ProjectionQuery();
            EagerLoadingNinjas();
        }

        private static void EagerLoadingNinjas()
        {
            context.Database.Log = Console.WriteLine;
            var ninjas = context.Ninjas.Include(n => n.EquipmentOwned).ToList();
            foreach (var ninja in ninjas)
            {
                Console.WriteLine("Ninja: {0}, Equipment Count: {1}", ninja.Name, ninja.EquipmentOwned.Count);
            }
        }

        private static void ProjectionQuery()
        {
            context.Database.Log = Console.WriteLine;
            var ninjas = context.Ninjas
                .Select(n => new { n.Name, n.DateOfBirth, n.EquipmentOwned })
                .ToList();

        }


        private static void SimpleNinjaGraph()
        {
            //Example of Eager Loading: joining a child table with the query
            //var ninja = context.Ninjas
                //.Include(n => n.EquipmentOwned)
                //.FirstOrDefault(n => n.Name.StartsWith("Baby"));
            //Example of Lazy loading
           using (var lazyContext = new NinjaContext())
            {
                lazyContext.Database.Log = Console.WriteLine;
                var ninja = lazyContext.Ninjas.FirstOrDefault(n => n.Name.StartsWith("Baby"));
                Console.WriteLine("Ninja equipment count: {0}", ninja.EquipmentOwned.Count);
                Console.WriteLine("Ninja equipment count: {0}", ninja.EquipmentOwned.Count);
            }
        }

        private static void InsertNinjaWithEquipment()
        {
            var ninja = new Ninja()
            {
                Name = "Baby Noah",
                ServedInOniwaban = false,
                DateOfBirth = new DateTime(2007, 4, 2),
                ClanId = 1
            };

            var nunchunks = new NinjaEquipment()
            {
                Name = "nunchucks",
                Type = EquipmentType.Weapon
            };

            var grapplingHook = new NinjaEquipment()
            {
                Name = "grappling hook",
                Type = EquipmentType.Tool
            };
            context.Ninjas.Add(ninja);
            ninja.EquipmentOwned.Add(nunchunks);
            ninja.EquipmentOwned.Add(grapplingHook);
            context.SaveChanges();
        }

        private static void RemoveNinja()
        {
            var ninja = context.Ninjas.FirstOrDefault();
            //This works fine when the retrieval of the object and deletion action are in the same context frame
            //context.Ninjas.Remove(ninja);
            //context.SaveChanges();
            using (var differentContext = new NinjaContext())
            {
                //This is how we can delete when we have a ninja object but in a different context state:
                differentContext.Entry(ninja).State = EntityState.Deleted;
                differentContext.SaveChanges();
            }
        }

        private static void QueryAndUpdateNinja()
        {
            context.Database.Log = Console.WriteLine;
            var ninja = context.Ninjas.FirstOrDefault();
            ninja.ServedInOniwaban = (!ninja.ServedInOniwaban);

            using (var someContext = new NinjaContext())
            {
                someContext.Database.Log = Console.WriteLine;
                someContext.Ninjas.Attach(ninja);
                someContext.Entry(ninja).State = EntityState.Modified;
                someContext.SaveChanges();
            }
            
        }

        private static void SimpleNinjaQueries()
        {
            //Look up this use of using -> appears to be creating a block element vs using a global context variable
            var ninjas = context.Ninjas.Where(n => n.Name == "Leonardo");
            var singleNinja = context.Ninjas.Where(n => n.DateOfBirth >= new DateTime(1982, 1, 1)).FirstOrDefault();
            foreach (var ninja in ninjas)
            {
                Console.WriteLine(ninja.Name);
            }
            Console.WriteLine("Born after 01/01/82: " + singleNinja.Name);
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
