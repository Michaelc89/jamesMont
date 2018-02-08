using System;
using System.Collections.Generic;
using System.Text;

namespace jamesMont.Model
{
    public class TimesClass
    {
        public string Time { get; set; }

        public TimesClass()
        {
        }

        public TimesClass(string t)
        {
            this.Time = t;
        }

        public override string ToString()
        {
            return this.Time;
        }
    }
}
