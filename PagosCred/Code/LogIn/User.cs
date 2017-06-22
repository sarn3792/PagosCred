using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PagosCredijal
{
    public class User
    {
        public String userName;
        public String password;
        public String name;
        private String IDUser;

        public User(String user, String pass)
        {
            this.userName = user;
            this.password = pass;
        }

        public User(String user, String pass, String name)
        {
            this.userName = user;
            this.password = pass;
            this.name = name;
        }

        public User(String userName)
        {
            this.userName = userName;
        }

        public String GetID()
        {
            String query = String.Format("SELECT IDUser FROM PaymentsUsers WHERE UserName = '{0}'", userName);
            DataBaseSettings db = new DataBaseSettings();
            DataTable aux = db.GetDataTable(query);
            IDUser = aux.Rows.Count > 0 ? aux.Rows[0]["IDUser"].ToString() : "IDUser was not found";
            return IDUser;
        }
    }
}