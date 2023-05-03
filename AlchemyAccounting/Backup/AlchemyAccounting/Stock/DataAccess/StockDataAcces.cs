using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using AlchemyAccounting.Stock.Interface;

namespace AlchemyAccounting.Stock.DataAccess
{
    public class StockDataAcces
    {
        SqlConnection con;
        SqlCommand cmd;

        public StockDataAcces()
        {
            con = new SqlConnection(Global.connection);
            cmd = new SqlCommand("", con);
        }

        public string insertPS(AlchemyAccounting.Stock.Interface.StockInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO STK_PS( PSTP, PSID, CITY, ADDRESS, CONTACTNO, EMAIL, WEBID, CPNM, CPNO, REMARKS, STATUS, USERPC, USERID, IPADDRESS,PS_ID) " +
                                  "VALUES (@PSTP,@PSID,@CITY,@ADDRESS,@CONTACTNO,@EMAIL,@WEBID,@CPNM,@CPNO,@REMARKS,@STATUS,@USERPC,@USERID,@IPADDRESS,@PS_ID)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@PSTP", SqlDbType.NVarChar).Value = ob.Pstp;
                cmd.Parameters.Add("@PSID", SqlDbType.NVarChar).Value = ob.Pscd;
                cmd.Parameters.Add("@CITY", SqlDbType.NVarChar).Value = ob.City;
                cmd.Parameters.Add("@ADDRESS", SqlDbType.NVarChar).Value = ob.Address;
                cmd.Parameters.Add("@CONTACTNO", SqlDbType.NVarChar).Value = ob.Contactno;
                cmd.Parameters.Add("@EMAIL", SqlDbType.NVarChar).Value = ob.Email;
                cmd.Parameters.Add("@WEBID", SqlDbType.NVarChar).Value = ob.Webid;
                cmd.Parameters.Add("@CPNM", SqlDbType.NVarChar).Value = ob.Cpnm;
                cmd.Parameters.Add("@CPNO", SqlDbType.NVarChar).Value = ob.Cpno;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@STATUS", SqlDbType.NVarChar).Value = ob.Status;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.Username;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@PS_ID", SqlDbType.NVarChar).Value = ob.Ps_ID;

                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }
    }
}