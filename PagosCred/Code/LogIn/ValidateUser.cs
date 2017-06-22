using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace PagosCredijal
{
    public class ValidateUser
    {
        private User user;
        public ValidateUser(User user)
        {
            this.user = user;
        }

        public bool UserExists()
        {
            DataTable data = new DataTable();
            try
            {
                String query = String.Format("Select * from PaymentsUsers where UserName = '{0}' and Password = '{1}'", user.userName, user.password);

                DataBaseSettings dbObject = new DataBaseSettings();
                data = dbObject.GetDataTable(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return data.Rows.Count > 0 ? true : false;
        }

        public bool UserNameExist()
        {
            DataTable data = new DataTable();
            try
            {
                String query = String.Format("Select * from PaymentsUsers where UserName = '{0}'", user.userName);

                DataBaseSettings dbObject = new DataBaseSettings();
                data = dbObject.GetDataTable(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return data.Rows.Count > 0 ? true : false;
        }
    }
}