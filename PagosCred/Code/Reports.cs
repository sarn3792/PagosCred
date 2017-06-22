using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PagosCredijal
{
    public class Reports
    {
        private DateTime fechaInicial;
        private DateTime fechaFinal;
        private String query = String.Format(@"SELECT CR.CustId 'No. Cliente', CR.RazonSocial 'Cliente', PSC.StatusCallName 'Estatus', PU.Name 'Gestor', PR.StartDate 'Fecha Inicio', PR.FinalDate 'Fecha Final'
                                            FROM PaymentsRecord PR
                                            LEFT JOIN PaymentsStatusCall PSC ON PR.FKStatusCall = PSC.PKStatusCall
                                            LEFT JOIN PaymentsUsers PU ON PR.FKUser = PU.IDUser
                                            LEFT JOIN xSOAddress CR ON CR.CustId = PR.FKCusId
                                            WHERE PR.FinalDate BETWEEN ");

        public Reports(DateTime fechaFinal)
        {
            this.fechaInicial = fechaFinal;
            this.fechaFinal = fechaFinal;
            this.fechaFinal = this.fechaFinal.AddHours(23);
            this.fechaFinal = this.fechaFinal.AddMinutes(59);
        }

        public DataTable GetReportByDay()
        {
            try
            {
                //initial date
                this.fechaInicial = this.fechaInicial.AddMinutes(-1);

                String finalQuery = String.Format("{0}'{1}' AND '{2}'", query, this.fechaInicial.ToString("yyyy-MM-dd HH:mm:ss"), this.fechaFinal.ToString("yyyy-MM-dd HH:mm:ss"));
                DataBaseSettings db = new DataBaseSettings();
                return db.GetDataTable(finalQuery);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetReportByWeek()
        {
            try
            {
                //initial date
                this.fechaInicial = this.fechaInicial.AddDays(-7);
                this.fechaInicial = this.fechaInicial.AddMinutes(-1);

                String finalQuery = String.Format("{0}'{1}' AND '{2}'", query, this.fechaInicial.ToString("yyyy-MM-dd HH:mm:ss"), this.fechaFinal.ToString("yyyy-MM-dd HH:mm:ss"));
                DataBaseSettings db = new DataBaseSettings();
                return db.GetDataTable(finalQuery);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetReportByMonth()
        {
            try
            {
                this.fechaInicial = this.fechaInicial.AddDays(-31);
                this.fechaInicial = this.fechaInicial.AddMinutes(-1);

                String finalQuery = String.Format("{0}'{1}' AND '{2}'", query, this.fechaInicial.ToString("yyyy-MM-dd HH:mm:ss"), this.fechaFinal.ToString("yyyy-MM-dd HH:mm:ss"));
                DataBaseSettings db = new DataBaseSettings();
                return db.GetDataTable(finalQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}