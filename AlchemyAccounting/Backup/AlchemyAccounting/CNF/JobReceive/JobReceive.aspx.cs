using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace AlchemyAccounting.CNF.JobReceive
{
    public partial class JobReceive : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        JobReceiveModel jrm = new JobReceiveModel();
        JobReceiveDataAccess jda = new JobReceiveDataAccess();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["UserName"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                if (!IsPostBack)
                {
                    DateTime td = DateTime.Now;
                    txtReceiveDate.Text = td.ToString("dd/MM/yyyy");
                    string mon = td.ToString("MMM").ToUpper();
                    string year = td.ToString("yy");
                    lblMY.Text = mon + "-" + year;
                    txtTransMY.Text = lblMY.Text;


                    //DateTime transdate = (DateTime.Parse(txtReceiveDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal));
                    //txtReceiveDate.Text = transdate.ToString("dd/MM/yyyy");
                    //string month = transdate.ToString("MMM").ToUpper();
                    //string years = transdate.ToString("yyyy");
                    //txtTransMY.Text = month + "-" + years;

                    Session["dis"] = null;
                    Session["dis"] = ddlRcvType.Text;

                    txtReceiveDate.Focus();

                    Global.lblAdd("select MAX(TRANSNO) from CNF_JOBRCV where TRANSMY='" + txtTransMY.Text + "' ", lblSL);
                    if (lblSL.Text == "")
                    {
                        txtVoucher.Text = "1";
                    }
                    else
                    {
                        var id = Int64.Parse(lblSL.Text) + 1;
                        txtVoucher.Text = id.ToString();

                    }
                }
            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListJob_No_Year_Type(string prefixText, int count, string contextKey)
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
                cmd.CommandText = ("SELECT (CONVERT(NVARCHAR(20),JOBNO,103) + '|' + CONVERT(NVARCHAR(20),JOBYY,103) + '|' + JOBTP) AS JOBPAR FROM CNF_JOB WHERE (CONVERT(NVARCHAR(20),JOBNO,103) + '|' + CONVERT(NVARCHAR(20),JOBYY,103) + '|' + JOBTP) LIKE '" + prefixText + "%'");
            }
            else
            {
                cmd.CommandText = ("SELECT (CONVERT(NVARCHAR(20),JOBNO,103) + '|' + CONVERT(NVARCHAR(20),JOBYY,103) + '|' + JOBTP) AS JOBPAR FROM CNF_JOB WHERE (CONVERT(NVARCHAR(20),JOBNO,103) + '|' + CONVERT(NVARCHAR(20),JOBYY,103) + '|' + JOBTP) LIKE '" + prefixText + "%' AND COMPID ='" + brCD + "'");
            }

            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["JOBPAR"].ToString());
            return CompletionSet.ToArray();

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                if (txtCompanyID.Text == "")
                {
                    lblErrmsg.Visible = true;
                    lblErrmsg.Text = "Select job no.";
                    txtJobID.Focus();
                }
                else if (txtJobYear.Text == "")
                {
                    lblErrmsg.Visible = true;
                    lblErrmsg.Text = "Select job no.";
                    txtJobID.Focus();
                }
                else if (txtJobType.Text == "")
                {
                    lblErrmsg.Visible = true;
                    lblErrmsg.Text = "Select job no.";
                    txtJobID.Focus();
                }
                else if (txtPartyID.Text == "")
                {
                    lblErrmsg.Visible = true;
                    lblErrmsg.Text = "Select job no.";
                    txtJobID.Focus();
                }
                else if (txtCashBankID.Text == "")
                {
                    lblErrmsg.Visible = true;
                    lblErrmsg.Text = "Select cash/bank id.";
                    txtCashBankNM.Focus();
                }
                else if (txtAmount.Text == "")
                {
                    lblErrmsg.Visible = true;
                    lblErrmsg.Text = "Type amount.";
                    txtAmount.Focus();
                }

                jrm.TRANSDT = DateTime.Parse(txtReceiveDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                jrm.TRANSMY = txtTransMY.Text;
                //jrm.TRANSNO = Convert.ToInt64(txtVoucher.Text);
                jrm.TRANSFOR = ddlRcvType.Text;
                jrm.COMPID = txtCompanyID.Text;
                jrm.JOBNO = Convert.ToInt64(txtJobID.Text);
                jrm.JOBYY = Convert.ToInt64(txtJobYear.Text);
                jrm.JOBTP = txtJobType.Text;
                jrm.PARTYID = txtPartyID.Text;
                jrm.DEBITCD = txtCashBankID.Text;
                jrm.REMARKS = txtRemarks.Text;
                jrm.AMOUNT = Convert.ToDecimal(txtAmount.Text);

                jrm.UserPC = HttpContext.Current.Session["PCName"].ToString();
                jrm.IPAddress = HttpContext.Current.Session["IpAddress"].ToString();
                jrm.InTime = DateTime.Now;
                jrm.UpdateTime = DateTime.Now;

                if (btnSave.Text == "Save")
                {
                    if (txtTransMY.Text == "")
                    {
                        lblErrmsg.Visible = true;
                        lblErrmsg.Text = "Select date.";
                        txtReceiveDate.Focus();
                    }
                    else
                    {
                        Global.lblAdd("select MAX(TRANSNO) from CNF_JOBRCV where TRANSMY='" + txtTransMY.Text + "' ", lblSL);
                        if (lblSL.Text == "")
                        {
                            txtVoucher.Text = "1";
                            jrm.TRANSNO = Convert.ToInt64(txtVoucher.Text);
                        }

                        else
                        {
                            var id = Int64.Parse(lblSL.Text) + 1;
                            txtVoucher.Text = id.ToString();
                            jrm.TRANSNO = Convert.ToInt64(txtVoucher.Text);
                        }

                        jda.SaveJobReceive(jrm);
                        Refresh();
                        txtJobID.Focus();
                    }
                }

                else if (btnSave.Text == "Update")
                {
                    if (ddlTransMy.Text == "Select")
                    {
                        lblErrmsg.Visible = true;
                        lblErrmsg.Text = "Select month year.";
                        ddlTransMy.Focus();
                    }
                    else if (ddlVouchNo.Text == "")
                    {
                        lblErrmsg.Visible = true;
                        lblErrmsg.Text = "Select voucher no.";
                        ddlVouchNo.Focus();
                    }
                    else
                    {
                        jrm.TRANSNO = Convert.ToInt64(ddlVouchNo.Text);

                        jda.UpdateJobReceive(jrm);
                        Refresh();
                        ddlVouchNo.Focus();
                    }
                }

            }

            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }




        public void Refresh()
        {
            ddlRcvType.SelectedIndex = -1;
            txtCompanyID.Text = "";
            txtCompanyNM.Text = "";
            txtJobID.Text = "";
            txtJobType.Text = "";
            txtJobYear.Text = "";
            txtPartyID.Text = "";
            txtPartyNM.Text = "";
            txtAmount.Text = "";
            txtRemarks.Text = "";
            txtInwords.Text = "";
            txtCashBankID.Text = "";
            txtCashBankNM.Text = "";
            ddlVouchNo.SelectedIndex = -1;


            DateTime transdate = (DateTime.Parse(txtReceiveDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal));
            string month = transdate.ToString("MMM").ToUpper();
            string years = transdate.ToString("yy");

            txtTransMY.Text = month + "-" + years;

            Global.lblAdd("select MAX(TRANSNO) from CNF_JOBRCV where TRANSMY='" + lblMY.Text + "' ", lblSL);
            if (lblSL.Text == "")
            {
                txtVoucher.Text = "1";
            }
            else
            {
                var id = Int64.Parse(lblSL.Text) + 1;
                txtVoucher.Text = id.ToString();

            }

        }

        protected void txtJobID_TextChanged(object sender, EventArgs e)
        {
            if (txtJobID.Text == "")
            {
                lblErrmsg.Visible = true;
                lblErrmsg.Text = "Select job no.";
                txtJobID.Focus();
            }
            else
            {
                lblErrmsg.Visible = false;

                string jobno = "";
                string jobyear = "";
                string jobtp = "";
                string searchPar = txtJobID.Text;

                int splitter = searchPar.IndexOf("|");
                if (splitter != -1)
                {
                    string[] lineSplit = searchPar.Split('|');

                    jobno = lineSplit[0];
                    jobyear = lineSplit[1];
                    jobtp = lineSplit[2];

                    txtJobID.Text = jobno.Trim();
                    txtJobYear.Text = jobyear.Trim();
                    txtJobType.Text = jobtp.Trim();
                    txtCompanyID.Text = "";

                    Global.txtAdd("SELECT COMPID FROM CNF_JOB WHERE JOBNO =" + jobno + " AND JOBYY =" + jobyear + " AND JOBTP ='" + jobtp + "'", txtCompanyID);
                    Global.txtAdd("SELECT (BRANCHID + '-' + COMPNM) AS COMPNM FROM ASL_COMPANY WHERE COMPID = '" + txtCompanyID.Text + "' ", txtCompanyNM);

                    Global.txtAdd("SELECT PARTYID FROM CNF_JOB WHERE JOBNO =" + jobno + " AND JOBYY =" + jobyear + " AND JOBTP ='" + jobtp + "'", txtPartyID);
                    Global.txtAdd("SELECT ACCOUNTNM FROM GL_ACCHART WHERE ACCOUNTCD = '" + txtPartyID.Text + "' ", txtPartyNM);

                    txtCashBankNM.Focus();
                }
                else
                {
                    txtJobID.Text = "";
                    txtJobYear.Text = "";
                    txtJobType.Text = "";
                    txtCompanyID.Text = "";
                }
            }
        }



        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListCash_Bank(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            string uTp = HttpContext.Current.Session["UserTp"].ToString();
            string brCD = HttpContext.Current.Session["BrCD"].ToString();
            string rcvTp = HttpContext.Current.Session["dis"].ToString();
            // Try to use parameterized inline query/sp to protect sql injection
            SqlCommand cmd = new SqlCommand("", conn);
            if (rcvTp == "Discount")
            {
                cmd = new SqlCommand("SELECT ACCOUNTNM FROM GL_ACCHART WHERE ACCOUNTCD ='402012300001' and ACCOUNTNM LIKE '" + prefixText + "%' AND STATUSCD = 'P'", conn);
            }
            else
            {
                if (uTp == "ADMIN")
                {
                    cmd = new SqlCommand("SELECT ACCOUNTNM FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,5) IN ('10201') and ACCOUNTNM LIKE '" + prefixText + "%' AND STATUSCD = 'P'", conn);
                }
                else
                {
                    cmd = new SqlCommand("SELECT ACCOUNTNM FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,5) IN ('10201') and ACCOUNTNM LIKE '" + prefixText + "%' AND STATUSCD = 'P' AND (BRANCHCD ='" + brCD + "' OR BRANCHCD IS NULL OR BRANCHCD ='')", conn);
                }
            }
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["ACCOUNTNM"].ToString());
            return CompletionSet.ToArray();

        }

        protected void txtCashBankNM_TextChanged(object sender, EventArgs e)
        {
            if (txtCashBankNM.Text == "")
            {
                lblErrmsg.Visible = true;
                lblErrmsg.Text = "Select cash/bank id.";
                txtCashBankNM.Focus();
            }
            else
            {
                Global.txtAdd("SELECT ACCOUNTCD FROM GL_ACCHART WHERE ACCOUNTNM = '" + txtCashBankNM.Text + "' AND STATUSCD ='P'", txtCashBankID);
                if (txtCashBankID.Text == "")
                {
                    lblErrmsg.Visible = true;
                    lblErrmsg.Text = "Select cash/bank id.";
                    txtCashBankNM.Text = "";
                    txtCashBankNM.Focus();
                }
                else
                {
                    lblErrmsg.Visible = false;
                    txtRemarks.Focus();
                }
            }
        }

        protected void txtAmount_TextChanged(object sender, EventArgs e)
        {
            decimal dec;
            Boolean ValidInput = Decimal.TryParse(txtAmount.Text, out dec);
            if (!ValidInput)
            {
                txtInwords.ForeColor = System.Drawing.Color.Red;
                txtInwords.Text = "Enter the Proper Amount...";
                return;
            }
            if (txtAmount.Text.ToString().Trim() == "")
            {
                txtInwords.ForeColor = System.Drawing.Color.Red;
                txtInwords.Text = "Amount Cannot Be Empty...";
                return;
            }
            else
            {
                if (Convert.ToDecimal(txtAmount.Text) == 0)
                {
                    txtInwords.ForeColor = System.Drawing.Color.Red;
                    txtInwords.Text = "Amount Cannot Be Empty...";
                    return;
                }
            }

            string x1 = "";
            string x2 = "";

            if (txtAmount.Text.Contains("."))
            {
                x1 = txtAmount.Text.ToString().Trim().Substring(0, txtAmount.Text.ToString().Trim().IndexOf("."));
                x2 = txtAmount.Text.ToString().Trim().Substring(txtAmount.Text.ToString().Trim().IndexOf(".") + 1);
            }
            else
            {
                x1 = txtAmount.Text.ToString().Trim();
                x2 = "00";
            }

            if (x1.ToString().Trim() != "")
            {
                x1 = Convert.ToInt64(x1.Trim()).ToString().Trim();
            }
            else
            {
                x1 = "0";
            }

            txtAmount.Text = x1 + "." + x2;

            if (x2.Length > 2)
            {
                txtAmount.Text = Math.Round(Convert.ToDouble(txtAmount.Text), 2).ToString().Trim();
            }

            string AmtConv = SpellAmount.MoneyConvFn(txtAmount.Text.ToString().Trim());
            //string amntComma = SpellAmount.comma(Convert.ToDecimal(txtAmount.Text));
            //Label3.Text = amntComma;

            txtInwords.Text = AmtConv.Trim();
            txtInwords.ForeColor = System.Drawing.Color.Green;

            btnSave.Focus();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            lblErrmsg.Visible = false;
            txtTransMY.Visible = false;
            txtVoucher.Visible = false;

            txtReceiveDate.Enabled = false;
            ddlTransMy.Visible = true;
            ddlVouchNo.Visible = true;

            btnSave.Text = "Update";

            btnUpdate.Visible = false;
            btnCancel.Visible = true;
            btnDelete.Visible = true;
            //btnUpdate.Text = "Cancel";

            Global.dropDownAddWithSelect(ddlTransMy, "Select distinct TRANSMY from CNF_JOBRCV");

            Global.dropDownAddWithSelect(ddlVouchNo, "Select TRANSNO from CNF_JOBRCV where TRANSMY='" + ddlTransMy.Text + "' ");
            ddlTransMy.Focus();



            //Global.dropDownAddWithSelect(ddlVouchNo, "Select TRANSNO from CNF_JOBRCV where TRANSMY='" + ddlTransMy.Text + "' ");


        }

        protected void ddlTransMy_SelectedIndexChanged(object sender, EventArgs e)
        {

            Global.dropDownAddWithSelect(ddlVouchNo, "Select TRANSNO from CNF_JOBRCV where TRANSMY='" + ddlTransMy.Text + "' ");


            txtCompanyID.Text = "";
            txtCompanyNM.Text = "";
            txtJobID.Text = "";
            txtJobType.Text = "";
            txtJobYear.Text = "";
            txtPartyID.Text = "";
            txtPartyNM.Text = "";
            txtAmount.Text = "";
            txtRemarks.Text = "";
            txtInwords.Text = "";
            txtCashBankID.Text = "";
            txtCashBankNM.Text = "";
            ddlVouchNo.Focus();


            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString);

            //SqlCommand cmd = new SqlCommand("Select TRANSNO from CNF_JOBRCV where TRANSMY='"+ ddlTransMy.Text +"' ", con);

            //SqlDataAdapter da = new SqlDataAdapter(cmd);

            //DataSet ds = new DataSet();

            //da.Fill(ds);

            //ddlVouchNo.DataTextField = ds.Tables[0].Columns["TRANSNO"].ToString();
            //ddlVouchNo.DataValueField = ds.Tables[0].Columns["TRANSNO"].ToString();

            //ddlVouchNo.DataSource = ds.Tables[0];
            //ddlVouchNo.DataBind();
        }

        protected void ddlVouchNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Global.txtAdd(" SELECT COMPID FROM CNF_JOBRCV WHERE TRANSNO='" + ddlVouchNo.Text + "' AND TRANSMY='" + ddlTransMy.Text + "' ", txtCompanyID);
            Global.txtAdd("SELECT COMPNM FROM ASL_COMPANY WHERE COMPID = '" + txtCompanyID.Text + "' ", txtCompanyNM);

            Global.txtAdd(" SELECT JOBYY FROM CNF_JOBRCV WHERE TRANSNO='" + ddlVouchNo.Text + "' AND TRANSMY='" + ddlTransMy.Text + "' ", txtJobYear);
            Global.txtAdd(" SELECT JOBTP FROM CNF_JOBRCV WHERE TRANSNO='" + ddlVouchNo.Text + "' AND TRANSMY='" + ddlTransMy.Text + "' ", txtJobType);
            Global.txtAdd(" SELECT JOBNO FROM CNF_JOBRCV WHERE TRANSNO='" + ddlVouchNo.Text + "' AND TRANSMY='" + ddlTransMy.Text + "' ", txtJobID);


            Global.txtAdd(" SELECT PARTYID FROM CNF_JOBRCV WHERE TRANSNO='" + ddlVouchNo.Text + "' AND TRANSMY='" + ddlTransMy.Text + "' ", txtPartyID);
            Global.txtAdd("SELECT ACCOUNTNM FROM GL_ACCHART WHERE ACCOUNTCD = '" + txtPartyID.Text + "' AND STATUSCD ='P'", txtPartyNM);

            Global.txtAdd(" SELECT DEBITCD FROM CNF_JOBRCV WHERE TRANSNO='" + ddlVouchNo.Text + "' AND TRANSMY='" + ddlTransMy.Text + "' ", txtCashBankID);
            Global.txtAdd("SELECT  ACCOUNTNM FROM GL_ACCHART WHERE ACCOUNTCD = '" + txtCashBankID.Text + "' ", txtCashBankNM);


            Global.txtAdd(" SELECT AMOUNT FROM CNF_JOBRCV WHERE TRANSNO='" + ddlVouchNo.Text + "' AND TRANSMY='" + ddlTransMy.Text + "' ", txtAmount);

            Global.txtAdd(" SELECT REMARKS FROM CNF_JOBRCV WHERE TRANSNO='" + ddlVouchNo.Text + "' AND TRANSMY='" + ddlTransMy.Text + "' ", txtRemarks);

            Global.txtAdd(" SELECT CONVERT(NVARCHAR(20),TRANSDT,103) AS TRANSD FROM CNF_JOBRCV WHERE TRANSNO='" + ddlVouchNo.Text + "' AND TRANSMY='" + ddlTransMy.Text + "' ", txtReceiveDate);

            Global.lblAdd(" SELECT TRANSFOR FROM CNF_JOBRCV WHERE TRANSNO='" + ddlVouchNo.Text + "' AND TRANSMY='" + ddlTransMy.Text + "' ", lbltransfor);
            ddlRcvType.Text = lbltransfor.Text;

            Session["dis"] = null;
            Session["dis"] = ddlRcvType.Text;

            decimal dec;
            Boolean ValidInput = Decimal.TryParse(txtAmount.Text, out dec);
            if (!ValidInput)
            {
                txtInwords.ForeColor = System.Drawing.Color.Red;
                txtInwords.Text = "Enter the Proper Amount...";
                return;
            }
            if (txtAmount.Text.ToString().Trim() == "")
            {
                txtInwords.ForeColor = System.Drawing.Color.Red;
                txtInwords.Text = "Amount Cannot Be Empty...";
                return;
            }
            else
            {
                if (Convert.ToDecimal(txtAmount.Text) == 0)
                {
                    txtInwords.ForeColor = System.Drawing.Color.Red;
                    txtInwords.Text = "Amount Cannot Be Empty...";
                    return;
                }
            }

            string x1 = "";
            string x2 = "";

            if (txtAmount.Text.Contains("."))
            {
                x1 = txtAmount.Text.ToString().Trim().Substring(0, txtAmount.Text.ToString().Trim().IndexOf("."));
                x2 = txtAmount.Text.ToString().Trim().Substring(txtAmount.Text.ToString().Trim().IndexOf(".") + 1);
            }
            else
            {
                x1 = txtAmount.Text.ToString().Trim();
                x2 = "00";
            }

            if (x1.ToString().Trim() != "")
            {
                x1 = Convert.ToInt64(x1.Trim()).ToString().Trim();
            }
            else
            {
                x1 = "0";
            }

            txtAmount.Text = x1 + "." + x2;

            if (x2.Length > 2)
            {
                txtAmount.Text = Math.Round(Convert.ToDouble(txtAmount.Text), 2).ToString().Trim();
            }

            string AmtConv = SpellAmount.MoneyConvFn(txtAmount.Text.ToString().Trim());
            //string amntComma = SpellAmount.comma(Convert.ToDecimal(txtAmount.Text));
            //Label3.Text = amntComma;

            txtInwords.Text = AmtConv.Trim();
            txtInwords.ForeColor = System.Drawing.Color.Green;

        }

        protected void txtReceiveDate_TextChanged(object sender, EventArgs e)
        {

            DateTime transdate = (DateTime.Parse(txtReceiveDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal));
            string month = transdate.ToString("MMM").ToUpper();
            string years = transdate.ToString("yy");

            txtTransMY.Text = month + "-" + years;

            Global.lblAdd("select MAX(TRANSNO) from CNF_JOBRCV where TRANSMY='" + txtTransMY.Text + "' ", lblSL);
            if (lblSL.Text == "")
            {
                txtVoucher.Text = "1";
            }
            else
            {
                var id = Int64.Parse(lblSL.Text) + 1;
                txtVoucher.Text = id.ToString();
            }
            ddlRcvType.Focus();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblErrmsg.Visible = false;
            txtTransMY.Visible = true;
            txtVoucher.Visible = true;

            txtReceiveDate.Enabled = true;
            ddlTransMy.Visible = false;
            ddlVouchNo.Visible = false;

            btnSave.Text = "Save";

            btnUpdate.Visible = true;
            btnCancel.Visible = false;

            Refresh();

            DateTime td = DateTime.Now;
            txtReceiveDate.Text = td.ToString("dd/MM/yyyy");
            string mon = td.ToString("MMM").ToUpper();
            string year = td.ToString("yy");
            lblMY.Text = mon + "-" + year;
            txtTransMY.Text = lblMY.Text;
            btnDelete.Visible = false;
            txtJobID.Focus();
        }

        protected void ddlRcvType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["dis"] = null;
            Session["dis"] = ddlRcvType.Text;
            txtJobID.Focus();
        }

        public void Save_Print()
        {
            Session["ReceiveTp"] = "";
            Session["CompID"] = "";
            Session["Jobno"] = "";
            Session["JobYear"] = "";
            Session["JobTp"] = "";
            Session["PartyID"] = "";
            Session["DebitCD"] = "";
            Session["Remarks"] = "";
            Session["Amount"] = "";
            Session["Inwords"] = "";
            Session["voucherNo"] = "";

            try
            {
                if (txtCompanyID.Text == "")
                {
                    lblErrmsg.Visible = true;
                    lblErrmsg.Text = "Select job no.";
                    txtJobID.Focus();
                }
                else if (txtJobYear.Text == "")
                {
                    lblErrmsg.Visible = true;
                    lblErrmsg.Text = "Select job no.";
                    txtJobID.Focus();
                }
                else if (txtJobType.Text == "")
                {
                    lblErrmsg.Visible = true;
                    lblErrmsg.Text = "Select job no.";
                    txtJobID.Focus();
                }
                else if (txtPartyID.Text == "")
                {
                    lblErrmsg.Visible = true;
                    lblErrmsg.Text = "Select job no.";
                    txtJobID.Focus();
                }
                else if (txtCashBankID.Text == "")
                {
                    lblErrmsg.Visible = true;
                    lblErrmsg.Text = "Select cash/bank id.";
                    txtCashBankNM.Focus();
                }
                else if (txtAmount.Text == "")
                {
                    lblErrmsg.Visible = true;
                    lblErrmsg.Text = "Type amount.";
                    txtAmount.Focus();
                }

                jrm.TRANSDT = DateTime.Parse(txtReceiveDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                jrm.TRANSMY = txtTransMY.Text;
                //jrm.TRANSNO = Convert.ToInt64(txtVoucher.Text);


                jrm.TRANSFOR = ddlRcvType.Text;
                Session["ReceiveTp"] = ddlRcvType.Text;

                jrm.COMPID = txtCompanyID.Text;
                Session["CompID"] = txtCompanyID.Text;

                jrm.JOBNO = Convert.ToInt64(txtJobID.Text);
                Session["Jobno"] = Convert.ToInt64(txtJobID.Text);

                jrm.JOBYY = Convert.ToInt64(txtJobYear.Text);
                Session["JobYear"] = Convert.ToInt64(txtJobYear.Text);

                jrm.JOBTP = txtJobType.Text;
                Session["JobTp"] = txtJobType.Text;

                jrm.PARTYID = txtPartyID.Text;
                Session["PartyID"] = txtPartyID.Text;

                jrm.DEBITCD = txtCashBankID.Text;
                Session["DebitCD"] = txtCashBankID.Text;

                jrm.REMARKS = txtRemarks.Text;
                Session["Remarks"] = txtRemarks.Text;

                jrm.AMOUNT = Convert.ToDecimal(txtAmount.Text);
                Session["Amount"] = Convert.ToDecimal(txtAmount.Text);


                Session["Inwords"] = txtInwords.Text;

                jrm.UserPC = HttpContext.Current.Session["PCName"].ToString();
                jrm.IPAddress = HttpContext.Current.Session["IpAddress"].ToString();
                jrm.InTime = DateTime.Now;
                jrm.UpdateTime = DateTime.Now;

                //if (btnSave.Text == "Print")
                //{
                //    if (txtTransMY.Text == "")
                //    {
                //        lblErrmsg.Visible = true;
                //        lblErrmsg.Text = "Select date.";
                //        txtReceiveDate.Focus();
                //    }
                //    else
                //    {
                Global.lblAdd("select MAX(TRANSNO) from CNF_JOBRCV where TRANSMY='" + txtTransMY.Text + "' ", lblSL);
                if (lblSL.Text == "")
                {
                    txtVoucher.Text = "1";
                    jrm.TRANSNO = Convert.ToInt64(txtVoucher.Text);

                    Session["voucherNo"] = Convert.ToInt64(txtVoucher.Text);
                }

                else
                {
                    var id = Int64.Parse(lblSL.Text) + 1;
                    txtVoucher.Text = id.ToString();
                    jrm.TRANSNO = Convert.ToInt64(txtVoucher.Text);

                    Session["voucherNo"] = Convert.ToInt64(txtVoucher.Text);
                }

                jda.SaveJobReceive(jrm);
                Refresh();

                txtJobID.Focus();
            }
            //}

                //else if (btnSave.Text == "Update")
            //{
            //    if (ddlTransMy.Text == "Select")
            //    {
            //        lblErrmsg.Visible = true;
            //        lblErrmsg.Text = "Select month year.";
            //        ddlTransMy.Focus();
            //    }
            //    else if (ddlVouchNo.Text == "")
            //    {
            //        lblErrmsg.Visible = true;
            //        lblErrmsg.Text = "Select voucher no.";
            //        ddlVouchNo.Focus();
            //    }
            //    else
            //    {
            //        jrm.TRANSNO = Convert.ToInt64(ddlVouchNo.Text);

                //        jda.UpdateJobReceive(jrm);
            //        Refresh();
            //        ddlVouchNo.Focus();
            //    }
            //}

            //}

            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }


        protected void btnSave_Print_Click(object sender, EventArgs e)
        {
            Save_Print();

            Page.ClientScript.RegisterStartupScript(
                        this.GetType(), "OpenWindow", "window.open('../report/vis-rep/RptReceive_VR.aspx','_newtab');", true);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (ddlTransMy.Text == "Select")
            {
                lblErrmsg.Visible = true;
                lblErrmsg.Text = "Select month.";
                ddlTransMy.Focus();
            }
            else if (ddlVouchNo.Text == "Select")
            {
                lblErrmsg.Visible = true;
                lblErrmsg.Text = "Select Voucher no.";
                ddlVouchNo.Focus();
            }
            else
            {
                lblErrmsg.Visible = false;
                jrm.TRANSMY = ddlTransMy.Text;
                jrm.TRANSNO = Convert.ToInt64(ddlVouchNo.Text);

                jda.DeleteJobReceive(jrm);
                Refresh();

                Global.dropDownAddWithSelect(ddlVouchNo, "Select TRANSNO from CNF_JOBRCV where TRANSMY='" + ddlTransMy.Text + "' ");

                lblErrmsg.Visible = true;
                lblErrmsg.Text = "Data deleted.";
            }
        }
    }
}