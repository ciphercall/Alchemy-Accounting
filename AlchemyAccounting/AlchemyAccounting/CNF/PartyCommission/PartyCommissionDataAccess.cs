using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace AlchemyAccounting.CNF.PartyCommission
{
    public class PartyCommissionDataAccess
    {
        SqlConnection con;
        SqlCommand cmd;

        public PartyCommissionDataAccess()
        {
            con = new SqlConnection(Global.connection);
            cmd = new SqlCommand("", con);
        }

        public string SaveExpenseInfo(PartyCommissionModel pcm)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "insert into CNF_COMMISSION (PARTYID, COMMSL, EXCTP, VALUEFR,VALUETO,VALUETP, COMMAMT, JOBTP, USERPC, INTIME, UPDATETIME, IPADDRESS)" +
                    "values(@PARTYID, @COMMSL,@EXCTP, @VALUEFR,@VALUETO, @VALUETP, @COMMAMT, @JOBTP, @USERPC, @INTIME , @UPDATETIME,@IPADDRESS  )";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@PARTYID", SqlDbType.NVarChar).Value = pcm.PARTYID;
                cmd.Parameters.Add("@COMMSL", SqlDbType.BigInt).Value =pcm.COMMSL ;
                cmd.Parameters.Add("@EXCTP", SqlDbType.NVarChar).Value =pcm.EXCTP ;
                cmd.Parameters.Add("@VALUEFR", SqlDbType.Decimal).Value =pcm.VALUEFROM ;
                cmd.Parameters.Add("@VALUETO", SqlDbType.Decimal).Value =pcm.VALUETO ;
                cmd.Parameters.Add("@VALUETP", SqlDbType.NVarChar).Value = pcm.VALUETP;
                cmd.Parameters.Add("@COMMAMT", SqlDbType.Decimal).Value =pcm.COMMAMT ;
                cmd.Parameters.Add("@JOBTP", SqlDbType.NVarChar).Value = pcm.JobType;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = pcm.UserPC;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = pcm.InTime;
                cmd.Parameters.Add("@UPDATETIME", SqlDbType.SmallDateTime).Value = pcm.UpdateTime;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = pcm.IPAddress;

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

        public string UpdateExpenseInfo(PartyCommissionModel pcm)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "update CNF_COMMISSION set EXCTP=@EXCTP ,VALUEFR=@VALUEFR, JOBTP=@JOBTP, VALUETO=@VALUETO, VALUETP=@VALUETP, COMMAMT=@COMMAMT where PARTYID=@PARTYID and COMMSL=@COMMSL ";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@PARTYID", SqlDbType.NVarChar).Value = pcm.PARTYID;
                cmd.Parameters.Add("@COMMSL", SqlDbType.BigInt).Value = pcm.COMMSL;
                cmd.Parameters.Add("@EXCTP", SqlDbType.NVarChar).Value = pcm.EXCTP;
                cmd.Parameters.Add("@VALUEFR", SqlDbType.Decimal).Value = pcm.VALUEFROM;
                cmd.Parameters.Add("@VALUETO", SqlDbType.Decimal).Value = pcm.VALUETO;
                cmd.Parameters.Add("@VALUETP", SqlDbType.NVarChar).Value = pcm.VALUETP;
                cmd.Parameters.Add("@COMMAMT", SqlDbType.Decimal).Value = pcm.COMMAMT;
                cmd.Parameters.Add("@JOBTP", SqlDbType.NVarChar).Value = pcm.JobType;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = pcm.UserPC;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = pcm.InTime;
                cmd.Parameters.Add("@UPDATETIME", SqlDbType.SmallDateTime).Value = pcm.UpdateTime;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = pcm.IPAddress;

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



        public string DeleteExpenseInfo(PartyCommissionModel pcm)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM CNF_COMMISSION WHERE PARTYID =@PARTYID AND COMMSL =@COMMSL";
                cmd.Parameters.Clear();

                cmd.Parameters.Add("@PARTYID", SqlDbType.NVarChar).Value = pcm.PARTYID;
                cmd.Parameters.Add("@COMMSL", SqlDbType.BigInt).Value = pcm.COMMSL;

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