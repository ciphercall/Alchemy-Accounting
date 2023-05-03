using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Net;
using System.Data;

namespace AlchemyAccounting.Login.UI
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Clear();

            }
            //RegisterHyperLink.NavigateUrl = "~/Login/UI/Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            //RegisterHyperLink.NavigateUrl = "~/Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            txtUserName.Focus();
        }

        protected void loginButton_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(Global.connection);

                string uname = txtUserName.Text.Trim(); //Get the username from the control
                string password = txtPassword.Text.Trim();//get the Password from the control
                bool flag = AuthenticateUser(uname, password);
                if (flag == true)
                {
                    Session["UserName"] = txtUserName.Text;
                    //Session["UserPC"] = Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName;
                    //string a = Session["UserPC"].ToString();

                    string strHostName = System.Net.Dns.GetHostName();
                    IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                    IPAddress ipAddress = ipHostInfo.AddressList[0];

                    string brCD = "";

                    SqlCommand cmd = new SqlCommand("SELECT BranchCD FROM User_Registration WHERE UserID =@UserID AND Password =@Password", con);
                    con.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@UserID", uname);
                    cmd.Parameters.AddWithValue("@Password", password);
                    SqlDataReader brRead = null;
                    brRead = cmd.ExecuteReader();

                    if (brRead.Read())
                    {
                        brCD = brRead["BranchCD"].ToString();
                    }

                    brRead.Close();
                    con.Close();

                    string UserTP = "";

                    SqlCommand cmdd = new SqlCommand("SELECT USERTP FROM User_Registration WHERE UserID =@UserID AND Password =@Password", con);
                    con.Open();
                    cmdd.Parameters.Clear();
                    cmdd.Parameters.AddWithValue("@UserID", uname);
                    cmdd.Parameters.AddWithValue("@Password", password);
                    SqlDataReader tpRead = null;
                    tpRead = cmdd.ExecuteReader();

                    if (tpRead.Read())
                    {
                        UserTP = tpRead["USERTP"].ToString();
                    }
                    tpRead.Close();
                    con.Close();

                    Session["PCName"] = strHostName;
                    Session["IpAddress"] = ipAddress;
                    Session["BrCD"] = brCD;
                    Session["UserTp"] = UserTP;
                    //InsertLogOnHistory();                    
                    //LoginUser.DestinationPageUrl = "~/WebForm1.aspx";
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    lblErrmsg.Visible = true;
                    lblErrmsg.Text = "User name or password mismatch.";
                    txtPassword.Text = "";
                    txtPassword.Focus();
                }

            }
            catch (Exception)
            {

            }
        }

        protected void LoginUser_Authenticate(object sender, AuthenticateEventArgs e)
        {
            //    try

            //    {
            //    string uname = LoginUser.UserName.Trim(); //Get the username from the control
            //    string password = LoginUser.Password.Trim(); //get the Password from the control
            //    bool flag = AuthenticateUser(uname, password);
            //    if (flag == true)
            //    {
            //        e.Authenticated = true;
            //        Session["UserName"] = LoginUser.UserName;
            //        //LoginUser.DestinationPageUrl = "~/WebForm1.aspx";
            //        LoginUser.DestinationPageUrl = "~/Default.aspx";

            //    }
            //    else
            //        e.Authenticated = false;
            //}
            //catch (Exception)
            //{
            //    e.Authenticated = false;
            //}
            //DataTable table = new DataTable();
            //try
            //{
            //    LoginDataAccess dob = new LoginDataAccess();
            //    table = dob.showEmpLoginInfo();
            //    for (int i = 0; i < table.Rows.Count; i++)
            //    {
            //        if (LoginUser.UserName == table.Rows[i][0].ToString() && LoginUser.Password == "ss")// && table.Rows[i][1].ToString() != "Top")
            //        {
            //            Session["UserName"] = LoginUser.UserName;
            //            //Response.Redirect("~/WebForm1.aspx");
            //            Server.Transfer("~/WebForm1.aspx");
            //        }
            //        //if (Login1.UserName == table.Rows[i][0].ToString() && Login1.Password == "aa" && table.Rows[i][1].ToString() == "Top")
            //        //{
            //        //    Session["UserName"] = Login1.UserName;
            //        //    Response.Redirect("~/Login/UI/TopHome.aspx");
            //        //}
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Response.Write(ex.Message);
            //}
        }
        private bool AuthenticateUser(string uname, string password)
        {
            bool bflag = false;
            DataTable table = new DataTable();
            try
            {
                DataAccess.LoginDataAccess dob = new DataAccess.LoginDataAccess();
                table = dob.showEmpLoginInfo(uname, password);
                DataSet userDS = new DataSet();
            }
            catch (Exception ex)
            {
                table = null;
                Response.Write(ex.Message);
            }
            if (table != null)
            {
                if (table.Rows.Count > 0)
                    bflag = true;
            }
            return bflag;
        }


        private void InsertLogOnHistory()
        {


            //IFormatProvider dateFormat = new System.Globalization.CultureInfo("fr-FR", true);
            //DateTime Cheque_Date, _preparedate = new DateTime();

            //Cheque_Date = DateTime.Parse(Cheque_Date, dateFormat, System.Globalization.DateTimeStyles.AssumeLocal);
            //_preparedate = DateTime.Parse(_preparedate, dateFormat, System.Globalization.DateTimeStyles.AssumeLocal);

            try
            {
                DataAccess.LoginDataAccess dob = new DataAccess.LoginDataAccess();
                string user = txtUserName.Text;
                DateTime inTime = DateTime.Now;
                DateTime date = DateTime.Today;

                //string strr = System.Windows.Forms.SystemInformation.ComputerName.ToString();// SystemInformation.Network.ToString();
                //string dom = System.Windows.Forms.SystemInformation.UserDomainName.ToString();
                string IP = "";
                String strHostName = Dns.GetHostName();
                IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
                IPAddress[] addlist = ipEntry.AddressList;
                try
                {
                    for (int i = 0; i < addlist.Length; i++)
                    {
                        IP = ipEntry.AddressList[i].ToString();// addlist[i].ToString;// ipEntry.AddressList[3].ToString();
                    }
                }
                catch { IP = "127.0.0.1"; }
                string _ip = IP;
                // String str = dob.InsertLogonHistory(user, inTime, strr, date, dom, IP);
                // MessageBox.Show(str);
            }
            catch (Exception er)
            {
                Page.Response.Write(er.Message);
            }
        }

        protected void txtUserName_TextChanged(object sender, EventArgs e)
        {
            if (txtUserName.Text == "")
            {
                lblErrmsg.Visible = true;
                lblErrmsg.Text = "User Name Missing.";
            }
            else
            {
                lblErrmsg.Visible = false;
                txtPassword.Focus();
            }
        }

        protected void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if (txtPassword.Text == "")
            {
                lblErrmsg.Visible = true;
                lblErrmsg.Text = "Password Missing.";
            }
            else
            {
                lblErrmsg.Visible = false;
                loginButton.Focus();
            }
        }


    }
}