using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using AlchemyAccounting;
using System.Data.SqlClient;
using System.Web.Services;
using System.Text;
using System.Collections.Specialized;
using AlchemyAccounting.Accounts.DataAccess;
using AlchemyAccounting.Accounts.Interface;
using System.Drawing;

namespace AlchemyAccounting.Accounts.UI
{
    public partial class ChartofAccounts : System.Web.UI.Page
    {
        public string prefixText { get; set; }

        public int count { get; set; }

        public string contextKey { get; set; }
        public int index { get; set; }
        private DateTime inTM;

        public DateTime InTM
        {
            get { return inTM; }
            set { inTM = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    ddlLevelID.AutoPostBack = true;
                    txtExpen.Visible = false;
                    txtIncome.Visible = false;
                    txtLiabilty.Visible = false;
                    lblAccTP.Visible = false;
                    lblIncrLevel.Visible = false;
                    lblLvlID.Visible = false;
                    lblMxAccCode.Visible = false;
                    lblNewLvlCD.Visible = false;
                    lblresult.Visible = false;
                    lblSelLvlCD.Visible = false;
                    lblStatus.Visible = false;
                    ddlLevelID.Focus();
                }
            }
        }

        protected void ddlLevelID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                if (ddlLevelID.Text == "1")
                {
                    txtHdName.Text = "";
                    txtHdName.Focus();
                    txtHdName.Visible = true;
                    txtLiabilty.Visible = false;
                    txtIncome.Visible = false;
                    txtExpen.Visible = false;
                    txtCode.Text = "";
                    lblAccTP.Text = "D";
                    GetCompletionList(prefixText, count, contextKey);
                    lblLvlID.Text = "";
                    gvDetails.Visible = false;
                }
                else if (ddlLevelID.Text == "2")
                {
                    txtLiabilty.Text = "";
                    txtHdName.Visible = false;
                    txtLiabilty.Focus();
                    txtLiabilty.Visible = true;
                    txtIncome.Visible = false;
                    txtExpen.Visible = false;
                    txtCode.Text = "";
                    lblAccTP.Text = "C";
                    lblLvlID.Text = "";
                    GetCompletionListL(prefixText, count, contextKey);
                    gvDetails.Visible = false;
                }
                else if (ddlLevelID.Text == "3")
                {
                    txtIncome.Text = "";
                    txtHdName.Visible = false;
                    txtLiabilty.Visible = false;
                    txtIncome.Focus();
                    txtIncome.Visible = true;
                    txtExpen.Visible = false;
                    txtCode.Text = "";
                    lblAccTP.Text = "C";
                    lblLvlID.Text = "";
                    GetCompletionListI(prefixText, count, contextKey);
                    gvDetails.Visible = false;
                }
                else if (ddlLevelID.Text == "4")
                {
                    txtExpen.Text = "";
                    txtHdName.Visible = false;
                    txtLiabilty.Visible = false;
                    txtIncome.Visible = false;
                    txtExpen.Focus();
                    txtExpen.Visible = true;
                    txtCode.Text = "";
                    lblAccTP.Text = "D";
                    lblLvlID.Text = "";
                    GetCompletionListE(prefixText, count, contextKey);
                    gvDetails.Visible = false;
                }
                else
                {
                    return;
                }
            }
        }


        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT ACCOUNTNM FROM GL_ACCHART WHERE ACCOUNTCD like '1%' and LEVELCD between 1 and 4 and ACCOUNTNM like '" + prefixText + "%'", conn);
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["ACCOUNTNM"].ToString());
            return CompletionSet.ToArray();
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListL(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT ACCOUNTNM FROM GL_ACCHART WHERE ACCOUNTCD like '2%' and LEVELCD between 1 and 4  and ACCOUNTNM like '" + prefixText + "%'", conn);
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["ACCOUNTNM"].ToString());
            return CompletionSet.ToArray();
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListI(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT ACCOUNTNM FROM GL_ACCHART WHERE ACCOUNTCD like '3%'  and LEVELCD between 1 and 4  and ACCOUNTNM like '" + prefixText + "%'", conn);
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["ACCOUNTNM"].ToString());
            return CompletionSet.ToArray();
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListE(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT ACCOUNTNM FROM GL_ACCHART WHERE ACCOUNTCD like '4%'  and LEVELCD between 1 and 4  and ACCOUNTNM like '" + prefixText + "%'", conn);
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["ACCOUNTNM"].ToString());
            return CompletionSet.ToArray();
        }

        protected void txtHdName_TextChanged(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                if (ddlLevelID.Text == "SELECT")
                {
                    Response.Write("<script>alert('Select Transaction Type?');</script>");
                }
                else
                {
                    if (txtHdName.Text != "")
                    {
                        Global.txtAdd(@"Select ACCOUNTCD from GL_ACCHART where ACCOUNTCD like '1%' AND ACCOUNTNM = '" + txtHdName.Text + "'", txtCode);
                    }
                    else
                        txtCode.Text = "";
                    lblLvlID.Visible = true;
                    Global.lblAdd(@"Select LEVELCD from GL_ACCHART where ACCOUNTNM='" + txtHdName.Text + "'", lblLvlID);
                    lblBotCode.Text = "";
                    lblBotCode.Text = (Convert.ToDecimal(lblLvlID.Text) + 1).ToString();
                    gvDetails.Visible = false;
                    btnSubmit.Focus();
                }
            }
        }

        protected void txtLiabilty_TextChanged(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                if (ddlLevelID.Text == "SELECT")
                {
                    Response.Write("<script>alert('Select Transaction Type?');</script>");
                }
                else
                {
                    if (txtLiabilty.Text != "")
                    {
                        Global.txtAdd(@"Select ACCOUNTCD from GL_ACCHART where ACCOUNTCD like '2%'  AND ACCOUNTNM = '" + txtLiabilty.Text + "'", txtCode);
                    }
                    else
                        txtCode.Text = "";
                    lblLvlID.Visible = true;
                    Global.lblAdd(@"Select LEVELCD from GL_ACCHART where ACCOUNTNM='" + txtLiabilty.Text + "'", lblLvlID);
                    lblBotCode.Text = "";
                    lblBotCode.Text = (Convert.ToDecimal(lblLvlID.Text) + 1).ToString();
                    gvDetails.Visible = false;
                    btnSubmit.Focus();
                }
            }
        }

        protected void txtIncome_TextChanged(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                if (ddlLevelID.Text == "SELECT")
                {
                    Response.Write("<script>alert('Select Transaction Type?');</script>");
                }
                else
                {
                    if (txtIncome.Text != "")
                    {
                        Global.txtAdd(@"Select ACCOUNTCD from GL_ACCHART where ACCOUNTCD like '3%'  AND ACCOUNTNM = '" + txtIncome.Text + "'", txtCode);
                    }
                    else
                        txtCode.Text = "";
                    lblLvlID.Visible = true;
                    Global.lblAdd(@"Select LEVELCD from GL_ACCHART where ACCOUNTNM='" + txtIncome.Text + "'", lblLvlID);
                    lblBotCode.Text = "";
                    lblBotCode.Text = (Convert.ToDecimal(lblLvlID.Text) + 1).ToString();
                    gvDetails.Visible = false;
                    btnSubmit.Focus();
                }
            }
        }

        protected void txtExpen_TextChanged(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                if (ddlLevelID.Text == "SELECT")
                {
                    Response.Write("<script>alert('Select Transaction Type?');</script>");
                }
                else
                {
                    if (txtExpen.Text != "")
                    {
                        Global.txtAdd(@"Select ACCOUNTCD from GL_ACCHART where ACCOUNTCD like '4%'  AND ACCOUNTNM = '" + txtExpen.Text + "' AND LEVELCD <> 5", txtCode);
                    }
                    else
                        txtCode.Text = "";
                    lblLvlID.Visible = true;
                    Global.lblAdd(@"Select LEVELCD from GL_ACCHART where ACCOUNTNM='" + txtExpen.Text + "' AND LEVELCD <> 5", lblLvlID);
                    lblBotCode.Text = "";
                    lblBotCode.Text = (Convert.ToDecimal(lblLvlID.Text) + 1).ToString();
                    gvDetails.Visible = false;
                    btnSubmit.Focus();
                }
            }
        }

        protected void BindEmployeeDetails()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            int level = Convert.ToInt16(lblLvlID.Text) + 1;

            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT GL_ACCHART.ACCOUNTCD, GL_ACCHART.ACCOUNTNM, GL_ACCHART.OPENINGDT, GL_ACCHART.LEVELCD, GL_ACCHART.CONTROLCD, GL_ACCHART.ACCOUNTTP, " +
                  " GL_ACCHART.BRANCHCD, GL_ACCHART.STATUSCD, GL_ACCHART.ACTIVE, GL_ACCHART.USERPC, GL_ACCHART.USERID, GL_ACCHART.INTIME, GL_ACCHART.IPADDRESS, GL_COSTPMST.CATNM " +
                  " FROM GL_ACCHART LEFT OUTER JOIN GL_COSTPMST ON GL_ACCHART.BRANCHCD = GL_COSTPMST.CATID WHERE GL_ACCHART.CONTROLCD='" + txtCode.Text + "' AND GL_ACCHART.LEVELCD='" + level + "'", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
                TextBox txtAccHead = (TextBox)gvDetails.FooterRow.FindControl("txtAccHead");
                txtAccHead.Focus();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
                int columncount = gvDetails.Rows[0].Cells.Count;
                gvDetails.Rows[0].Cells.Clear();
                gvDetails.Rows[0].Cells.Add(new TableCell());
                gvDetails.Rows[0].Cells[0].ColumnSpan = columncount;
                gvDetails.Rows[0].Cells[0].Text = "No Records Found";
                TextBox txtAccHead = (TextBox)gvDetails.FooterRow.FindControl("txtAccHead");
                txtAccHead.Focus();
            }
        }



        protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            ////getting username from particular row
            //string username = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "UserName"));
            ////identifying the control in gridview
            //ImageButton lnkbtnresult = (ImageButton)e.Row.FindControl("imgbtnDelete");
            ////raising javascript confirmationbox whenver user clicks on link button
            //if (lnkbtnresult != null)
            //{
            //    lnkbtnresult.Attributes.Add("onclick", "javascript:return ConfirmationBox('" + username + "')");
            //}
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (lblBotCode.Text == "5")
                {
                    e.Row.Cells[3].Enabled = true;
                }
                else
                {
                    e.Row.Cells[3].Enabled = false;
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                int level = Convert.ToInt16(lblLvlID.Text) + 1;
                Global.lblAdd(@"select MAX(ACCOUNTCD) from GL_ACCHART where LEVELCD='" + level + "' and CONTROLCD ='" + txtCode.Text + "'", lblMxAccCode);

                string conTrlCd = txtCode.Text;
                string mxCode;
                if (lblMxAccCode.Text == "")
                {
                    mxCode = txtCode.Text;
                }
                else
                {
                    mxCode = lblMxAccCode.Text;
                }

                string lvl2, lvl3, lvl4, lvl5, L2, L3, L4, L5, mid, accCode;
                int lv2, lv3, lv4, lv5, nLvlCode;
                int l2, l3, l4, l5;
                if (lblLvlID.Text == "1")
                {

                    lvl2 = mxCode.Substring(1, 2);
                    lv2 = int.Parse(lvl2);
                    l2 = lv2 + 1;
                    if (l2 < 10)
                    {
                        mid = l2.ToString();
                        L2 = "0" + mid;
                    }
                    else
                        L2 = l2.ToString();
                    lvl3 = mxCode.Substring(3, 2);
                    lv3 = int.Parse(lvl3);
                    l3 = lv3;
                    lvl4 = mxCode.Substring(5, 2);
                    lv4 = int.Parse(lvl4);
                    l4 = lv4;
                    lvl5 = mxCode.Substring(7, 5);
                    lv5 = int.Parse(lvl5);
                    l5 = lv5;

                    accCode = ddlLevelID.Text + L2 + lvl3 + lvl4 + lvl5;
                    e.Row.Cells[1].Text = accCode;
                    e.Row.Cells[2].Text = conTrlCd;
                    nLvlCode = 2;
                    lblNewLvlCD.Text = nLvlCode.ToString();
                    lblStatus.Text = "N"; ///status = Level 1 to 4 N or P
                }
                else if (lblLvlID.Text == "2")
                {
                    lvl2 = mxCode.Substring(1, 2);
                    lv2 = int.Parse(lvl2);
                    l2 = lv2;
                    lvl3 = mxCode.Substring(3, 2);
                    lv3 = int.Parse(lvl3);
                    l3 = lv3 + 1;
                    if (l3 < 10)
                    {
                        mid = l3.ToString();
                        L3 = "0" + mid;
                    }
                    else
                        L3 = l3.ToString();
                    lvl4 = mxCode.Substring(5, 2);
                    lv4 = int.Parse(lvl4);
                    l4 = lv4;
                    lvl5 = mxCode.Substring(7, 5);
                    lv5 = int.Parse(lvl5);
                    l5 = lv5;

                    accCode = ddlLevelID.Text + lvl2 + L3 + lvl4 + lvl5;
                    e.Row.Cells[1].Text = accCode;
                    e.Row.Cells[2].Text = conTrlCd;
                    nLvlCode = 3;
                    lblNewLvlCD.Text = nLvlCode.ToString();
                    lblStatus.Text = "N"; ///status = Level if 1 to 4 N or else P
                }
                else if (lblLvlID.Text == "3")
                {
                    lvl2 = mxCode.Substring(1, 2);
                    lv2 = int.Parse(lvl2);
                    l2 = lv2;
                    lvl3 = mxCode.Substring(3, 2);
                    lvl4 = mxCode.Substring(5, 2);
                    lv4 = int.Parse(lvl4);
                    l4 = lv4 + 1;
                    if (l4 < 10)
                    {
                        mid = l4.ToString();
                        L4 = "0" + mid;
                    }
                    else
                        L4 = l4.ToString();
                    lvl5 = mxCode.Substring(7, 5);
                    lv5 = int.Parse(lvl5);
                    l5 = lv5;

                    accCode = ddlLevelID.Text + lvl2 + lvl3 + L4 + lvl5;
                    e.Row.Cells[1].Text = accCode;
                    e.Row.Cells[2].Text = conTrlCd;
                    nLvlCode = 4;
                    lblNewLvlCD.Text = nLvlCode.ToString();
                    lblStatus.Text = "N"; ///status = Level if 1 to 4 N or else P
                }
                else if (lblLvlID.Text == "4")
                {
                    lvl2 = mxCode.Substring(1, 2);
                    lv2 = int.Parse(lvl2);
                    l2 = lv2;
                    lvl3 = mxCode.Substring(3, 2);
                    lvl4 = mxCode.Substring(5, 2);

                    lvl5 = mxCode.Substring(7, 5);
                    lv5 = int.Parse(lvl5);
                    l5 = lv5 + 1;
                    if (l5 < 10)
                    {
                        mid = l5.ToString();
                        L5 = "0000" + mid;
                    }
                    else if (l5 < 100)
                    {
                        mid = l5.ToString();
                        L5 = "000" + mid;
                    }
                    else if (l5 < 1000)
                    {
                        mid = l5.ToString();
                        L5 = "00" + mid;
                    }
                    else if (l5 < 10000)
                    {
                        mid = l5.ToString();
                        L5 = "0" + mid;
                    }
                    //else if (l5 < 11110)
                    //{
                    //    mid = l5.ToString();
                    //    L5 = "0000" + mid;
                    //}
                    //else if (l5 < 11100)
                    //{
                    //    mid = l5.ToString();
                    //    L5 = "000" + mid;
                    //}
                    //else if (l5 < 11000)
                    //{
                    //    mid = l5.ToString();
                    //    L5 = "00" + mid;
                    //}
                    //else if (l5 < 10000)
                    //{
                    //    mid = l5.ToString();
                    //    L5 = "0" + mid;
                    //}
                    else
                        L5 = l5.ToString();
                    accCode = ddlLevelID.Text + lvl2 + lvl3 + lvl4 + L5;
                    e.Row.Cells[1].Text = accCode;
                    e.Row.Cells[2].Text = conTrlCd;
                    nLvlCode = 5;
                    lblNewLvlCD.Text = nLvlCode.ToString();
                    lblStatus.Text = "P"; ///status = Level if 1 to 4 N or else P
                }

                if (lblBotCode.Text == "5")
                {
                    e.Row.Cells[3].Enabled = true;
                }
                else
                {
                    e.Row.Cells[3].Enabled = false;
                }

                DropDownList ddlAccess = (DropDownList)e.Row.FindControl("ddlAccess");
                Global.dropDownAddWithSelect(ddlAccess, "SELECT CATNM FROM GL_COSTPMST ORDER BY CATID");
            }
        }

        protected void ddlAccess_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((DropDownList)sender).NamingContainer);
            DropDownList ddlAccess = (DropDownList)row.FindControl("ddlAccess");
            string brNM = ddlAccess.Text;
            Label lblAccessCDFot = (Label)row.FindControl("lblAccessCDFot");
            Global.lblAdd("SELECT CATID FROM GL_COSTP WHERE COSTPNM ='" + brNM + "'", lblAccessCDFot);
            ImageButton imgbtnAdd = (ImageButton)row.FindControl("imgbtnAdd");
            imgbtnAdd.Focus();
        }

        protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            string query = "";
            SqlCommand comm = new SqlCommand(query, conn);


            DateTime openDT = DateTime.Now;
            int levelCD = Convert.ToInt16(lblNewLvlCD.Text);
            string AccCode, ControlCode;

            if (e.CommandName.Equals("AddNew"))
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("~/Login/UI/Login.aspx");
                }
                else
                {
                    TextBox txtAccHead = (TextBox)gvDetails.FooterRow.FindControl("txtAccHead");
                    txtAccHead.Focus();
                    AccCode = gvDetails.FooterRow.Cells[1].Text;
                    ControlCode = gvDetails.FooterRow.Cells[2].Text;
                    Label lblAccessCDFot = (Label)gvDetails.FooterRow.FindControl("lblAccessCDFot");

                    AlchemyAccounting.Accounts.Interface.SingleVoucher iob = new Interface.SingleVoucher();

                    iob.Userpc = HttpContext.Current.Session["PCName"].ToString();
                    iob.Username = HttpContext.Current.Session["UserName"].ToString();
                    iob.InTM = DateTime.Now;
                    iob.Ipaddress = HttpContext.Current.Session["IpAddress"].ToString();
                    

                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Select CONTROLCD from GL_ACCHARTMST where CONTROLCD='" + txtCode.Text + "'", conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    conn.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        query = ("insert into GL_ACCHART (ACCOUNTCD, ACCOUNTNM, OPENINGDT, LEVELCD, CONTROLCD, ACCOUNTTP, BRANCHCD, STATUSCD, ACTIVE, USERPC, USERID, INTIME, IPADDRESS) " +
                                "values(@AccCode,'" + txtAccHead.Text + "',@OPENINGDT,@LEVELCD,@ControlCode,'" + lblAccTP.Text + "', '" + lblAccessCDFot.Text + "','" + lblStatus.Text + "','A', @USERPC, @USERID, @INTIME, @IPADDRESS)");

                        comm = new SqlCommand(query, conn);
                        comm.Parameters.AddWithValue("@AccCode", AccCode);
                        comm.Parameters.AddWithValue("@ControlCode", ControlCode);
                        comm.Parameters.AddWithValue("@OPENINGDT", openDT);
                        comm.Parameters.AddWithValue("@LEVELCD", levelCD);
                        comm.Parameters.AddWithValue("@USERPC", iob.Userpc);
                        comm.Parameters.AddWithValue("@USERID", iob.Username);
                        comm.Parameters.AddWithValue("@INTIME", iob.InTM);
                        comm.Parameters.AddWithValue("@IPADDRESS", iob.Ipaddress);

                        conn.Open();
                        int result = comm.ExecuteNonQuery();
                        conn.Close();
                        BindEmployeeDetails();
                        //if (result == 1)
                        //{
                        //    Response.Write("<script>alert('Successfully Saved');</script>");
                        //    
                        //}
                        //else
                        //{
                        //    Response.Write("<script>alert('Data not Saved');</script>");
                        //}
                    }
                    else
                    {

                        query = "insert into GL_ACCHARTMST (CONTROLCD, USERPC, USERID, INTIME, IPADDRESS) " +
                                "values(@CONTROLCD,@USERPC, @USERID, @INTIME, @IPADDRESS)";
                        comm = new SqlCommand(query, conn);
                        comm.Parameters.AddWithValue("@CONTROLCD", ControlCode);
                        comm.Parameters.AddWithValue("@USERPC", iob.Userpc);
                        comm.Parameters.AddWithValue("@USERID", iob.Username);
                        comm.Parameters.AddWithValue("@INTIME", iob.InTM);
                        comm.Parameters.AddWithValue("@IPADDRESS", iob.Ipaddress);
                        conn.Open();
                        comm.ExecuteNonQuery();
                        conn.Close();

                        query = ("insert into GL_ACCHART (ACCOUNTCD, ACCOUNTNM, OPENINGDT, LEVELCD, CONTROLCD, ACCOUNTTP, STATUSCD, ACTIVE, USERPC, USERID, INTIME, IPADDRESS) " +
                               "values(@AccCode,'" + txtAccHead.Text + "',@OPENINGDT,@LEVELCD,@ControlCode,'" + lblAccTP.Text + "','" + lblStatus.Text + "','A',@USERPC, @USERID, @INTIME, @IPADDRESS)");

                        comm = new SqlCommand(query, conn);
                        comm.Parameters.AddWithValue("@AccCode", AccCode);
                        comm.Parameters.AddWithValue("@ControlCode", ControlCode);
                        comm.Parameters.AddWithValue("@OPENINGDT", openDT);
                        comm.Parameters.AddWithValue("@LEVELCD", levelCD);
                        comm.Parameters.AddWithValue("@USERPC", iob.Userpc);
                        comm.Parameters.AddWithValue("@USERID", iob.Username);
                        comm.Parameters.AddWithValue("@INTIME", iob.InTM);
                        comm.Parameters.AddWithValue("@IPADDRESS", iob.Ipaddress);

                        conn.Open();
                        int result = comm.ExecuteNonQuery();
                        conn.Close();
                        BindEmployeeDetails();
                        //Response.Write("<script>alert('Successfully Saved');</script>");
                        //if (result == 1)
                        //{
                        //    Response.Write("<script>alert('Successfully Saved');</script>");

                        //}
                        //else
                        //{
                        //    Response.Write("<script>alert('Data not Saved');</script>");
                        //}
                    }
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                if (ddlLevelID.Text == "Select")
                {
                    Response.Write("<script>alert('Select Transaction Type?');</script>");
                }
                else if (txtHdName.Text == "" && txtLiabilty.Text == "" && txtIncome.Text == "" && txtExpen.Text == "")
                {
                    Response.Write("<script>alert('Type Account Head?');</script>");
                }
                else
                {
                    BindEmployeeDetails();
                    gvDetails.Visible = true;
                }
            }
        }

        protected void gvDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDetails.EditIndex = e.NewEditIndex;
            BindEmployeeDetails();

            DropDownList ddlAccessEdit = (DropDownList)gvDetails.Rows[e.NewEditIndex].FindControl("ddlAccessEdit");
            Global.dropDownAddWithSelect(ddlAccessEdit, "SELECT CATNM FROM GL_COSTPMST ORDER BY CATID");
            TextBox txtAccCode = (TextBox)gvDetails.Rows[e.NewEditIndex].FindControl("txtAccCode");
            lblBRCD.Text = "";
            Global.lblAdd("SELECT GL_COSTP.COSTPNM FROM GL_ACCHART INNER JOIN GL_COSTP ON GL_ACCHART.BRANCHCD = GL_COSTP.CATID WHERE GL_ACCHART.ACCOUNTCD='" + txtAccCode.Text + "'", lblBRCD);
            ddlAccessEdit.Text = lblBRCD.Text;

            TextBox txtAccHead = (TextBox)gvDetails.Rows[e.NewEditIndex].FindControl("txtAccHead");
            txtAccHead.Focus();
        }

        protected void ddlAccessEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((DropDownList)sender).NamingContainer);
            DropDownList ddlAccessEdit = (DropDownList)row.FindControl("ddlAccessEdit");
            string brNM = ddlAccessEdit.Text;
            Label lblAccessCDEdit = (Label)row.FindControl("lblAccessCDEdit");
            Global.lblAdd("SELECT CATID FROM GL_COSTP WHERE COSTPNM ='" + brNM + "'", lblAccessCDEdit);
            ImageButton imgbtnUpdate = (ImageButton)row.FindControl("imgbtnUpdate");
            imgbtnUpdate.Focus();
        }

        protected void gvDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
                SqlConnection conn = new SqlConnection(connectionString);
                string userName = HttpContext.Current.Session["UserName"].ToString();

                TextBox txtAccHead = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtAccHead");
                TextBox AccCode = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtAccCode");
                TextBox ControlCode = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtContolCode");
                Label lblAccessCDEdit = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblAccessCDEdit");

                AlchemyAccounting.Accounts.Interface.SingleVoucher iob = new Interface.SingleVoucher();

                iob.Username = HttpContext.Current.Session["UserName"].ToString();
                iob.UpTM = DateTime.Now;
                iob.Ipaddress = HttpContext.Current.Session["IpAddress"].ToString();

                conn.Open();
                SqlCommand cmd = new SqlCommand("update GL_ACCHART set ACCOUNTNM='" + txtAccHead.Text + "', BRANCHCD ='" + lblAccessCDEdit.Text + "', UPDATEUSERID = @UPDATEUSERID, UPDATETIME =@UPDATETIME, UPDATEIPADDRESS =@UPDATEIPADDRESS  where ACCOUNTCD = '" + AccCode.Text + "' and CONTROLCD = '" + ControlCode.Text + "'", conn);

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@UPDATEUSERID", iob.Username);
                cmd.Parameters.AddWithValue("@UPDATETIME", iob.UpTM);
                cmd.Parameters.AddWithValue("@UPDATEIPADDRESS", iob.Ipaddress);

                cmd.ExecuteNonQuery();
                conn.Close();
                //Response.Write("<script>alert('Successfully Updated');</script>");
                //lblresult.ForeColor = Color.Green;
                //lblresult.Text = "Details Updated successfully";
                gvDetails.EditIndex = -1;
                BindEmployeeDetails();
            }
        }

        protected void gvDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDetails.EditIndex = -1;
            BindEmployeeDetails();
        }

        protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
                SqlConnection conn = new SqlConnection(connectionString);
                //int userid = Convert.ToInt32(gvDetails.DataKeys[e.RowIndex].Values["UserId"].ToString());
                //string username = gvDetails.DataKeys[e.RowIndex].Values["UserName"].ToString();

                Label AccCode = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblAcountCode");
                Label ControlCode = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblControlCode");

                string accCd = AccCode.Text;
                string accSubCD2nd = accCd.Substring(0, 3);
                string accSubCD3rd = accCd.Substring(0, 5);
                string accSubCD4th = accCd.Substring(0, 7);

                //Global.lblAdd(@"select CONTROLCD from GL_ACCHARTMST where CONTROLCD='" + AccCode.Text + "'", lblDelCtrlCD);
                Global.lblAdd(@"select LEVELCD from  GL_ACCHART where ACCOUNTCD='" + AccCode.Text + "'", lblSelLvlCD);
                Global.lblAdd(@"select (LEVELCD+1)as LEVELCD from GL_ACCHART where LEVELCD='" + lblSelLvlCD.Text + "' and LEVELCD<>5", lblIncrLevel);

                conn.Open();
                SqlCommand cmd1 = new SqlCommand();

                if (lblLvlID.Text == "1")
                {
                    cmd1 = new SqlCommand("select DEBITCD from GL_MASTER where DEBITCD like '" + accSubCD2nd + "%'", conn);
                }

                else if (lblLvlID.Text == "2")
                {
                    cmd1 = new SqlCommand("select DEBITCD from GL_MASTER where DEBITCD like '" + accSubCD3rd + "%'", conn);
                }
                else if (lblLvlID.Text == "3")
                {
                    cmd1 = new SqlCommand("select DEBITCD from GL_MASTER where DEBITCD like '" + accSubCD4th + "%'", conn);
                }
                else if (lblLvlID.Text == "4")
                {
                    cmd1 = new SqlCommand("select DEBITCD from GL_MASTER where DEBITCD = '" + AccCode.Text + "'", conn);
                }

                SqlDataAdapter chk = new SqlDataAdapter(cmd1);
                DataSet ch = new DataSet();
                chk.Fill(ch);
                conn.Close();

                if (ch.Tables[0].Rows.Count > 0)
                {
                    Response.Write("<script>alert('This Account Head have Child Data.');</script>");
                }
                else
                {
                    //if (lblIncrLevel.Text == "")
                    //{
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM GL_ACCHARTMST INNER JOIN GL_ACCHART ON GL_ACCHARTMST.CONTROLCD = GL_ACCHART.CONTROLCD WHERE (GL_ACCHARTMST.CONTROLCD = '" + accCd + "')", conn);
                    SqlDataAdapter chcekCntrCD = new SqlDataAdapter(cmd);
                    DataSet check = new DataSet();
                    chcekCntrCD.Fill(check);

                    if (check.Tables[0].Rows.Count > 0)
                    {
                        Response.Write("<script>alert('This Account Head have Child Data.');</script>");
                    }
                    else
                    
                    {
                        string ACCOUNTCD = "";
                        string ACCOUNTNM = "";
                        string OPENINGDT = "";
                        string LEVELCD = "";
                        string CONTROLCD = "";
                        string ACCOUNTTP = "";
                        string BRANCHCD = "";
                        string STATUSCD = "";
                        string ACTIVE = "";
                        string USERPC = "";
                        string USERID = "";
                        string UPDATEUSERID = "";
                        string INTIME = "";
                        string UPDATETIME = "";
                        string IPADDRESS = "";
                        string UPDATEIPADDRESS = "";
                   
                        string userName = HttpContext.Current.Session["UserName"].ToString();
                        string userpc = HttpContext.Current.Session["PCName"].ToString(); ;
                        string ipadd = HttpContext.Current.Session["IpAddress"].ToString();
                   
                        

                        SqlCommand cmdselectdata = new SqlCommand("SELECT * FROM GL_ACCHART where ACCOUNTCD = '" + AccCode.Text + "' and LEVELCD = '" + lblSelLvlCD.Text + "'", conn);

                        SqlDataReader dr = cmdselectdata.ExecuteReader();
                        while (dr.Read())
                        {
                            ACCOUNTCD = dr["ACCOUNTCD"].ToString();
                            ACCOUNTNM = dr["ACCOUNTNM"].ToString();
                            OPENINGDT = dr["OPENINGDT"].ToString();
                            LEVELCD = dr["LEVELCD"].ToString();
                            CONTROLCD = dr["CONTROLCD"].ToString();
                            ACCOUNTTP = dr["ACCOUNTTP"].ToString();
                            BRANCHCD = dr["BRANCHCD"].ToString();
                            STATUSCD = dr["STATUSCD"].ToString();
                            ACTIVE = dr["ACTIVE"].ToString();
                            USERPC = dr["USERPC"].ToString();
                            USERID = dr["USERID"].ToString();
                            UPDATEUSERID = dr["UPDATEUSERID"].ToString();
                            INTIME = dr["INTIME"].ToString();
                            UPDATETIME = dr["UPDATETIME"].ToString();
                            IPADDRESS = dr["IPADDRESS"].ToString();
                            UPDATEIPADDRESS = dr["UPDATEIPADDRESS"].ToString();
                            
                        }
                        dr.Close();

                        string alldata = ACCOUNTCD + ", " + ACCOUNTNM + ", " + OPENINGDT + ", " + LEVELCD + ", " + CONTROLCD + ", " + ACCOUNTTP
                            + ", " + BRANCHCD + ", " + STATUSCD + ", " + ACTIVE + ", " + USERPC + ", " + USERID + ", " + UPDATEUSERID + ", " + INTIME + ", " + UPDATETIME
                            + ", " + IPADDRESS + ", " + UPDATEIPADDRESS;

                        InTM = DateTime.Now;

                        SqlCommand cmdinsert = new SqlCommand("insert into ASL_DLT values('GL_ACCHART',@DESCRP,@USERPC,@USERID,@INTIME,@IPADD)", conn);
                        cmdinsert.Parameters.AddWithValue("@DESCRP", alldata);
                        cmdinsert.Parameters.AddWithValue("@USERPC", userpc);
                        cmdinsert.Parameters.AddWithValue("@USERID", userName);
                        cmdinsert.Parameters.AddWithValue("@INTIME", InTM);
                        cmdinsert.Parameters.AddWithValue("@IPADD", ipadd);

                        cmdinsert.ExecuteNonQuery();

                        SqlCommand cmd2 = new SqlCommand("delete FROM GL_ACCHART where ACCOUNTCD = '" + AccCode.Text + "' and LEVELCD = '" + lblSelLvlCD.Text + "'", conn);
                        int result = cmd2.ExecuteNonQuery();
                        SqlCommand cmd3 = new SqlCommand("delete FROM GL_ACCHARTMST where CONTROLCD = '" + AccCode.Text + "' ", conn);
                        int result1 = cmd3.ExecuteNonQuery();
                        conn.Close();

                        if (result == 1)
                        {
                            //Response.Write("<script>alert('Successfully Deleted.');</script>");
                            BindEmployeeDetails();
                        }
                    }
                    //}
                    //else
                    //{
                    //    Response.Write("<script>alert('This Account Head have Child Data.');</script>");
                    //}
                }
            }


        }

    }
}