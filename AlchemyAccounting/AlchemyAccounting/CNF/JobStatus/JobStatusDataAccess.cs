using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace AlchemyAccounting.CNF.JobStatus
{
    public class JobStatusDataAccess
    {
        SqlConnection con;
        SqlCommand cmd;

        public JobStatusDataAccess()
        {
            con = new SqlConnection(Global.connection);
            cmd = new SqlCommand("", con);
        }


        public string SaveJobStatus(JobStatusModel jsm)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "insert into CNF_JOBSTATUS (TRANSDT, TRANSYY, TRANSNO, COMPID ,JOBYY,JOBTP, JOBNO,BILLFDT,STATUS, USERPC, USERID, INTIME, IPADDRESS)" +
                    "values(@TRANSDT, @TRANSYY,@TRANSNO, @COMPID,@JOBYY, @JOBTP, @JOBNO,@BILLFDT,@STATUS, @USERPC, @USERID, @INTIME , @IPADDRESS  )";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = jsm.TRANSDT;
                cmd.Parameters.Add("@TRANSYY", SqlDbType.BigInt).Value = jsm.TRANSYY;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = jsm.TRANSNO;
                cmd.Parameters.Add("@COMPID", SqlDbType.NVarChar).Value = jsm.COMPID;
                cmd.Parameters.Add("@JOBYY", SqlDbType.BigInt).Value = jsm.JOBYY;
                cmd.Parameters.Add("@JOBTP", SqlDbType.NVarChar).Value = jsm.JOBTP;
                cmd.Parameters.Add("@JOBNO", SqlDbType.BigInt).Value = jsm.JOBNO;
                cmd.Parameters.Add("@BILLFDT", SqlDbType.SmallDateTime).Value = jsm.BILLFDT;
                cmd.Parameters.Add("@STATUS", SqlDbType.Char).Value = jsm.STATUS;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = jsm.UserPC;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = jsm.UserNm;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = jsm.InTime;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = jsm.IPAddress;

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




        public string UpdateJobStatus(JobStatusModel jsm)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "update CNF_JOBSTATUS set BILLFDT=@BILLFDT , STATUS=@STATUS, USERPC =@USERPC, UPDATEUSERID =@UPDATEUSERID, UPDATETIME =@UPDATETIME, IPADDRESS =@IPADDRESS where TRANSNO=@TRANSNO and COMPID=@COMPID and JOBNO=JOBNO and JOBTP=@JOBTP and JOBYY=@JOBYY and TRANSDT=@TRANSDT";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = jsm.TRANSDT;
                cmd.Parameters.Add("@TRANSYY", SqlDbType.BigInt).Value = jsm.TRANSYY;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = jsm.TRANSNO;
                cmd.Parameters.Add("@COMPID", SqlDbType.NVarChar).Value = jsm.COMPID;
                cmd.Parameters.Add("@JOBYY", SqlDbType.BigInt).Value = jsm.JOBYY;
                cmd.Parameters.Add("@JOBTP", SqlDbType.NVarChar).Value = jsm.JOBTP;
                cmd.Parameters.Add("@JOBNO", SqlDbType.BigInt).Value = jsm.JOBNO;
                cmd.Parameters.Add("@BILLFDT", SqlDbType.SmallDateTime).Value = jsm.BILLFDT;
                cmd.Parameters.Add("@STATUS", SqlDbType.Char).Value = jsm.STATUS;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = jsm.UserPC;
                cmd.Parameters.Add("@UPDATEUSERID", SqlDbType.NVarChar).Value = jsm.UpdateuserNm;
                cmd.Parameters.Add("@UPDATETIME", SqlDbType.SmallDateTime).Value = jsm.UpdateTime;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = jsm.IPAddress;

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

        public string DeleteJobStatus(JobStatusModel jsm)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "delete from CNF_JOBSTATUS where TRANSDT=@TRANSDT and TRANSNO=@TRANSNO and COMPID=@COMPID and JOBYY=@JOBYY and JOBTP=@JOBTP and JOBNO=@JOBNO";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = jsm.TRANSDT;
                cmd.Parameters.Add("@TRANSYY", SqlDbType.BigInt).Value = jsm.TRANSYY;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = jsm.TRANSNO;
                cmd.Parameters.Add("@COMPID", SqlDbType.NVarChar).Value = jsm.COMPID;
                cmd.Parameters.Add("@JOBYY", SqlDbType.BigInt).Value = jsm.JOBYY;
                cmd.Parameters.Add("@JOBTP", SqlDbType.NVarChar).Value = jsm.JOBTP;
                cmd.Parameters.Add("@JOBNO", SqlDbType.BigInt).Value = jsm.JOBNO;
               


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

        public string Job_Bill_Process(JobStatusModel jsm)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = " INSERT INTO CNF_JOBBILL (PROCESSDT, COMPID, JOBYY, JOBTP, JOBNO, PARTYID, BILLDT, BILLNO, EXPSL, EXPID, EXPAMT,USERID, USERPC, INTIME, IPADDRESS) " +
                    " VALUES (@PROCESSDT, @COMPID, @JOBYY, @JOBTP, @JOBNO, @PARTYID, @BILLDT, @BILLNO, @EXPSL, @EXPID, @EXPAMT, @USERID, @USERPC, @INTIME, @IPADDRESS)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@PROCESSDT", SqlDbType.SmallDateTime).Value = jsm.TRANSDT;
                cmd.Parameters.Add("@COMPID", SqlDbType.NVarChar).Value = jsm.COMPID;
                cmd.Parameters.Add("@JOBYY", SqlDbType.BigInt).Value = jsm.JOBYY;
                cmd.Parameters.Add("@JOBTP", SqlDbType.NVarChar).Value = jsm.JOBTP;
                cmd.Parameters.Add("@JOBNO", SqlDbType.BigInt).Value = jsm.JOBNO;
                cmd.Parameters.Add("@PARTYID", SqlDbType.NVarChar).Value = jsm.PartyID;
                cmd.Parameters.Add("@BILLDT", SqlDbType.SmallDateTime).Value = jsm.TRANSDT;
                cmd.Parameters.Add("@BILLNO", SqlDbType.Decimal).Value = jsm.TRANSNO;
                cmd.Parameters.Add("@EXPSL", SqlDbType.BigInt).Value = jsm.ExSL;
                cmd.Parameters.Add("@EXPID", SqlDbType.NVarChar).Value = jsm.ExpID;
                cmd.Parameters.Add("@EXPAMT", SqlDbType.Decimal).Value = jsm.Amount;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = jsm.UserNm;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = jsm.UserPC;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = jsm.InTime;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = jsm.IPAddress;

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
        public string Job_Bill_ProcessForI1BillAmount(JobStatusModel jsm)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = " INSERT INTO CNF_JOBBILL (PROCESSDT, COMPID, JOBYY, JOBTP, JOBNO, PARTYID, BILLDT, BILLNO, EXPSL, EXPID, EXPAMT,BILLAMT,USERID, USERPC, INTIME, IPADDRESS) " +
                    " VALUES (@PROCESSDT, @COMPID, @JOBYY, @JOBTP, @JOBNO, @PARTYID, @BILLDT, @BILLNO, @EXPSL, @EXPID, @EXPAMT, @BILLAMT,@USERID, @USERPC, @INTIME, @IPADDRESS)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@PROCESSDT", SqlDbType.SmallDateTime).Value = jsm.TRANSDT;
                cmd.Parameters.Add("@COMPID", SqlDbType.NVarChar).Value = jsm.COMPID;
                cmd.Parameters.Add("@JOBYY", SqlDbType.BigInt).Value = jsm.JOBYY;
                cmd.Parameters.Add("@JOBTP", SqlDbType.NVarChar).Value = jsm.JOBTP;
                cmd.Parameters.Add("@JOBNO", SqlDbType.BigInt).Value = jsm.JOBNO;
                cmd.Parameters.Add("@PARTYID", SqlDbType.NVarChar).Value = jsm.PartyID;
                cmd.Parameters.Add("@BILLDT", SqlDbType.SmallDateTime).Value = jsm.TRANSDT;
                cmd.Parameters.Add("@BILLNO", SqlDbType.BigInt).Value = jsm.TRANSNO;
                cmd.Parameters.Add("@EXPSL", SqlDbType.BigInt).Value = jsm.ExSL;
                cmd.Parameters.Add("@EXPID", SqlDbType.NVarChar).Value = jsm.ExpID;
                cmd.Parameters.Add("@EXPAMT", SqlDbType.Decimal).Value = jsm.Amount;
                cmd.Parameters.Add("@BILLAMT", SqlDbType.Decimal).Value = jsm.Amount;

                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = jsm.UserNm;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = jsm.UserPC;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = jsm.InTime;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = jsm.IPAddress;

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