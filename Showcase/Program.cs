using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SessionsShowcase;

namespace Showcase
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

            List<Session> sessions = new List<Session>();
            string sessionsString;
            using (var context = new Context())
            {
                sessions = context.Sessions.Include(a => a.SessionTypeLocales).ToList();
                sessionsString = JsonConvert.SerializeObject(sessions);
            }

            Console.WriteLine(sessionsString);
            Console.ReadLine();

            sessions[0].TypeId = 2;
            using (var context = new Context())
            {
                context.Sessions.Update(sessions[0]);
                context.SaveChanges();
            }
            
            using (var context = new Context())
            {
                sessions = context.Sessions.Include(a => a.SessionTypeLocales).ToList();
                sessionsString = JsonConvert.SerializeObject(sessions);
            }

            Console.WriteLine(sessionsString);
            Console.ReadLine();
        }
    }
}