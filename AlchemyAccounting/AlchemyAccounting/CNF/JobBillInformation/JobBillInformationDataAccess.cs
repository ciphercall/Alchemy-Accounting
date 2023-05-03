using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace AlchemyAccounting.CNF.JobBillInformation
{
    public class JobBillInformationDataAccess
    {
        SqlConnection con;
        SqlCommand cmd;

        public JobBillInformationDataAccess()
        {
            con = new SqlConnection(Global.connection);
            cmd = new SqlCommand("", con);
        }

        public string SaveJobBillInfo(JobBillInformationModel jbfm)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "insert into CNF_JOBBILL(COMPID, JOBYY, JOBTP,JOBNO,PARTYID,BILLDT,BILLNO,EXPSL,EXPID,EXPAMT, BILLAMT,EXPPDT,REMARKS,BILLSL, USERPC, USERID, INTIME, IPADDRESS)" +
                    "values(@COMPID, @JOBYY, @JOBTP,@JOBNO,@PARTYID,@BILLDT, @BILLNO, @EXPSL, @EXPID,@EXPAMT, @BILLAMT,@EXPPDT,@REMARKS,@BILLSL, @USERPC, @USERID, @INTIME, @IPADDRESS )";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.NVarChar).Value = jbfm.COMPID;
                cmd.Parameters.Add("@JOBYY", SqlDbType.BigInt).Value = jbfm.JOBYY;
                cmd.Parameters.Add("@JOBTP", SqlDbType.NVarChar).Value = jbfm.JOBTP;
                cmd.Parameters.Add("@JOBNO", SqlDbType.BigInt).Value = jbfm.JOBNO;

                cmd.Parameters.Add("@PARTYID", SqlDbType.NVarChar).Value = jbfm.PARTYID;
                cmd.Parameters.Add("@BILLDT", SqlDbType.SmallDateTime).Value = jbfm.BILLDT;
                cmd.Parameters.Add("@BILLNO", SqlDbType.BigInt).Value = jbfm.BILLNO;
                cmd.Parameters.Add("@EXPSL", SqlDbType.BigInt).Value = jbfm.EXPSL;
                cmd.Parameters.Add("@EXPID", SqlDbType.NVarChar).Value = jbfm.EXPID;
                cmd.Parameters.Add("@EXPAMT", SqlDbType.Decimal).Value = jbfm.EXPAMT;
                cmd.Parameters.Add("@BILLAMT", SqlDbType.Decimal).Value = jbfm.BILLAMT;
                cmd.Parameters.Add("@EXPPDT", SqlDbType.SmallDateTime).Value = jbfm.EXPPDT;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = jbfm.REMARKS;
                cmd.Parameters.Add("@BILLSL", SqlDbType.BigInt).Value = jbfm.BILLSL;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = jbfm.UserPC;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = jbfm.Userid;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = jbfm.InTime;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = jbfm.IPAddress;

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

        public string UpdateJobBillInfo(JobBillInformationModel jbfm)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "update CNF_JOBBILL set BILLAMT=@BILLAMT, REMARKS=@REMARKS, EXPPDT=@EXPPDT, BILLSL=@BILLSL, USERPC =@USERPC, UPDATEUSERID =@UPDATEUSERID, UPDATETIME =@UPDATETIME, IPADDRESS =@IPADDRESS where BILLDT=@BILLDT and BILLNO=@BILLNO and EXPSL=@EXPSL and JOBYY=@JOBYY and JOBTP=@JOBTP and JOBNO=@JOBNO and EXPID=@EXPID";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.NVarChar).Value = jbfm.COMPID;
                cmd.Parameters.Add("@JOBYY", SqlDbType.BigInt).Value = jbfm.JOBYY;
                cmd.Parameters.Add("@JOBTP", SqlDbType.NVarChar).Value = jbfm.JOBTP;
                cmd.Parameters.Add("@JOBNO", SqlDbType.BigInt).Value = jbfm.JOBNO;

                cmd.Parameters.Add("@PARTYID", SqlDbType.NVarChar).Value = jbfm.PARTYID;
                cmd.Parameters.Add("@BILLDT", SqlDbType.SmallDateTime).Value = jbfm.BILLDT;
                cmd.Parameters.Add("@BILLNO", SqlDbType.BigInt).Value = jbfm.BILLNO;
                cmd.Parameters.Add("@EXPSL", SqlDbType.BigInt).Value = jbfm.EXPSL;
                cmd.Parameters.Add("@EXPID", SqlDbType.NVarChar).Value = jbfm.EXPID;
                cmd.Parameters.Add("@EXPAMT", SqlDbType.Decimal).Value = jbfm.EXPAMT;
                cmd.Parameters.Add("@BILLAMT", SqlDbType.Decimal).Value = jbfm.BILLAMT;
                cmd.Parameters.Add("@EXPPDT", SqlDbType.SmallDateTime).Value = jbfm.EXPPDT;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = jbfm.REMARKS;
                cmd.Parameters.Add("@BILLSL", SqlDbType.BigInt).Value = jbfm.BILLSL;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = jbfm.UserPC;
                cmd.Parameters.Add("@UPDATEUSERID", SqlDbType.NVarChar).Value = jbfm.UpdateuserID;
                cmd.Parameters.Add("@UPDATETIME", SqlDbType.SmallDateTime).Value = jbfm.UpdateTime;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = jbfm.IPAddress;

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

        public string RemoveJobBillInfo(JobBillInformationModel jbfm)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "delete from CNF_JOBBILL where BILLDT=@BILLDT and BILLNO=@BILLNO and EXPSL=@EXPSL and JOBYY=@JOBYY and JOBTP=@JOBTP and JOBNO=@JOBNO and EXPID=@EXPID";

                cmd.Parameters.Clear();

                cmd.Parameters.Add("@COMPID", SqlDbType.NVarChar).Value = jbfm.COMPID;
                cmd.Parameters.Add("@JOBYY", SqlDbType.BigInt).Value = jbfm.JOBYY;
                cmd.Parameters.Add("@JOBTP", SqlDbType.NVarChar).Value = jbfm.JOBTP;
                cmd.Parameters.Add("@JOBNO", SqlDbType.BigInt).Value = jbfm.JOBNO;

                cmd.Parameters.Add("@PARTYID", SqlDbType.NVarChar).Value = jbfm.PARTYID;
                cmd.Parameters.Add("@BILLDT", SqlDbType.SmallDateTime).Value = jbfm.BILLDT;
                cmd.Parameters.Add("@BILLNO", SqlDbType.BigInt).Value = jbfm.BILLNO;
                cmd.Parameters.Add("@EXPSL", SqlDbType.BigInt).Value = jbfm.EXPSL;
                cmd.Parameters.Add("@EXPID", SqlDbType.NVarChar).Value = jbfm.EXPID;
                cmd.Parameters.Add("@EXPAMT", SqlDbType.Decimal).Value = jbfm.EXPAMT;
                cmd.Parameters.Add("@BILLAMT", SqlDbType.Decimal).Value = jbfm.BILLAMT;
                cmd.Parameters.Add("@EXPPDT", SqlDbType.SmallDateTime).Value = jbfm.EXPPDT;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = jbfm.REMARKS;
                cmd.Parameters.Add("@BILLSL", SqlDbType.BigInt).Value = jbfm.BILLSL;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = jbfm.UserPC;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = jbfm.InTime;
                cmd.Parameters.Add("@UPDATETIME", SqlDbType.SmallDateTime).Value = jbfm.UpdateTime;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = jbfm.IPAddress;

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

        /// <summary>
        /// Tracking
        /// </summary>
        /// <param name="jbfm"></param>
        /// <returns></returns>

        public string SaveJobBillInfo_Tracking(JobBillInformationModel jbfm)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = @"Insert into CNF_DOCSTATS(COMPID,JOBTP,JOBYY,JOBNO,STATSDT,STATUS,REMARKS,USERPC,USERID,INTIME,IPADDRESS) 
                                values(@COMPID,@JOBTP,@JOBYY,@JOBNO,@STATSDT,@STATUS,@REMARKS,@USERPC,@USERID,@INTIME,@IPADDRESS)";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.NVarChar).Value = jbfm.COMPID;
                cmd.Parameters.Add("@JOBTP", SqlDbType.NVarChar).Value = jbfm.JOBTP;
                cmd.Parameters.Add("@JOBYY", SqlDbType.Int).Value = jbfm.JOBYY;
                cmd.Parameters.Add("@JOBNO", SqlDbType.BigInt).Value = jbfm.JOBNO;
                cmd.Parameters.Add("@STATSDT", SqlDbType.SmallDateTime).Value = jbfm.Statusdt;
                cmd.Parameters.Add("@STATUS", SqlDbType.NVarChar).Value = jbfm.Status;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = jbfm.REMARKS;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = jbfm.UserPC;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = jbfm.Userid;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = jbfm.InTime;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = jbfm.IPAddress;
                

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

        public string UpdateJobBillInfo_Tracking(JobBillInformationModel jbfm)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = @"Update CNF_DOCSTATS set STATSDT=@STATSDT, STATUS=@STATUS, REMARKS=@REMARKS, UPDATEPC=@UPDATEPC, 
                            UPDATEUSERID=@UPDATEUSERID, UPDATETIME=@UPDATETIME, UPDATEIP=@UPDATEIP
                                where COMPID=@COMPID and JOBTP=@JOBTP and JOBYY=@JOBYY and JOBNO=@JOBNO and SERIAL=@SERIAL";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.NVarChar).Value = jbfm.COMPID;
                cmd.Parameters.Add("@JOBYY", SqlDbType.BigInt).Value = jbfm.JOBYY;
                cmd.Parameters.Add("@JOBTP", SqlDbType.NVarChar).Value = jbfm.JOBTP;
                cmd.Parameters.Add("@JOBNO", SqlDbType.BigInt).Value = jbfm.JOBNO;
                cmd.Parameters.Add("@SERIAL", SqlDbType.BigInt).Value = jbfm.Serial;

                cmd.Parameters.Add("@STATSDT", SqlDbType.SmallDateTime).Value = jbfm.Statusdt;
                cmd.Parameters.Add("@STATUS", SqlDbType.NVarChar).Value = jbfm.Status;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = jbfm.REMARKS;

                cmd.Parameters.Add("@UPDATEPC", SqlDbType.NVarChar).Value = jbfm.UserPC;
                cmd.Parameters.Add("@UPDATEUSERID", SqlDbType.NVarChar).Value = jbfm.UpdateuserID;
                cmd.Parameters.Add("@UPDATETIME", SqlDbType.SmallDateTime).Value = jbfm.UpdateTime;
                cmd.Parameters.Add("@UPDATEIP", SqlDbType.NVarChar).Value = jbfm.IPAddress;

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

        public string RemoveJobBillInfo_Tracking(JobBillInformationModel jbfm)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = @"delete from CNF_DOCSTATS 
                        where COMPID=@COMPID and JOBTP=@JOBTP and JOBYY=@JOBYY and JOBNO=@JOBNO and SERIAL=@SERIAL ";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.NVarChar).Value = jbfm.COMPID;
                cmd.Parameters.Add("@JOBYY", SqlDbType.BigInt).Value = jbfm.JOBYY;
                cmd.Parameters.Add("@JOBTP", SqlDbType.NVarChar).Value = jbfm.JOBTP;
                cmd.Parameters.Add("@JOBNO", SqlDbType.BigInt).Value = jbfm.JOBNO;
                cmd.Parameters.Add("@SERIAL", SqlDbType.BigInt).Value = jbfm.Serial;

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