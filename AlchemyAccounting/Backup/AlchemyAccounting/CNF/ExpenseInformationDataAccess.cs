using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace AlchemyAccounting.CNF
{
    public class ExpenseInformationDataAccess
    {
        SqlConnection con;
        SqlCommand cmd;

        public ExpenseInformationDataAccess()
        {
            con = new SqlConnection(Global.connection);
            cmd = new SqlCommand("", con);
        }


        public string SaveExpenseInfo(ExpenseInformationModel eim)
        {

            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "insert into CNF_EXPENSE (EXPCID, EXPID, EXPNM, REMARKS, USERPC, INTIME, UPDATETIME, IPADDRESS)" +
                    "values(@EXPCID, @EXPID,@EXPNM, @REMARKS,@USERPC, @INTIME , @UPDATETIME,@IPADDRESS  )";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@EXPCID", SqlDbType.NVarChar).Value = eim.EXPCID;
                cmd.Parameters.Add("@EXPID", SqlDbType.NVarChar).Value = eim.EXPID;
                cmd.Parameters.Add("@EXPNM", SqlDbType.NVarChar).Value = eim.EXPNM;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = eim.REMARKS;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = eim.UserPC;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = eim.InTime;
                cmd.Parameters.Add("@UPDATETIME", SqlDbType.SmallDateTime).Value = eim.UpdateTime;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = eim.IPAddress;

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

        internal DataTable showExpenseInfo(string id)
        {
            DataTable table = new DataTable();
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select EXPCID , EXPCNM from CNF_EXPMST where EXPCID='" + id + "' ";
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch { }
            return table;
        }

        public string MstInput(ExpenseInformationModel eim)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "insert into CNF_EXPMST(EXPCID, EXPCNM, USERPC, INTIME, UPDATETIME, IPADDRESS)" +
                    "values(@EXPCID, @EXPCNM,@USERPC, @INTIME , @UPDATETIME,@IPADDRESS  )";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@EXPCID", SqlDbType.NVarChar).Value = eim.EXPCID;
                cmd.Parameters.Add("@EXPCNM", SqlDbType.NVarChar).Value = eim.EXPCNM;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = eim.UserPC;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = eim.InTime;
                cmd.Parameters.Add("@UPDATETIME", SqlDbType.SmallDateTime).Value = eim.UpdateTime;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = eim.IPAddress;

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

        internal string EditExpenseInfo(ExpenseInformationModel eim)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update CNF_EXPENSE set EXPNM=@EXPNM, REMARKS=@REMARKS  where EXPCID=@EXPCID and EXPID=@EXPID ";

                cmd.Parameters.Clear();

                cmd.Parameters.Add("@EXPCID", SqlDbType.NVarChar).Value = eim.EXPCID;
                cmd.Parameters.Add("@EXPID", SqlDbType.NVarChar).Value = eim.EXPID;

                cmd.Parameters.Add("@EXPNM", SqlDbType.NVarChar).Value = eim.EXPNM;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = eim.REMARKS;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = eim.UserPC;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = eim.InTime;
                cmd.Parameters.Add("@UPDATETIME", SqlDbType.SmallDateTime).Value = eim.UpdateTime;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = eim.IPAddress;



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

        internal string DeleteExpenseInfo(ExpenseInformationModel eim)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM CNF_EXPENSE WHERE EXPCID =@EXPCID AND EXPID =@EXPID";
                cmd.Parameters.Clear();

                cmd.Parameters.Add("@EXPCID", SqlDbType.NVarChar).Value = eim.EXPCID;
                cmd.Parameters.Add("@EXPID", SqlDbType.NVarChar).Value = eim.EXPID;

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