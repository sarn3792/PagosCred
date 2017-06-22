using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;

namespace PagosCredijal
{
    public class Balance
    {
        private decimal vigente;
        private decimal tDias;
        private decimal tDiasMoratorio;

        private decimal sDias;
        private decimal sDiasMoratorio;

        private decimal nDias;
        private decimal nDiasMoratorio;

        private decimal mDias;
        private decimal mDiasMoratorio;

        private decimal interesMoratorio;
        private decimal totalSaldoVencido;
        private decimal saldoCartera;
        private decimal interesNoDevengado;
        private decimal saldoParaLiquidar;
        private DateTime fechaACalcular;

        private List<DateTime> fechaVencimientoT = new List<DateTime>();
        private List<DateTime> fechaVencimientoS = new List<DateTime>();
        private List<DateTime> fechaVencimientoN = new List<DateTime>();
        private List<DateTime> fechaVencimientoM = new List<DateTime>();

        private decimal interesesT;
        private decimal interesesS;
        private decimal interesesN;
        private decimal interesesM;

        public decimal Vigente
        {
            get
            {
                return vigente;
            }

            set
            {
                vigente = value;
            }
        }

        public decimal TDias
        {
            get
            {
                return tDias;
            }

            set
            {
                tDias = value;
            }
        }

        public decimal SDias
        {
            get
            {
                return sDias;
            }

            set
            {
                sDias = value;
            }
        }

        public decimal NDias
        {
            get
            {
                return nDias;
            }

            set
            {
                nDias = value;
            }
        }

        public decimal MDias
        {
            get
            {
                return mDias;
            }

            set
            {
                mDias = value;
            }
        }

        public decimal TotalSaldoVencido
        {
            get
            {

                return totalSaldoVencido;
            }

        }

        public decimal SaldoCartera
        {
            get
            {

                return saldoCartera;
            }

        }

        public decimal InteresNoDevengado
        {
            get
            {
                return interesNoDevengado;
            }

        }

        public decimal SaldoParaLiquidar
        {
            get
            {
                return saldoParaLiquidar;
            }
        }

        public decimal InteresMoratorio
        {
            get
            {
                return interesMoratorio;
            }
        }

        public Balance(DataTable data, DateTime fechaACalcular)
        {
            if (data.Rows.Count > 0)
            {
                this.fechaACalcular = fechaACalcular;

                //Saldo vigente
                this.vigente = data.Rows[0]["SaldoVigente"] != DBNull.Value ? Convert.ToDecimal(data.Rows[0]["SaldoVigente"].ToString()) : 0;

                //30 dias
                DataRow[] dr = data.AsEnumerable().Where(r => r.Field<String>("DiasVencimiento") == "30").ToArray();
                if (dr != null)
                {
                    foreach (DataRow row in dr)
                    {
                        this.TDias += Convert.ToDecimal(row["DocBal"].ToString());
                        if (Convert.ToDecimal(row["DocBal"].ToString()) >= 0) tDiasMoratorio += Convert.ToDecimal(row["DocBal"].ToString());
                        this.fechaVencimientoT.Add(DateTime.Parse(row["DueDate"].ToString()));
                        //this.interesesT += Convert.ToDecimal(row["Interes"].ToString());
                    }
                }
                else
                {
                    this.TDias = 0;
                    //this.interesesT = 0;
                }

                //60 días
                DataRow[] dr2 = data.AsEnumerable().Where(r => r.Field<String>("DiasVencimiento") == "60").ToArray();
                if (dr2 != null)
                {
                    foreach (DataRow row in dr2)
                    {
                        this.SDias += Convert.ToDecimal(row["DocBal"].ToString());
                        if (Convert.ToDecimal(row["DocBal"].ToString()) >= 0) sDiasMoratorio += Convert.ToDecimal(row["DocBal"].ToString());
                        this.fechaVencimientoS.Add(DateTime.Parse(row["DueDate"].ToString()));
                        //this.interesesS += Convert.ToDecimal(row["Interes"].ToString());
                    }
                }
                else
                {
                    this.SDias = 0;
                    //this.interesesS = 0;
                }

                //90 días
                DataRow[] dr3 = data.AsEnumerable().Where(r => r.Field<String>("DiasVencimiento") == "90").ToArray();
                if (dr3 != null)
                {
                    foreach (DataRow row in dr3)
                    {
                        this.NDias = Convert.ToDecimal(row["DocBal"].ToString());
                        if (Convert.ToDecimal(row["DocBal"].ToString()) >= 0) nDiasMoratorio += Convert.ToDecimal(row["DocBal"].ToString());
                        //this.interesesN = Convert.ToDecimal(row["Interes"].ToString());
                        this.fechaVencimientoN.Add(DateTime.Parse(row["DueDate"].ToString()));
                    }
                }
                else
                {
                    this.NDias = 0;
                    //this.interesesN = 0;
                }

                //+ 90 días
                DataRow[] dr4 = data.AsEnumerable().Where(r => r.Field<String>("DiasVencimiento") == "+90").ToArray();
                if (dr4 != null)
                {
                    foreach (DataRow row in dr4)
                    {
                        this.mDias += Convert.ToDecimal(row["DocBal"].ToString());
                        if (Convert.ToDecimal(row["DocBal"].ToString()) >= 0) mDiasMoratorio += Convert.ToDecimal(row["DocBal"].ToString());
                        //this.interesesM += Convert.ToDecimal(row["Interes"].ToString());
                        this.fechaVencimientoM.Add(DateTime.Parse(row["DueDate"].ToString()));
                    }

                }
                else
                {
                    this.mDias = 0;
                    //this.interesesM = 0;
                }

                //Interés moratorio
                decimal moratorio30 = CalcularInteresMoratorio(this.tDiasMoratorio, this.fechaVencimientoT);
                decimal moratorio60 = CalcularInteresMoratorio(this.sDiasMoratorio, this.fechaVencimientoS);
                decimal moratorio90 = CalcularInteresMoratorio(this.nDiasMoratorio, this.fechaVencimientoN);
                decimal moratorioM90 = CalcularInteresMoratorio(this.mDiasMoratorio, this.fechaVencimientoM);
                this.interesMoratorio = moratorio30 + moratorio60 + moratorio90 + moratorioM90;

                //Saldo vencido
                this.totalSaldoVencido = interesMoratorio + TDias + SDias + NDias + MDias;
                //Saldo Cartera
                this.saldoCartera = this.totalSaldoVencido + this.vigente;

                //Interes no devengado
                //this.interesNoDevengado = interesesT + interesesS + interesesN + interesesM;
                this.interesNoDevengado = Convert.ToDecimal(data.Rows[0]["Interes"].ToString());

                //Saldo para liquidar
                this.saldoParaLiquidar = this.saldoCartera - this.interesNoDevengado;

            }
        }

        /*
        private decimal CalcularInteresMoratorio(decimal adeudo, DateTime fechaVencimiento)
        {
            if (fechaVencimiento != null)
            {
                decimal porcentaje = 0.06M;
                int diasVencido = (int)(fechaACalcular - fechaVencimiento).TotalDays;
                decimal result = ((adeudo * porcentaje) / 30) * diasVencido;
                return result * 1.16M;
            }
            else
            {
                return 0M;
            }
        }
        */

        private decimal CalcularInteresMoratorio(decimal adeudo, List<DateTime> fechaVencimiento)
        {
            int diasVencido = 0;
            decimal result = 0;
            decimal porcentaje = 0.06M;
            if (fechaVencimiento != null)
            {
                foreach (DateTime date in fechaVencimiento)
                {
                    diasVencido = (int)(fechaACalcular - date).TotalDays;
                    result += ((adeudo * porcentaje) / 30) * diasVencido;

                }
                return result * 1.16M;
            }
            else
            {
                return 0M;
            }
        }
    }
}