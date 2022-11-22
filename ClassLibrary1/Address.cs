using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public struct Address
    {
        private string city;
        public string City { get { return city; } set { city = value; } }
        private string streetName;
        public string StreetName { get { return streetName; } set { streetName = value; } }
        private int streetNumber;
        public int StreetNumber { get { return streetNumber; } set { streetNumber = value; } }
    }
}
