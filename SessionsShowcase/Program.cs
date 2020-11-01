using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace SessionsShowcase
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new Context())
            {
                context.Database.EnsureCreated();

                context.Database.ExecuteSqlCommand(
                    "REPLACE INTO SessionTypeLocale (id, locale, name) values (1,'en', 'External web session')");
                context.Database.ExecuteSqlCommand(
                    "REPLACE INTO SessionTypeLocale (id, locale,name) values (1,'de','Externe Web-Sitzung')");

                context.Database.ExecuteSqlCommand(
                    "REPLACE INTO SessionTypeLocale (id, locale, name) values (2,'en', 'Embedded web session')");
                context.Database.ExecuteSqlCommand(
                    "REPLACE INTO SessionTypeLocale (id, locale,name) values (2,'de','Eingebettete Web-Sitzung')");

                context.Database.ExecuteSqlCommand("REPLACE INTO Session (name, formatId) values ('first session',1)");
                context.Database.ExecuteSqlCommand("REPLACE INTO Session (name, formatId) values ('second session',2)");

                context.SaveChanges();
            }

            string addresses;
            using (var context = new Context())
            {
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                var states = context.Sessions.Include(a => a.SessionTypeLocales).ToList();
                addresses = JsonConvert.SerializeObject(states);
            }

            Console.WriteLine(addresses);
            Console.ReadLine();
        }
    }
}