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
using AlchemyAccounting.LC.Interface;

namespace AlchemyAccounting.LC.DataAccess
{
    public class LCDataAcces
    {
        SqlConnection con;
        SqlCommand cmd;

        public LCDataAcces()
        {
            con = new SqlConnection(Global.connection);
            cmd = new SqlCommand("", con);
        }

        public string insertLC(LCInterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO  LC_BASIC( LCTP, LCID, BANKCD, LCNO, LCDT, IMPORTERNM, BENEFICIARY, SCPINO, SCPIDT, MCNM, MCNO, MCDT, MPINO, MPIDT, LCVUSD, LCVERT, LCVBDT, REMARKS, USERPC, USERID, INTIME, IPADDRESS) " +
                                  "VALUES (@LCTP,@LCID,@BANKCD,@LCNO,@LCDT,@IMPORTERNM,@BENEFICIARY,@SCPINO,@SCPIDT,@MCNM,@MCNO,@MCDT,@MPINO,@MPIDT,@LCVUSD,@LCVERT,@LCVBDT,@REMARKS,@USERPC,@USERID,@INTIME,@IPADDRESS)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@LCTP", SqlDbType.NVarChar).Value = ob.LcTp;
                cmd.Parameters.Add("@LCID", SqlDbType.NVarChar).Value = ob.LcID;
                cmd.Parameters.Add("@BANKCD", SqlDbType.NVarChar).Value = ob.BnkCD;
                cmd.Parameters.Add("@LCNO", SqlDbType.NVarChar).Value = ob.LcNo;
                cmd.Parameters.Add("@LCDT", SqlDbType.DateTime).Value = ob.LcDT;
                cmd.Parameters.Add("@IMPORTERNM", SqlDbType.NVarChar).Value = ob.ImporterNM;
                cmd.Parameters.Add("@BENEFICIARY", SqlDbType.NVarChar).Value = ob.Beneficiary;
                cmd.Parameters.Add("@SCPINO", SqlDbType.NVarChar).Value = ob.ScipNO;
                cmd.Parameters.Add("@SCPIDT", SqlDbType.DateTime).Value = ob.ScipDT;
                cmd.Parameters.Add("@MCNM", SqlDbType.NVarChar).Value = ob.McNM;
                cmd.Parameters.Add("@MCNO", SqlDbType.NVarChar).Value = ob.McNO;
                cmd.Parameters.Add("@MCDT", SqlDbType.DateTime).Value = ob.McDT;
                cmd.Parameters.Add("@MPINO", SqlDbType.NVarChar).Value = ob.MpiNO;
                cmd.Parameters.Add("@MPIDT", SqlDbType.DateTime).Value = ob.MpiDT;
                cmd.Parameters.Add("@LCVUSD", SqlDbType.Decimal).Value = ob.LcUSD;
                cmd.Parameters.Add("@LCVERT", SqlDbType.Decimal).Value = ob.LcERT;
                cmd.Parameters.Add("@LCVBDT", SqlDbType.Decimal).Value = ob.LcBDT;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.Usernm;
                cmd.Parameters.Add("@INTIME", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = "";

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