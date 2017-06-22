using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PagosCredijal
{
    public partial class Settings : System.Web.UI.Page
    {
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("LogIn.aspx");
            }
            else if (Session["UserName"].ToString() != "administrator" && Session["UserName"].ToString().ToLower() != "gtecobranza")
            {
                Response.Redirect("Default.aspx");
            }

            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.btnAddUser);
            scriptManager.RegisterPostBackControl(this.btnCrearCola);
            scriptManager.RegisterPostBackControl(this.gvQueueUsers);
            scriptManager.RegisterPostBackControl(this.btnGenerarReporte);
            scriptManager.RegisterPostBackControl(this.gvRecordReport);

            if (IsPostBack)
            {
                TabName.Value = Request.Form[TabName.UniqueID];
            }
            else
            {
                SetInitialData();
            }
        }
        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            try
            {
                User user = new User(txtUserName.Text.Trim(), txtUsrPassword.Text.Trim(), txtUsrFirstName.Text.Trim() + " " + txtUsrLastName.Text.Trim());
                ValidateUser validate = new ValidateUser(user);
                if (!validate.UserNameExist())
                {
                    RegisterUser register = new RegisterUser(user);
                    register.Register();
                    ClearRegisterUserData();
                    lblMessageAddUser.Text = "Usuario creado exitosamente";
                    lblMessageAddUser.CssClass = "successfully";
                    lblMessageAddUser.Visible = true;
                }
                else
                {
                    lblMessageAddUser.Text = "Ya existe un usuario con el Username: " + txtUserName.Text.Trim();
                    lblMessageAddUser.CssClass = "error";
                    lblMessageAddUser.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "tmp2", "var t2 =     document.getElementById('" + txtUserName.ClientID + "'); t2.focus();t2.value = t2.value;", true);
                }

            }
            catch (Exception ex)
            {
                lblMessageAddUser.Text = "Ha occurrido un error. Póngase en contacto con el administrador";
                lblMessageAddUser.CssClass = "error";
                lblMessageAddUser.Visible = true;
            }
        }

        protected void btnCrearCola_Click(object sender, EventArgs e)
        {
            try
            {

                QueueOperations queue = new QueueOperations(rblMora.SelectedItem != null ? true : false, cbScoring.Checked, txtContadormora.Text.Trim() != String.Empty ? true : false, cbMontoFinanciado.Checked,
                                                            cbMontoVencido.Checked, ddlUltimoEstatusRegistrado.SelectedItem.Value != "0" ? true : false, cbPromesaPagoRota.Checked, rblTipoFinanciamiento.SelectedItem != null ? true : false,
                                                            rblCreditoSimple.SelectedItem != null ? true : false, rblArrendamiento.SelectedItem != null ? true : false);

                queue.SaveQueueU(txtNombreCola.Text.Trim(), rblMora.SelectedItem != null ? rblMora.SelectedItem.Value : String.Empty, txtContadormora.Text.Trim(), 
                    ddlUltimoEstatusRegistrado.SelectedItem.Value != "0" ? ddlUltimoEstatusRegistrado.SelectedItem.Text : String.Empty
                    , rblTipoFinanciamiento.SelectedItem != null ? rblTipoFinanciamiento.SelectedItem.Value : String.Empty
                                , rblCreditoSimple.SelectedItem != null ? rblCreditoSimple.SelectedItem.Value : String.Empty, rblArrendamiento.SelectedItem != null ? rblArrendamiento.SelectedItem.Value : String.Empty);
                ClearQueueData();
                FillQueueUsersGrid();
                lblMensajeColasTrabajo.Text = "Cola creada correctamente";
                lblMensajeColasTrabajo.CssClass = "successfully";
                lblMensajeColasTrabajo.Visible = true;
            }
            catch (Exception ex)
            {
                lblMensajeColasTrabajo.Text = "Ha occurrido un error. Póngase en contacto con el administrador";
                lblMensajeColasTrabajo.CssClass = "error";
                lblMensajeColasTrabajo.Visible = true;
            }
        }

        protected void btnGenerarReporte_Click1(object sender, EventArgs e)
        {
            BindDataReport();
        }

        protected void gvQueueUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Find the DropDownList in the Row
                DropDownList ddlQueues = (e.Row.FindControl("ddlQueues") as DropDownList);
                DropDownElements.SetQueues(ddlQueues);

                //Select the Queue of User in DropDownList
                String queue = (e.Row.FindControl("lblQueue") as Label).Text;
                if (queue != String.Empty)
                {
                    ddlQueues.Items.FindByText(queue).Selected = true;
                }
            }
        }
        protected void ddlQueues_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl = (DropDownList)sender;
                GridViewRow row = (GridViewRow)ddl.Parent.Parent;
                String userName = HttpUtility.HtmlDecode(row.Cells[1].Text);
                String pkUser = new User(userName).GetID();
                String pkQueue = ddl.SelectedItem.Value;

                QueueOperations qo = new QueueOperations(Convert.ToInt32(pkUser), Convert.ToInt32(pkQueue));
                qo.SaveQueueByUser();
            } catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void gvRecordReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvRecordReport.PageIndex = e.NewPageIndex;
                BindDataReport();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }
        #endregion

        private void ClearRegisterUserData()
        {
            txtUsrFirstName.Text = String.Empty;
            txtUsrLastName.Text = String.Empty;
            txtUserName.Text = String.Empty;
        }

        private void BindDataReport()
        {
            try
            {
                String tipoReporte = rblReportes.SelectedItem.Value;
                DateTime fechaFinal = DateTime.ParseExact(DatePicker.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                Reports reporte = new Reports(fechaFinal);
                switch (tipoReporte)
                {
                    case "dia":
                        gvRecordReport.DataSource = reporte.GetReportByDay();
                        gvRecordReport.DataBind();
                        break;

                    case "semana":
                        gvRecordReport.DataSource = reporte.GetReportByWeek();
                        gvRecordReport.DataBind();
                        break;

                    case "mes":
                        gvRecordReport.DataSource = reporte.GetReportByMonth();
                        gvRecordReport.DataBind();
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ClearQueueData()
        {
            rblMora.SelectedIndex = -1;
            cbScoring.Checked = false;
            txtContadormora.Text = String.Empty;
            cbMontoFinanciado.Checked = false;
            cbMontoVencido.Checked = false;
            ddlUltimoEstatusRegistrado.SelectedValue = "0";
            cbPromesaPagoRota.Checked = false;
            rblTipoFinanciamiento.SelectedIndex = -1;
            rblCreditoSimple.SelectedIndex = -1;
            rblArrendamiento.SelectedIndex = -1;
            txtNombreCola.Text = String.Empty;
        }

        private void SetInitialData()
        {
            FillQueueUsersGrid();
            FillDropDown();
        }

        private void FillQueueUsersGrid()
        {
            gvQueueUsers.DataSource = QueueOperations.GetQueueByUsers();
            gvQueueUsers.DataBind();
        }

        private void FillDropDown()
        {
            DropDownElements.SetStatus(ddlUltimoEstatusRegistrado);
        }


    }
}