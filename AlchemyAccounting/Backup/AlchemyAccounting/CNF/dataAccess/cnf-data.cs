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
using AlchemyAccounting.CNF.model;

namespace AlchemyAccounting.CNF.dataAccess
{
    public class cnf_data
    {
        SqlConnection con;
        SqlCommand cmd;

        public cnf_data()
        {
            con = new SqlConnection(Global.connection);
            cmd = new SqlCommand("", con);
        }

        public string save_cnf_job(cnf_model ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO CNF_JOB (JOBCDT, COMPID, REGID, JOBYY, JOBTP, JOBNO, PARTYID, CONSIGNEENM, CONSIGNEEADD, SUPPLIERNM, PKGS, GOODS, WTGROSS, WTNET, CNFV_USD, CNFV_ETP," +
                      " CNFV_ERT, CNFV_BDT, CRFV_USD, ASSV_BDT, COMM_AMT, CONTNO, DOCINVNO, DOCRCVDT, CRFNO, CRFDT, BENO, BEDT, BLNO, BLDT, LCNO, LCDT, PERMITNO, PERMITDT, DELIVERYDT, WFDT, AWBNO, AWBDT, HBLNO, HBLDT, HAWBNO, HAWBDT, UNTKNO, UNTKDT, COM_REMARKS, STATUS, USERPC, USERID, INTIME, IPADDRESS) " +
                      " VALUES (@JOBCDT, @COMPID, @REGID, @JOBYY, @JOBTP, @JOBNO, @PARTYID, @CONSIGNEENM, @CONSIGNEEADD, @SUPPLIERNM, @PKGS, @GOODS, @WTGROSS, @WTNET, @CNFV_USD, @CNFV_ETP," +
                      " @CNFV_ERT, @CNFV_BDT, @CRFV_USD, @ASSV_BDT, @COMM_AMT, @CONTNO, @DOCINVNO, @DOCRCVDT, @CRFNO, @CRFDT, @BENO, @BEDT, @BLNO, @BLDT, @LCNO, @LCDT, @PERMITNO, @PERMITDT, @DELIVERYDT, @WFDT, @AWBNO, @AWBDT, @HBLNO, @HBLDT, @HAWBNO, @HAWBDT, @UNTKNO, @UNTKDT, @COM_REMARKS, @STATUS, @USERPC, @USERID, @INTIME, @IPADDRESS)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@JOBCDT", SqlDbType.Date).Value = ob.JobCrDT;
                cmd.Parameters.Add("@COMPID", SqlDbType.NVarChar).Value = ob.CompID;
                cmd.Parameters.Add("@REGID", SqlDbType.NVarChar).Value = ob.RegID;
                cmd.Parameters.Add("@JOBYY", SqlDbType.Int).Value = ob.JobYear;
                cmd.Parameters.Add("@JOBTP", SqlDbType.NVarChar).Value = ob.JobTP;
                cmd.Parameters.Add("@JOBNO", SqlDbType.BigInt).Value = ob.JobNo;
                cmd.Parameters.Add("@PARTYID", SqlDbType.NVarChar).Value = ob.PartyID;
                cmd.Parameters.Add("@CONSIGNEENM", SqlDbType.NVarChar).Value = ob.ConsigneeName;
                cmd.Parameters.Add("@CONSIGNEEADD", SqlDbType.NVarChar).Value = ob.ConsigneeAddress;
                cmd.Parameters.Add("@SUPPLIERNM", SqlDbType.NVarChar).Value = ob.SupplierNM;
                cmd.Parameters.Add("@PKGS", SqlDbType.NVarChar).Value = ob.PkgDetails;
                cmd.Parameters.Add("@GOODS", SqlDbType.NVarChar).Value = ob.GoodsDesc;
                cmd.Parameters.Add("@WTGROSS", SqlDbType.Decimal).Value = ob.GrossWeight;
                cmd.Parameters.Add("@WTNET", SqlDbType.Decimal).Value = ob.NetWeight;
                cmd.Parameters.Add("@CNFV_USD", SqlDbType.Decimal).Value = ob.CnfUSD;
                cmd.Parameters.Add("@CNFV_ETP", SqlDbType.NVarChar).Value = ob.ExTP;
                cmd.Parameters.Add("@CNFV_ERT", SqlDbType.Decimal).Value = ob.ExchangeRT;
                cmd.Parameters.Add("@CNFV_BDT", SqlDbType.Decimal).Value = ob.CnfBDT;
                cmd.Parameters.Add("@CRFV_USD", SqlDbType.Decimal).Value = ob.CnfUSD;
                cmd.Parameters.Add("@ASSV_BDT", SqlDbType.Decimal).Value = ob.AssessableAMT;
                cmd.Parameters.Add("@COMM_AMT", SqlDbType.Decimal).Value = ob.Commission;
                cmd.Parameters.Add("@CONTNO", SqlDbType.NVarChar).Value = ob.ContainerNo;
                cmd.Parameters.Add("@DOCINVNO", SqlDbType.NVarChar).Value = ob.InNO;
                cmd.Parameters.Add("@DOCRCVDT", SqlDbType.Date).Value = ob.InDT;
                cmd.Parameters.Add("@CRFNO", SqlDbType.NVarChar).Value = ob.CrfNO;
                cmd.Parameters.Add("@CRFDT", SqlDbType.Date).Value = ob.CrfDT;
                cmd.Parameters.Add("@BENO", SqlDbType.NVarChar).Value = ob.BeNO;
                cmd.Parameters.Add("@BEDT", SqlDbType.Date).Value = ob.BeDT;
                cmd.Parameters.Add("@BLNO", SqlDbType.NVarChar).Value = ob.BlNO;
                cmd.Parameters.Add("@BLDT", SqlDbType.Date).Value = ob.BlDT;
                cmd.Parameters.Add("@LCNO", SqlDbType.NVarChar).Value = ob.LcNO;
                cmd.Parameters.Add("@LCDT", SqlDbType.Date).Value = ob.LcDT;
                cmd.Parameters.Add("@PERMITNO", SqlDbType.NVarChar).Value = ob.PermitNO;
                cmd.Parameters.Add("@PERMITDT", SqlDbType.Date).Value = ob.PermitDT;
                cmd.Parameters.Add("@DELIVERYDT", SqlDbType.Date).Value = ob.DelDT;
                cmd.Parameters.Add("@WFDT", SqlDbType.Date).Value = ob.WharfentDT;
                cmd.Parameters.Add("@UNTKNO", SqlDbType.NVarChar).Value = ob.UnderTakeNo;
                cmd.Parameters.Add("@UNTKDT", SqlDbType.Date).Value = ob.UnderTakeDt;
                cmd.Parameters.Add("@COM_REMARKS", SqlDbType.NVarChar).Value = ob.ComRemarks;
                cmd.Parameters.Add("@STATUS", SqlDbType.NVarChar).Value = ob.Status;
                cmd.Parameters.Add("@AWBNO", SqlDbType.NVarChar).Value = ob.Awbno;
                cmd.Parameters.Add("@AWBDT", SqlDbType.DateTime).Value = ob.Awbdt;
                cmd.Parameters.Add("@HBLNO", SqlDbType.NVarChar).Value = ob.Hbl;
                cmd.Parameters.Add("@HBLDT", SqlDbType.DateTime).Value = ob.Hbldt;
                cmd.Parameters.Add("@HAWBNO", SqlDbType.NVarChar).Value = ob.Hawbno;
                cmd.Parameters.Add("@HAWBDT", SqlDbType.DateTime).Value = ob.Hawbdt;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.Userpc;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserNM;
                cmd.Parameters.Add("@INTIME", SqlDbType.DateTime).Value = ob.InTM;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;

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

        public string update_cnf_job(cnf_model ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE CNF_JOB SET JOBCDT =@JOBCDT, REGID =@REGID, PARTYID =@PARTYID, CONSIGNEENM =@CONSIGNEENM, CONSIGNEEADD =@CONSIGNEEADD, SUPPLIERNM =@SUPPLIERNM, PKGS =@PKGS, GOODS =@GOODS, WTGROSS =@WTGROSS, " +
                      " WTNET =@WTNET, CNFV_USD =@CNFV_USD, CNFV_ETP =@CNFV_ETP, CNFV_ERT =@CNFV_ERT, CNFV_BDT =@CNFV_BDT, CRFV_USD =@CRFV_USD, ASSV_BDT =@ASSV_BDT, COMM_AMT =@COMM_AMT, CONTNO =@CONTNO, DOCINVNO =@DOCINVNO, DOCRCVDT =@DOCRCVDT, CRFNO =@CRFNO, CRFDT =@CRFDT, BENO =@BENO, BEDT =@BEDT, " +
                      " BLNO =@BLNO, BLDT =@BLDT, LCNO =@LCNO, LCDT =@LCDT, PERMITNO =@PERMITNO, PERMITDT =@PERMITDT, DELIVERYDT =@DELIVERYDT, WFDT =@WFDT, AWBNO =@AWBNO, AWBDT =@AWBDT, HBLNO =@HBLNO, HBLDT =@HBLDT, HAWBNO =@HAWBNO, HAWBDT =@HAWBDT," +
                      " UNTKNO =@UNTKNO, UNTKDT =@UNTKDT, COM_REMARKS =@COM_REMARKS, STATUS =@STATUS, USERPC =@USERPC, UPDATEUSERID =@UPDATEUSERID, UPDATETIME =@UPDATETIME, IPADDRESS =@IPADDRESS WHERE JOBYY =@JOBYY AND JOBTP =@JOBTP AND JOBNO =@JOBNO";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@JOBCDT", SqlDbType.Date).Value = ob.JobCrDT;
                cmd.Parameters.Add("@REGID", SqlDbType.NVarChar).Value = ob.RegID;
                cmd.Parameters.Add("@JOBYY", SqlDbType.Int).Value = ob.JobYear;
                cmd.Parameters.Add("@JOBTP", SqlDbType.NVarChar).Value = ob.JobTP;
                cmd.Parameters.Add("@JOBNO", SqlDbType.BigInt).Value = ob.JobNo;
                cmd.Parameters.Add("@PARTYID", SqlDbType.NVarChar).Value = ob.PartyID;
                cmd.Parameters.Add("@CONSIGNEENM", SqlDbType.NVarChar).Value = ob.ConsigneeName;
                cmd.Parameters.Add("@CONSIGNEEADD", SqlDbType.NVarChar).Value = ob.ConsigneeAddress;
                cmd.Parameters.Add("@SUPPLIERNM", SqlDbType.NVarChar).Value = ob.SupplierNM;
                cmd.Parameters.Add("@PKGS", SqlDbType.NVarChar).Value = ob.PkgDetails;
                cmd.Parameters.Add("@GOODS", SqlDbType.NVarChar).Value = ob.GoodsDesc;
                cmd.Parameters.Add("@WTGROSS", SqlDbType.Decimal).Value = ob.GrossWeight;
                cmd.Parameters.Add("@WTNET", SqlDbType.Decimal).Value = ob.NetWeight;
                cmd.Parameters.Add("@CNFV_USD", SqlDbType.Decimal).Value = ob.CnfUSD;
                cmd.Parameters.Add("@CNFV_ETP", SqlDbType.NVarChar).Value = ob.ExTP;
                cmd.Parameters.Add("@CNFV_ERT", SqlDbType.Decimal).Value = ob.ExchangeRT;
                cmd.Parameters.Add("@CNFV_BDT", SqlDbType.Decimal).Value = ob.CnfBDT;
                cmd.Parameters.Add("@CRFV_USD", SqlDbType.Decimal).Value = ob.CnfUSD;
                cmd.Parameters.Add("@ASSV_BDT", SqlDbType.Decimal).Value = ob.AssessableAMT;
                cmd.Parameters.Add("@COMM_AMT", SqlDbType.Decimal).Value = ob.Commission;
                cmd.Parameters.Add("@CONTNO", SqlDbType.NVarChar).Value = ob.ContainerNo;
                cmd.Parameters.Add("@DOCINVNO", SqlDbType.NVarChar).Value = ob.InNO;
                cmd.Parameters.Add("@DOCRCVDT", SqlDbType.Date).Value = ob.InDT;
                cmd.Parameters.Add("@CRFNO", SqlDbType.NVarChar).Value = ob.CrfNO;
                cmd.Parameters.Add("@CRFDT", SqlDbType.Date).Value = ob.CrfDT;
                cmd.Parameters.Add("@BENO", SqlDbType.NVarChar).Value = ob.BeNO;
                cmd.Parameters.Add("@BEDT", SqlDbType.Date).Value = ob.BeDT;
                cmd.Parameters.Add("@BLNO", SqlDbType.NVarChar).Value = ob.BlNO;
                cmd.Parameters.Add("@BLDT", SqlDbType.Date).Value = ob.BlDT;
                cmd.Parameters.Add("@LCNO", SqlDbType.NVarChar).Value = ob.LcNO;
                cmd.Parameters.Add("@LCDT", SqlDbType.Date).Value = ob.LcDT;
                cmd.Parameters.Add("@PERMITNO", SqlDbType.NVarChar).Value = ob.PermitNO;
                cmd.Parameters.Add("@PERMITDT", SqlDbType.Date).Value = ob.PermitDT;
                cmd.Parameters.Add("@DELIVERYDT", SqlDbType.Date).Value = ob.DelDT;
                cmd.Parameters.Add("@WFDT", SqlDbType.Date).Value = ob.WharfentDT;
                cmd.Parameters.Add("@AWBNO", SqlDbType.NVarChar).Value = ob.Awbno;
                cmd.Parameters.Add("@AWBDT", SqlDbType.DateTime).Value = ob.Awbdt;
                cmd.Parameters.Add("@HBLNO", SqlDbType.NVarChar).Value = ob.Hbl;
                cmd.Parameters.Add("@HBLDT", SqlDbType.DateTime).Value = ob.Hbldt;
                cmd.Parameters.Add("@HAWBNO", SqlDbType.NVarChar).Value = ob.Hawbno;
                cmd.Parameters.Add("@HAWBDT", SqlDbType.DateTime).Value = ob.Hawbdt;
                cmd.Parameters.Add("@UNTKNO", SqlDbType.NVarChar).Value = ob.UnderTakeNo;
                cmd.Parameters.Add("@UNTKDT", SqlDbType.Date).Value = ob.UnderTakeDt;
                cmd.Parameters.Add("@COM_REMARKS", SqlDbType.NVarChar).Value = ob.ComRemarks;
                cmd.Parameters.Add("@STATUS", SqlDbType.NVarChar).Value = ob.Status;
                cmd.Parameters.Add("@UPDATETIME", SqlDbType.DateTime).Value = ob.UpTM;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.Userpc;
                cmd.Parameters.Add("@UPDATEUSERID", SqlDbType.NVarChar).Value = ob.UpdateUser;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;

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

        public string save_cnf_job_expense_master(cnf_model ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO CNF_JOBEXPMST (TRANSDT, TRANSMY, TRANSNO, COMPID, JOBYY, JOBTP, JOBNO, EXPCD, USERPC, USERID, INTIME, IPADDRESS) " +
                     " VALUES (@TRANSDT, @TRANSMY, @TRANSNO, @COMPID, @JOBYY, @JOBTP, @JOBNO, @EXPCD, @USERPC, @USERID, @INTIME, @IPADDRESS)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSDT", SqlDbType.Date).Value = ob.ExDT;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.ExMY;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.InvoiceNO;
                cmd.Parameters.Add("@COMPID", SqlDbType.NVarChar).Value = ob.CompID;
                cmd.Parameters.Add("@JOBYY", SqlDbType.Int).Value = ob.JobYear;
                cmd.Parameters.Add("@JOBTP", SqlDbType.NVarChar).Value = ob.JobTP;
                cmd.Parameters.Add("@JOBNO", SqlDbType.BigInt).Value = ob.JobNo;
                cmd.Parameters.Add("@EXPCD", SqlDbType.NVarChar).Value = ob.ExpenseCD;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.Userpc;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserNM;
                cmd.Parameters.Add("@INTIME", SqlDbType.DateTime).Value = ob.InTM;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;

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

        public string update_cnf_job_expense_master(cnf_model ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE CNF_JOBEXPMST SET TRANSDT=@TRANSDT, COMPID =@COMPID, JOBYY =@JOBYY, JOBTP =@JOBTP, JOBNO =@JOBNO, EXPCD =@EXPCD, USERPC =@USERPC, USERID =@UPDATEUSERID, UPDATETIME =@UPDATETIME, IPADDRESS =@IPADDRESS " +
                    " WHERE TRANSMY =@TRANSMY AND TRANSNO =@TRANSNO";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSDT", SqlDbType.Date).Value = ob.ExDT;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.ExMY;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.InvoiceNO;
                cmd.Parameters.Add("@COMPID", SqlDbType.NVarChar).Value = ob.CompID;
                cmd.Parameters.Add("@JOBYY", SqlDbType.Int).Value = ob.JobYear;
                cmd.Parameters.Add("@JOBTP", SqlDbType.NVarChar).Value = ob.JobTP;
                cmd.Parameters.Add("@JOBNO", SqlDbType.BigInt).Value = ob.JobNo;
                cmd.Parameters.Add("@EXPCD", SqlDbType.NVarChar).Value = ob.ExpenseCD;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.Userpc;
                cmd.Parameters.Add("@UPDATEUSERID", SqlDbType.NVarChar).Value = ob.UpdateUser;
                cmd.Parameters.Add("@UPDATETIME", SqlDbType.DateTime).Value = ob.UpTM;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;

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

        public string update_cnf_job_expense_top(cnf_model ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE CNF_JOBEXP SET TRANSDT=@TRANSDT, COMPID =@COMPID, JOBYY =@JOBYY, JOBTP =@JOBTP, JOBNO =@JOBNO, EXPCD =@EXPCD, USERPC =@USERPC, USERID =@UPDATEUSERID, UPDATETIME =@UPDATETIME, IPADDRESS =@IPADDRESS " +
                    " WHERE TRANSMY =@TRANSMY AND TRANSNO =@TRANSNO";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSDT", SqlDbType.Date).Value = ob.ExDT;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.ExMY;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.InvoiceNO;
                cmd.Parameters.Add("@COMPID", SqlDbType.NVarChar).Value = ob.CompID;
                cmd.Parameters.Add("@JOBYY", SqlDbType.Int).Value = ob.JobYear;
                cmd.Parameters.Add("@JOBTP", SqlDbType.NVarChar).Value = ob.JobTP;
                cmd.Parameters.Add("@JOBNO", SqlDbType.BigInt).Value = ob.JobNo;
                cmd.Parameters.Add("@EXPCD", SqlDbType.NVarChar).Value = ob.ExpenseCD;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.Userpc;
                cmd.Parameters.Add("@UPDATEUSERID", SqlDbType.NVarChar).Value = ob.UpdateUser;
                cmd.Parameters.Add("@UPDATETIME", SqlDbType.DateTime).Value = ob.UpTM;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;

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

        public string update_cnf_job_expense_remarks_master(cnf_model ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE CNF_JOBEXPMST SET TRANSDT =@TRANSDT, COMPID =@COMPID, JOBYY =@JOBYY, JOBTP =@JOBTP, JOBNO =@JOBNO, EXPCD =@EXPCD, REMARKS =@REMARKS, USERPC =@USERPC, USERID =@USERID, IPADDRESS =@IPADDRESS " +
                    " WHERE TRANSMY =@TRANSMY AND TRANSNO =@TRANSNO";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSDT", SqlDbType.Date).Value = ob.ExDT;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.ExMY;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.InvoiceNO;
                cmd.Parameters.Add("@COMPID", SqlDbType.NVarChar).Value = ob.CompID;
                cmd.Parameters.Add("@JOBYY", SqlDbType.Int).Value = ob.JobYear;
                cmd.Parameters.Add("@JOBTP", SqlDbType.NVarChar).Value = ob.JobTP;
                cmd.Parameters.Add("@JOBNO", SqlDbType.BigInt).Value = ob.JobNo;
                cmd.Parameters.Add("@EXPCD", SqlDbType.NVarChar).Value = ob.ExpenseCD;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.RemarksTOP;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.Userpc;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserNM;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;

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

        public string save_cnf_job_expense(cnf_model ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO CNF_JOBEXP (TRANSDT, TRANSMY, TRANSNO, COMPID, JOBYY, JOBTP, JOBNO, EXPCD, SLNO, EXPID, EXPAMT, REMARKS, USERPC, USERID, INTIME, IPADDRESS) " +
                     " VALUES (@TRANSDT, @TRANSMY, @TRANSNO, @COMPID, @JOBYY, @JOBTP, @JOBNO, @EXPCD, @SLNO, @EXPID, @EXPAMT, @REMARKS, @USERPC, @USERID, @INTIME, @IPADDRESS)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSDT", SqlDbType.Date).Value = ob.ExDT;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.ExMY;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.InvoiceNO;
                cmd.Parameters.Add("@COMPID", SqlDbType.NVarChar).Value = ob.CompID;
                cmd.Parameters.Add("@JOBYY", SqlDbType.Int).Value = ob.JobYear;
                cmd.Parameters.Add("@JOBTP", SqlDbType.NVarChar).Value = ob.JobTP;
                cmd.Parameters.Add("@JOBNO", SqlDbType.BigInt).Value = ob.JobNo;
                cmd.Parameters.Add("@EXPCD", SqlDbType.NVarChar).Value = ob.ExpenseCD;
                cmd.Parameters.Add("@SLNO", SqlDbType.BigInt).Value = ob.Sl;
                cmd.Parameters.Add("@EXPID", SqlDbType.NVarChar).Value = ob.ExpensesID;
                cmd.Parameters.Add("@EXPAMT", SqlDbType.Decimal).Value = ob.Amount;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.RemarksBOT;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.Userpc;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserNM;
                cmd.Parameters.Add("@INTIME", SqlDbType.DateTime).Value = ob.InTM;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;

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

        public string update_cnf_job_expense(cnf_model ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE CNF_JOBEXP SET EXPID =@EXPID, EXPAMT =@EXPAMT, REMARKS =@REMARKS, USERPC =@USERPC, UPDATEUSERID =@UPDATEUSERID, UPDATETIME =@UPDATETIME, IPADDRESS =@IPADDRESS " +
                    " WHERE TRANSMY =@TRANSMY AND TRANSNO =@TRANSNO AND SLNO =@SLNO";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.ExMY;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.InvoiceNO;
                cmd.Parameters.Add("@SLNO", SqlDbType.BigInt).Value = ob.Sl;
                cmd.Parameters.Add("@EXPID", SqlDbType.NVarChar).Value = ob.ExpensesID;
                cmd.Parameters.Add("@EXPAMT", SqlDbType.Decimal).Value = ob.Amount;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.RemarksBOT;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.Userpc;
                cmd.Parameters.Add("@UPDATEUSERID", SqlDbType.NVarChar).Value = ob.UpdateUser;
                cmd.Parameters.Add("@UPDATETIME", SqlDbType.DateTime).Value = ob.UpTM;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;

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

        public string delete_cnf_job_expense(cnf_model ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM CNF_JOBEXP WHERE TRANSMY =@TRANSMY AND TRANSNO =@TRANSNO AND SLNO =@SLNO";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.ExMY;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.InvoiceNO;
                cmd.Parameters.Add("@SLNO", SqlDbType.BigInt).Value = ob.Sl;

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

        public string delete_cnf_job_expense_master(cnf_model ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM CNF_JOBEXPMST WHERE TRANSMY =@TRANSMY AND TRANSNO =@TRANSNO";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.ExMY;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.InvoiceNO;

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