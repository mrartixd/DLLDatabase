using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldDatabase
{
    public class Country //:IFormattable
    {
        public String Code { get; set; }
        public String Name { get; set; }
        public String Continent { get; set; }
        public String Region { get; set; }
        public double SurfaceArea { get; set; }
        public double InderYear { get; set; }
        public double Population { get; set; }
        public String GovernmentForm { get; set; }
        public String HeadOfState { get; set; }
        //public City Capital { get; set; }

        public Country() { }
        //public Country(parameters) 

        //public string ToString(string format, IFormatProvider formatProvider)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
