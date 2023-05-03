using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace AlchemyAccounting.CNF.Party
{
    public class PartyInformationDataAccess
    {
        SqlConnection con;
        SqlCommand cmd;

        public PartyInformationDataAccess()
        {
            con = new SqlConnection(Global.connection);
            cmd = new SqlCommand("", con);
        }


        public string CreateParty(PartyInformationModel pim)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "insert into CNF_PARTY (PARTYID, ADDRESS, CONTACTNO, EMAILID,WEBID,APNM,APNO,STATUS, USERPC, INTIME, UPDATETIME, IPADDRESS, LOGINID, LOGINPW)" +
                    "values(@PARTYID, @ADDRESS,@CONTACTNO, @EMAILID, @WEBID,@APNM,@APNO,@STATUS, @USERPC, @INTIME , @UPDATETIME,@IPADDRESS ,@LOGINID,@LOGINPW )";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@PARTYID", SqlDbType.NVarChar).Value = pim.PartyID;
                cmd.Parameters.Add("@ADDRESS", SqlDbType.NVarChar).Value = pim.Address;
                cmd.Parameters.Add("@CONTACTNO", SqlDbType.NVarChar).Value = pim.Contact;
                cmd.Parameters.Add("@EMAILID", SqlDbType.NVarChar).Value = pim.Email;
                cmd.Parameters.Add("@WEBID", SqlDbType.NVarChar).Value = pim.Web;
                cmd.Parameters.Add("@APNM", SqlDbType.NVarChar).Value = pim.APName;
                cmd.Parameters.Add("@APNO", SqlDbType.NVarChar).Value = pim.APContact;
                cmd.Parameters.Add("@STATUS", SqlDbType.Char).Value = pim.Status;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = pim.UserPC;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = pim.InTime;
                cmd.Parameters.Add("@UPDATETIME", SqlDbType.SmallDateTime).Value = pim.UpdateTime;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = pim.IPAddress;
                cmd.Parameters.Add("@LOGINID", SqlDbType.NVarChar).Value = pim.Logid;
                cmd.Parameters.Add("@LOGINPW", SqlDbType.NVarChar).Value = pim.Logpw;

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




        public DataTable ShowpartyInfo(string id)
        {
            DataTable table = new DataTable();
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select PARTYID  from CNF_PARTY where PARTYID='" + id + "' ";
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch { }
            return table;
        }

        public string UpdateParty(PartyInformationModel pim)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = @"update CNF_PARTY set ADDRESS=@ADDRESS, CONTACTNO=@CONTACTNO, EMAILID=@EMAILID, 
                             WEBID=@WEBID, APNM=@APNM, APNO=@APNO, STATUS=@STATUS, LOGINID=@LOGINID,LOGINPW=@LOGINPW 
                                     where PARTYID=@PARTYID";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@PARTYID", SqlDbType.NVarChar).Value = pim.PartyID;
                cmd.Parameters.Add("@ADDRESS", SqlDbType.NVarChar).Value = pim.Address;
                cmd.Parameters.Add("@CONTACTNO", SqlDbType.NVarChar).Value = pim.Contact;
                cmd.Parameters.Add("@EMAILID", SqlDbType.NVarChar).Value = pim.Email;
                cmd.Parameters.Add("@WEBID", SqlDbType.NVarChar).Value = pim.Web;
                cmd.Parameters.Add("@APNM", SqlDbType.NVarChar).Value = pim.APName;
                cmd.Parameters.Add("@APNO", SqlDbType.NVarChar).Value = pim.APContact;
                cmd.Parameters.Add("@STATUS", SqlDbType.Char).Value = pim.Status;
                cmd.Parameters.Add("@LOGINID", SqlDbType.Char).Value = pim.Logid;
                cmd.Parameters.Add("@LOGINPW", SqlDbType.Char).Value = pim.Logpw;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = pim.UserPC;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = pim.InTime;
                cmd.Parameters.Add("@UPDATETIME", SqlDbType.SmallDateTime).Value = pim.UpdateTime;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = pim.IPAddress;

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