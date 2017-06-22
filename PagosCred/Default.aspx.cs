using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PagosCredijal
{
    public partial class Default : System.Web.UI.Page
    {
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("LogIn.aspx");
            }
            else if (Session["UserName"].ToString() == "administrator" || Session["UserName"].ToString().ToLower() == "gtecobranza")
            {
                hrefSettings.Visible = true;
            }

            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.btnCalificarLlamada);
            scriptManager.RegisterPostBackControl(this.btnAddTelefonoTit);
            scriptManager.RegisterPostBackControl(this.btnAddDomicilioTit);
            scriptManager.RegisterPostBackControl(this.btnAddTelefonoAval);
            scriptManager.RegisterPostBackControl(this.btnAddDomicilioAval);
            scriptManager.RegisterPostBackControl(this.btnBitacora);
            scriptManager.RegisterPostBackControl(this.ddlBitStatus);
            scriptManager.RegisterPostBackControl(this.btnNumCliente);
            scriptManager.RegisterPostBackControl(this.rblType);
            scriptManager.RegisterPostBackControl(this.btnObtenerSaldos);
            scriptManager.RegisterPostBackControl(this.btnSearchByName);

            if (IsPostBack)
            {
                TabName.Value = Request.Form[TabName.UniqueID];
                lblMessageScreen1.Visible = false;
            }
            else
            {
                PaymentsSL payments = new PaymentsSL();
                Session["Information"] = payments.GetFirstPaymentByUser(Session["IDUser"].ToString());
                Session["StartTime"] = DateTime.Now;
                
                DataTable data = Session["Information"] as DataTable;
                if(data.Rows.Count > 0)
                {
                    payments.CustomerID = data.Rows[0]["PKCliente"].ToString().Trim();
                    payments.SetPaymentBusy();
                    SetInitialData();
                    btnBitacora.Enabled = true;
                }
                else //there aren't elements in queue
                {

                    DisableFields();
                    FillDropDowns();
                    btnBitacora.Enabled = false;
                }
            }
        }

        protected void btnBitacora_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable data = Session["information"] as DataTable;
                String FKCliente = data.Rows.Count > 0 ? data.Rows[0]["PKCliente"].ToString().Trim() : String.Empty;
                PaymentsRecords bitacora = new PaymentsRecords(Convert.ToInt32(Session["IDUser"].ToString()), FKCliente, Convert.ToInt32(ddlBitTipoLlamada.SelectedValue), txtBitTelefonoMarcado.Text.Trim(), Convert.ToInt32(ddlBitStatus.SelectedValue), txtBitComentarios.Text.Trim(), (DateTime)Session["StartTime"]);
                if (ddlBitStatus.SelectedItem.Text == "Promesa de pago")
                {
                    //DateTime date = DateTime.ParseExact(DatePicker.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    bitacora.Save(DateTime.ParseExact(DatePicker.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture), Convert.ToDecimal(txtCantidadPrometida.Text));
                }
                else
                {
                    bitacora.Save();
                }

                SetPaymentFree();
                RestartPaymentRecordData();
                SetDataToRecordGrid();
                lblBitMessage.Text = "Información guardada correctamente";
                lblBitMessage.CssClass = "successfully";
                lblBitMessage.Visible = true;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Información guardada correctamente.'); window.location.href=window.location;", true);
            }
            catch (Exception ex)
            {
                lblBitMessage.Text = "Ha occurrido un error. Póngase en contacto con el administrador";
                lblBitMessage.CssClass = "error";
                lblBitMessage.Visible = true;
            }
        }

        protected void btnObtenerSaldos_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime date = DateTime.ParseExact(DataPickerB.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                Balance bl = (new PaymentsSL(txtNumCliente.Text.Trim())).GetBalances(date);
                FillBalances(bl);
            } catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void btnAddDomicilioAval_Click(object sender, EventArgs e)
        {
            lblAvalAddPhone.Visible = false;

            try
            {
                DataTable data = Session["information"] as DataTable;
                String FKCustomer = data.Rows.Count > 0 ? data.Rows[0]["PKCliente"].ToString().Trim() : String.Empty;
                //String FKInvoice = data.Rows.Count > 0 ? data.Rows[0]["PKFactura"].ToString().Trim() : String.Empty;
                Guarantee gt = new Guarantee(FKCustomer /*, FKInvoice */); //It should be the SL Custumer ID 
                gt.SaveAddrees(new Address(ddlAvalTipoDomicilio.SelectedItem.Text, txtAvalAddDomicilio.Text.Trim(), txtAvalAddEntreCalles.Text.Trim(), txtAvalAddColonia.Text.Trim(), txtAvalAddCiudad.Text.Trim(), txtAvalAddCP.Text.Trim(), txtAvalAddPtoRef.Text.Trim()));
                RestartGuaranteeAddressData();
                SetDataToGuaranteeAddress();
                lblAvalAddAddress.Text = "Domicilio guardado correctamente";
                lblAvalAddAddress.CssClass = "successfully";
                lblAvalAddAddress.Visible = true;
            }
            catch (Exception ex)
            {
                lblAvalAddAddress.Text = "Ha occurrido un error. Póngase en contacto con el administrador";
                lblAvalAddAddress.CssClass = "error";
                lblAvalAddAddress.Visible = true;
            }
        }

        protected void btnAddTelefonoAval_Click(object sender, EventArgs e)
        {
            lblAvalAddAddress.Visible = false;

            try
            {
                DataTable data = Session["information"] as DataTable;
                String FKCustomer = data.Rows.Count > 0 ? data.Rows[0]["PKCliente"].ToString().Trim() : String.Empty;
                //String FKInvoice = data.Rows.Count > 0 ? data.Rows[0]["PKFactura"].ToString().Trim() : String.Empty;
                Guarantee gt = new Guarantee(FKCustomer/*, FKInvoice */);
                gt.SavePhone(new Phone(ddlTipoTelefonoAval.SelectedItem.Text, txtAvalAddTelefonoAval.Text.Trim()));
                RestartGuaranteePhoneData();
                SetDataToGuaranteePhone();
                lblAvalAddPhone.Text = "Teléfono guardado correctamente";
                lblAvalAddPhone.CssClass = "successfully";
                lblAvalAddPhone.Visible = true;
            }
            catch (Exception ex)
            {
                lblAvalAddPhone.Text = "Ha occurrido un error. Póngase en contacto con el administrador";
                lblAvalAddPhone.CssClass = "error";
                lblAvalAddPhone.Visible = true;
            }
        }

        protected void btnAddDomicilioTit_Click(object sender, EventArgs e)
        {
            lblTitAddPhone.Visible = false; //hide phone label

            try
            {
                DataTable data = Session["information"] as DataTable;
                String FKCustomer = data.Rows.Count > 0 ? data.Rows[0]["PKCliente"].ToString().Trim() : String.Empty;
                Holder holder = new Holder(FKCustomer);
                holder.SaveAddrees(new Address(ddlTitTipoDomicilio.SelectedItem.Text, txtTitAddDomicilio.Text.Trim(), txtTitAddEntreCalles.Text.Trim(), txtTitAddColonia.Text.Trim(), txtTitAddCiudad.Text.Trim(), txtTitAddCP.Text.Trim(), txtTitAddPtoRef.Text.Trim()));
                RestartHolderAddressData();
                SetDataToHolderAddress();
                lblTitAddAddress.Text = "Domicilio guardado correctamente";
                lblTitAddAddress.CssClass = "successfully";
                lblTitAddAddress.Visible = true;
            }
            catch (Exception ex)
            {
                lblTitAddAddress.Text = "Ha occurrido un error. Póngase en contacto con el administrador";
                lblTitAddAddress.CssClass = "error";
                lblTitAddAddress.Visible = true;
            }
        }

        protected void btnAddTelefonoTit_Click(object sender, EventArgs e)
        {
            lblTitAddAddress.Visible = false; //hide address label

            try
            {
                DataTable data = Session["information"] as DataTable;
                String FKCustomer = data.Rows.Count > 0 ? data.Rows[0]["PKCliente"].ToString().Trim() : String.Empty;
                Holder holder = new Holder(FKCustomer);
                holder.SavePhone(new Phone(ddlTipoTelefonoTit.SelectedItem.Text, txtTitAddTelefonoTit.Text.Trim()));
                RestartHolderPhoneData();
                SetDataToHolderPhone();
                lblTitAddPhone.Text = "Teléfono guardado correctamente";
                lblTitAddPhone.CssClass = "successfully";
                lblTitAddPhone.Visible = true;
            }
            catch (Exception ex)
            {
                lblTitAddPhone.Text = "Ha occurrido un error. Póngase en contacto con el administrador";
                lblTitAddPhone.CssClass = "error";
                lblTitAddPhone.Visible = true;
            }
        }
        /// <summary>
        /// It catch when user clicks enter on Num Cliente's textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNumCliente_Click(object sender, EventArgs e)
        {
            try
            {
                PaymentsSL payments = new PaymentsSL();
                DataTable aux = payments.GetFirstPayment(txtNumCliente.Text.Trim());
                if (aux.Rows.Count > 0)
                {
                    Session["Information"] = aux;
                    Session["StartTime"] = DateTime.Now;
                    FillTextBoxes();
                    SetDataToGrids();
                    ClearBalances();
                }
                else
                {
                    lblMessageScreen1.Text = String.Format("El cliente con el código {0} no fue encontrado", txtNumCliente.Text.Trim());
                    lblMessageScreen1.CssClass = "error";
                    lblMessageScreen1.Visible = true;
                    ClearAllData();
                }
                //pnlMainScreen.Enabled = false;
            }
            catch (Exception ex)
            {

            }
        }

        protected void ddlBitStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBitStatus.SelectedItem.Text == "Promesa de pago")
            {
                //enable DataPicker and quantity
                pnlPromesaPago.Visible = true;
            }
        }

        protected void rblType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                PaymentsSL payments = new PaymentsSL(txtNumCliente.Text.Trim());
                payments.SetPaymentFree();
                ClearAllData();
                if (rblType.SelectedItem.Value == "Automatico")
                {
                    //queue
                    Session["Information"] = payments.GetFirstPaymentByUser(Session["IDUser"].ToString());
                    Session["StartTime"] = DateTime.Now;

                    DataTable data = Session["Information"] as DataTable;
                    if (data.Rows.Count > 0)
                    {
                        payments.CustomerID = data.Rows[0]["PKCliente"].ToString().Trim();
                        payments.SetPaymentBusy();
                        SetInitialData();
                        btnBitacora.Enabled = true;
                    }


                    btnSearchByName.Visible = false;
                    pnlMainScreen.Enabled = false;
                }
                else
                {
                    pnlMainScreen.Enabled = true;
                    txtNombre.Focus();
                    btnSearchByName.Visible = true;
                }
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnSearchByName_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                PaymentsSL payments = new PaymentsSL();
                DataTable aux = payments.GetFirstPaymentByName(txtNombre.Text.Trim());
                if (aux.Rows.Count > 0)
                {
                    Session["Information"] = aux;
                    Session["StartTime"] = DateTime.Now;
                    FillTextBoxes();
                    SetDataToGrids();
                    ClearBalances();
                }
                else
                {
                    lblMessageScreen1.Text = String.Format("El cliente con el nombre {0} no fue encontrado", txtNombre.Text.Trim());
                    lblMessageScreen1.CssClass = "error";
                    lblMessageScreen1.Visible = true;
                    ClearAllData();
                }

            } catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void hrefSettings_ServerClick(object sender, EventArgs e)
        {
            try
            {
                PaymentsSL payments = new PaymentsSL(txtNumCliente.Text.Trim());
                payments.SetPaymentFree();
                Response.Redirect("Settings.aspx");
            } catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region PaymentRecord
        private void RestartPaymentRecordData()
        {
            ddlBitStatus.SelectedValue = "0";
            ddlBitTipoLlamada.SelectedValue = "0";
            txtBitTelefonoMarcado.Text = String.Empty;
            txtBitComentarios.Text = String.Empty;
        }

        private void SetDataToRecordGrid()
        {
            DataTable data = Session["information"] as DataTable;
            String FKCliente = data.Rows.Count > 0 ? data.Rows[0]["PKCliente"].ToString().Trim() : String.Empty;
            PaymentsRecords bitacora = new PaymentsRecords(FKCliente);
            gvReport.DataSource = bitacora.Get();
            gvReport.DataBind();
        }

        private void SetDataToRecord2Grid()
        {
            DataTable data = Session["information"] as DataTable;
            String FKCliente = data.Rows.Count > 0 ? data.Rows[0]["PKCliente"].ToString().Trim() : String.Empty;
            PaymentsRecords bitacora = new PaymentsRecords(FKCliente);
            gvReport2.DataSource = bitacora.GetReport2();
            gvReport2.DataBind();
        }
        #endregion

        #region HolderData
        private void RestartHolderAddressData()
        {
            ddlTitTipoDomicilio.SelectedValue = "0";
            txtTitAddDomicilio.Text = String.Empty;
            txtTitAddEntreCalles.Text = String.Empty;
            txtTitAddCiudad.Text = String.Empty;
            txtTitAddColonia.Text = String.Empty;
            txtTitAddCP.Text = String.Empty;
            txtTitAddPtoRef.Text = String.Empty;
        }

        private void RestartHolderPhoneData()
        {
            ddlTipoTelefonoTit.SelectedValue = "0";
            txtTitAddTelefonoTit.Text = String.Empty;
        }

        private void SetDataToHolderAddress()
        {
            DataTable data = Session["information"] as DataTable;
            String FKCustomer = data.Rows.Count > 0 ? data.Rows[0]["PKCliente"].ToString().Trim() : String.Empty;
            Holder holder = new Holder(FKCustomer);
            gvDomicilioTitular.DataSource = holder.GetAddress();
            gvDomicilioTitular.DataBind();
        }

        private void SetDataToHolderPhone()
        {
            DataTable data = Session["information"] as DataTable;
            String FKCustomer = data.Rows.Count > 0 ? data.Rows[0]["PKCliente"].ToString().Trim() : String.Empty;
            Holder holder = new Holder(FKCustomer); //It should be the SL Custumer ID
            gvTelefonosTitular.DataSource = holder.GetPhone();
            gvTelefonosTitular.DataBind();
        }

        #endregion

        #region Guarantee
        private void RestartGuaranteeAddressData()
        {
            ddlAvalTipoDomicilio.SelectedValue = "0";
            txtAvalAddDomicilio.Text = String.Empty;
            txtAvalAddEntreCalles.Text = String.Empty;
            txtAvalAddCiudad.Text = String.Empty;
            txtAvalAddColonia.Text = String.Empty;
            txtAvalAddCP.Text = String.Empty;
            txtAvalAddPtoRef.Text = String.Empty;
        }

        private void RestartGuaranteePhoneData()
        {
            ddlTipoTelefonoAval.SelectedValue = "0";
            txtAvalAddTelefonoAval.Text = String.Empty;
        }

        private void SetDataToGuaranteeAddress()
        {
            DataTable data = Session["information"] as DataTable;
            String FKCustomer = data.Rows.Count > 0 ? data.Rows[0]["PKCliente"].ToString().Trim() : String.Empty;
            //String FKInvoice = data.Rows.Count > 0 ? data.Rows[0]["PKFactura"].ToString().Trim() : String.Empty;
            Guarantee gt = new Guarantee(FKCustomer /*, FKInvoice */); //It should be the SL Custumer ID and Gurantee ID
            gvDomicilioAval.DataSource = gt.GetAddress();
            gvDomicilioAval.DataBind();
        }

        private void SetDataToGuaranteePhone()
        {
            DataTable data = Session["information"] as DataTable;
            String FKCustomer = data.Rows.Count > 0 ? data.Rows[0]["PKCliente"].ToString().Trim() : String.Empty;
            //String FKInvoice = data.Rows.Count > 0 ? data.Rows[0]["PKFactura"].ToString().Trim() : String.Empty;
            Guarantee gt = new Guarantee(FKCustomer/* , FKInvoice */); //It should be the SL Custumer ID and Gurantee ID
            gvTelefonoAval.DataSource = gt.GetPhone();
            gvTelefonoAval.DataBind();
        }

        #endregion

        #region FrontEnd
        private void SetDataToGrids()
        {
            SetDataToRecordGrid();
            SetDataToRecord2Grid();
            SetDataToHolderAddress();
            SetDataToHolderPhone();
            SetDataToGuaranteeAddress();
            SetDataToGuaranteePhone();
        }

        private void FillTextBoxes()
        {
            DataTable information = Session["Information"] as DataTable;
            if (information.Rows.Count > 0) // it has information...
            {
                //first tab
                txtNombre.Text = information.Rows[0]["NombreTitular"].ToString().Trim();
                txtNumCliente.Text = information.Rows[0]["PKCliente"].ToString().Trim();
                txtMovil.Text = information.Rows[0]["CelularTitular"].ToString().Trim();
                txtCasa.Text = information.Rows[0]["TelefonoCasa"].ToString().Trim();
                txtEmpleo.Text = information.Rows[0]["TelefonoEmpleo"].ToString().Trim();
                txtOtros1.Text = information.Rows[0]["Nextel"].ToString().Trim();
                txtOtros2.Text = information.Rows[0]["LargaDistanciaLocal"].ToString().Trim();
                txtCorreoElectronico.Text = information.Rows[0]["CorreoElectronico"].ToString().Trim();
                txtFechaMaxVencimiento.Text = information.Rows[0]["FechaMaximoVencimiento"].ToString().Trim();
                txtMontoFinanciado.Text = String.Format("{0:C}", information.Rows[0]["MontoFinanciado"]);
                txtMontoActual.Text = String.Format("{0:C}", information.Rows[0]["MontoActual"]);
                txtFechaUltimaPago.Text = information.Rows[0]["FechaUltimoPago"].ToString().Trim();
                txtMontoUltimoPago.Text = String.Format("{0:C}", information.Rows[0]["MontoUltimoPago"]);

                txtNumRefPago1.Text = information.Rows[0]["NumReferencia1"].ToString().Trim() != String.Empty && information.Rows[0]["NumReferencia1"] != DBNull.Value
                                        ? information.Rows[0]["NumReferencia1"].ToString().Trim() : information.Rows[0]["NumReferencia2"].ToString().Trim();
                txtSerieDoctoCurso.Text = information.Rows[0]["DocumentoCurso"].ToString().Trim();
                txtUnidadFinanciada.Text = information.Rows[0]["UnidadEquipoFinanciado"].ToString().Trim();
                txtDoctosVencidos.Text = information.Rows[0]["DocumentosVencidos"].ToString().Trim();
                txtDiasDcoMaximoVencidos.Text = information.Rows[0]["DiasVencidosDocumento"].ToString().Trim();
                txtTipoCredito.Text = information.Rows[0]["TipoCredito"].ToString().Trim();
                txtHistoricoDeMora.Text = information.Rows[0]["HistorialMora"].ToString().Trim();

                //contracts
                DataTable aux = new PaymentsSL().GetContracts(txtNumCliente.Text);
                if(aux.Rows.Count > 0)
                {
                    txtNumContrato1.Text = aux.Rows[0]["Contrato"].ToString();
                    txtNumContrato2.Text = aux.Rows.Count > 1 ? aux.Rows[1]["Contrato"].ToString() : String.Empty;
                    txtNumContrato3.Text = aux.Rows.Count > 2 ? aux.Rows[2]["Contrato"].ToString() : String.Empty;
                }

                //second tab
                txtTitNombre.Text = information.Rows[0]["NombreTitular"].ToString().Trim();
                txtTitNumCliente.Text = information.Rows[0]["PKCliente"].ToString().Trim();
                txtTitEmail.Text = information.Rows[0]["CorreoElectronico"].ToString().Trim();
                txtTitNumRef1.Text = information.Rows[0]["NumReferencia1"].ToString().Trim();
                txtTitNumRef2.Text = information.Rows[0]["NumReferencia2"].ToString().Trim();
                //thrid tab
                txtAvalNombre.Text = information.Rows[0]["NombreAval"].ToString().Trim();
                txtAvalNumCliente.Text = information.Rows[0]["PKCliente"].ToString().Trim();
                if(txtAvalNombre.Text == String.Empty)
                {
                    btnAddDomicilioAval.Enabled = false;
                    btnAddTelefonoAval.Enabled = false;
                }

                //fourth tab
                txtBitNombre.Text = information.Rows[0]["NombreTitular"].ToString().Trim();
                txtBitNumCliente.Text = information.Rows[0]["PKCliente"].ToString().Trim();
                txtBitNumRef1.Text = information.Rows[0]["NumReferencia1"].ToString().Trim();
                txtBitNumRef2.Text = information.Rows[0]["NumReferencia2"].ToString().Trim();

            }

        }

        private void ClearAllData()
        {
            txtNombre.Text = String.Empty;
            txtNumCliente.Text = String.Empty;
            txtMovil.Text = String.Empty;
            txtCasa.Text = String.Empty;
            txtEmpleo.Text = String.Empty;
            txtOtros1.Text = String.Empty;
            txtOtros2.Text = String.Empty;
            txtCorreoElectronico.Text = String.Empty;
            txtFechaMaxVencimiento.Text = String.Empty;
            txtMontoFinanciado.Text = String.Empty;
            txtMontoActual.Text = String.Empty;
            txtFechaUltimaPago.Text = String.Empty;
            txtMontoUltimoPago.Text = String.Empty;

            txtNumRefPago1.Text = String.Empty;
            txtSerieDoctoCurso.Text = String.Empty;
            txtUnidadFinanciada.Text = String.Empty;
            txtDoctosVencidos.Text = String.Empty;
            txtDiasDcoMaximoVencidos.Text = String.Empty;
            txtTipoCredito.Text = String.Empty;
            txtHistoricoDeMora.Text = String.Empty;
            txtNumContrato1.Text = String.Empty;
            txtNumContrato2.Text = String.Empty;
            txtNumContrato3.Text = String.Empty;
            ClearBalances();

            //second tab
            txtTitNombre.Text = String.Empty;
            txtTitNumCliente.Text = String.Empty;
            txtTitEmail.Text = String.Empty;
            txtTitNumRef1.Text = String.Empty;
            txtTitNumRef2.Text = String.Empty;
            //grids
            gvDomicilioTitular.DataSource = null;
            gvDomicilioTitular.DataBind();
            gvTelefonosTitular.DataSource = null;
            gvTelefonosTitular.DataBind();
            RestartHolderPhoneData();
            //cmb
            RestartHolderAddressData();
            RestartHolderPhoneData();

            //thrid tab
            txtAvalNombre.Text = String.Empty;
            txtAvalNumCliente.Text = String.Empty;
            //grids
            gvDomicilioAval.DataSource = null;
            gvDomicilioAval.DataBind();
            gvTelefonoAval.DataSource = null;
            gvTelefonoAval.DataBind();
            RestartGuaranteeAddressData();
            RestartGuaranteePhoneData();

            //fourth tab
            txtBitNombre.Text = String.Empty;
            txtBitNumCliente.Text = String.Empty;
            txtBitNumRef1.Text = String.Empty;
            txtBitNumRef2.Text = String.Empty;
            RestartPaymentRecordData();
            //grids
            gvReport.DataSource = null;
            gvReport.DataBind();
            gvReport2.DataSource = null;
            gvReport2.DataBind();
        }
        private void SetInitialData()
        {
            FillTextBoxes();
            SetDataToGrids();
            FillDropDowns();
            DisableFields();
        }

        private void FillBalances(Balance bl)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyNegativePattern = 1;

            txtVigente.Text = String.Format("{0:C}", bl.Vigente);
            txt30Dias.Text = String.Format("{0:C}", bl.TDias);
            txt60Dias.Text = String.Format("{0:C}", bl.SDias);
            txt90Dias.Text = String.Format("{0:C}", bl.NDias);
            txtMas90Dias.Text = String.Format("{0:C}", bl.MDias);
            txtInteresMoratorio.Text = String.Format("{0:C}", bl.InteresMoratorio);
            txtSaldoVencido.Text = String.Format("{0:C}", bl.TotalSaldoVencido);
            txtSaldoCartera.Text = String.Format("{0:C}", bl.SaldoCartera);
            txtInteresNoDevengado.Text = String.Format("{0:C}", bl.InteresNoDevengado);
            txtSaldoParaLiquidar.Text = String.Format("{0:C}", bl.SaldoParaLiquidar);
        }

        private void ClearBalances()
        {
            txtVigente.Text = String.Empty;
            txt30Dias.Text = String.Empty;
            txt60Dias.Text = String.Empty;
            txt90Dias.Text = String.Empty;
            txtMas90Dias.Text = String.Empty;
            txtInteresMoratorio.Text = String.Empty;
            txtSaldoVencido.Text = String.Empty;
            txtSaldoCartera.Text = String.Empty;
            txtInteresNoDevengado.Text = String.Empty;
            txtSaldoParaLiquidar.Text = String.Empty;
            DataPickerB.Text = String.Empty;

        }
            
        private void DisableFields()
        {
            pnlMainScreen.Enabled = false;
            pnlCustomerData.Enabled = false;
            pnlAval.Enabled = false;
            pnlPaymentsRecords.Enabled = false;
        }

        private void FillDropDowns()
        {
            DropDownElements.SetCallTypes(ddlBitTipoLlamada);
            DropDownElements.SetStatus(ddlBitStatus);
        }

        private void SetPaymentFree()
        {
            PaymentsSL payments = new PaymentsSL();

            DataTable data = Session["Information"] as DataTable;
            if (data.Rows.Count > 0)
            {
                payments.CustomerID = data.Rows[0]["PKCliente"].ToString().Trim();
                payments.SetPaymentFree();
            }
        }

        [WebMethod]
        public static List<string> GetCustomers(string username)
        {
            List<string> customers = new List<string>();
            try
            {
                if (username != String.Empty)
                {
                    //Obtiene clientes que tengan deuda
                    String query = String.Format(@"SELECT DISTINCT XS.RazonSocial
                                            FROM Customer C
                                            INNER JOIN xSOAddress XS ON C.CustId = XS.CustId
                                            INNER JOIN ARDoc AD ON C.CustId = AD.CustId
                                            WHERE AD.DocBal > 0 AND GETDATE() > AD.DueDate AND XS.RazonSocial LIKE '%{0}%'", username);
                    DataBaseSettings db = new DataBaseSettings();
                    DataTable aux = db.GetDataTable(query);

                    foreach (DataRow row in aux.Rows)
                    {
                        customers.Add(row["RazonSocial"].ToString().Trim());
                    }
                }
            } catch (Exception ex)
            {
                throw ex;
            }

            return customers;
        }
        #endregion
    }
}