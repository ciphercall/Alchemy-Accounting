using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace AlchemyAccounting.CNF.JobStatus
{
    public partial class JobStatus : System.Web.UI.Page
    {

        SqlConnection conn;
        SqlCommand cmdd;

        JobStatusDataAccess jsda = new JobStatusDataAccess();
        JobStatusModel jsm = new JobStatusModel();

        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["UserName"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                if (!IsPostBack)
                {
                    txtCompNM.Focus();
                    Session["jobtp"] = null;
                    Session["jobtp"] = ddlJobTP.Text;
                    //GridShow();
                }
            }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            if (txtCompNM.Text == "")
            {
                gvDetails.Visible = false;
                lblError.Visible = true;
                lblError.Text = "";
                lblError.Text = "Insert Company Name.";

            }
            else if (txtJobYR.Text == "")
            {
                gvDetails.Visible = false;

                Erryear.Visible = true;
                Erryear.Text = "";
                Erryear.Text = "Select Year.";
                txtJobYR.Focus();

            }
            else
            {
                Erryear.Visible = false;
                lblError.Visible = false;
                GridShow();
                Erryear.Text = "";
                lblError.Text = "";
            }


        }


        private void GridShow()
        {
            conn = new SqlConnection(Global.connection);
            conn.Open();


            Int64 jobyr = 0;

            if (txtJobYR.Text == "")
                jobyr = 0;
            else
                jobyr = Convert.ToInt64(txtJobYR.Text);


            if (txtBilldt.Text == "")
            {
                cmdd = new SqlCommand("SELECT CNF_JOB.PARTYID, GL_ACCHART.ACCOUNTNM AS PARTYNM, CONVERT(NVARCHAR(20),CNF_JOBSTATUS.TRANSDT,103) AS TRANSD  , CNF_JOBSTATUS.TRANSNO, CNF_JOBSTATUS.JOBNO, CNF_JOBSTATUS.COMPID,(CASE WHEN BILLFDT ='1900-01-01' THEN NULL ELSE CONVERT(NVARCHAR(20),CNF_JOBSTATUS.BILLFDT ,103) END) AS BILLFD, (CASE WHEN CNF_JOBSTATUS.STATUS = 'P' THEN 'PENDING'  ELSE 'COMPLETED'  END) AS STATUS , CNF_JOBSTATUS.TRANSYY" +
                                " FROM CNF_JOBSTATUS INNER JOIN " +
                          " CNF_JOB ON CNF_JOBSTATUS.JOBTP = CNF_JOB.JOBTP AND CNF_JOBSTATUS.JOBYY = CNF_JOB.JOBYY AND " +
                          " CNF_JOBSTATUS.JOBNO = CNF_JOB.JOBNO INNER JOIN " +
                          " GL_ACCHART ON CNF_JOB.PARTYID = GL_ACCHART.ACCOUNTCD " +
                " WHERE CNF_JOBSTATUS.COMPID='" + txtCompID.Text + "' AND CNF_JOBSTATUS.JOBYY=" + jobyr + " AND CNF_JOBSTATUS.JOBTP='" + ddlJobTP.Text + "' ", conn);
            }
            else
            {
                DateTime FRDT = DateTime.Parse(txtBilldt.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string FDT = FRDT.ToString("yyyy-MM-dd");

                cmdd = new SqlCommand("SELECT CNF_JOB.PARTYID, GL_ACCHART.ACCOUNTNM AS PARTYNM, CONVERT(NVARCHAR(20),CNF_JOBSTATUS.TRANSDT,103) AS TRANSD , CNF_JOBSTATUS.TRANSNO, CNF_JOBSTATUS.JOBNO, CNF_JOBSTATUS.COMPID, (CASE WHEN BILLFDT ='1900-01-01' THEN NULL ELSE CONVERT(NVARCHAR(20),CNF_JOBSTATUS.BILLFDT ,103) END) AS BILLFD, (CASE WHEN CNF_JOBSTATUS.STATUS = 'P' THEN 'PENDING'  ELSE 'COMPLETED'  END) AS STATUS, CNF_JOBSTATUS.TRANSYY" +
                                " FROM CNF_JOBSTATUS INNER JOIN " +
                          " CNF_JOB ON CNF_JOBSTATUS.JOBTP = CNF_JOB.JOBTP AND CNF_JOBSTATUS.JOBYY = CNF_JOB.JOBYY AND " +
                          " CNF_JOBSTATUS.JOBNO = CNF_JOB.JOBNO INNER JOIN " +
                          " GL_ACCHART ON CNF_JOB.PARTYID = GL_ACCHART.ACCOUNTCD " +
                " WHERE CNF_JOBSTATUS.COMPID='" + txtCompID.Text + "' AND CNF_JOBSTATUS.JOBYY=" + jobyr + " AND CNF_JOBSTATUS.JOBTP='" + ddlJobTP.Text + "' AND  CNF_JOBSTATUS.TRANSDT='" + FDT + "'", conn);
            }
            SqlDataAdapter da = new SqlDataAdapter(cmdd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();


            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
                gvDetails.Visible = true;

                TextBox txtBillDate = (TextBox)gvDetails.FooterRow.FindControl("txtBillDate");
                txtBillDate.Focus();

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
                gvDetails.Rows[0].Visible = false;

            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListCompanyName(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("", conn);

            string uTp = HttpContext.Current.Session["UserTp"].ToString();
            string brCD = HttpContext.Current.Session["BrCD"].ToString();

            cmd.CommandType = CommandType.Text;
            if (uTp == "ADMIN")
            {
                cmd.CommandText = ("SELECT (BRANCHID + '-' + COMPNM) AS COMPNM FROM ASL_COMPANY WHERE (BRANCHID + '-' + COMPNM) LIKE '" + prefixText + "%'");
            }
            else
            {
                cmd.CommandText = ("SELECT (BRANCHID + '-' + COMPNM) AS COMPNM FROM ASL_COMPANY WHERE (BRANCHID + '-' + COMPNM) LIKE '" + prefixText + "%' AND COMPID ='" + brCD + "'");
            }

            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["COMPNM"].ToString());
            return CompletionSet.ToArray();

        }



        protected void txtCompNM_TextChanged(object sender, EventArgs e)
        {
            if (txtCompNM.Text == "")
            {
                gvDetails.Visible = false;
                lblError.Visible = true;
                lblError.Text = "Select Company.";
                txtCompNM.Focus();
            }
            else
            {
                string compadd = "";
                string compnm = "";
                string searchPar = txtCompNM.Text;

                int splitter = searchPar.IndexOf("-");
                if (splitter != -1)
                {
                    string[] lineSplit = searchPar.Split('-');

                    compadd = lineSplit[0];
                    compnm = lineSplit[1];

                }

                txtCompID.Text = "";
                Global.txtAdd("SELECT COMPID FROM ASL_COMPANY WHERE BRANCHID ='" + compadd + "'", txtCompID);

                Session["compid"] = txtCompID.Text;

                if (txtCompID.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select Company.";
                    txtCompNM.Text = "";
                    txtCompNM.Focus();
                }
                else
                {
                    lblError.Visible = false;
                    gvDetails.Visible = false;
                    ddlJobTP.Focus();
                }
            }
        }


        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListYear(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("", conn);

            string uTp = HttpContext.Current.Session["UserTp"].ToString();
            string brCD = HttpContext.Current.Session["BrCD"].ToString();



            cmd.CommandType = CommandType.Text;
            if (uTp == "ADMIN")
            {
                cmd.CommandText = ("SELECT DISTINCT JOBYY FROM CNF_JOB WHERE JOBYY LIKE '" + prefixText + "%'");
            }
            else
            {
                cmd.CommandText = ("SELECT DISTINCT JOBYY FROM CNF_JOB WHERE JOBYY LIKE '" + prefixText + "%' AND COMPID ='" + brCD + "'");
            }

            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["JOBYY"].ToString());
            return CompletionSet.ToArray();

        }
        protected void txtBilldt_TextChanged(object sender, EventArgs e)
        {
            //TextBox txtBillDate = (TextBox)gvDetails.FooterRow.FindControl("txtBillDate");
            //txtBillDate.Focus();
            gvDetails.Visible = false;
            btnShow.Focus();


        }

        protected void ddlJobTP_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["jobtp"] = null;
            Session["jobtp"] = ddlJobTP.Text;
            gvDetails.Visible = false;
            txtJobYR.Focus();
        }

        protected void txtJobYR_TextChanged(object sender, EventArgs e)
        {

            if (txtJobYR.Text == "")
            {
                gvDetails.Visible = false;
                Erryear.Visible = true;
                Erryear.Text = "Select Year.";
                txtJobYR.Focus();
            }
            else
            {
                Session["jobyr"] = null;
                Session["jobyr"] = txtJobYR.Text;
                Erryear.Visible = false;
                gvDetails.Visible = false;
                txtBilldt.Focus();
            }
        }


        public void Refresh()
        {

        }

        protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            TextBox txtBillDate = (TextBox)gvDetails.FooterRow.FindControl("txtBillDate");
            Label txtyear = (Label)gvDetails.FooterRow.FindControl("txtyear");
            TextBox txtJobNo = (TextBox)gvDetails.FooterRow.FindControl("txtJobNo");
            Label txtPrtyID = (Label)gvDetails.FooterRow.FindControl("txtPrtyID");
            Label txtPrtyNM = (Label)gvDetails.FooterRow.FindControl("txtPrtyNM");

            TextBox txtFwdDate = (TextBox)gvDetails.FooterRow.FindControl("txtFwdDate");
            DropDownList ddlStatus = (DropDownList)gvDetails.FooterRow.FindControl("ddlStatus");



            if (e.CommandName.Equals("SaveCon"))
            {

                if (txtCompID.Text == "")
                {
                    lblErrMsgExist.Visible = true;
                    lblErrMsgExist.Text = "particular input missing";
                    txtCompNM.Focus();
                }
                else if (txtCompID.Text == "")
                {
                    lblErrMsgExist.Visible = true;
                    lblErrMsgExist.Text = "particular input missing";
                    txtCompNM.Focus();
                }

                else
                {

                    jsm.TRANSDT = DateTime.Parse(txtBillDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    jsm.TRANSYY = Convert.ToInt64(txtyear.Text);
                    //jsm.TRANSNO = Convert.ToInt64(gvDetails.FooterRow.Cells[2].Text);
                    jsm.COMPID = txtCompID.Text;
                    jsm.JOBYY = Convert.ToInt64(txtJobYR.Text);
                    jsm.JOBTP = ddlJobTP.Text;
                    jsm.JOBNO = Convert.ToInt64(txtJobNo.Text);
                    if (txtFwdDate.Text == "")
                        txtFwdDate.Text = "1900-01-01";
                    jsm.BILLFDT = DateTime.Parse(txtFwdDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    jsm.STATUS = Convert.ToChar(ddlStatus.Text);
                    jsm.InTime = DateTime.Now;
                    jsm.UpdateTime = DateTime.Now;
                    jsm.UserPC = HttpContext.Current.Session["PCName"].ToString();
                    jsm.UserNm = HttpContext.Current.Session["UserName"].ToString();
                    jsm.IPAddress = HttpContext.Current.Session["IpAddress"].ToString();

                    lblChkInternalID.Text = "";
                    Global.lblAdd(" select MAX (TRANSNO) from CNF_JOBSTATUS where TRANSYY=" + txtyear.Text + " ", lblChkInternalID);

                    if (lblChkInternalID.Text == "")
                    {
                        string cid = "1";
                        gvDetails.FooterRow.Cells[2].Text = cid;
                        jsm.TRANSNO = Convert.ToInt64(gvDetails.FooterRow.Cells[2].Text);
                    }

                    else
                    {
                        var id = Int32.Parse(lblChkInternalID.Text) + 1;
                        gvDetails.FooterRow.Cells[2].Text = id.ToString();
                        jsm.TRANSNO = Convert.ToInt64(gvDetails.FooterRow.Cells[2].Text);
                    }


                    jsda.SaveJobStatus(jsm);

                    GridShow();

                }

                txtBillDate.Focus();
            }
        }


        protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    Label lblStatus = (Label)e.Row.FindControl("lblStatus");
            //    Button btnprocess =(Button)e.Row.FindControl("btnprocess");
            //    if (lblStatus.Text == "PENDING")
            //        btnprocess.Enabled = false;
            //    else
            //        btnprocess.Enabled = true;
            //}
            //else 
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                TextBox txtBillDate = (TextBox)e.Row.FindControl("txtBillDate");
                Label txtyear = (Label)e.Row.FindControl("txtyear");
                TextBox txtFwdDate = (TextBox)e.Row.FindControl("txtFwdDate");

                DateTime today = DateTime.Now;
                string td = Global.Dayformat(today);
                txtBillDate.Text = td;

                txtFwdDate.Text = td;

                string mon = today.ToString("MMM").ToUpper();
                string year = today.ToString("yyyy");

                txtyear.Text = year;


                //Int64 trnsy = 0;
                //if (txtyear.Text == "")
                //    trnsy = 0;
                //else
                //    trnsy = Convert.ToInt64(txtyear.Text);


                Global.lblAdd(" select MAX (TRANSNO) from CNF_JOBSTATUS where TRANSYY=" + txtyear.Text + " ", lblChkInternalID);

                if (lblChkInternalID.Text == "")
                {
                    string cid = "1";
                    e.Row.Cells[2].Text = cid;
                }

                else
                {
                    var id = Int32.Parse(lblChkInternalID.Text) + 1;
                    e.Row.Cells[2].Text = id.ToString();
                }

                txtBillDate.Focus();
            }

        }

        protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Label lblBillNo = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblBillNo");
            Label lblBillDate = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblBillDate");
            Label lblyear = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblyear");
            Label lblJobNo = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblJobNo");
            Label lblPrtyID = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblPrtyID");
            Label lblPrtyNM = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblPrtyNM");
            Label lblFwdDate = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblFwdDate");
            Label lblStatus = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblStatus");

            jsm.TRANSDT = DateTime.Parse(lblBillDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            jsm.TRANSYY = Convert.ToInt64(lblyear.Text);
            jsm.TRANSNO = Convert.ToInt64(lblBillNo.Text);
            jsm.COMPID = txtCompID.Text;
            jsm.JOBYY = Convert.ToInt64(txtJobYR.Text);
            jsm.JOBTP = ddlJobTP.Text;
            jsm.JOBNO = Convert.ToInt64(lblJobNo.Text);
            //jsm.BILLFDT = DateTime.Parse(lblFwdDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            //jsm.STATUS = Convert.ToChar(lblStatus.Text);
            //jsm.InTime = DateTime.Now;
            //jsm.UpdateTime = DateTime.Now;
            //jsm.UserPC = HttpContext.Current.Session["PCName"].ToString();
            //jsm.IPAddress = HttpContext.Current.Session["IpAddress"].ToString();

            jsda.DeleteJobStatus(jsm);

            gvDetails.EditIndex = -1;
            GridShow();

            TextBox txtBillDate = (TextBox)gvDetails.FooterRow.FindControl("txtBillDate");
            txtBillDate.Focus();
        }

        protected void gvDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDetails.EditIndex = -1;
            GridShow();
        }

        protected void gvDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDetails.EditIndex = e.NewEditIndex;
            GridShow();

        }

        protected void gvDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            Label lblBillNoEdit = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblBillNoEdit");
            Label lblBillDateEdit = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblBillDateEdit");
            Label lblyearEdit = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblyearEdit");
            Label txtJobNoEdit = (Label)gvDetails.Rows[e.RowIndex].FindControl("txtJobNoEdit");
            Label txtPrtyIDEdit = (Label)gvDetails.Rows[e.RowIndex].FindControl("txtPrtyIDEdit");
            Label txtPrtyNMEdit = (Label)gvDetails.Rows[e.RowIndex].FindControl("txtPrtyNMEdit");
            TextBox txtFwdDateEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtFwdDateEdit");

            DropDownList ddlStatusEdit = (DropDownList)gvDetails.Rows[e.RowIndex].FindControl("ddlStatusEdit");



            if (txtCompID.Text == "")
            {
                lblErrMsgExist.Visible = true;
                lblErrMsgExist.Text = "particular input missing";
                txtCompNM.Focus();
            }

            else
            {

                jsm.TRANSDT = DateTime.Parse(lblBillDateEdit.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                jsm.TRANSYY = Convert.ToInt64(lblyearEdit.Text);
                jsm.TRANSNO = Convert.ToInt64(lblBillNoEdit.Text);
                jsm.COMPID = txtCompID.Text;
                jsm.JOBYY = Convert.ToInt64(txtJobYR.Text);
                jsm.JOBTP = ddlJobTP.Text;
                jsm.JOBNO = Convert.ToInt64(txtJobNoEdit.Text);
                if (txtFwdDateEdit.Text == "")
                    txtFwdDateEdit.Text = "1900-01-01";
                jsm.BILLFDT = DateTime.Parse(txtFwdDateEdit.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                jsm.STATUS = Convert.ToChar(ddlStatusEdit.Text);
                jsm.InTime = DateTime.Now;
                jsm.UpdateTime = DateTime.Now;
                jsm.UserPC = HttpContext.Current.Session["PCName"].ToString();
                jsm.UpdateuserNm = HttpContext.Current.Session["UserName"].ToString();
                jsm.IPAddress = HttpContext.Current.Session["IpAddress"].ToString();


                jsda.UpdateJobStatus(jsm);

                gvDetails.EditIndex = -1;
                GridShow();

                TextBox txtBillDate = (TextBox)gvDetails.FooterRow.FindControl("txtBillDate");
                txtBillDate.Focus();

            }


            //TextBox txtBillDate = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtBillDate");
            //txtBillDate.Focus();
        }


        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListJob_No_Year_Type(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("", conn);

            string uTp = HttpContext.Current.Session["UserTp"].ToString();
            string brCD = HttpContext.Current.Session["compid"].ToString();

            string jobtp = HttpContext.Current.Session["jobtp"].ToString();
            Int64 jobyr = Convert.ToInt64(HttpContext.Current.Session["jobyr"].ToString());


            cmd.CommandType = CommandType.Text;
            //if (uTp == "ADMIN")
            //{
            //cmd.CommandText = ("SELECT (JOBNO + '|' + CONVERT(NVARCHAR(20),JOBYY,103) + '|' + JOBTP) AS JOBPAR FROM CNF_JOB WHERE (JOBNO + '|' + CONVERT(NVARCHAR(20),JOBYY,103) + '|' + JOBTP) LIKE '" + prefixText + "%'");
            cmd.CommandText = ("SELECT JOBNO FROM CNF_JOB WHERE JOBNO LIKE '" + prefixText + "%' AND COMPID ='" + brCD + "' AND JOBYY=" + jobyr + " AND JOBTP='" + jobtp + "' EXCEPT  SELECT JOBNO  FROM CNF_JOBSTATUS WHERE JOBNO  LIKE '" + prefixText + "%' AND JOBYY=" + jobyr + " AND JOBTP='" + jobtp + "' ");
            //}
            //else
            //{
            //    //cmd.CommandText = ("SELECT (JOBNO + '|' + CONVERT(NVARCHAR(20),JOBYY,103) + '|' + JOBTP) AS JOBPAR FROM CNF_JOB WHERE (JOBNO + '|' + CONVERT(NVARCHAR(20),JOBYY,103) + '|' + JOBTP) LIKE '" + prefixText + "%' AND COMPID ='" + brCD + "'");
            //    cmd.CommandText = ("SELECT JOBNO FROM CNF_JOB WHERE JOBNO  LIKE '" + prefixText + "%' AND COMPID ='" + brCD + "' AND JOBYY=" + jobyr + " AND JOBTP='" + jobtp + "' ");
            //}

            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["JOBNO"].ToString());
            return CompletionSet.ToArray();

        }

        protected void txtJobNo_TextChanged(object sender, EventArgs e)
        {

            TextBox txtJobNo = (TextBox)gvDetails.FooterRow.FindControl("txtJobNo");
            Label txtPrtyID = (Label)gvDetails.FooterRow.FindControl("txtPrtyID");
            Label txtPrtyNM = (Label)gvDetails.FooterRow.FindControl("txtPrtyNM");

            TextBox txtFwdDate = (TextBox)gvDetails.FooterRow.FindControl("txtFwdDate");

            if (txtJobNo.Text == "")
            {
                lblErrMsgExist.Visible = true;
                lblErrMsgExist.Text = "Select job no.";
                txtJobNo.Focus();
            }
            else
            {
                lblErrMsgExist.Visible = false;


                //Global.txtAdd("SELECT CONVERT(NVARCHAR(20),TRANSDT,103) AS TRANSD  FROM CNF_JOBSTATUS WHERE JOBNO =" + txtJobNo.Text + " AND JOBYY =" + txtJobYR.Text + " AND JOBTP ='" + ddlJobTP.Text + "'", txtPrtyID);

                Global.lblAdd("SELECT PARTYID FROM CNF_JOB WHERE JOBNO =" + txtJobNo.Text + " AND JOBYY =" + txtJobYR.Text + " AND JOBTP ='" + ddlJobTP.Text + "' AND COMPID ='" + txtCompID.Text + "'", txtPrtyID);
                if (txtPrtyID.Text == "")
                {
                    lblErrMsgExist.Visible = true;
                    lblErrMsgExist.Text = "Party name missing.";
                    txtJobNo.Focus();
                }
                else
                {
                    Global.lblAdd("SELECT ACCOUNTNM FROM GL_ACCHART WHERE ACCOUNTCD = '" + txtPrtyID.Text + "' ", txtPrtyNM);
                    txtFwdDate.Focus();
                }

                //lblErrMsgExist.Text = "";
                //lblErrMsgExist.Text = "Job No didn't matches with Job year & Job Type";
            }
        }

        protected void txtBillDate_TextChanged(object sender, EventArgs e)
        {
            TextBox txtBillDate = (TextBox)gvDetails.FooterRow.FindControl("txtBillDate");
            Label txtyear = (Label)gvDetails.FooterRow.FindControl("txtyear");
            TextBox txtJobNo = (TextBox)gvDetails.FooterRow.FindControl("txtJobNo");

            DateTime transdate = DateTime.Parse(txtBillDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string month = transdate.ToString("MMM").ToUpper();
            string years = transdate.ToString("yyyy");

            txtyear.Text = years;
            txtJobNo.Focus();

        }

        protected void btnprocess_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((Button)sender).NamingContainer);
            Button btnprocess = (Button)row.FindControl("btnprocess");
            Label lblStatus = (Label)row.FindControl("lblStatus");
            Label lblJobNo = (Label)row.FindControl("lblJobNo");
            ImageButton imgbtnEdit = (ImageButton)row.FindControl("imgbtnEdit");
            if (lblStatus.Text == "PENDING")
            {
                ErrDt.Visible = true;
                ErrDt.Text = "Status is not completed";
                imgbtnEdit.Focus();
            }
            else
            {
                ErrDt.Visible = false;
                Int64 jobno = Convert.ToInt64(lblJobNo.Text);
                ShowGrid_Bill_Process(jobno);

                foreach (GridViewRow grid in GridView1.Rows)
                {
                    try
                    {
                        jsm.JOBNO = jobno;
                        jsm.JOBTP = ddlJobTP.Text;
                        jsm.JOBYY = Convert.ToInt64(txtJobYR.Text);
                        jsm.ExpID = grid.Cells[4].Text;
                        jsm.COMPID = grid.Cells[0].Text;
                        jsm.TRANSDT = DateTime.Parse(grid.Cells[1].Text);
                        jsm.TRANSNO = Convert.ToInt64(grid.Cells[2].Text);
                        jsm.PartyID = grid.Cells[3].Text;
                        jsm.Amount = Convert.ToDecimal(grid.Cells[5].Text);

                        jsm.IPAddress = HttpContext.Current.Session["IpAddress"].ToString();
                        jsm.UserPC = HttpContext.Current.Session["PCName"].ToString();
                        jsm.InTime = DateTime.Now;
                        jsm.AUTHUSERID = HttpContext.Current.Session["UserName"].ToString();

                        conn = new SqlConnection(Global.connection);
                        conn.Open();
                        cmdd = new SqlCommand("Select EXPID from CNF_JOBBILL where COMPID ='" + jsm.COMPID + "' AND JOBYY =" + txtJobYR.Text + " AND JOBTP ='" + ddlJobTP.Text + "' AND JOBNO =" + jobno + " AND BILLAMT =0.00 AND EXPID ='" + jsm.ExpID + "'", conn);
                        SqlDataAdapter da = new SqlDataAdapter(cmdd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        conn.Close();
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            conn.Open();
                            cmdd = new SqlCommand("Delete from CNF_JOBBILL where EXPID ='" + jsm.ExpID + "' AND COMPID ='" + jsm.COMPID + "' AND JOBYY =" + txtJobYR.Text + " AND JOBTP ='" + ddlJobTP.Text + "' AND JOBNO =" + jobno + " AND BILLAMT =0.00", conn);
                            cmdd.ExecuteNonQuery();
                            conn.Close();

                            //lblExsl.Text = "";
                            //Global.lblAdd("SELECT MAX(EXPSL) AS EXPSL FROM CNF_JOBBILL where EXPID ='" + jsm.ExpID + "' AND COMPID ='" + jsm.COMPID + "' AND JOBYY =" + txtJobYR.Text + " AND JOBTP ='" + ddlJobTP.Text + "' AND JOBNO =" + jobno + "", lblExsl);
                            //if (lblExsl.Text == "")
                            //{
                            //    lblExsl.Text = "1";
                            //}
                            //else
                            //{
                            //    lblExsl.Text = (Convert.ToInt64(lblExsl.Text) + 1).ToString();
                            //}
                            //jsm.ExSL = Convert.ToInt64(lblExsl.Text);

                            jsda.Job_Bill_Process(jsm);
                        }
                        else
                        {
                            //lblExsl.Text = "";
                            //Global.lblAdd("SELECT MAX(EXPSL) AS EXPSL FROM CNF_JOBBILL where EXPID ='" + jsm.ExpID + "' AND COMPID ='" + jsm.COMPID + "' AND JOBYY =" + txtJobYR.Text + " AND JOBTP ='" + ddlJobTP.Text + "' AND JOBNO =" + jobno + "", lblExsl);
                            //if (lblExsl.Text == "")
                            //{
                            //    lblExsl.Text = "1";
                            //}
                            //else
                            //{
                            //    lblExsl.Text = (Convert.ToInt64(lblExsl.Text) + 1).ToString();
                            //}
                            //jsm.ExSL = Convert.ToInt64(lblExsl.Text);

                            jsda.Job_Bill_Process(jsm);
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.Message);
                    }
                }
                ErrDt.Visible = true;
                ErrDt.Text = "Process Completed.";
                TextBox txtBillDate = (TextBox)gvDetails.FooterRow.FindControl("txtBillDate");
                txtBillDate.Focus();
            }
        }

        public void ShowGrid_Bill_Process(Int64 jNo)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();
            SqlCommand cmd = new SqlCommand(" SELECT CNF_JOBEXP.COMPID, CNF_JOBSTATUS.TRANSDT AS BILLDATE, CNF_JOBSTATUS.TRANSNO AS BILLNO, CNF_JOB.PARTYID, CNF_JOBEXP.EXPID, SUM(CNF_JOBEXP.EXPAMT) AS AMOUNT " +
                       " FROM CNF_JOBEXP INNER JOIN CNF_JOBSTATUS ON CNF_JOBEXP.JOBNO = CNF_JOBSTATUS.JOBNO AND CNF_JOBEXP.JOBTP = CNF_JOBSTATUS.JOBTP AND  CNF_JOBEXP.JOBYY = CNF_JOBSTATUS.JOBYY INNER JOIN " +
                       " CNF_JOB ON CNF_JOBSTATUS.JOBYY = CNF_JOB.JOBYY AND CNF_JOBSTATUS.JOBTP = CNF_JOB.JOBTP AND CNF_JOBSTATUS.JOBNO = CNF_JOB.JOBNO WHERE (CNF_JOBEXP.JOBNO = " + jNo + ") AND (CNF_JOBEXP.JOBYY = " + txtJobYR.Text + ") AND (CNF_JOBEXP.JOBTP = '" + ddlJobTP.Text + "') AND (CNF_JOBEXP.COMPID ='" + txtCompID.Text + "')" +
                       " GROUP BY CNF_JOBEXP.COMPID, CNF_JOBEXP.EXPID, CNF_JOBSTATUS.TRANSDT, CNF_JOBSTATUS.TRANSNO, CNF_JOB.PARTYID", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.Visible = false;
            }
            else
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.Visible = false;
                //Response.Write("<script>alert('No Data Found');</script>");
                //GridView1.Visible = false;
            }
        }
    }
}