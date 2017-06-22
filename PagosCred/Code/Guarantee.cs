using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace PagosCredijal
{
    public class Guarantee
    {
        private String fkCustomer;
        //private String fkOportunity;
        private String pkGuarantee;

        public Guarantee(String fkCustomer /*, String fkOportunity */)
        {
            this.fkCustomer = fkCustomer;
            //this.fkOportunity = fkOportunity;
            this.pkGuarantee = this.GetPKGuarantee();
        }

        public void SaveAddrees(Address information)
        {
            try
            {
                String query = String.Format(@"INSERT INTO PaymentsGuaranteeAddress (FKGuarantee, AddressType, Address, BetweenStreets, Colony, City, AreaCode, Reference)
                                               VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}')", this.pkGuarantee, information.addressType, information.myAddress, information.betweenStreets, information.colony, information.city, information.CP, information.reference);

                DataBaseSettings db = new DataBaseSettings();
                db.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetAddress()
        {
            DataTable data = new DataTable();
            try
            {
                String query = String.Format(@"SELECT ga.AddressType as 'Tipo', ga.Address as 'Domicilio', ga.BetweenStreets as 'Entre calles', ga.Colony as 'Colonia', ga.City, ga.AreaCode as 'CP', ga.Reference as 'Pto referencia'
                                            FROM PaymentsGuaranteeAddress ga INNER JOIN PaymentsGuarantee PG ON GA.FKGuarantee = PG.PKGuarantee
                                            INNER JOIN Customer C ON C.CustId = PG.FKCustomerSL
                                            WHERE C.CustId = '{0}'", this.fkCustomer);
                DataBaseSettings db = new DataBaseSettings();
                data = db.GetDataTable(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public void SavePhone(Phone phone)
        {
            try
            {
                String query = String.Format(@"INSERT INTO PaymentsGuaranteePhone (FKGuarantee, PhoneType, Phone) VALUES ('{0}', '{1}', '{2}')", this.pkGuarantee, phone.phoneType, phone.phone);
                DataBaseSettings db = new DataBaseSettings();
                db.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable GetPhone()
        {
            DataTable data = new DataTable();
            try
            {
                String query = String.Format(@"SELECT hp.PhoneType as 'Tipo de teléfono', hp.Phone as 'Número' 
                                            FROM PaymentsGuaranteePhone hp INNER JOIN PaymentsGuarantee PG ON HP.FKGuarantee = PG.PKGuarantee
                                            INNER JOIN Customer C ON C.CustId = PG.FKCustomerSL
                                            WHERE C.CustId = '{0}'", this.fkCustomer);
                DataBaseSettings db = new DataBaseSettings();
                data = db.GetDataTable(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return data;
        }

        private String GetPKGuarantee()
        {
            String pkGuarantee = String.Empty;
            try
            {
                String query = String.Format(@"SELECT PKGuarantee FROM PaymentsGuarantee WHERE FKCustomerSL = '{0}'", this.fkCustomer);
                DataBaseSettings db = new DataBaseSettings();
                DataTable aux = db.GetDataTable(query);
                pkGuarantee = aux.Rows.Count > 0 ? aux.Rows[0]["PKGuarantee"].ToString().Trim() : String.Empty;
            } catch (Exception ex)
            {
                throw ex;
            }

            return pkGuarantee;
        }
    }
}