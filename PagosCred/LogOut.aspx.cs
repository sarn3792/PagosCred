using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PagosCredijal
{
    public partial class LogOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("LogIn.aspx");
            }
        }

        private void SetPaymentFree()
        {
            PaymentsSL payments = new PagosCredijal.PaymentsSL();

            DataTable data = HttpContext.Current.Session["Information"] as DataTable;
            if (data.Rows.Count > 0)
            {
                payments.CustomerID = data.Rows[0]["PKCliente"].ToString().Trim();
                payments.SetPaymentFree();
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            SetPaymentFree();
            Session.Clear();
            Session.Abandon();
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            try
            {
                Session.Abandon();
                FormsAuthentication.SignOut();
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Buffer = true;
                Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
                Response.Expires = -1000;
                Response.CacheControl = "no-cache";
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            Response.Redirect("~/LogIn.aspx");
        }
    }
}