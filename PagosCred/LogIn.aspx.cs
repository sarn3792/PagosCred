using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PagosCredijal
{
    public partial class LogIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                User user = new User(txtUser.Text.Trim(), txtPassword.Text.Trim());
                ValidateUser validate = new ValidateUser(user);
                if (validate.UserExists()) //user and password correct
                {
                    lblError.Visible = false;
                    Session["UserName"] = user.userName;
                    Session["IDUser"] = user.GetID();
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    lblError.Visible = true; //show error
                }
            } catch (Exception ex)
            {

            }
        }
    }
}