using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace AlchemyAccounting.CNF.JobReceive
{
    public class JobReceiveDataAccess
    {
        SqlConnection con;
        SqlCommand cmd;

        public JobReceiveDataAccess()
        {
            con = new SqlConnection(Global.connection);
            cmd = new SqlCommand("", con);
        }


        public string SaveJobReceive(JobReceiveModel jrm)
        {

            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "insert into CNF_JOBRCV (TRANSDT, TRANSMY, TRANSNO,TRANSFOR,COMPID,JOBYY,JOBTP,JOBNO,PARTYID,DEBITCD, REMARKS,AMOUNT, USERPC, INTIME, IPADDRESS)" +
                    "values(@TRANSDT, @TRANSMY, @TRANSNO,@TRANSFOR,@COMPID,@JOBYY,@JOBTP,@JOBNO,@PARTYID,@DEBITCD, @REMARKS,@AMOUNT, @USERPC, @INTIME, @IPADDRESS )";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = jrm.TRANSDT;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = jrm.TRANSMY;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = jrm.TRANSNO;
                cmd.Parameters.Add("@TRANSFOR", SqlDbType.NVarChar).Value = jrm.TRANSFOR;

                cmd.Parameters.Add("@COMPID", SqlDbType.NVarChar).Value = jrm.COMPID;
                cmd.Parameters.Add("@JOBYY", SqlDbType.BigInt).Value = jrm.JOBYY;
                cmd.Parameters.Add("@JOBTP", SqlDbType.NVarChar).Value = jrm.JOBTP;
                cmd.Parameters.Add("@JOBNO", SqlDbType.BigInt).Value = jrm.JOBNO;
                cmd.Parameters.Add("@PARTYID", SqlDbType.NVarChar).Value = jrm.PARTYID;
                cmd.Parameters.Add("@DEBITCD", SqlDbType.NVarChar).Value = jrm.DEBITCD;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = jrm.REMARKS;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = jrm.AMOUNT;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = jrm.UserPC;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = jrm.InTime;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = jrm.IPAddress;

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

        public string UpdateJobReceive(JobReceiveModel jrm)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "update CNF_JOBRCV set TRANSFOR=@TRANSFOR, COMPID=@COMPID, JOBYY=@JOBYY, JOBTP=@JOBTP, JOBNO=@JOBNO, PARTYID=@PARTYID, DEBITCD=@DEBITCD, REMARKS=@REMARKS, AMOUNT=@AMOUNT, USERPC =@USERPC, UPDATETIME =@UPDATETIME, IPADDRESS =@IPADDRESS where TRANSDT=@TRANSDT and TRANSMY=@TRANSMY and TRANSNO=@TRANSNO";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = jrm.TRANSDT;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = jrm.TRANSMY;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = jrm.TRANSNO;
                cmd.Parameters.Add("@TRANSFOR", SqlDbType.NVarChar).Value = jrm.TRANSFOR;

                cmd.Parameters.Add("@COMPID", SqlDbType.NVarChar).Value = jrm.COMPID;
                cmd.Parameters.Add("@JOBYY", SqlDbType.BigInt).Value = jrm.JOBYY;
                cmd.Parameters.Add("@JOBTP", SqlDbType.NVarChar).Value = jrm.JOBTP;
                cmd.Parameters.Add("@JOBNO", SqlDbType.BigInt).Value = jrm.JOBNO;
                cmd.Parameters.Add("@PARTYID", SqlDbType.NVarChar).Value = jrm.PARTYID;
                cmd.Parameters.Add("@DEBITCD", SqlDbType.NVarChar).Value = jrm.DEBITCD;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = jrm.REMARKS;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = jrm.AMOUNT;
                
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = jrm.UserPC;
                cmd.Parameters.Add("@UPDATETIME", SqlDbType.SmallDateTime).Value = jrm.UpdateTime;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = jrm.IPAddress;

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

        public string DeleteJobReceive(JobReceiveModel jrm)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "DELETE FROM CNF_JOBRCV where TRANSMY=@TRANSMY and TRANSNO=@TRANSNO";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = jrm.TRANSMY;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = jrm.TRANSNO;

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