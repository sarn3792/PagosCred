using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PagosCredijal
{
    public class PaymentsRecords
    {
        private int fkUser;
        private int fkCallType;
        private String phoneCalled;
        private int fkStatusCall;
        private String coments;
        private String PKCustomer;
        private DateTime startTime;

        public PaymentsRecords(String PKCustomer)
        {
            this.PKCustomer = PKCustomer;
        }

        public PaymentsRecords(int fkUser, String PKCustomer, int fkCallType, String phoneCalled, int fkStatusCall, String coments, DateTime startTime)
        {
            this.fkUser = fkUser;
            this.PKCustomer = PKCustomer;
            this.fkCallType = fkCallType;
            this.phoneCalled = phoneCalled;
            this.fkStatusCall = fkStatusCall;
            this.coments = coments;
            this.startTime = startTime;
        }

        public DataTable Get()
        {
            DataTable data = new DataTable();
            try
            {
                String query = String.Format(@"SELECT TOP 10 USR.Name AS 'Gestor', PR.FinalDate AS 'Fecha y hora de gestión', CT.CallTypeName AS 'Tipo de llamada', PR.PhoneCalleD AS 'Teléfono marcado', ST.StatusCallAbbreviation AS 'Estatus', ST.StatusCallName AS 'Descripción estatus', PR.Coments AS 'Comentario'
                                              , PR.PaymentPromise AS 'Fecha promesa de pago', PR.MoneyPromise AS 'Cantidad promesa de pago'
                                              FROM PaymentsRecord PR
                                              INNER JOIN PaymentsUsers USR ON PR.FKUser = USR.IDUser
                                              INNER JOIN PaymentsCallTypes CT ON PR.FKCallType = CT.PKCallType
                                              INNER JOIN PaymentsStatusCall ST ON PR.FKStatusCall = ST.PKStatusCall
                                              WHERE PR.FKCusId = '{0}'
                                              ORDER BY PR.FinalDate DESC", this.PKCustomer);
                DataBaseSettings db = new DataBaseSettings();
                data = db.GetDataTable(query);
            } catch (Exception ex)
            {
                throw ex;
            }

            return data;
        }

        public DataTable GetReport2()
        {
            try
            {
                String query = String.Format(@"SELECT PR.User1, PR.User2, PR.User3, PR.User4, PR.User5
                                                FROM PaymentsRecord PR
                                                WHERE PR.FKCusId = '{0}'", this.PKCustomer);
                DataBaseSettings db = new DataBaseSettings();
                return db.GetDataTable(query);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save()
        {
            try
            {
                String query = String.Format(@"INSERT INTO PaymentsRecord (FKUser, FKCusId, FKCallType, PhoneCalled, FkStatusCall, Coments, StartDate) 
                                               VALUES ({0}, '{1}', {2}, '{3}', {4}, '{5}', '{6}')", fkUser, PKCustomer, fkCallType, phoneCalled, fkStatusCall, coments, startTime);

                DataBaseSettings db = new DataBaseSettings();
                db.ExecuteQuery(query);
            } catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public void Save(DateTime paymentPromise, decimal moneyPromise)
        {
            try
            {
                String query = String.Format(@"INSERT INTO PaymentsRecord (FKUser, FKCusId, FKCallType, PhoneCalled, FkStatusCall, Coments, StartDate, PaymentPromise, MoneyPromise) 
                                               VALUES ({0}, '{1}', {2}, '{3}', {4}, '{5}', '{6}', '{7}', {8})", fkUser, PKCustomer, fkCallType, phoneCalled, fkStatusCall, coments, startTime, paymentPromise, moneyPromise);

                DataBaseSettings db = new DataBaseSettings();
                db.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}