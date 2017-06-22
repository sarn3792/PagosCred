using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PagosCredijal
{
    public class Phone
    {
        public String phoneType;
        public String phone;

        public Phone(String phoneType, String phone)
        {
            this.phoneType = phoneType;
            this.phone = phone;
        }
    }
}