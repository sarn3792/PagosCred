using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PagosCredijal
{
    public class Address
    {
        public String addressType;
        public String myAddress;
        public String betweenStreets;
        public String colony;
        public String city;
        public String CP;
        public String reference;

        public Address(String addressType, String myAddress, String betweenStreets, String colony, String city, String CP, String reference)
        {
            this.addressType = addressType;
            this.myAddress = myAddress;
            this.betweenStreets = betweenStreets;
            this.colony = colony;
            this.city = city;
            this.CP = CP;
            this.reference = reference;
        }
    }
}