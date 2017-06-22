using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagosCredijal
{
    public class RegisterUser
    {
        private User user;
        public RegisterUser(User user)
        {
            this.user = user;
        }
        public void Register()
        {
            try
            {
                String query = String.Format("INSERT INTO PaymentsUsers (UserName, Password, Name) VALUES ('{0}', '{1}', '{2}')", user.userName, user.password, user.name);
                DataBaseSettings db = new DataBaseSettings();
                db.ExecuteQuery(query);
            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
