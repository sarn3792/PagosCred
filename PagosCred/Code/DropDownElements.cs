using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace PagosCredijal
{
    public class DropDownElements
    {
        public static void SetCallTypes(DropDownList ddl)
        {
            try
            {
                String query = String.Format("SELECT CT.PKCallType, CT.CallTypeName FROM PaymentsCallTypes CT");
                DataBaseSettings db = new DataBaseSettings();
                DataTable aux = db.GetDataTable(query);

                ddl.Items.Insert(0, new ListItem("--Seleccionar tipo--", "0"));
                List<ListItem> data = aux.AsEnumerable().Select(m => new ListItem()
                {
                    Value = Convert.ToString(m.Field<int>("PKCallType")),
                    Text = m.Field<String>("CallTypeName"),
                }).ToList();

                int i = 1;
                foreach (ListItem item in data)
                {
                    ddl.Items.Insert(i, item);
                    i++;
                }
                ddl.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SetStatus(DropDownList ddl)
        {
            try
            {
                String query = String.Format("SELECT SC.PKStatusCall, SC.StatusCallName FROM PaymentsStatusCall SC");
                DataBaseSettings db = new DataBaseSettings();
                DataTable aux = db.GetDataTable(query);

                ddl.Items.Insert(0, new ListItem("--Seleccionar estatus--", "0"));
                List<ListItem> data = aux.AsEnumerable().Select(m => new ListItem()
                {
                    Value = Convert.ToString(m.Field<int>("PKStatusCall")),
                    Text = m.Field<String>("StatusCallName"),
                }).ToList();

                int i = 1;
                foreach (ListItem item in data)
                {
                    ddl.Items.Insert(i, item);
                    i++;
                }
                ddl.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SetQueues(DropDownList ddl)
        {
            try
            {
                String query = String.Format("SELECT PKQueueU, QueueUName FROM PaymentsQueueU");
                DataBaseSettings db = new DataBaseSettings();
                DataTable aux = db.GetDataTable(query);

                ddl.Items.Insert(0, new ListItem("--Seleccionar cola--", "0"));
                List<ListItem> data = aux.AsEnumerable().Select(m => new ListItem()
                {
                    Value = Convert.ToString(m.Field<int>("PKQueueU")),
                    Text = m.Field<String>("QueueUName"),
                }).ToList();

                int i = 1;
                foreach (ListItem item in data)
                {
                    ddl.Items.Insert(i, item);
                    i++;
                }
                ddl.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}