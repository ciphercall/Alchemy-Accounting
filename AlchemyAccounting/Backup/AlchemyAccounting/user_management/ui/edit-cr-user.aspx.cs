using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Services;
using System.Text;
using System.Collections.Specialized;

namespace AlchemyAccounting.cr_user.ui
{
    public partial class edit_cr_user : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    txtSearch.Focus();

                    string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
                    SqlConnection conn = new SqlConnection(connectionString);

                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT User_Registration.LoginID, User_Registration.UserID, User_Registration.Name, User_Registration.Password, User_Registration.Email, GL_COSTP.COSTPNM, " +
                      " User_Registration.USERTP FROM User_Registration INNER JOIN GL_COSTP ON User_Registration.BranchCD = GL_COSTP.CATID", conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    conn.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvDetails.DataSource = ds;
                        gvDetails.DataBind();
                        gvDetails.Visible = true;
                    }
                    else
                    {
                        Response.Write("<script>alert('No Data Found');</script>");
                        gvDetails.Visible = false;
                    }
                }
            }
        }

        public void ShowGrid()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            string src = txtSearch.Text;

            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT User_Registration.LoginID, User_Registration.UserID, User_Registration.Name, User_Registration.Password, User_Registration.Email, GL_COSTP.COSTPNM, " +
                      " User_Registration.USERTP FROM User_Registration INNER JOIN GL_COSTP ON User_Registration.BranchCD = GL_COSTP.CATID WHERE User_Registration.Name like '" + txtSearch.Text + "%' or User_Registration.UserID like '" + txtSearch.Text + "%' or User_Registration.Email like '" + txtSearch.Text + "%' ", conn);
            cmd.Parameters.AddWithValue("@src", src);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
                gvDetails.Visible = true;
            }
            else
            {
                Response.Write("<script>alert('No Data Found');</script>");
                gvDetails.Visible = false;
            }
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ShowGrid();
            txtSearch.Focus();
        }

        protected void gvDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDetails.EditIndex = -1;
            ShowGrid();
            txtSearch.Focus();
        }

        protected void gvDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDetails.EditIndex = e.NewEditIndex;
            ShowGrid();

            TextBox txtuseridEdit = (TextBox)gvDetails.Rows[e.NewEditIndex].FindControl("txtuseridEdit");
            DropDownList ddlBranchEdit = (DropDownList)gvDetails.Rows[e.NewEditIndex].FindControl("ddlBranchEdit");
            DropDownList ddlUserTypeEdit = (DropDownList)gvDetails.Rows[e.NewEditIndex].FindControl("ddlUserTypeEdit");
            Global.dropDownAddWithSelect(ddlBranchEdit, "SELECT CATNM FROM GL_COSTPMST ORDER BY CATID");
            lblBranchNM.Text = "";
            Global.lblAdd("SELECT GL_COSTP.COSTPNM FROM GL_COSTP INNER JOIN User_Registration ON GL_COSTP.CATID = User_Registration.BranchCD WHERE User_Registration.UserID ='" + txtuseridEdit.Text + "'", lblBranchNM);
            ddlBranchEdit.Text = lblBranchNM.Text;

            lblBranchCD.Text = "";
            Global.lblAdd("SELECT CATID FROM GL_COSTP WHERE COSTPNM ='" + ddlBranchEdit.Text + "'", lblBranchCD);

            lblUserTp.Text = "";
            Global.lblAdd("SELECT USERTP FROM User_Registration WHERE UserID ='" + txtuseridEdit.Text + "'", lblUserTp);
            ddlUserTypeEdit.Text = lblUserTp.Text;

            TextBox txtnameEdit = (TextBox)gvDetails.Rows[e.NewEditIndex].FindControl("txtnameEdit");
            txtnameEdit.Focus();
            Label lblLoginIDEdit = (Label)gvDetails.Rows[e.NewEditIndex].FindControl("lblLoginIDEdit");
        }

        protected void txtEmailEdit_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtEmailEdit = (TextBox)row.FindControl("txtEmailEdit");
            TextBox txtuseridEdit = (TextBox)row.FindControl("txtuseridEdit");
            DropDownList ddlBranchEdit = (DropDownList)row.FindControl("ddlBranchEdit");
            txtuseridEdit.Text = txtEmailEdit.Text;
            ddlBranchEdit.Focus();
        }

        protected void ddlBranchEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((DropDownList)sender).NamingContainer);
            DropDownList ddlBranchEdit = (DropDownList)row.FindControl("ddlBranchEdit");
            DropDownList ddlUserTypeEdit = (DropDownList)row.FindControl("ddlUserTypeEdit");
            lblBranchCD.Text = "";
            Global.lblAdd("SELECT CATID FROM GL_COSTP WHERE COSTPNM ='" + ddlBranchEdit.Text + "'", lblBranchCD);
            ddlUserTypeEdit.Focus();
        }

        protected void ddlUserTypeEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((DropDownList)sender).NamingContainer);
            TextBox txtPassEdit = (TextBox)row.FindControl("txtPassEdit");
            txtPassEdit.Focus();
        }

        protected void gvDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            string OpenUserName = HttpContext.Current.Session["UserName"].ToString();
            string PcName = HttpContext.Current.Session["PCName"].ToString();
            string ip = HttpContext.Current.Session["IpAddress"].ToString();

            TextBox txtnameEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtnameEdit");
            TextBox txtEmailEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtEmailEdit");
            TextBox txtuseridEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtuseridEdit");
            DropDownList ddlUserTypeEdit = (DropDownList)gvDetails.Rows[e.RowIndex].FindControl("ddlUserTypeEdit");
            TextBox txtPassEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtPassEdit");
            Label lblLoginIDEdit = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblLoginIDEdit");

            //DateTime lst_UpDT = DateTime.Now;
            //string lastUp = lst_UpDT.ToString("yyyy-MM-dd");

            //conn.Open();
            //SqlCommand cmd = new SqlCommand(" INSERT INTO logData(userSID, tableID, description, userPc, userID, ipAddress) " +
            //                                " VALUES (" + lblUserSidEdit.Text + ",'userRegistration',(SELECT (name + ' ' + phone + ' ' + company + ' ' + address + ' ' + email + ' ' + webID + ' ' + userName + ' ' + password + ' ' + maskName + ' ' + convert(nvarchar(20),creditLimit,103) + ' ' + status + ' ' + userPc + ' ' + userID + ' ' + ipAddress) FROM userRegistration where userSID= " + lblUserSidEdit.Text + "), " +
            //                                " '" + PcName + "','" + userName + "','" + ip + "')", conn);
            //cmd.ExecuteNonQuery();
            //conn.Close();

            conn.Open();
            SqlCommand cmd1 = new SqlCommand(" update User_Registration set Name = '" + txtnameEdit.Text + "', Email = '" + txtEmailEdit.Text + "', UserID = '" + txtuseridEdit.Text + "', " +
                                             " Password = '" + txtPassEdit.Text + "',  BranchCD ='" + lblBranchCD.Text + "', USERTP ='" + ddlUserTypeEdit.Text + "',userPc = '" + PcName + "', OpenUser = '" + OpenUserName + "', ipAddress = '" + ip + "' WHERE LoginID= " + lblLoginIDEdit.Text + " ", conn);
            cmd1.ExecuteNonQuery();
            conn.Close();
            //Response.Write("<script>alert('Successfully Updated');</script>");
            gvDetails.EditIndex = -1;
            ShowGrid();
        }

        protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            string OpenUserName = HttpContext.Current.Session["UserName"].ToString();
            string PcName = HttpContext.Current.Session["PCName"].ToString();
            string ip = HttpContext.Current.Session["IpAddress"].ToString();

            Label lblLoginID = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblLoginID");

            //DateTime lst_UpDT = DateTime.Now;
            //string lastUp = lst_UpDT.ToString("yyyy-MM-dd");

            //conn.Open();
            //SqlCommand cmd = new SqlCommand(" INSERT INTO logData(userSID, tableID, description, userPc, userID, ipAddress) " +
            //                                " VALUES (" + lblUserSid.Text + ",'userRegistration',(SELECT (name + ' ' + phone + ' ' + company + ' ' + address + ' ' + email + ' ' + webID + ' ' + userName + ' ' + password + ' ' + maskName + ' ' + convert(nvarchar(20),creditLimit,103) + ' ' + status + ' ' + userPc + ' ' + userID + ' ' + ipAddress) FROM userRegistration where userSID= " + lblUserSid.Text + "), " +
            //                                " '" + PcName + "','" + userName + "','" + ip + "')", conn);
            //cmd.ExecuteNonQuery();
            //conn.Close();

            conn.Open();
            SqlCommand cmd1 = new SqlCommand("delete from User_Registration where LoginID = '" + lblLoginID.Text + "' ", conn);
            cmd1.ExecuteNonQuery();
            conn.Close();
            ShowGrid();
        }
    }
}