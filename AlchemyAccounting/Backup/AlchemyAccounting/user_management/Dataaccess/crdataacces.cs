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
using AlchemyAccounting.cr_user.Interface;

namespace AlchemyAccounting.cr_user.Dataaccess
{
    public class crdataacces
    {
        SqlConnection con;
        SqlCommand cmd;

        public crdataacces()
        {
            con = new SqlConnection(Global.connection);
            cmd = new SqlCommand("",con);
        }

        public string insertUser(crinterface ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = " INSERT INTO User_Registration (UserID, Name, Password, Email, BranchCD, USERTP, OpenUser, UserPc, IpAddress) " +
                                  " VALUES (@UserID, @Name, @Password, @Email, @BranchCD, @USERTP, @OpenUser, @UserPc, @IpAddress)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = ob.Name;
                cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = ob.Email;
                cmd.Parameters.Add("@UserID", SqlDbType.NVarChar).Value = ob.Usid;
                cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = ob.Pass;
                cmd.Parameters.Add("@BranchCD", SqlDbType.NVarChar).Value = ob.Branch;
                cmd.Parameters.Add("@USERTP", SqlDbType.NVarChar).Value = ob.UserType;
                cmd.Parameters.Add("@OpenUser", SqlDbType.NVarChar).Value = ob.Openuser;
                cmd.Parameters.Add("@userPc", SqlDbType.NVarChar).Value = ob.Pcname;
                cmd.Parameters.Add("@ipAddress", SqlDbType.NVarChar).Value = ob.Ipaddress;

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