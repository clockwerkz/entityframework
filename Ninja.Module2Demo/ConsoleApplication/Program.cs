using System;
using NinjaDomain.Classes;
using NinjaDomain.DataModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        private static void InsertNinja()
        {
            var ninja = new Ninja()
            {
                Name = "CarlosSan",
                ServedInOniwaban = false,
                DateOfBirth = new DateTime(1974, 11, 2),
                ClanId = 1
            };
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine; //Logging feature in Entity Framework
                context.Ninjas.Add(ninja);
                context.SaveChanges();
            }
        }
    }
}
