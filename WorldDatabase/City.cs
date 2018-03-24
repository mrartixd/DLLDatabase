using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldDatabase
{
    public class City //:IFormattable
    {
        public double ID { get; set; }
        public String Name { get; set; }
        public String CountryCode { get; set; }
        public String District { get; set; }
        public double Population { get; set; }

        public City() { }
        //public City(parameters) 

        //public string ToString(string format, IFormatProvider formatProvider)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
