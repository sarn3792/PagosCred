using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PagosCredijal
{
    public class QueueOperations
    {
        private bool mora;
        private bool riesgo;
        private bool historialMora;
        private bool montoFinanciado;
        private bool montoVencido;
        private bool estatusCobro;
        private bool promesaPagoRota;
        private bool productoFinanciado;
        private bool creditoSimple;
        private bool arrendamiento;

        private int pkQueueU;
        private List<int> pkQueueC = new List<int>();

        private int pkUser;

        public QueueOperations(bool mora, bool riesgo, bool historialMora, bool montoFinanciado, bool montoVencido, bool estatusCobro, bool promesaPagoRota, bool productoFinanciado, bool creditoSimple, bool arrendamiento)
        {
            this.mora = mora;
            this.riesgo = riesgo;
            this.historialMora = historialMora;
            this.montoFinanciado = montoFinanciado;
            this.montoVencido = montoVencido;
            this.estatusCobro = estatusCobro;
            this.promesaPagoRota = promesaPagoRota;
            this.productoFinanciado = productoFinanciado;
            this.creditoSimple = creditoSimple;
            this.arrendamiento = arrendamiento;
        }

        public QueueOperations(int pkUser, int pkQueue)
        {
            this.pkUser = pkUser;
            this.pkQueueU = pkQueue;
        }

        public void SaveQueueU(String queueName, String moraValue, String historialMoraValue, String status, String productoFinanciadoValue, String creditoSimpleValue, String arrendamientoValue)
        {
            try
            {
                String query = String.Format(@"INSERT INTO PaymentsQueueU (QueueUName) VALUES ('{0}')", queueName);
                DataBaseSettings db = new DataBaseSettings();
                db.ExecuteQuery(query);
                SetPKQueueU(queueName);
                SaveQueueMix(moraValue, historialMoraValue, status, productoFinanciadoValue, creditoSimpleValue, arrendamientoValue);
            } catch (Exception ex)
            {
                throw ex;
            }

        }
        private void SaveQueueMix(String moraValue, String historialMoraValue, String status, String productoFinanciadoValue, String creditoSimpleValue, String arrendamientoValue)
        {
            try
            {
                SetPKQueueC();
                String query = String.Empty;
                DataBaseSettings db = new DataBaseSettings();
                foreach (int pkQueueC in pkQueueC)
                {
                    switch (pkQueueC)
                    {
                        case 1:
                            query = String.Format(@"INSERT INTO PaymentsQueueMix (FKQueueC, FKQueueU, Value) VALUES ({0}, {1}, '{2}')", pkQueueC, this.pkQueueU, moraValue);
                            break;

                        case 3:
                            query = String.Format(@"INSERT INTO PaymentsQueueMix (FKQueueC, FKQueueU, Value) VALUES ({0}, {1}, '{2}')", pkQueueC, this.pkQueueU, historialMoraValue);
                            break;

                        case 6:
                            query = String.Format(@"INSERT INTO PaymentsQueueMix (FKQueueC, FKQueueU, Value) VALUES ({0}, {1}, '{2}')", pkQueueC, this.pkQueueU, status);
                            break;

                        case 8:
                            query = String.Format(@"INSERT INTO PaymentsQueueMix (FKQueueC, FKQueueU, Value) VALUES ({0}, {1}, '{2}')", pkQueueC, this.pkQueueU, productoFinanciadoValue);
                            break;
                        /*
                        case 9:
                            query = String.Format(@"INSERT INTO PaymentsQueueMix (FKQueueC, FKQueueU, Value) VALUES ({0}, {1}, '{2}')", pkQueueC, this.pkQueueU, creditoSimpleValue);
                            break;

                        case 10:
                            query = String.Format(@"INSERT INTO PaymentsQueueMix (FKQueueC, FKQueueU, Value) VALUES ({0}, {1}, '{2}')", pkQueueC, this.pkQueueU, arrendamientoValue);
                            break;
                        */

                        default:
                            query = String.Format(@"INSERT INTO PaymentsQueueMix (FKQueueC, FKQueueU) VALUES ({0}, {1})", pkQueueC, this.pkQueueU); //without value
                            break;

                    }
                    db.ExecuteQuery(query);
            }
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetPKQueueU(String queueName)
        {
            try
            {
                String query = String.Format(@"SELECT PKQueueU FROM PaymentsQueueU Where QueueUName = '{0}'", queueName);
                DataBaseSettings db = new DataBaseSettings();
                DataTable aux = db.GetDataTable(query);
                this.pkQueueU = aux.Rows.Count > 0 ? Convert.ToInt32(aux.Rows[0]["PKQueueU"].ToString()) : 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetPKQueueC()
        {
            if (mora) pkQueueC.Add(1); //the number is PK on PaymentsQueueC table
            if (riesgo) pkQueueC.Add(2);
            if (historialMora) pkQueueC.Add(3);
            if (montoFinanciado) pkQueueC.Add(4);
            if (montoVencido) pkQueueC.Add(5);
            if (estatusCobro) pkQueueC.Add(6);
            if (promesaPagoRota) pkQueueC.Add(7);
            if (productoFinanciado) pkQueueC.Add(8);
            if (creditoSimple) pkQueueC.Add(9);
            if (arrendamiento) pkQueueC.Add(10);
        }

        public void SaveQueueByUser()
        {
            try
            {
                if (CheckUserInQueue())
                {
                    UpdateQueueByUser();
                }
                else
                {
                    InsertQueueByUser();
                }
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        private void UpdateQueueByUser()
        {
            try
            {
                String query = String.Format(@"UPDATE PaymentsQueueUsers SET FKQueueU = {0} WHERE FKUser = {1}", this.pkQueueU, this.pkUser);
                DataBaseSettings db = new DataBaseSettings();
                db.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void InsertQueueByUser()
        {
            try
            {
                String query = String.Format(@"INSERT INTO PaymentsQueueUsers (FKQueueU, FKUser) VALUES ({0}, {1})", this.pkQueueU, this.pkUser);
                DataBaseSettings db = new DataBaseSettings();
                db.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool CheckUserInQueue()
        {
            DataTable aux = new DataTable();
            try
            {
                String query = String.Format(@"SELECT *
                                              FROM PaymentsUsers PU 
                                              INNER JOIN PaymentsQueueUsers PQU ON PU.IDUser = PQU.FKUser
                                              INNER JOIN PaymentsQueueU PQQ ON PQQ.PKQueueU = PQU.FKQueueU
                                              WHERE PQU.FKUser = {0}", this.pkUser);
                DataBaseSettings db = new DataBaseSettings();
                aux = db.GetDataTable(query);
            } catch (Exception ex)
            {
                throw ex;
            }

            return aux.Rows.Count > 0 ? true : false;
        }

        public static DataTable GetQueueByUsers()
        {
            DataTable data = new DataTable();
            try
            {
                String query = String.Format(@"SELECT PU.Name, PU.UserName 'Username', PS.QueueUName 'Queue'
                                              FROM PaymentsUsers PU 
                                              LEFT JOIN PaymentsQueueUsers PQ on PU.IDUser = PQ.FKUser
                                              LEFT JOIN PaymentsQueueU PS on PQ.FKQueueU = PS.PKQueueU");
                DataBaseSettings db = new DataBaseSettings();
                data = db.GetDataTable(query);
            } catch (Exception ex)
            {
                throw ex;
            }

            return data;
        }

        public static String GetQueueConditionsByUsers(String pkUser)
        {
            String result = String.Empty;
            try
            {
                String orderBy = String.Format("ORDER BY EnCola");
                //bool flag = false;
                String query = String.Format(@"SELECT PQC.Query, PQM.Value
                                                FROM PaymentsUsers PU
                                                INNER JOIN PaymentsQueueUsers PQU ON PU.IDUser = PQU.FKUser
                                                INNER JOIN PaymentsQueueU PQUU ON PQU.FKQueueU = PQUU.PKQueueU
                                                INNER JOIN PaymentsQueueMix PQM ON PQM.FKQueueU = PQUU.PKQueueU
                                                INNER JOIN PaymentsQueueC PQC ON PQM.FKQueueC = PQC.PKQueueC
                                                WHERE PQU.FKUser = '{0}'", pkUser);
                DataBaseSettings db = new DataBaseSettings();
                DataTable aux = db.GetDataTable(query);
                if (aux.Rows.Count > 0) //the user does not have queue assigned
                {
                    foreach (DataRow row in aux.Rows)
                    {
                        if (row["Query"].ToString().Contains("BETWEEN"))
                        {
                            result += String.Format(" AND {0} {1} ", row["Query"].ToString(), row["Value"].ToString());
                        }
                        else if (row["Query"].ToString().Contains("ORDER BY"))
                        {
                            //if (flag)
                            //{
                            orderBy += String.Format(",{0} ", row["Query"].ToString().Replace("ORDER BY", String.Empty));
                            //}
                            //else //first order by
                            //{
                                //orderBy += String.Format("{0} ", row["Query"].ToString());
                            //    flag = true;
                            //}

                        }
                        else
                        {
                            result += String.Format(" AND {0} '{1} '", row["Query"].ToString(), row["Value"].ToString());
                        }
                        
                    }

                    result += orderBy;
                }
            } catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}