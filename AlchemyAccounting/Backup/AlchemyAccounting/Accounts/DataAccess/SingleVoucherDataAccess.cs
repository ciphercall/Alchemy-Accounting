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
using AlchemyAccounting.Accounts.Interface;

namespace AlchemyAccounting.Accounts.DataAccess
{
    public class SingleVoucher
    {
        SqlConnection con;
        SqlCommand cmd;

        public SingleVoucher()
        {
            con = new SqlConnection(Global.connection);
            cmd = new SqlCommand("", con);
        }

        public string insertSingleVouch(AlchemyAccounting.Accounts.Interface.SingleVoucher ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open) 
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO GL_STRANS(TRANSTP, TRANSDT, TRANSMY, TRANSNO, SERIALNO, TRANSFOR, COSTPID, TRANSMODE, DEBITCD, CREDITCD, CHEQUENO, CHEQUEDT, AMOUNT, REMARKS, USERPC, USERID, INTIME, IPADDRESS) " +
                                  "VALUES (@TRANSTP,@TRANSDT,@TRANSMY,@TRANSNO,@SERIALNO,@TRANSFOR,@COSTPID,@TRANSMODE,@DEBITCD,@CREDITCD,@CHEQUENO,@CHEQUEDT,@AMOUNT,@REMARKS,@USERPC,@USERID, @INTIME, @IPADDRESS)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value=ob.Transtp;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.Transdt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.Monyear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.Voucher;
                cmd.Parameters.Add("@SERIALNO", SqlDbType.BigInt).Value = 0;
                cmd.Parameters.Add("@TRANSFOR", SqlDbType.NVarChar).Value = ob.Transfor;
                cmd.Parameters.Add("@COSTPID", SqlDbType.NVarChar).Value = ob.Costpid;
                cmd.Parameters.Add("@TRANSMODE", SqlDbType.NVarChar).Value = ob.Transmode;
                cmd.Parameters.Add("@DEBITCD", SqlDbType.NVarChar).Value = ob.Debitcd;
                cmd.Parameters.Add("@CREDITCD", SqlDbType.NVarChar).Value = ob.Creditcd;
                cmd.Parameters.Add("@CHEQUENO", SqlDbType.NVarChar).Value = ob.Chequeno;
                cmd.Parameters.Add("@CHEQUEDT", SqlDbType.DateTime).Value = ob.Chequedt;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.Userpc;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.Username;
                cmd.Parameters.Add("@INTIME", SqlDbType.DateTime).Value = ob.InTM;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;

                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch(Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }
        string inc_Serial = "";
        int ser;
        string final_Serial = "";

        public string doProcess_MREC(AlchemyAccounting.Accounts.Interface.SingleVoucher ob)
        {
            string s = "";
            SqlTransaction tran = null;
            

            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO GL_MASTER ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, SERIALNO, TRANSDRCR, TRANSFOR, COSTPID, TRANSMODE, DEBITCD, CREDITCD, DEBITAMT, CREDITAMT, " +
                                  " CHEQUENO, CHEQUEDT, REMARKS, TABLEID, USERPC, USERID, ACTDTI, IPADDRESS) " +
                                  " Values (@TRANSTP, @TRANSDT, @TRANSMY, @TRANSNO, @SERIALNO,'DEBIT', @TRANSFOR, @COSTPID, @TRANSMODE, @DEBITCD, @CREDITCD,@AMOUNT,0, @CHEQUENO, @CHEQUEDT, @REMARKS,'GL_STRANS', " +
                                  " @USERPC,@USERID, @ACTDTI, @IPADDRESS)";
                cmd.Parameters.Clear();

                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.Transtp;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.Transdt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.Monyear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                cmd.Parameters.Add("@SERIALNO", SqlDbType.BigInt).Value = ob.SerialNo_MREC;
                cmd.Parameters.Add("@TRANSFOR", SqlDbType.NVarChar).Value = ob.Transfor;
                if (ob.Costpid == "&nbsp;")
                {
                    ob.Costpid = "";
                }
                else
                    ob.Costpid = ob.Costpid;
                cmd.Parameters.Add("@COSTPID", SqlDbType.NVarChar).Value = ob.Costpid;
                cmd.Parameters.Add("@TRANSMODE", SqlDbType.NVarChar).Value = ob.Transmode; 
                cmd.Parameters.Add("@DEBITCD", SqlDbType.NVarChar).Value = ob.Debitcd;
                cmd.Parameters.Add("@CREDITCD", SqlDbType.NVarChar).Value = ob.Creditcd;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                if (ob.Chequeno == "&nbsp;")
                {
                    ob.Chequeno = "";
                }
                else
                    ob.Chequeno = ob.Chequeno;
                cmd.Parameters.Add("@CHEQUENO", SqlDbType.NVarChar).Value = ob.Chequeno;
                cmd.Parameters.Add("@CHEQUEDT", SqlDbType.DateTime).Value = ob.Chequedt;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.Username;
                cmd.Parameters.Add("@ACTDTI", SqlDbType.DateTime).Value = DateTime.Parse("01/01/1900");
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = "";
                
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO GL_MASTER ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, SERIALNO, TRANSDRCR, TRANSFOR, COSTPID, TRANSMODE, DEBITCD, CREDITCD, DEBITAMT, CREDITAMT, " +
                                  " CHEQUENO, CHEQUEDT, REMARKS, TABLEID, USERPC, USERID, ACTDTI, IPADDRESS) " +
                                  " Values (@TRANSTP, @TRANSDT, @TRANSMY, @TRANSNO, @SERIALNO,'CREDIT', @TRANSFOR, @COSTPID, @TRANSMODE,@CREDITCD, @DEBITCD,0,@AMOUNT, @CHEQUENO, @CHEQUEDT, @REMARKS,'GL_STRANS', " +
                                  " @USERPC,@USERID, @ACTDTI, @IPADDRESS)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.Transtp;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.Transdt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.Monyear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                inc_Serial = ob.SerialNo_MREC;
                ser = int.Parse(inc_Serial)+1;
                final_Serial = ser.ToString();
                cmd.Parameters.Add("@SERIALNO", SqlDbType.BigInt).Value = final_Serial;
                cmd.Parameters.Add("@TRANSFOR", SqlDbType.NVarChar).Value = ob.Transfor;
                if (ob.Costpid == "&nbsp;")
                {
                    ob.Costpid = "";
                }
                else
                    ob.Costpid = ob.Costpid;
                cmd.Parameters.Add("@COSTPID", SqlDbType.NVarChar).Value = ob.Costpid;
                cmd.Parameters.Add("@TRANSMODE", SqlDbType.NVarChar).Value = ob.Transmode;
                cmd.Parameters.Add("@DEBITCD", SqlDbType.NVarChar).Value = ob.Debitcd;
                cmd.Parameters.Add("@CREDITCD", SqlDbType.NVarChar).Value = ob.Creditcd;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                if (ob.Chequeno == "&nbsp;")
                {
                    ob.Chequeno = "";
                }
                else
                    ob.Chequeno = ob.Chequeno;
                cmd.Parameters.Add("@CHEQUENO", SqlDbType.NVarChar).Value = ob.Chequeno;
                cmd.Parameters.Add("@CHEQUEDT", SqlDbType.DateTime).Value = ob.Chequedt;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.Username;
                cmd.Parameters.Add("@ACTDTI", SqlDbType.DateTime).Value = DateTime.Parse("01/01/1900");
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

        public string doProcess_MPAY(AlchemyAccounting.Accounts.Interface.SingleVoucher ob)
        {
            string s = "";
            SqlTransaction tran = null;

            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO GL_MASTER ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, SERIALNO, TRANSDRCR, TRANSFOR, COSTPID, TRANSMODE, DEBITCD, CREDITCD, DEBITAMT, CREDITAMT, " +
                                  " CHEQUENO, CHEQUEDT, REMARKS, TABLEID, USERPC, USERID, ACTDTI, IPADDRESS) " +
                                  " Values (@TRANSTP, @TRANSDT, @TRANSMY, @TRANSNO, @SERIALNO,'DEBIT', @TRANSFOR, @COSTPID, @TRANSMODE, @DEBITCD, @CREDITCD,@AMOUNT,0, @CHEQUENO, @CHEQUEDT, @REMARKS,'GL_STRANS', " +
                                  " @USERPC,@USERID, @ACTDTI, @IPADDRESS)";
                cmd.Parameters.Clear();


                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.Transtp;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.Transdt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.Monyear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                cmd.Parameters.Add("@SERIALNO", SqlDbType.BigInt).Value = ob.SerialNo_MREC;
                cmd.Parameters.Add("@TRANSFOR", SqlDbType.NVarChar).Value = ob.Transfor;
                if (ob.Costpid == "&nbsp;")
                {
                    ob.Costpid = "";
                }
                else
                    ob.Costpid = ob.Costpid;
                cmd.Parameters.Add("@COSTPID", SqlDbType.NVarChar).Value = ob.Costpid;
                cmd.Parameters.Add("@TRANSMODE", SqlDbType.NVarChar).Value = ob.Transmode;
                cmd.Parameters.Add("@DEBITCD", SqlDbType.NVarChar).Value = ob.Debitcd;
                cmd.Parameters.Add("@CREDITCD", SqlDbType.NVarChar).Value = ob.Creditcd;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                if (ob.Chequeno == "&nbsp;")
                {
                    ob.Chequeno = "";
                }
                else
                    ob.Chequeno = ob.Chequeno;
                cmd.Parameters.Add("@CHEQUENO", SqlDbType.NVarChar).Value = ob.Chequeno;
                cmd.Parameters.Add("@CHEQUEDT", SqlDbType.DateTime).Value = ob.Chequedt;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.Username;
                cmd.Parameters.Add("@ACTDTI", SqlDbType.DateTime).Value = DateTime.Parse("01/01/1900");
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = "";

                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO GL_MASTER ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, SERIALNO, TRANSDRCR, TRANSFOR, COSTPID, TRANSMODE, DEBITCD, CREDITCD, DEBITAMT, CREDITAMT, " +
                                  " CHEQUENO, CHEQUEDT, REMARKS, TABLEID, USERPC, USERID, ACTDTI, IPADDRESS) " +
                                  " Values (@TRANSTP, @TRANSDT, @TRANSMY, @TRANSNO, @SERIALNO,'CREDIT', @TRANSFOR, @COSTPID, @TRANSMODE, @CREDITCD, @DEBITCD,0,@AMOUNT, @CHEQUENO, @CHEQUEDT, @REMARKS,'GL_STRANS', " +
                                  " @USERPC,@USERID, @ACTDTI, @IPADDRESS)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.Transtp;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.Transdt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.Monyear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                inc_Serial = ob.SerialNo_MREC;
                ser = int.Parse(inc_Serial) + 1;
                final_Serial = ser.ToString();
                cmd.Parameters.Add("@SERIALNO", SqlDbType.BigInt).Value = final_Serial;
                cmd.Parameters.Add("@TRANSFOR", SqlDbType.NVarChar).Value = ob.Transfor;
                if (ob.Costpid == "&nbsp;")
                {
                    ob.Costpid = "";
                }
                else
                    ob.Costpid = ob.Costpid;
                cmd.Parameters.Add("@COSTPID", SqlDbType.NVarChar).Value = ob.Costpid;
                cmd.Parameters.Add("@TRANSMODE", SqlDbType.NVarChar).Value = ob.Transmode;
                cmd.Parameters.Add("@DEBITCD", SqlDbType.NVarChar).Value = ob.Debitcd;
                cmd.Parameters.Add("@CREDITCD", SqlDbType.NVarChar).Value = ob.Creditcd;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                if (ob.Chequeno == "&nbsp;")
                {
                    ob.Chequeno = "";
                }
                else
                    ob.Chequeno = ob.Chequeno;
                cmd.Parameters.Add("@CHEQUENO", SqlDbType.NVarChar).Value = ob.Chequeno;
                cmd.Parameters.Add("@CHEQUEDT", SqlDbType.DateTime).Value = ob.Chequedt;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.Username;
                cmd.Parameters.Add("@ACTDTI", SqlDbType.DateTime).Value = DateTime.Parse("01/01/1900");
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

        public string doProcess_JOUR(AlchemyAccounting.Accounts.Interface.SingleVoucher ob)
        {
            string s = "";
            SqlTransaction tran = null;

            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO GL_MASTER ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, SERIALNO, TRANSDRCR, TRANSFOR, COSTPID, TRANSMODE, DEBITCD, CREDITCD, DEBITAMT, CREDITAMT, " +
                                  " CHEQUENO, CHEQUEDT, REMARKS, TABLEID, USERPC, USERID, ACTDTI, IPADDRESS) " +
                                  " Values (@TRANSTP, @TRANSDT, @TRANSMY, @TRANSNO, @SERIALNO,'DEBIT', @TRANSFOR, @COSTPID, @TRANSMODE, @DEBITCD, @CREDITCD,@AMOUNT,0, @CHEQUENO, @CHEQUEDT, @REMARKS,'GL_STRANS', " +
                                  " @USERPC,@USERID, @ACTDTI, @IPADDRESS)";
                cmd.Parameters.Clear();


                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.Transtp;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.Transdt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.Monyear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                cmd.Parameters.Add("@SERIALNO", SqlDbType.BigInt).Value = ob.SerialNo_MREC;
                cmd.Parameters.Add("@TRANSFOR", SqlDbType.NVarChar).Value = ob.Transfor;
                if (ob.Costpid == "&nbsp;")
                {
                    ob.Costpid = "";
                }
                else
                    ob.Costpid = ob.Costpid;
                cmd.Parameters.Add("@COSTPID", SqlDbType.NVarChar).Value = ob.Costpid;
                cmd.Parameters.Add("@TRANSMODE", SqlDbType.NVarChar).Value = ob.Transmode;
                cmd.Parameters.Add("@DEBITCD", SqlDbType.NVarChar).Value = ob.Debitcd;
                cmd.Parameters.Add("@CREDITCD", SqlDbType.NVarChar).Value = ob.Creditcd;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                if (ob.Chequeno == "&nbsp;")
                {
                    ob.Chequeno = "";
                }
                else
                    ob.Chequeno = ob.Chequeno;
                cmd.Parameters.Add("@CHEQUENO", SqlDbType.NVarChar).Value = ob.Chequeno;
                cmd.Parameters.Add("@CHEQUEDT", SqlDbType.DateTime).Value = ob.Chequedt;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.Username;
                cmd.Parameters.Add("@ACTDTI", SqlDbType.DateTime).Value = DateTime.Parse("01/01/1900");
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = "";

                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO GL_MASTER ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, SERIALNO, TRANSDRCR, TRANSFOR, COSTPID, TRANSMODE, DEBITCD, CREDITCD, DEBITAMT, CREDITAMT, " +
                                  " CHEQUENO, CHEQUEDT, REMARKS, TABLEID, USERPC, USERID, ACTDTI, IPADDRESS) " +
                                  " Values (@TRANSTP, @TRANSDT, @TRANSMY, @TRANSNO, @SERIALNO,'CREDIT', @TRANSFOR, @COSTPID, @TRANSMODE, @CREDITCD, @DEBITCD,0,@AMOUNT, @CHEQUENO, @CHEQUEDT, @REMARKS,'GL_STRANS', " +
                                  " @USERPC,@USERID, @ACTDTI, @IPADDRESS)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.Transtp;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.Transdt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.Monyear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                inc_Serial = ob.SerialNo_MREC;
                ser = int.Parse(inc_Serial) + 1;
                final_Serial = ser.ToString();
                cmd.Parameters.Add("@SERIALNO", SqlDbType.BigInt).Value = final_Serial;
                cmd.Parameters.Add("@TRANSFOR", SqlDbType.NVarChar).Value = ob.Transfor;
                if (ob.Costpid == "&nbsp;")
                {
                    ob.Costpid = "";
                }
                else
                    ob.Costpid = ob.Costpid;
                cmd.Parameters.Add("@COSTPID", SqlDbType.NVarChar).Value = ob.Costpid;
                cmd.Parameters.Add("@TRANSMODE", SqlDbType.NVarChar).Value = ob.Transmode;
                cmd.Parameters.Add("@DEBITCD", SqlDbType.NVarChar).Value = ob.Debitcd;
                cmd.Parameters.Add("@CREDITCD", SqlDbType.NVarChar).Value = ob.Creditcd;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                if (ob.Chequeno == "&nbsp;")
                {
                    ob.Chequeno = "";
                }
                else
                    ob.Chequeno = ob.Chequeno;
                cmd.Parameters.Add("@CHEQUENO", SqlDbType.NVarChar).Value = ob.Chequeno;
                cmd.Parameters.Add("@CHEQUEDT", SqlDbType.DateTime).Value = ob.Chequedt;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.Username;
                cmd.Parameters.Add("@ACTDTI", SqlDbType.DateTime).Value = DateTime.Parse("01/01/1900");
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

        public string doProcess_CONT(AlchemyAccounting.Accounts.Interface.SingleVoucher ob)
        {
            string s = "";
            SqlTransaction tran = null;

            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO GL_MASTER ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, SERIALNO, TRANSDRCR, TRANSFOR, COSTPID, TRANSMODE, DEBITCD, CREDITCD, DEBITAMT, CREDITAMT, " +
                                  " CHEQUENO, CHEQUEDT, REMARKS, TABLEID, USERPC, USERID, ACTDTI, IPADDRESS) " +
                                  " Values (@TRANSTP, @TRANSDT, @TRANSMY, @TRANSNO, @SERIALNO,'DEBIT', @TRANSFOR, @COSTPID, @TRANSMODE, @DEBITCD, @CREDITCD,@AMOUNT,0, @CHEQUENO, @CHEQUEDT, @REMARKS,'GL_STRANS', " +
                                  " @USERPC,@USERID, @ACTDTI, @IPADDRESS)";
                cmd.Parameters.Clear();


                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.Transtp;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.Transdt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.Monyear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                cmd.Parameters.Add("@SERIALNO", SqlDbType.BigInt).Value = ob.SerialNo_MREC;
                cmd.Parameters.Add("@TRANSFOR", SqlDbType.NVarChar).Value = ob.Transfor;
                if (ob.Costpid == "&nbsp;")
                {
                    ob.Costpid = "";
                }
                else
                    ob.Costpid = ob.Costpid;
                cmd.Parameters.Add("@COSTPID", SqlDbType.NVarChar).Value = ob.Costpid;
                cmd.Parameters.Add("@TRANSMODE", SqlDbType.NVarChar).Value = ob.Transmode;
                cmd.Parameters.Add("@DEBITCD", SqlDbType.NVarChar).Value = ob.Debitcd;
                cmd.Parameters.Add("@CREDITCD", SqlDbType.NVarChar).Value = ob.Creditcd;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                if (ob.Chequeno == "&nbsp;")
                {
                    ob.Chequeno = "";
                }
                else
                    ob.Chequeno = ob.Chequeno;
                cmd.Parameters.Add("@CHEQUENO", SqlDbType.NVarChar).Value = ob.Chequeno;
                cmd.Parameters.Add("@CHEQUEDT", SqlDbType.DateTime).Value = ob.Chequedt;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.Username;
                cmd.Parameters.Add("@ACTDTI", SqlDbType.DateTime).Value = DateTime.Parse("01/01/1900");
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = "";

                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO GL_MASTER ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, SERIALNO, TRANSDRCR, TRANSFOR, COSTPID, TRANSMODE, DEBITCD, CREDITCD, DEBITAMT, CREDITAMT, " +
                                  " CHEQUENO, CHEQUEDT, REMARKS, TABLEID, USERPC, USERID, ACTDTI, IPADDRESS) " +
                                  " Values (@TRANSTP, @TRANSDT, @TRANSMY, @TRANSNO, @SERIALNO,'CREDIT', @TRANSFOR, @COSTPID, @TRANSMODE, @CREDITCD, @DEBITCD,0,@AMOUNT, @CHEQUENO, @CHEQUEDT, @REMARKS,'GL_STRANS', " +
                                  " @USERPC,@USERID, @ACTDTI, @IPADDRESS)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.Transtp;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.Transdt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.Monyear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                inc_Serial = ob.SerialNo_MREC;
                ser = int.Parse(inc_Serial) + 1;
                final_Serial = ser.ToString();
                cmd.Parameters.Add("@SERIALNO", SqlDbType.BigInt).Value = final_Serial;
                cmd.Parameters.Add("@TRANSFOR", SqlDbType.NVarChar).Value = ob.Transfor;
                if (ob.Costpid == "&nbsp;")
                {
                    ob.Costpid = "";
                }
                else
                    ob.Costpid = ob.Costpid;
                cmd.Parameters.Add("@COSTPID", SqlDbType.NVarChar).Value = ob.Costpid;
                cmd.Parameters.Add("@TRANSMODE", SqlDbType.NVarChar).Value = ob.Transmode;
                cmd.Parameters.Add("@DEBITCD", SqlDbType.NVarChar).Value = ob.Debitcd;
                cmd.Parameters.Add("@CREDITCD", SqlDbType.NVarChar).Value = ob.Creditcd;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                if (ob.Chequeno == "&nbsp;")
                {
                    ob.Chequeno = "";
                }
                else
                    ob.Chequeno = ob.Chequeno;
                cmd.Parameters.Add("@CHEQUENO", SqlDbType.NVarChar).Value = ob.Chequeno;
                cmd.Parameters.Add("@CHEQUEDT", SqlDbType.DateTime).Value = ob.Chequedt;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.Username;
                cmd.Parameters.Add("@ACTDTI", SqlDbType.DateTime).Value = DateTime.Parse("01/01/1900");
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
        public DataTable rptCreditVoucher(string Transtype, DateTime TransDate, int VoucherNo,string Mode)
        {
            DataTable table = new DataTable();
            string msg = "";
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Exec sp_rptCreditVoucher @TransType,@TransDate,@VouchNo,@Mode";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TransType", SqlDbType.NVarChar).Value = Transtype;
                cmd.Parameters.Add("@TransDate", SqlDbType.DateTime).Value = TransDate;
                cmd.Parameters.Add("@VouchNo", SqlDbType.Int).Value = VoucherNo;
                cmd.Parameters.Add("@Mode", SqlDbType.NVarChar).Value = Mode;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch (Exception ob1)
            {
                msg = ob1.Message;
            }
            return table;

        }
        public DataTable CashBook(string DebitCD, DateTime From, DateTime To, string FilteredHead)
        {
            DataTable table = new DataTable();
            string msg = "";
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Exec sp_rptCashBook @DebitCD,@From,@To,@FilteredHead";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@DebitCD", SqlDbType.NVarChar).Value = DebitCD;
                cmd.Parameters.Add("@From", SqlDbType.DateTime).Value = From;
                cmd.Parameters.Add("@To", SqlDbType.DateTime).Value = To;
                cmd.Parameters.Add("@FilteredHead", SqlDbType.NVarChar).Value = FilteredHead;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch (Exception ob1)
            {
                msg = ob1.Message;
            }
            return table;

        }

        public DataTable BankBook(string DebitCD, DateTime From, DateTime To, string FilteredHead)
        {
            DataTable table = new DataTable();
            string msg = "";
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Exec sp_rptBankBook @DebitCD,@From,@To,@FilteredHead";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@DebitCD", SqlDbType.NVarChar).Value = DebitCD;
                cmd.Parameters.Add("@From", SqlDbType.DateTime).Value = From;
                cmd.Parameters.Add("@To", SqlDbType.DateTime).Value = To;
                cmd.Parameters.Add("@FilteredHead", SqlDbType.NVarChar).Value = FilteredHead;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch (Exception ob1)
            {
                msg = ob1.Message;
            }
            return table;

        }

        public DataTable LedgerBook(string debitCD, DateTime From, DateTime To, string searchHead)
        {
            DataTable table = new DataTable();
            string msg = "";
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Exec sp_rptLedgerBook @debitCD,@From,@To,@FilteredHead";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@debitCD", SqlDbType.NVarChar).Value = debitCD;
                cmd.Parameters.Add("@From", SqlDbType.DateTime).Value = From;
                cmd.Parameters.Add("@To", SqlDbType.DateTime).Value = To;
                cmd.Parameters.Add("@FilteredHead", SqlDbType.NVarChar).Value = searchHead;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch (Exception ob1)
            {
                msg = ob1.Message;
            }
            return table;

        }
        public DataTable ChartAcc()
        {
            DataTable table = new DataTable();
            string msg = "";
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Exec sp_rptChartAccount";
                cmd.Parameters.Clear();
                
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch (Exception ob1)
            {
                msg = ob1.Message;
            }
            return table;

        }

        public string doProcess_BUY(AlchemyAccounting.Accounts.Interface.SingleVoucher ob)
        {
            string s = "";
            SqlTransaction tran = null;


            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO GL_MASTER ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, SERIALNO, TRANSDRCR, TRANSFOR, COSTPID, TRANSMODE, DEBITCD, CREDITCD, DEBITAMT, CREDITAMT, " +
                                  " REMARKS, TABLEID, USERPC, USERID, ACTDTI, IPADDRESS) " +
                                  " Values (@TRANSTP, @TRANSDT, @TRANSMY, @TRANSNO, @SERIALNO,'DEBIT', '', '', '', @DEBITCD, @CREDITCD,@AMOUNT,0, @REMARKS,'STK_TRANS', " +
                                  " @USERPC,@USERID, @ACTDTI, @IPADDRESS)";
                cmd.Parameters.Clear();

                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.Transtp;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.Transdt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.Monyear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                cmd.Parameters.Add("@SERIALNO", SqlDbType.BigInt).Value = ob.Serial_BUY;
                //cmd.Parameters.Add("@TRANSFOR", SqlDbType.NVarChar).Value = ob.Transfor;

                // ob.Costpid = "";

                //cmd.Parameters.Add("@COSTPID", SqlDbType.NVarChar).Value = ob.Costpid;
                //cmd.Parameters.Add("@TRANSMODE", SqlDbType.NVarChar).Value = ob.Transmode;
                cmd.Parameters.Add("@DEBITCD", SqlDbType.NVarChar).Value = ob.Debitcd;
                cmd.Parameters.Add("@CREDITCD", SqlDbType.NVarChar).Value = ob.Creditcd;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                //if (ob.Chequeno == "&nbsp;")
                //{
                //    ob.Chequeno = "";
                //}
                //else
                //    ob.Chequeno = ob.Chequeno;
                //cmd.Parameters.Add("@CHEQUENO", SqlDbType.NVarChar).Value = ob.Chequeno;
                //cmd.Parameters.Add("@CHEQUEDT", SqlDbType.DateTime).Value = DateTime.Parse("01/01/1900");
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.Username;
                cmd.Parameters.Add("@ACTDTI", SqlDbType.DateTime).Value = DateTime.Parse("01/01/1900");
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = "";

                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO GL_MASTER ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, SERIALNO, TRANSDRCR, TRANSFOR, COSTPID, TRANSMODE, DEBITCD, CREDITCD, DEBITAMT, CREDITAMT, " +
                                  " REMARKS, TABLEID, USERPC, USERID, ACTDTI, IPADDRESS) " +
                                  " Values (@TRANSTP, @TRANSDT, @TRANSMY, @TRANSNO, @SERIALNO,'CREDIT', '', '', '',@CREDITCD, @DEBITCD,0,@AMOUNT, @REMARKS,'STK_TRANS', " +
                                  " @USERPC,@USERID, @ACTDTI, @IPADDRESS)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.Transtp;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.Transdt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.Monyear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                inc_Serial = ob.Serial_BUY;
                ser = int.Parse(inc_Serial) + 1;
                final_Serial = ser.ToString();
                cmd.Parameters.Add("@SERIALNO", SqlDbType.BigInt).Value = final_Serial;
                //cmd.Parameters.Add("@TRANSFOR", SqlDbType.NVarChar).Value = ob.Transfor;
                //if (ob.Costpid == "&nbsp;")
                //{
                //    ob.Costpid = "";
                //}
                //else
                //    ob.Costpid = ob.Costpid;
                //cmd.Parameters.Add("@COSTPID", SqlDbType.NVarChar).Value = ob.Costpid;
                //cmd.Parameters.Add("@TRANSMODE", SqlDbType.NVarChar).Value = ob.Transmode;
                cmd.Parameters.Add("@DEBITCD", SqlDbType.NVarChar).Value = ob.Debitcd;
                cmd.Parameters.Add("@CREDITCD", SqlDbType.NVarChar).Value = ob.Creditcd;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                //if (ob.Chequeno == "&nbsp;")
                //{
                //    ob.Chequeno = "";
                //}
                //else
                //    ob.Chequeno = ob.Chequeno;
                //cmd.Parameters.Add("@CHEQUENO", SqlDbType.NVarChar).Value = ob.Chequeno;
                //cmd.Parameters.Add("@CHEQUEDT", SqlDbType.DateTime).Value = DateTime.Parse("01/01/1900");
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.Username;
                cmd.Parameters.Add("@ACTDTI", SqlDbType.DateTime).Value = DateTime.Parse("01/01/1900");
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

        public string doProcess_BUY_Ret(AlchemyAccounting.Accounts.Interface.SingleVoucher ob)
        {
            string s = "";
            SqlTransaction tran = null;


            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO GL_MASTER ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, SERIALNO, TRANSDRCR, TRANSFOR, COSTPID, TRANSMODE, DEBITCD, CREDITCD, DEBITAMT, CREDITAMT, " +
                                  " REMARKS, TABLEID, USERPC, USERID, ACTDTI, IPADDRESS) " +
                                  " Values (@TRANSTP, @TRANSDT, @TRANSMY, @TRANSNO, @SERIALNO,'DEBIT', '', '', '', @DEBITCD, @CREDITCD,@AMOUNT,0, @REMARKS,'STK_TRANS', " +
                                  " @USERPC,@USERID, @ACTDTI, @IPADDRESS)";
                cmd.Parameters.Clear();

                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.Transtp;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.Transdt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.Monyear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                cmd.Parameters.Add("@SERIALNO", SqlDbType.BigInt).Value = ob.Serial_BUY;
                //cmd.Parameters.Add("@TRANSFOR", SqlDbType.NVarChar).Value = ob.Transfor;

                // ob.Costpid = "";

                //cmd.Parameters.Add("@COSTPID", SqlDbType.NVarChar).Value = ob.Costpid;
                //cmd.Parameters.Add("@TRANSMODE", SqlDbType.NVarChar).Value = ob.Transmode;
                cmd.Parameters.Add("@DEBITCD", SqlDbType.NVarChar).Value = ob.Debitcd;
                cmd.Parameters.Add("@CREDITCD", SqlDbType.NVarChar).Value = ob.Creditcd;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                //if (ob.Chequeno == "&nbsp;")
                //{
                //    ob.Chequeno = "";
                //}
                //else
                //    ob.Chequeno = ob.Chequeno;
                //cmd.Parameters.Add("@CHEQUENO", SqlDbType.NVarChar).Value = ob.Chequeno;
                //cmd.Parameters.Add("@CHEQUEDT", SqlDbType.DateTime).Value = DateTime.Parse("01/01/1900");
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.Username;
                cmd.Parameters.Add("@ACTDTI", SqlDbType.DateTime).Value = DateTime.Parse("01/01/1900");
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = "";

                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO GL_MASTER ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, SERIALNO, TRANSDRCR, TRANSFOR, COSTPID, TRANSMODE, DEBITCD, CREDITCD, DEBITAMT, CREDITAMT, " +
                                  " REMARKS, TABLEID, USERPC, USERID, ACTDTI, IPADDRESS) " +
                                  " Values (@TRANSTP, @TRANSDT, @TRANSMY, @TRANSNO, @SERIALNO,'CREDIT', '', '', '',@CREDITCD, @DEBITCD,0,@AMOUNT, @REMARKS,'STK_TRANS', " +
                                  " @USERPC,@USERID, @ACTDTI, @IPADDRESS)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.Transtp;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.Transdt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.Monyear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                inc_Serial = ob.Serial_BUY;
                ser = int.Parse(inc_Serial) + 1;
                final_Serial = ser.ToString();
                cmd.Parameters.Add("@SERIALNO", SqlDbType.BigInt).Value = final_Serial;
                //cmd.Parameters.Add("@TRANSFOR", SqlDbType.NVarChar).Value = ob.Transfor;
                //if (ob.Costpid == "&nbsp;")
                //{
                //    ob.Costpid = "";
                //}
                //else
                //    ob.Costpid = ob.Costpid;
                //cmd.Parameters.Add("@COSTPID", SqlDbType.NVarChar).Value = ob.Costpid;
                //cmd.Parameters.Add("@TRANSMODE", SqlDbType.NVarChar).Value = ob.Transmode;
                cmd.Parameters.Add("@DEBITCD", SqlDbType.NVarChar).Value = ob.Debitcd;
                cmd.Parameters.Add("@CREDITCD", SqlDbType.NVarChar).Value = ob.Creditcd;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                //if (ob.Chequeno == "&nbsp;")
                //{
                //    ob.Chequeno = "";
                //}
                //else
                //    ob.Chequeno = ob.Chequeno;
                //cmd.Parameters.Add("@CHEQUENO", SqlDbType.NVarChar).Value = ob.Chequeno;
                //cmd.Parameters.Add("@CHEQUEDT", SqlDbType.DateTime).Value = DateTime.Parse("01/01/1900");
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.Username;
                cmd.Parameters.Add("@ACTDTI", SqlDbType.DateTime).Value = DateTime.Parse("01/01/1900");
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

        public string doProcess_SALE(AlchemyAccounting.Accounts.Interface.SingleVoucher ob)
        {
            string s = "";
            SqlTransaction tran = null;


            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO GL_MASTER ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, SERIALNO, TRANSDRCR, TRANSFOR, COSTPID, TRANSMODE, DEBITCD, CREDITCD, DEBITAMT, CREDITAMT, " +
                                  " REMARKS, TABLEID, USERPC, USERID, ACTDTI, IPADDRESS) " +
                                  " Values (@TRANSTP, @TRANSDT, @TRANSMY, @TRANSNO, @SERIALNO,'DEBIT', '', '', '', @DEBITCD, @CREDITCD,@AMOUNT,0, @REMARKS,'STK_TRANS', " +
                                  " @USERPC,@USERID, @ACTDTI, @IPADDRESS)";
                cmd.Parameters.Clear();

                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.Transtp;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.Transdt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.Monyear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                cmd.Parameters.Add("@SERIALNO", SqlDbType.BigInt).Value = ob.Serial_SALE;
                //cmd.Parameters.Add("@TRANSFOR", SqlDbType.NVarChar).Value = ob.Transfor;

                // ob.Costpid = "";

                //cmd.Parameters.Add("@COSTPID", SqlDbType.NVarChar).Value = ob.Costpid;
                //cmd.Parameters.Add("@TRANSMODE", SqlDbType.NVarChar).Value = ob.Transmode;
                cmd.Parameters.Add("@DEBITCD", SqlDbType.NVarChar).Value = ob.Debitcd;
                cmd.Parameters.Add("@CREDITCD", SqlDbType.NVarChar).Value = ob.Creditcd;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                //if (ob.Chequeno == "&nbsp;")
                //{
                //    ob.Chequeno = "";
                //}
                //else
                //    ob.Chequeno = ob.Chequeno;
                //cmd.Parameters.Add("@CHEQUENO", SqlDbType.NVarChar).Value = ob.Chequeno;
                //cmd.Parameters.Add("@CHEQUEDT", SqlDbType.DateTime).Value = DateTime.Parse("01/01/1900");
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.Username;
                cmd.Parameters.Add("@ACTDTI", SqlDbType.DateTime).Value = DateTime.Parse("01/01/1900");
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = "";

                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO GL_MASTER ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, SERIALNO, TRANSDRCR, TRANSFOR, COSTPID, TRANSMODE, DEBITCD, CREDITCD, DEBITAMT, CREDITAMT, " +
                                  " REMARKS, TABLEID, USERPC, USERID, ACTDTI, IPADDRESS) " +
                                  " Values (@TRANSTP, @TRANSDT, @TRANSMY, @TRANSNO, @SERIALNO,'CREDIT', '', '', '',@CREDITCD, @DEBITCD,0,@AMOUNT, @REMARKS,'STK_TRANS', " +
                                  " @USERPC,@USERID, @ACTDTI, @IPADDRESS)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.Transtp;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.Transdt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.Monyear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                inc_Serial = ob.Serial_SALE;
                ser = int.Parse(inc_Serial) + 1;
                final_Serial = ser.ToString();
                cmd.Parameters.Add("@SERIALNO", SqlDbType.BigInt).Value = final_Serial;
                //cmd.Parameters.Add("@TRANSFOR", SqlDbType.NVarChar).Value = ob.Transfor;
                //if (ob.Costpid == "&nbsp;")
                //{
                //    ob.Costpid = "";
                //}
                //else
                //    ob.Costpid = ob.Costpid;
                //cmd.Parameters.Add("@COSTPID", SqlDbType.NVarChar).Value = ob.Costpid;
                //cmd.Parameters.Add("@TRANSMODE", SqlDbType.NVarChar).Value = ob.Transmode;
                cmd.Parameters.Add("@DEBITCD", SqlDbType.NVarChar).Value = ob.Debitcd;
                cmd.Parameters.Add("@CREDITCD", SqlDbType.NVarChar).Value = ob.Creditcd;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                //if (ob.Chequeno == "&nbsp;")
                //{
                //    ob.Chequeno = "";
                //}
                //else
                //    ob.Chequeno = ob.Chequeno;
                //cmd.Parameters.Add("@CHEQUENO", SqlDbType.NVarChar).Value = ob.Chequeno;
                //cmd.Parameters.Add("@CHEQUEDT", SqlDbType.DateTime).Value = DateTime.Parse("01/01/1900");
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.Username;
                cmd.Parameters.Add("@ACTDTI", SqlDbType.DateTime).Value = DateTime.Parse("01/01/1900");
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

        public string doProcess_SALE_Ret(AlchemyAccounting.Accounts.Interface.SingleVoucher ob)
        {
            string s = "";
            SqlTransaction tran = null;


            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO GL_MASTER ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, SERIALNO, TRANSDRCR, TRANSFOR, COSTPID, TRANSMODE, DEBITCD, CREDITCD, DEBITAMT, CREDITAMT, " +
                                  " REMARKS, TABLEID, USERPC, USERID, ACTDTI, IPADDRESS) " +
                                  " Values (@TRANSTP, @TRANSDT, @TRANSMY, @TRANSNO, @SERIALNO,'DEBIT', '', '', '', @DEBITCD, @CREDITCD,@AMOUNT,0, @REMARKS,'STK_TRANS', " +
                                  " @USERPC,@USERID, @ACTDTI, @IPADDRESS)";
                cmd.Parameters.Clear();

                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.Transtp;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.Transdt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.Monyear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                cmd.Parameters.Add("@SERIALNO", SqlDbType.BigInt).Value = ob.Serial_SALE;
                //cmd.Parameters.Add("@TRANSFOR", SqlDbType.NVarChar).Value = ob.Transfor;

                // ob.Costpid = "";

                //cmd.Parameters.Add("@COSTPID", SqlDbType.NVarChar).Value = ob.Costpid;
                //cmd.Parameters.Add("@TRANSMODE", SqlDbType.NVarChar).Value = ob.Transmode;
                cmd.Parameters.Add("@DEBITCD", SqlDbType.NVarChar).Value = ob.Debitcd;
                cmd.Parameters.Add("@CREDITCD", SqlDbType.NVarChar).Value = ob.Creditcd;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                //if (ob.Chequeno == "&nbsp;")
                //{
                //    ob.Chequeno = "";
                //}
                //else
                //    ob.Chequeno = ob.Chequeno;
                //cmd.Parameters.Add("@CHEQUENO", SqlDbType.NVarChar).Value = ob.Chequeno;
                //cmd.Parameters.Add("@CHEQUEDT", SqlDbType.DateTime).Value = DateTime.Parse("01/01/1900");
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.Username;
                cmd.Parameters.Add("@ACTDTI", SqlDbType.DateTime).Value = DateTime.Parse("01/01/1900");
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = "";

                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO GL_MASTER ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, SERIALNO, TRANSDRCR, TRANSFOR, COSTPID, TRANSMODE, DEBITCD, CREDITCD, DEBITAMT, CREDITAMT, " +
                                  " REMARKS, TABLEID, USERPC, USERID, ACTDTI, IPADDRESS) " +
                                  " Values (@TRANSTP, @TRANSDT, @TRANSMY, @TRANSNO, @SERIALNO,'CREDIT', '', '', '',@CREDITCD, @DEBITCD,0,@AMOUNT, @REMARKS,'STK_TRANS', " +
                                  " @USERPC,@USERID, @ACTDTI, @IPADDRESS)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.Transtp;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.Transdt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.Monyear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                inc_Serial = ob.Serial_SALE;
                ser = int.Parse(inc_Serial) + 1;
                final_Serial = ser.ToString();
                cmd.Parameters.Add("@SERIALNO", SqlDbType.BigInt).Value = final_Serial;
                //cmd.Parameters.Add("@TRANSFOR", SqlDbType.NVarChar).Value = ob.Transfor;
                //if (ob.Costpid == "&nbsp;")
                //{
                //    ob.Costpid = "";
                //}
                //else
                //    ob.Costpid = ob.Costpid;
                //cmd.Parameters.Add("@COSTPID", SqlDbType.NVarChar).Value = ob.Costpid;
                //cmd.Parameters.Add("@TRANSMODE", SqlDbType.NVarChar).Value = ob.Transmode;
                cmd.Parameters.Add("@DEBITCD", SqlDbType.NVarChar).Value = ob.Debitcd;
                cmd.Parameters.Add("@CREDITCD", SqlDbType.NVarChar).Value = ob.Creditcd;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                //if (ob.Chequeno == "&nbsp;")
                //{
                //    ob.Chequeno = "";
                //}
                //else
                //    ob.Chequeno = ob.Chequeno;
                //cmd.Parameters.Add("@CHEQUENO", SqlDbType.NVarChar).Value = ob.Chequeno;
                //cmd.Parameters.Add("@CHEQUEDT", SqlDbType.DateTime).Value = DateTime.Parse("01/01/1900");
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.Username;
                cmd.Parameters.Add("@ACTDTI", SqlDbType.DateTime).Value = DateTime.Parse("01/01/1900");
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

        public string doProcess_SALE_DisCount(AlchemyAccounting.Accounts.Interface.SingleVoucher ob)
        {
            string s = "";
            SqlTransaction tran = null;


            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO GL_MASTER ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, SERIALNO, TRANSDRCR, TRANSFOR, COSTPID, TRANSMODE, DEBITCD, CREDITCD, DEBITAMT, CREDITAMT, " +
                                  " REMARKS, TABLEID, USERPC, USERID, ACTDTI, IPADDRESS) " +
                                  " Values (@TRANSTP, @TRANSDT, @TRANSMY, @TRANSNO, @SERIALNO,'DEBIT', '', '', '', @DEBITCD, @CREDITCD,@AMOUNT,0, @REMARKS,'STK_TRANS', " +
                                  " @USERPC,@USERID, @ACTDTI, @IPADDRESS)";
                cmd.Parameters.Clear();

                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.Transtp;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.Transdt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.Monyear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                cmd.Parameters.Add("@SERIALNO", SqlDbType.BigInt).Value = ob.Sl_Sale_dis;
                //cmd.Parameters.Add("@TRANSFOR", SqlDbType.NVarChar).Value = ob.Transfor;

                // ob.Costpid = "";

                //cmd.Parameters.Add("@COSTPID", SqlDbType.NVarChar).Value = ob.Costpid;
                //cmd.Parameters.Add("@TRANSMODE", SqlDbType.NVarChar).Value = ob.Transmode;
                cmd.Parameters.Add("@DEBITCD", SqlDbType.NVarChar).Value = ob.Debitcd;
                cmd.Parameters.Add("@CREDITCD", SqlDbType.NVarChar).Value = ob.Creditcd;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                //if (ob.Chequeno == "&nbsp;")
                //{
                //    ob.Chequeno = "";
                //}
                //else
                //    ob.Chequeno = ob.Chequeno;
                //cmd.Parameters.Add("@CHEQUENO", SqlDbType.NVarChar).Value = ob.Chequeno;
                //cmd.Parameters.Add("@CHEQUEDT", SqlDbType.DateTime).Value = DateTime.Parse("01/01/1900");
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.Username;
                cmd.Parameters.Add("@ACTDTI", SqlDbType.DateTime).Value = DateTime.Parse("01/01/1900");
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = "";

                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO GL_MASTER ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, SERIALNO, TRANSDRCR, TRANSFOR, COSTPID, TRANSMODE, DEBITCD, CREDITCD, DEBITAMT, CREDITAMT, " +
                                  " REMARKS, TABLEID, USERPC, USERID, ACTDTI, IPADDRESS) " +
                                  " Values (@TRANSTP, @TRANSDT, @TRANSMY, @TRANSNO, @SERIALNO,'CREDIT', '', '', '',@CREDITCD, @DEBITCD,0,@AMOUNT, @REMARKS,'STK_TRANS', " +
                                  " @USERPC,@USERID, @ACTDTI, @IPADDRESS)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.Transtp;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.Transdt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.Monyear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                inc_Serial = ob.Sl_Sale_dis;
                ser = int.Parse(inc_Serial) + 1;
                final_Serial = ser.ToString();
                cmd.Parameters.Add("@SERIALNO", SqlDbType.BigInt).Value = final_Serial;
                //cmd.Parameters.Add("@TRANSFOR", SqlDbType.NVarChar).Value = ob.Transfor;
                //if (ob.Costpid == "&nbsp;")
                //{
                //    ob.Costpid = "";
                //}
                //else
                //    ob.Costpid = ob.Costpid;
                //cmd.Parameters.Add("@COSTPID", SqlDbType.NVarChar).Value = ob.Costpid;
                //cmd.Parameters.Add("@TRANSMODE", SqlDbType.NVarChar).Value = ob.Transmode;
                cmd.Parameters.Add("@DEBITCD", SqlDbType.NVarChar).Value = ob.Debitcd;
                cmd.Parameters.Add("@CREDITCD", SqlDbType.NVarChar).Value = ob.Creditcd;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                //if (ob.Chequeno == "&nbsp;")
                //{
                //    ob.Chequeno = "";
                //}
                //else
                //    ob.Chequeno = ob.Chequeno;
                //cmd.Parameters.Add("@CHEQUENO", SqlDbType.NVarChar).Value = ob.Chequeno;
                //cmd.Parameters.Add("@CHEQUEDT", SqlDbType.DateTime).Value = DateTime.Parse("01/01/1900");
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.Username;
                cmd.Parameters.Add("@ACTDTI", SqlDbType.DateTime).Value = DateTime.Parse("01/01/1900");
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

        public string doProcess_LC(AlchemyAccounting.Accounts.Interface.SingleVoucher ob)
        {
            string s = "";
            SqlTransaction tran = null;

            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO GL_MASTER ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, SERIALNO, TRANSDRCR, TRANSFOR, COSTPID, TRANSMODE, DEBITCD, CREDITCD, DEBITAMT, CREDITAMT, " +
                                  " CHEQUENO, CHEQUEDT, REMARKS, TABLEID, USERPC, USERID, ACTDTI, IPADDRESS) " +
                                  " Values (@TRANSTP, @TRANSDT, @TRANSMY, @TRANSNO, @SERIALNO,'DEBIT', '', '', '', @DEBITCD, @CREDITCD,@AMOUNT,0, @CHEQUENO,@CHEQUEDT, @REMARKS,'LC_EXPENSE', " +
                                  " @USERPC,@USERID, @ACTDTI, @IPADDRESS)";
                cmd.Parameters.Clear();


                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.Transtp;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.Transdt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.Monyear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                cmd.Parameters.Add("@SERIALNO", SqlDbType.BigInt).Value = ob.SerialNo_MREC;
                //cmd.Parameters.Add("@TRANSFOR", SqlDbType.NVarChar).Value = ob.Transfor;
                //if (ob.Costpid == "&nbsp;")
                //{
                //    ob.Costpid = "";
                //}
                //else
                //    ob.Costpid = ob.Costpid;
                //cmd.Parameters.Add("@COSTPID", SqlDbType.NVarChar).Value = ob.Costpid;
                //cmd.Parameters.Add("@TRANSMODE", SqlDbType.NVarChar).Value = ob.Transmode;
                cmd.Parameters.Add("@DEBITCD", SqlDbType.NVarChar).Value = ob.Debitcd;
                cmd.Parameters.Add("@CREDITCD", SqlDbType.NVarChar).Value = ob.Creditcd;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                //if (ob.Chequeno == "&nbsp;")
                //{
                    ob.Chequeno = "";
                //}
                //else
                //    ob.Chequeno = ob.Chequeno;
                cmd.Parameters.Add("@CHEQUENO", SqlDbType.NVarChar).Value = ob.Chequeno;
                cmd.Parameters.Add("@CHEQUEDT", SqlDbType.DateTime).Value = DateTime.Parse("01/01/1900");
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.Userpc;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.Username;
                cmd.Parameters.Add("@ACTDTI", SqlDbType.DateTime).Value = DateTime.Parse("01/01/1900");
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;

                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO GL_MASTER ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, SERIALNO, TRANSDRCR, TRANSFOR, COSTPID, TRANSMODE, DEBITCD, CREDITCD, DEBITAMT, CREDITAMT, " +
                                  " CHEQUENO, CHEQUEDT, REMARKS, TABLEID, USERPC, USERID, ACTDTI, IPADDRESS) " +
                                  " Values (@TRANSTP, @TRANSDT, @TRANSMY, @TRANSNO, @SERIALNO,'CREDIT', '', '', '', @CREDITCD, @DEBITCD,0, @AMOUNT, @CHEQUENO,@CHEQUEDT, @REMARKS,'LC_EXPENSE', " +
                                  " @USERPC,@USERID, @ACTDTI, @IPADDRESS)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.Transtp;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.Transdt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.Monyear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                inc_Serial = ob.SerialNo_MREC;
                ser = int.Parse(inc_Serial) + 1;
                final_Serial = ser.ToString();
                cmd.Parameters.Add("@SERIALNO", SqlDbType.BigInt).Value = final_Serial;
                //cmd.Parameters.Add("@TRANSFOR", SqlDbType.NVarChar).Value = ob.Transfor;
                //if (ob.Costpid == "&nbsp;")
                //{
                //    ob.Costpid = "";
                //}
                //else
                //    ob.Costpid = ob.Costpid;
                //cmd.Parameters.Add("@COSTPID", SqlDbType.NVarChar).Value = ob.Costpid;
                //cmd.Parameters.Add("@TRANSMODE", SqlDbType.NVarChar).Value = ob.Transmode;
                cmd.Parameters.Add("@DEBITCD", SqlDbType.NVarChar).Value = ob.Debitcd;
                cmd.Parameters.Add("@CREDITCD", SqlDbType.NVarChar).Value = ob.Creditcd;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                //if (ob.Chequeno == "&nbsp;")
                //{
                    ob.Chequeno = "";
                //}
                //else
                //    ob.Chequeno = ob.Chequeno;
                cmd.Parameters.Add("@CHEQUENO", SqlDbType.NVarChar).Value = ob.Chequeno;
                cmd.Parameters.Add("@CHEQUEDT", SqlDbType.DateTime).Value = DateTime.Parse("01/01/1900");
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.Userpc;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.Username;
                cmd.Parameters.Add("@ACTDTI", SqlDbType.DateTime).Value = DateTime.Parse("01/01/1900");
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

        public string doProcess_MREC_Multiple(AlchemyAccounting.Accounts.Interface.SingleVoucher ob)
        {
            string s = "";
            SqlTransaction tran = null;


            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();




                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO GL_MASTER ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, SERIALNO, TRANSDRCR, TRANSFOR, COSTPID, TRANSMODE, DEBITCD, CREDITCD, DEBITAMT, CREDITAMT, " +
                                  " CHEQUENO, CHEQUEDT, REMARKS, TABLEID, USERPC, USERID, ACTDTI, IPADDRESS) " +
                                  " Values (@TRANSTP, @TRANSDT, @TRANSMY, @TRANSNO, @SERIALNO,'DEBIT', @TRANSFOR, @COSTPID, @TRANSMODE, @DEBITCD, @CREDITCD,@AMOUNT,0, @CHEQUENO, @CHEQUEDT, @REMARKS,'GL_MTRANS', " +
                                  " @USERPC,@USERID, @ACTDTI, @IPADDRESS)";
                cmd.Parameters.Clear();

                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.Transtp;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.Transdt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.Monyear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                cmd.Parameters.Add("@SERIALNO", SqlDbType.BigInt).Value = ob.SerialNo_MREC;
                cmd.Parameters.Add("@TRANSFOR", SqlDbType.NVarChar).Value = ob.Transfor;
                if (ob.Costpid == "&nbsp;")
                {
                    ob.Costpid = "";
                }
                else
                    ob.Costpid = ob.Costpid;
                cmd.Parameters.Add("@COSTPID", SqlDbType.NVarChar).Value = ob.Costpid;
                cmd.Parameters.Add("@TRANSMODE", SqlDbType.NVarChar).Value = ob.Transmode;
                cmd.Parameters.Add("@DEBITCD", SqlDbType.NVarChar).Value = ob.Debitcd;
                cmd.Parameters.Add("@CREDITCD", SqlDbType.NVarChar).Value = ob.Creditcd;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                if (ob.Chequeno == "&nbsp;")
                {
                    ob.Chequeno = "";
                }
                else
                    ob.Chequeno = ob.Chequeno;
                cmd.Parameters.Add("@CHEQUENO", SqlDbType.NVarChar).Value = ob.Chequeno;
                cmd.Parameters.Add("@CHEQUEDT", SqlDbType.DateTime).Value = ob.Chequedt;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.Username;
                cmd.Parameters.Add("@ACTDTI", SqlDbType.DateTime).Value = DateTime.Parse("01/01/1900");
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = "";

                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO GL_MASTER ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, SERIALNO, TRANSDRCR, TRANSFOR, COSTPID, TRANSMODE, DEBITCD, CREDITCD, DEBITAMT, CREDITAMT, " +
                                  " CHEQUENO, CHEQUEDT, REMARKS, TABLEID, USERPC, USERID, ACTDTI, IPADDRESS) " +
                                  " Values (@TRANSTP, @TRANSDT, @TRANSMY, @TRANSNO, @SERIALNO,'CREDIT', @TRANSFOR, @COSTPID, @TRANSMODE,@CREDITCD, @DEBITCD,0,@AMOUNT, @CHEQUENO, @CHEQUEDT, @REMARKS,'GL_MTRANS', " +
                                  " @USERPC,@USERID, @ACTDTI, @IPADDRESS)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.Transtp;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.Transdt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.Monyear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                inc_Serial = ob.SerialNo_MREC;
                ser = int.Parse(inc_Serial) + 1;
                final_Serial = ser.ToString();
                cmd.Parameters.Add("@SERIALNO", SqlDbType.BigInt).Value = final_Serial;
                cmd.Parameters.Add("@TRANSFOR", SqlDbType.NVarChar).Value = ob.Transfor;
                if (ob.Costpid == "&nbsp;")
                {
                    ob.Costpid = "";
                }
                else
                    ob.Costpid = ob.Costpid;
                cmd.Parameters.Add("@COSTPID", SqlDbType.NVarChar).Value = ob.Costpid;
                cmd.Parameters.Add("@TRANSMODE", SqlDbType.NVarChar).Value = ob.Transmode;
                cmd.Parameters.Add("@DEBITCD", SqlDbType.NVarChar).Value = ob.Debitcd;
                cmd.Parameters.Add("@CREDITCD", SqlDbType.NVarChar).Value = ob.Creditcd;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                if (ob.Chequeno == "&nbsp;")
                {
                    ob.Chequeno = "";
                }
                else
                    ob.Chequeno = ob.Chequeno;
                cmd.Parameters.Add("@CHEQUENO", SqlDbType.NVarChar).Value = ob.Chequeno;
                cmd.Parameters.Add("@CHEQUEDT", SqlDbType.DateTime).Value = ob.Chequedt;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.Username;
                cmd.Parameters.Add("@ACTDTI", SqlDbType.DateTime).Value = DateTime.Parse("01/01/1900");
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

        public string doProcess_MPAY_Multiple(AlchemyAccounting.Accounts.Interface.SingleVoucher ob)
        {
            string s = "";
            SqlTransaction tran = null;

            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO GL_MASTER ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, SERIALNO, TRANSDRCR, TRANSFOR, COSTPID, TRANSMODE, DEBITCD, CREDITCD, DEBITAMT, CREDITAMT, " +
                                  " CHEQUENO, CHEQUEDT, REMARKS, TABLEID, USERPC, USERID, ACTDTI, IPADDRESS) " +
                                  " Values (@TRANSTP, @TRANSDT, @TRANSMY, @TRANSNO, @SERIALNO,'DEBIT', @TRANSFOR, @COSTPID, @TRANSMODE, @DEBITCD, @CREDITCD,@AMOUNT,0, @CHEQUENO, @CHEQUEDT, @REMARKS,'GL_MTRANS', " +
                                  " @USERPC,@USERID, @ACTDTI, @IPADDRESS)";
                cmd.Parameters.Clear();


                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.Transtp;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.Transdt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.Monyear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                cmd.Parameters.Add("@SERIALNO", SqlDbType.BigInt).Value = ob.SerialNo_MREC;
                cmd.Parameters.Add("@TRANSFOR", SqlDbType.NVarChar).Value = ob.Transfor;
                if (ob.Costpid == "&nbsp;")
                {
                    ob.Costpid = "";
                }
                else
                    ob.Costpid = ob.Costpid;
                cmd.Parameters.Add("@COSTPID", SqlDbType.NVarChar).Value = ob.Costpid;
                cmd.Parameters.Add("@TRANSMODE", SqlDbType.NVarChar).Value = ob.Transmode;
                cmd.Parameters.Add("@DEBITCD", SqlDbType.NVarChar).Value = ob.Debitcd;
                cmd.Parameters.Add("@CREDITCD", SqlDbType.NVarChar).Value = ob.Creditcd;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                if (ob.Chequeno == "&nbsp;")
                {
                    ob.Chequeno = "";
                }
                else
                    ob.Chequeno = ob.Chequeno;
                cmd.Parameters.Add("@CHEQUENO", SqlDbType.NVarChar).Value = ob.Chequeno;
                cmd.Parameters.Add("@CHEQUEDT", SqlDbType.DateTime).Value = ob.Chequedt;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.Username;
                cmd.Parameters.Add("@ACTDTI", SqlDbType.DateTime).Value = DateTime.Parse("01/01/1900");
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = "";

                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO GL_MASTER ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, SERIALNO, TRANSDRCR, TRANSFOR, COSTPID, TRANSMODE, DEBITCD, CREDITCD, DEBITAMT, CREDITAMT, " +
                                  " CHEQUENO, CHEQUEDT, REMARKS, TABLEID, USERPC, USERID, ACTDTI, IPADDRESS) " +
                                  " Values (@TRANSTP, @TRANSDT, @TRANSMY, @TRANSNO, @SERIALNO,'CREDIT', @TRANSFOR, @COSTPID, @TRANSMODE, @CREDITCD, @DEBITCD,0,@AMOUNT, @CHEQUENO, @CHEQUEDT, @REMARKS,'GL_MTRANS', " +
                                  " @USERPC,@USERID, @ACTDTI, @IPADDRESS)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.Transtp;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.Transdt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.Monyear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                inc_Serial = ob.SerialNo_MREC;
                ser = int.Parse(inc_Serial) + 1;
                final_Serial = ser.ToString();
                cmd.Parameters.Add("@SERIALNO", SqlDbType.BigInt).Value = final_Serial;
                cmd.Parameters.Add("@TRANSFOR", SqlDbType.NVarChar).Value = ob.Transfor;
                if (ob.Costpid == "&nbsp;")
                {
                    ob.Costpid = "";
                }
                else
                    ob.Costpid = ob.Costpid;
                cmd.Parameters.Add("@COSTPID", SqlDbType.NVarChar).Value = ob.Costpid;
                cmd.Parameters.Add("@TRANSMODE", SqlDbType.NVarChar).Value = ob.Transmode;
                cmd.Parameters.Add("@DEBITCD", SqlDbType.NVarChar).Value = ob.Debitcd;
                cmd.Parameters.Add("@CREDITCD", SqlDbType.NVarChar).Value = ob.Creditcd;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                if (ob.Chequeno == "&nbsp;")
                {
                    ob.Chequeno = "";
                }
                else
                    ob.Chequeno = ob.Chequeno;
                cmd.Parameters.Add("@CHEQUENO", SqlDbType.NVarChar).Value = ob.Chequeno;
                cmd.Parameters.Add("@CHEQUEDT", SqlDbType.DateTime).Value = ob.Chequedt;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.Username;
                cmd.Parameters.Add("@ACTDTI", SqlDbType.DateTime).Value = DateTime.Parse("01/01/1900");
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

        public string doProcess_JOUR_Multiple(AlchemyAccounting.Accounts.Interface.SingleVoucher ob)
        {
            string s = "";
            SqlTransaction tran = null;

            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO GL_MASTER ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, SERIALNO, TRANSDRCR, TRANSFOR, COSTPID, TRANSMODE, DEBITCD, CREDITCD, DEBITAMT, CREDITAMT, " +
                                  " CHEQUENO, CHEQUEDT, REMARKS, TABLEID, USERPC, USERID, ACTDTI, IPADDRESS) " +
                                  " Values (@TRANSTP, @TRANSDT, @TRANSMY, @TRANSNO, @SERIALNO,'DEBIT', @TRANSFOR, @COSTPID, @TRANSMODE, @DEBITCD, @CREDITCD,@AMOUNT,0, @CHEQUENO, @CHEQUEDT, @REMARKS,'GL_MTRANS', " +
                                  " @USERPC,@USERID, @ACTDTI, @IPADDRESS)";
                cmd.Parameters.Clear();


                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.Transtp;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.Transdt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.Monyear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                cmd.Parameters.Add("@SERIALNO", SqlDbType.BigInt).Value = ob.SerialNo_MREC;
                cmd.Parameters.Add("@TRANSFOR", SqlDbType.NVarChar).Value = ob.Transfor;
                if (ob.Costpid == "&nbsp;")
                {
                    ob.Costpid = "";
                }
                else
                    ob.Costpid = ob.Costpid;
                cmd.Parameters.Add("@COSTPID", SqlDbType.NVarChar).Value = ob.Costpid;
                cmd.Parameters.Add("@TRANSMODE", SqlDbType.NVarChar).Value = ob.Transmode;
                cmd.Parameters.Add("@DEBITCD", SqlDbType.NVarChar).Value = ob.Debitcd;
                cmd.Parameters.Add("@CREDITCD", SqlDbType.NVarChar).Value = ob.Creditcd;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                if (ob.Chequeno == "&nbsp;")
                {
                    ob.Chequeno = "";
                }
                else
                    ob.Chequeno = ob.Chequeno;
                cmd.Parameters.Add("@CHEQUENO", SqlDbType.NVarChar).Value = ob.Chequeno;
                cmd.Parameters.Add("@CHEQUEDT", SqlDbType.DateTime).Value = ob.Chequedt;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.Username;
                cmd.Parameters.Add("@ACTDTI", SqlDbType.DateTime).Value = DateTime.Parse("01/01/1900");
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = "";

                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO GL_MASTER ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, SERIALNO, TRANSDRCR, TRANSFOR, COSTPID, TRANSMODE, DEBITCD, CREDITCD, DEBITAMT, CREDITAMT, " +
                                  " CHEQUENO, CHEQUEDT, REMARKS, TABLEID, USERPC, USERID, ACTDTI, IPADDRESS) " +
                                  " Values (@TRANSTP, @TRANSDT, @TRANSMY, @TRANSNO, @SERIALNO,'CREDIT', @TRANSFOR, @COSTPID, @TRANSMODE, @CREDITCD, @DEBITCD,0,@AMOUNT, @CHEQUENO, @CHEQUEDT, @REMARKS,'GL_MTRANS', " +
                                  " @USERPC,@USERID, @ACTDTI, @IPADDRESS)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.Transtp;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.Transdt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.Monyear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                inc_Serial = ob.SerialNo_MREC;
                ser = int.Parse(inc_Serial) + 1;
                final_Serial = ser.ToString();
                cmd.Parameters.Add("@SERIALNO", SqlDbType.BigInt).Value = final_Serial;
                cmd.Parameters.Add("@TRANSFOR", SqlDbType.NVarChar).Value = ob.Transfor;
                if (ob.Costpid == "&nbsp;")
                {
                    ob.Costpid = "";
                }
                else
                    ob.Costpid = ob.Costpid;
                cmd.Parameters.Add("@COSTPID", SqlDbType.NVarChar).Value = ob.Costpid;
                cmd.Parameters.Add("@TRANSMODE", SqlDbType.NVarChar).Value = ob.Transmode;
                cmd.Parameters.Add("@DEBITCD", SqlDbType.NVarChar).Value = ob.Debitcd;
                cmd.Parameters.Add("@CREDITCD", SqlDbType.NVarChar).Value = ob.Creditcd;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                if (ob.Chequeno == "&nbsp;")
                {
                    ob.Chequeno = "";
                }
                else
                    ob.Chequeno = ob.Chequeno;
                cmd.Parameters.Add("@CHEQUENO", SqlDbType.NVarChar).Value = ob.Chequeno;
                cmd.Parameters.Add("@CHEQUEDT", SqlDbType.DateTime).Value = ob.Chequedt;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.Username;
                cmd.Parameters.Add("@ACTDTI", SqlDbType.DateTime).Value = DateTime.Parse("01/01/1900");
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

        public string doProcess_CONT_Multiple(AlchemyAccounting.Accounts.Interface.SingleVoucher ob)
        {
            string s = "";
            SqlTransaction tran = null;

            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO GL_MASTER ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, SERIALNO, TRANSDRCR, TRANSFOR, COSTPID, TRANSMODE, DEBITCD, CREDITCD, DEBITAMT, CREDITAMT, " +
                                  " CHEQUENO, CHEQUEDT, REMARKS, TABLEID, USERPC, USERID, ACTDTI, IPADDRESS) " +
                                  " Values (@TRANSTP, @TRANSDT, @TRANSMY, @TRANSNO, @SERIALNO,'DEBIT', @TRANSFOR, @COSTPID, @TRANSMODE, @DEBITCD, @CREDITCD,@AMOUNT,0, @CHEQUENO, @CHEQUEDT, @REMARKS,'GL_MTRANS', " +
                                  " @USERPC,@USERID, @ACTDTI, @IPADDRESS)";
                cmd.Parameters.Clear();


                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.Transtp;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.Transdt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.Monyear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                cmd.Parameters.Add("@SERIALNO", SqlDbType.BigInt).Value = ob.SerialNo_MREC;
                cmd.Parameters.Add("@TRANSFOR", SqlDbType.NVarChar).Value = ob.Transfor;
                if (ob.Costpid == "&nbsp;")
                {
                    ob.Costpid = "";
                }
                else
                    ob.Costpid = ob.Costpid;
                cmd.Parameters.Add("@COSTPID", SqlDbType.NVarChar).Value = ob.Costpid;
                cmd.Parameters.Add("@TRANSMODE", SqlDbType.NVarChar).Value = ob.Transmode;
                cmd.Parameters.Add("@DEBITCD", SqlDbType.NVarChar).Value = ob.Debitcd;
                cmd.Parameters.Add("@CREDITCD", SqlDbType.NVarChar).Value = ob.Creditcd;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                if (ob.Chequeno == "&nbsp;")
                {
                    ob.Chequeno = "";
                }
                else
                    ob.Chequeno = ob.Chequeno;
                cmd.Parameters.Add("@CHEQUENO", SqlDbType.NVarChar).Value = ob.Chequeno;
                cmd.Parameters.Add("@CHEQUEDT", SqlDbType.DateTime).Value = ob.Chequedt;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.Username;
                cmd.Parameters.Add("@ACTDTI", SqlDbType.DateTime).Value = DateTime.Parse("01/01/1900");
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = "";

                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO GL_MASTER ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, SERIALNO, TRANSDRCR, TRANSFOR, COSTPID, TRANSMODE, DEBITCD, CREDITCD, DEBITAMT, CREDITAMT, " +
                                  " CHEQUENO, CHEQUEDT, REMARKS, TABLEID, USERPC, USERID, ACTDTI, IPADDRESS) " +
                                  " Values (@TRANSTP, @TRANSDT, @TRANSMY, @TRANSNO, @SERIALNO,'CREDIT', @TRANSFOR, @COSTPID, @TRANSMODE, @CREDITCD, @DEBITCD,0,@AMOUNT, @CHEQUENO, @CHEQUEDT, @REMARKS,'GL_MTRANS', " +
                                  " @USERPC,@USERID, @ACTDTI, @IPADDRESS)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.Transtp;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.Transdt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.Monyear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                inc_Serial = ob.SerialNo_MREC;
                ser = int.Parse(inc_Serial) + 1;
                final_Serial = ser.ToString();
                cmd.Parameters.Add("@SERIALNO", SqlDbType.BigInt).Value = final_Serial;
                cmd.Parameters.Add("@TRANSFOR", SqlDbType.NVarChar).Value = ob.Transfor;
                if (ob.Costpid == "&nbsp;")
                {
                    ob.Costpid = "";
                }
                else
                    ob.Costpid = ob.Costpid;
                cmd.Parameters.Add("@COSTPID", SqlDbType.NVarChar).Value = ob.Costpid;
                cmd.Parameters.Add("@TRANSMODE", SqlDbType.NVarChar).Value = ob.Transmode;
                cmd.Parameters.Add("@DEBITCD", SqlDbType.NVarChar).Value = ob.Debitcd;
                cmd.Parameters.Add("@CREDITCD", SqlDbType.NVarChar).Value = ob.Creditcd;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                if (ob.Chequeno == "&nbsp;")
                {
                    ob.Chequeno = "";
                }
                else
                    ob.Chequeno = ob.Chequeno;
                cmd.Parameters.Add("@CHEQUENO", SqlDbType.NVarChar).Value = ob.Chequeno;
                cmd.Parameters.Add("@CHEQUEDT", SqlDbType.DateTime).Value = ob.Chequedt;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.Username;
                cmd.Parameters.Add("@ACTDTI", SqlDbType.DateTime).Value = DateTime.Parse("01/01/1900");
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

        public string doProcess_MicroCreditCollection(AlchemyAccounting.Accounts.Interface.SingleVoucher ob)
        {
            string s = "";
            SqlTransaction tran = null;

            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO GL_MASTER ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, SERIALNO, COSTPID, TRANSDRCR, DEBITCD, CREDITCD, DEBITAMT, CREDITAMT, " +
                                  " REMARKS, TABLEID, USERPC, USERID, ACTDTI, IPADDRESS) " +
                                  " Values (@TRANSTP, @TRANSDT, @TRANSMY, @TRANSNO, @SERIALNO, @COSTPID, 'DEBIT', @DEBITCD, @CREDITCD, @AMOUNT, 0, @REMARKS,'MC_COLLECT', " +
                                  " @USERPC,@USERID, @ACTDTI, @IPADDRESS)";
                cmd.Parameters.Clear();

                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.Transtp;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.Transdt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.Monyear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                cmd.Parameters.Add("@SERIALNO", SqlDbType.BigInt).Value = ob.SerialNo_MREC;
                cmd.Parameters.Add("@COSTPID", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@DEBITCD", SqlDbType.NVarChar).Value = ob.Debitcd;
                cmd.Parameters.Add("@CREDITCD", SqlDbType.NVarChar).Value = ob.Creditcd;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.Userpc;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.Username;
                cmd.Parameters.Add("@ACTDTI", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;

                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO GL_MASTER ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, SERIALNO, COSTPID, TRANSDRCR, DEBITCD, CREDITCD, DEBITAMT, CREDITAMT, " +
                                  " REMARKS, TABLEID, USERPC, USERID, ACTDTI, IPADDRESS) " +
                                  " Values (@TRANSTP, @TRANSDT, @TRANSMY, @TRANSNO, @SERIALNO, @COSTPID, 'CREDIT', @CREDITCD, @DEBITCD, 0, @AMOUNT, @REMARKS,'MC_COLLECT', " +
                                  " @USERPC,@USERID, @ACTDTI, @IPADDRESS)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.Transtp;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.Transdt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.Monyear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                inc_Serial = ob.SerialNo_MREC;
                ser = int.Parse(inc_Serial) + 1;
                final_Serial = ser.ToString();
                cmd.Parameters.Add("@SERIALNO", SqlDbType.BigInt).Value = final_Serial;
                cmd.Parameters.Add("@COSTPID", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@DEBITCD", SqlDbType.NVarChar).Value = ob.Debitcd;
                cmd.Parameters.Add("@CREDITCD", SqlDbType.NVarChar).Value = ob.Creditcd;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.Userpc;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.Username;
                cmd.Parameters.Add("@ACTDTI", SqlDbType.DateTime).Value = DateTime.Now;
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

        //public string doProcess_MicroCreditCollectionMember(AlchemyAccounting.multipurpose.InterFace.multipuposeInterface ob)
        //{
        //    string s = "";
        //    SqlTransaction tran = null;

        //    try
        //    {
        //        if (ob.SchTP=="DEPOSIT")
        //        {

        //            if (con.State != ConnectionState.Open)
        //                con.Open();
        //            tran = con.BeginTransaction();
        //            cmd.CommandType = CommandType.Text;
        //            cmd.CommandText = "INSERT INTO MC_MLEDGER (TRANSTP, TRANSDT, TRANSMY, TRANSYY, TRANSNO, TRANSSL, MEMBER_ID, SCHEME_ID, INTERNALID, DEBITAMT, CREDITAMT, REMARKS, TABLEID, USERPC, USERID, IPADDRESS) " +
        //                              " Values (@TRANSTP, @TRANSDT, @TRANSMY, @TRANSYY, @TRANSNO, @TRANSSL, @MEMBER_ID, @SCHEME_ID, @INTERNALID, 0, @AMOUNT, @REMARKS,'MC_COLLECT', " +
        //                              " @USERPC, @USERID, @IPADDRESS)";
        //            cmd.Parameters.Clear();
        //            cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.TransTP;
        //            cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.TransDT;
        //            cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.TransMY;
        //            cmd.Parameters.Add("@TRANSYY", SqlDbType.Int).Value = ob.Year;
        //            cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.DocNo;
        //            //inc_Serial = ob.SerialNo_MREC;
        //            //ser = int.Parse(inc_Serial) + 1;
        //            //final_Serial = ser.ToString();
        //            cmd.Parameters.Add("@TRANSSL", SqlDbType.BigInt).Value = ob.SerialNo_MREC;
        //            cmd.Parameters.Add("@MEMBER_ID", SqlDbType.NVarChar).Value = ob.MemberID;
        //            cmd.Parameters.Add("@SCHEME_ID", SqlDbType.NVarChar).Value = ob.SchemeID;
        //            cmd.Parameters.Add("@INTERNALID", SqlDbType.NVarChar).Value = ob.InternalID;
        //            cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
        //            cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
        //            cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPc;
        //            cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserNm;
        //            cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ip;

        //            cmd.Transaction = tran;
        //            cmd.ExecuteNonQuery();
        //        }
        //        else
        //        {
        //            cmd.CommandType = CommandType.Text;
        //            cmd.CommandText = "INSERT INTO MC_MLEDGER (TRANSTP, TRANSDT, TRANSMY, TRANSYY, TRANSNO, TRANSSL, MEMBER_ID, SCHEME_ID, INTERNALID, DEBITAMT, CREDITAMT, REMARKS, TABLEID, USERPC, USERID, IPADDRESS) " +
        //                              " Values (@TRANSTP, @TRANSDT, @TRANSMY, @TRANSYY, @TRANSNO, @TRANSSL, @MEMBER_ID, @SCHEME_ID, @INTERNALID, @AMOUNT, 0, @REMARKS,'MC_COLLECT', " +
        //                              " @USERPC,@USERID, @IPADDRESS)";
        //            cmd.Parameters.Clear();

        //            cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.TransTP;
        //            cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.TransDT;
        //            cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.TransMY;
        //            cmd.Parameters.Add("@TRANSYY", SqlDbType.Int).Value = ob.Year;
        //            cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.DocNo;
        //            cmd.Parameters.Add("@TRANSSL", SqlDbType.BigInt).Value = ob.SerialNo_MREC;
        //            cmd.Parameters.Add("@MEMBER_ID", SqlDbType.NVarChar).Value = ob.MemberID;
        //            cmd.Parameters.Add("@SCHEME_ID", SqlDbType.NVarChar).Value = ob.SchemeID;
        //            cmd.Parameters.Add("@INTERNALID", SqlDbType.NVarChar).Value = ob.InternalID;
        //            cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
        //            cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
        //            cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPc;
        //            cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserNm;
        //            cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ip;

        //            cmd.Transaction = tran;
        //            cmd.ExecuteNonQuery();
        //        }

        //        tran.Commit();
        //        if (con.State != ConnectionState.Closed)
        //            con.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        tran.Rollback();
        //        s = ex.Message;
        //    }
        //    return s;
        //}

        public string doProcess_job_Expense(AlchemyAccounting.Accounts.Interface.SingleVoucher ob)
        {
            string s = "";
            SqlTransaction tran = null;

            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO GL_MASTER ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, SERIALNO, TRANSDRCR, TRANSFOR, COSTPID, TRANSMODE, DEBITCD, CREDITCD, DEBITAMT, CREDITAMT, " +
                                  " REMARKS, TABLEID, USERPC, USERID, ACTDTI, IPADDRESS) " +
                                  " Values (@TRANSTP, @TRANSDT, @TRANSMY, @TRANSNO, @SERIALNO,'DEBIT', '', @COSTPID, '', @DEBITCD, @CREDITCD,@AMOUNT,0, @REMARKS,'CNF_JOBEXP', " +
                                  " @USERPC,@USERID, @ACTDTI, @IPADDRESS)";
                cmd.Parameters.Clear();


                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.Transtp;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.Transdt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.Monyear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                cmd.Parameters.Add("@SERIALNO", SqlDbType.BigInt).Value = ob.Serial_Job_Exp_Jour;
                //cmd.Parameters.Add("@TRANSFOR", SqlDbType.NVarChar).Value = ob.Transfor;
                //if (ob.Costpid == "&nbsp;")
                //{
                //    ob.Costpid = "";
                //}
                //else
                //    ob.Costpid = ob.Costpid;
                cmd.Parameters.Add("@COSTPID", SqlDbType.NVarChar).Value = ob.Costpid;
                //cmd.Parameters.Add("@TRANSMODE", SqlDbType.NVarChar).Value = ob.Transmode;
                cmd.Parameters.Add("@DEBITCD", SqlDbType.NVarChar).Value = ob.Debitcd;
                cmd.Parameters.Add("@CREDITCD", SqlDbType.NVarChar).Value = ob.Creditcd;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                //if (ob.Chequeno == "&nbsp;")
                //{
                //    ob.Chequeno = "";
                //}
                //else
                //    ob.Chequeno = ob.Chequeno;
                //cmd.Parameters.Add("@CHEQUENO", SqlDbType.NVarChar).Value = ob.Chequeno;
                //cmd.Parameters.Add("@CHEQUEDT", SqlDbType.DateTime).Value = ob.Chequedt;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.Userpc;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.Username;
                cmd.Parameters.Add("@ACTDTI", SqlDbType.DateTime).Value = DateTime.Parse("01/01/1900");
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;

                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO GL_MASTER ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, SERIALNO, TRANSDRCR, TRANSFOR, COSTPID, TRANSMODE, DEBITCD, CREDITCD, DEBITAMT, CREDITAMT, " +
                                  " REMARKS, TABLEID, USERPC, USERID, ACTDTI, IPADDRESS) " +
                                  " Values (@TRANSTP, @TRANSDT, @TRANSMY, @TRANSNO, @SERIALNO,'CREDIT', '', @COSTPID, '', @CREDITCD, @DEBITCD,0,@AMOUNT, @REMARKS,'CNF_JOBEXP', " +
                                  " @USERPC,@USERID, @ACTDTI, @IPADDRESS)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.Transtp;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.Transdt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.Monyear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                inc_Serial = ob.Serial_Job_Exp_Jour;
                ser = int.Parse(inc_Serial) + 1;
                final_Serial = ser.ToString();
                cmd.Parameters.Add("@SERIALNO", SqlDbType.BigInt).Value = final_Serial;
                //cmd.Parameters.Add("@TRANSFOR", SqlDbType.NVarChar).Value = ob.Transfor;
                //if (ob.Costpid == "&nbsp;")
                //{
                //    ob.Costpid = "";
                //}
                //else
                //    ob.Costpid = ob.Costpid;
                cmd.Parameters.Add("@COSTPID", SqlDbType.NVarChar).Value = ob.Costpid;
                //cmd.Parameters.Add("@TRANSMODE", SqlDbType.NVarChar).Value = ob.Transmode;
                cmd.Parameters.Add("@DEBITCD", SqlDbType.NVarChar).Value = ob.Debitcd;
                cmd.Parameters.Add("@CREDITCD", SqlDbType.NVarChar).Value = ob.Creditcd;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                //if (ob.Chequeno == "&nbsp;")
                //{
                //    ob.Chequeno = "";
                //}
                //else
                //    ob.Chequeno = ob.Chequeno;
                //cmd.Parameters.Add("@CHEQUENO", SqlDbType.NVarChar).Value = ob.Chequeno;
                //cmd.Parameters.Add("@CHEQUEDT", SqlDbType.DateTime).Value = ob.Chequedt;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.Userpc;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.Username;
                cmd.Parameters.Add("@ACTDTI", SqlDbType.DateTime).Value = DateTime.Parse("01/01/1900");
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

        public string doProcess_Job_Receive(AlchemyAccounting.Accounts.Interface.SingleVoucher ob)
        {
            string s = "";
            SqlTransaction tran = null;


            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO GL_MASTER ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, SERIALNO, TRANSDRCR, TRANSFOR, COSTPID, TRANSMODE, DEBITCD, CREDITCD, DEBITAMT, CREDITAMT, " +
                                  " REMARKS, TABLEID, USERPC, USERID, ACTDTI, IPADDRESS) " +
                                  " Values (@TRANSTP, @TRANSDT, @TRANSMY, @TRANSNO, @SERIALNO,'DEBIT', @TRANSFOR, @COSTPID, '', @DEBITCD, @CREDITCD,@AMOUNT,0, @REMARKS,'CNF_JOBRCV', " +
                                  " @USERPC,@USERID, @ACTDTI, @IPADDRESS)";
                cmd.Parameters.Clear();

                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.Transtp;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.Transdt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.Monyear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                cmd.Parameters.Add("@SERIALNO", SqlDbType.BigInt).Value = ob.Serial_Job_Rec;
                cmd.Parameters.Add("@TRANSFOR", SqlDbType.NVarChar).Value = ob.Transfor;
                //if (ob.Costpid == "&nbsp;")
                //{
                //    ob.Costpid = "";
                //}
                //else
                //    ob.Costpid = ob.Costpid;
                cmd.Parameters.Add("@COSTPID", SqlDbType.NVarChar).Value = ob.Costpid;
                //cmd.Parameters.Add("@TRANSMODE", SqlDbType.NVarChar).Value = ob.Transmode;
                cmd.Parameters.Add("@DEBITCD", SqlDbType.NVarChar).Value = ob.Debitcd;
                cmd.Parameters.Add("@CREDITCD", SqlDbType.NVarChar).Value = ob.Creditcd;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                //if (ob.Chequeno == "&nbsp;")
                //{
                //    ob.Chequeno = "";
                //}
                //else
                //    ob.Chequeno = ob.Chequeno;
                //cmd.Parameters.Add("@CHEQUENO", SqlDbType.NVarChar).Value = ob.Chequeno;
                //cmd.Parameters.Add("@CHEQUEDT", SqlDbType.DateTime).Value = ob.Chequedt;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.Userpc;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.Username;
                cmd.Parameters.Add("@ACTDTI", SqlDbType.DateTime).Value = DateTime.Parse("01/01/1900");
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;

                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO GL_MASTER ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, SERIALNO, TRANSDRCR, TRANSFOR, COSTPID, TRANSMODE, DEBITCD, CREDITCD, DEBITAMT, CREDITAMT, " +
                                  " REMARKS, TABLEID, USERPC, USERID, ACTDTI, IPADDRESS) " +
                                  " Values (@TRANSTP, @TRANSDT, @TRANSMY, @TRANSNO, @SERIALNO,'CREDIT', @TRANSFOR, @COSTPID, '',@CREDITCD, @DEBITCD,0,@AMOUNT, @REMARKS,'CNF_JOBRCV', " +
                                  " @USERPC,@USERID, @ACTDTI, @IPADDRESS)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.Transtp;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.Transdt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.Monyear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                inc_Serial = ob.Serial_Job_Rec;
                ser = int.Parse(inc_Serial) + 1;
                final_Serial = ser.ToString();
                cmd.Parameters.Add("@SERIALNO", SqlDbType.BigInt).Value = final_Serial;
                cmd.Parameters.Add("@TRANSFOR", SqlDbType.NVarChar).Value = ob.Transfor;
                //if (ob.Costpid == "&nbsp;")
                //{
                //    ob.Costpid = "";
                //}
                //else
                //    ob.Costpid = ob.Costpid;
                cmd.Parameters.Add("@COSTPID", SqlDbType.NVarChar).Value = ob.Costpid;
                //cmd.Parameters.Add("@TRANSMODE", SqlDbType.NVarChar).Value = ob.Transmode;
                cmd.Parameters.Add("@DEBITCD", SqlDbType.NVarChar).Value = ob.Debitcd;
                cmd.Parameters.Add("@CREDITCD", SqlDbType.NVarChar).Value = ob.Creditcd;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                //if (ob.Chequeno == "&nbsp;")
                //{
                //    ob.Chequeno = "";
                //}
                //else
                //    ob.Chequeno = ob.Chequeno;
                //cmd.Parameters.Add("@CHEQUENO", SqlDbType.NVarChar).Value = ob.Chequeno;
                //cmd.Parameters.Add("@CHEQUEDT", SqlDbType.DateTime).Value = ob.Chequedt;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.Userpc;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.Username;
                cmd.Parameters.Add("@ACTDTI", SqlDbType.DateTime).Value = DateTime.Parse("01/01/1900");
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

        public string doProcess_Job_Bill(AlchemyAccounting.Accounts.Interface.SingleVoucher ob)
        {
            string s = "";
            SqlTransaction tran = null;


            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO GL_MASTER ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, SERIALNO, TRANSDRCR, COSTPID, TRANSMODE, DEBITCD, CREDITCD, DEBITAMT, CREDITAMT, " +
                                  " REMARKS, TABLEID, USERPC, USERID, ACTDTI, IPADDRESS) " +
                                  " Values (@TRANSTP, @TRANSDT, @TRANSMY, @TRANSNO, @SERIALNO,'DEBIT', @COSTPID, '', @DEBITCD, @CREDITCD,@AMOUNT,0, @REMARKS, 'CNF_JOBBILL', " +
                                  " @USERPC,@USERID, @ACTDTI, @IPADDRESS)";
                cmd.Parameters.Clear();

                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.Transtp;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.Transdt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.Monyear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                cmd.Parameters.Add("@SERIALNO", SqlDbType.BigInt).Value = ob.Serial_Job_Bill;
                //cmd.Parameters.Add("@TRANSFOR", SqlDbType.NVarChar).Value = ob.Transfor;
                //if (ob.Costpid == "&nbsp;")
                //{
                //    ob.Costpid = "";
                //}
                //else
                //    ob.Costpid = ob.Costpid;
                cmd.Parameters.Add("@COSTPID", SqlDbType.NVarChar).Value = ob.Costpid;
                //cmd.Parameters.Add("@TRANSMODE", SqlDbType.NVarChar).Value = ob.Transmode;
                cmd.Parameters.Add("@DEBITCD", SqlDbType.NVarChar).Value = ob.Debitcd;
                cmd.Parameters.Add("@CREDITCD", SqlDbType.NVarChar).Value = ob.Creditcd;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                //if (ob.Chequeno == "&nbsp;")
                //{
                //    ob.Chequeno = "";
                //}
                //else
                //    ob.Chequeno = ob.Chequeno;
                //cmd.Parameters.Add("@CHEQUENO", SqlDbType.NVarChar).Value = ob.Chequeno;
                //cmd.Parameters.Add("@CHEQUEDT", SqlDbType.DateTime).Value = ob.Chequedt;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.Userpc;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.Username;
                cmd.Parameters.Add("@ACTDTI", SqlDbType.DateTime).Value = DateTime.Parse("01/01/1900");
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;

                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO GL_MASTER ( TRANSTP, TRANSDT, TRANSMY, TRANSNO, SERIALNO, TRANSDRCR, COSTPID, TRANSMODE, DEBITCD, CREDITCD, DEBITAMT, CREDITAMT, " +
                                  " REMARKS, TABLEID, USERPC, USERID, ACTDTI, IPADDRESS) " +
                                  " Values (@TRANSTP, @TRANSDT, @TRANSMY, @TRANSNO, @SERIALNO,'CREDIT', @COSTPID, '',@CREDITCD, @DEBITCD,0,@AMOUNT, @REMARKS, 'CNF_JOBBILL', " +
                                  " @USERPC,@USERID, @ACTDTI, @IPADDRESS)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.Transtp;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.Transdt;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.Monyear;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TransNo;
                inc_Serial = ob.Serial_Job_Bill;
                ser = int.Parse(inc_Serial) + 1;
                final_Serial = ser.ToString();
                cmd.Parameters.Add("@SERIALNO", SqlDbType.BigInt).Value = final_Serial;
                //cmd.Parameters.Add("@TRANSFOR", SqlDbType.NVarChar).Value = ob.Transfor;
                //if (ob.Costpid == "&nbsp;")
                //{
                //    ob.Costpid = "";
                //}
                //else
                //    ob.Costpid = ob.Costpid;
                cmd.Parameters.Add("@COSTPID", SqlDbType.NVarChar).Value = ob.Costpid;
                //cmd.Parameters.Add("@TRANSMODE", SqlDbType.NVarChar).Value = ob.Transmode;
                cmd.Parameters.Add("@DEBITCD", SqlDbType.NVarChar).Value = ob.Debitcd;
                cmd.Parameters.Add("@CREDITCD", SqlDbType.NVarChar).Value = ob.Creditcd;
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amount;
                //if (ob.Chequeno == "&nbsp;")
                //{
                //    ob.Chequeno = "";
                //}
                //else
                //    ob.Chequeno = ob.Chequeno;
                //cmd.Parameters.Add("@CHEQUENO", SqlDbType.NVarChar).Value = ob.Chequeno;
                //cmd.Parameters.Add("@CHEQUEDT", SqlDbType.DateTime).Value = ob.Chequedt;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.Userpc;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.Username;
                cmd.Parameters.Add("@ACTDTI", SqlDbType.DateTime).Value = DateTime.Parse("01/01/1900");
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

    }
}