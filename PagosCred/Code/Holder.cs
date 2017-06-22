using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PagosCredijal
{
    public class Holder
    {
        private String fkHolder;

        public Holder(String fkHolder)
        {
            this.fkHolder = fkHolder;
        }

        public void SaveAddrees(Address information)
        {
            try
            {
                String query = String.Format(@"INSERT INTO PaymentsCustomerAddress (FKCustomerSL, AddressType, Address, BetweenStreets, Colony, City, AreaCode, Reference)
                                               VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}')", this.fkHolder, information.addressType, information.myAddress, information.betweenStreets, information.colony, information.city, information.CP, information.reference);

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
                                                FROM PaymentsCustomerAddress ga WHERE ga.FKCustomerSL = '{0}'", this.fkHolder);
                DataBaseSettings db = new DataBaseSettings();
                data = db.GetDataTable(query);
            } catch(Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public void SavePhone(Phone phone)
        {
            try
            {
                String query = String.Format(@"INSERT INTO PaymentsCustomerPhone (FKCustomerSL, PhoneType, Phone) VALUES ('{0}', '{1}', '{2}')", this.fkHolder, phone.phoneType, phone.phone);
                DataBaseSettings db = new DataBaseSettings();
                db.ExecuteQuery(query);
            } catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public DataTable GetPhone()
        {
            DataTable data = new DataTable();
            try
            {
                String query = String.Format("SELECT hp.PhoneType as 'Tipo de teléfono', hp.Phone as 'Número' FROM PaymentsCustomerPhone hp WHERE hp.FKCustomerSL = '{0}'", this.fkHolder);
                DataBaseSettings db = new DataBaseSettings();
                data = db.GetDataTable(query);
            } catch (Exception ex)
            {
                throw ex;
            }

            return data;
        }
    }
}