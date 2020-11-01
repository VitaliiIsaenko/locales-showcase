using System;
using System.Collections.Generic;

namespace SessionsShowcase
{
    public class Session
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
        public List<SessionTypeLocale> SessionTypeLocales { get; set; }
    }
}