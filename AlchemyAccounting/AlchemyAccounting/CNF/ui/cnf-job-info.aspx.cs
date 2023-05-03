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
using AlchemyAccounting.CNF.model;
using AlchemyAccounting.CNF.dataAccess;

namespace AlchemyAccounting.CNF.ui
{
    public partial class cnf_job_info : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmdd;

        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        cnf_model iob = new cnf_model();
        cnf_data dob = new cnf_data();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                if (!IsPostBack)
                {
                    DateTime td = Global.Timezone(DateTime.Now);
                    txtCrDt.Text = td.ToString("dd/MM/yyyy");
                    txtJobYear.Text = td.ToString("yyyy");
                    string brCD = HttpContext.Current.Session["BrCD"].ToString();
                    Global.txtAdd("SELECT BRANCHID+'-'+COMPNM FROM ASL_COMPANY WHERE COMPID='" + brCD + "'", txtCompNM);
                    txtCompID.Text = brCD;
                    check_Job_No();
                    txtCompNM.Focus();
                }
            }
        }

        public void check_Job_No()
        {
            lblJobNo.Text = "";
            Global.lblAdd("SELECT MAX(JOBNO) AS JOBNO FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "'", lblJobNo);
            if (lblJobNo.Text == "")
            {
                txtNo.Text = "1";
            }
            else
            {
                Int64 trns, ftrns = 0;
                trns = Convert.ToInt64(lblJobNo.Text);
                ftrns = trns + 1;
                txtNo.Text = ftrns.ToString();
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
                    ddlJobTp.Focus();
                }
            }
        }

        protected void ddlJobTp_SelectedIndexChanged(object sender, EventArgs e)
        {
            string uTp = HttpContext.Current.Session["UserTp"].ToString();
            string brCD = HttpContext.Current.Session["BrCD"].ToString();

            if (btnEdit.Text == "Edit")
            {
                check_Job_No();
            }
            else
            {
                if (uTp == "ADMIN")
                {
                    Global.dropDownAddWithSelect(ddlJobNo, "SELECT JOBNO FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "'");
                }
                else
                {
                    Global.dropDownAddWithSelect(ddlJobNo, "SELECT JOBNO FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND COMPID ='" + brCD + "'");
                }
            }
            txtCrDt.Focus();
        }

        protected void txtCrDt_TextChanged(object sender, EventArgs e)
        {
            DateTime ctd = DateTime.Parse(txtCrDt.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            txtCrDt.Text = ctd.ToString("dd/MM/yyyy");
            txtJobYear.Text = ctd.ToString("yyyy");
            check_Job_No();
            txtJobYear.Focus();
        }

        protected void txtJobYear_TextChanged(object sender, EventArgs e)
        {
            string uTp = HttpContext.Current.Session["UserTp"].ToString();
            string brCD = HttpContext.Current.Session["BrCD"].ToString();

            if (btnEdit.Text == "Edit")
            {
                check_Job_No();
            }
            else
            {
                if (uTp == "ADMIN")
                {
                    Global.dropDownAddWithSelect(ddlJobNo, "SELECT JOBNO FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "'");
                }
                else
                {
                    Global.dropDownAddWithSelect(ddlJobNo, "SELECT JOBNO FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND COMPID ='" + brCD + "'");
                }
            }
            ddlRegID.Focus();
        }

        protected void ddlRegID_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPartyNM.Focus();
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListParty(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            string uTp = HttpContext.Current.Session["UserTp"].ToString();
            string brCD = HttpContext.Current.Session["BrCD"].ToString();
            // Try to use parameterized inline query/sp to protect sql injection
            SqlCommand cmd = new SqlCommand("", conn);
            if (uTp == "ADMIN")
            {
                cmd = new SqlCommand("SELECT ACCOUNTNM FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,5) IN ('10202') and ACCOUNTNM LIKE '" + prefixText + "%' AND STATUSCD = 'P'", conn);
            }
            else
            {
                cmd = new SqlCommand("SELECT ACCOUNTNM FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,5) IN ('10202') and ACCOUNTNM LIKE '" + prefixText + "%' AND STATUSCD = 'P' AND (BRANCHCD ='" + brCD + "' OR BRANCHCD IS NULL OR BRANCHCD ='')", conn);
            }
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["ACCOUNTNM"].ToString());
            return CompletionSet.ToArray();
        }

        protected void txtPartyNM_TextChanged(object sender, EventArgs e)
        {
            if (txtPartyNM.Text == "")
            {
                lblError.Visible = true;
                lblError.Text = "Select Party.";
                txtPartyNM.Focus();
            }
            else
            {
                txtPartyID.Text = "";
                Global.txtAdd("SELECT ACCOUNTCD FROM GL_ACCHART WHERE ACCOUNTNM ='" + txtPartyNM.Text + "' AND STATUSCD ='P'", txtPartyID);
                if (txtPartyID.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select Party.";
                    txtPartyNM.Text = "";
                    txtPartyNM.Focus();
                }
                else
                {
                    lblError.Visible = false;
                    txtConsigneeNM.Focus();
                }
            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListConsigName(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            // Try to use parameterized inline query/sp to protect sql injection
            SqlCommand cmd = new SqlCommand("", conn);
            cmd = new SqlCommand("SELECT DISTINCT CONSIGNEENM FROM CNF_JOB WHERE CONSIGNEENM LIKE '" + prefixText + "%'", conn);
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["CONSIGNEENM"].ToString());
            return CompletionSet.ToArray();
        }

        protected void txtConsigneeNM_TextChanged(object sender, EventArgs e)
        {
            if (txtConsigneeNM.Text == "")
            {
                txtConsigneeAdd.Text = "";
            }
            else
            {
                txtConsigneeAdd.Text = "";
                Global.txtAdd("SELECT CONSIGNEEADD FROM CNF_JOB WHERE CONSIGNEENM ='" + txtConsigneeNM.Text + "' ", txtConsigneeAdd);
                if (txtConsigneeAdd.Text == "")
                {
                    txtConsigneeAdd.Focus();
                }
                else
                {
                    txtSuppNM.Focus();
                }
            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListSupplier(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            // Try to use parameterized inline query/sp to protect sql injection
            SqlCommand cmd = new SqlCommand("", conn);
            cmd = new SqlCommand("SELECT DISTINCT SUPPLIERNM FROM CNF_JOB WHERE SUPPLIERNM LIKE '" + prefixText + "%'", conn);
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["SUPPLIERNM"].ToString());
            return CompletionSet.ToArray();
        }

        protected void txtSuppNM_TextChanged(object sender, EventArgs e)
        {
            txtGoodsDesc.Focus();
        }

        protected void txtChangeRT_TextChanged(object sender, EventArgs e)
        {
            if (txtCNFVUSD.Text == "")
                txtCNFVUSD.Text = "0";
            else if (txtChangeRT.Text == "")
                txtChangeRT.Text = "0";

            if (txtPartyID.Text == "")
            {
                lblError.Visible = true;
                lblError.Text = "Select Party Name.";
                txtChangeRT.Text = ".00";
                txtPartyNM.Focus();
            }
            else
            {
                txtCNFVBDT.Text = (Convert.ToDecimal(txtCNFVUSD.Text) * Convert.ToDecimal(txtChangeRT.Text)).ToString();
                txtAssessableVal.Text = txtCNFVBDT.Text;

                conn = new SqlConnection(Global.connection);
                conn.Open();

                cmdd = new SqlCommand("SELECT EXCTP, VALUETP, COMMAMT FROM CNF_COMMISSION WHERE (PARTYID = '" + txtPartyID.Text + "') AND JOBTP='" + ddlJobTp.Text + "' AND " + txtAssessableVal.Text + " BETWEEN VALUEFR AND VALUETO AND EXCTP='BDT' " +
                        " UNION ALL " +
                        " SELECT EXCTP, VALUETP, COMMAMT FROM CNF_COMMISSION WHERE (PARTYID = '" + txtPartyID.Text + "') AND JOBTP='" + ddlJobTp.Text + "' AND " + txtCNFVUSD.Text + " BETWEEN VALUEFR AND VALUETO AND EXCTP='USD'", conn);

                SqlDataAdapter da = new SqlDataAdapter(cmdd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();

                if (ds.Tables[0].Rows.Count > 1)
                {
                    lblError.Visible = true;
                    lblError.Text = "An error occured. Please check party commission form.";
                    txtChangeRT.Text = ".00";
                    txtCommission.Text = ".00";
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    GridView1.Visible = false;
                    txtChangeRT.Focus();
                }
                else if (ds.Tables[0].Rows.Count > 0)
                {
                    lblError.Visible = false;
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    GridView1.Visible = false;

                    foreach (GridViewRow grid in GridView1.Rows)
                    {
                        try
                        {
                            string comMainVal = "0";
                            if (grid.Cells[0].Text == "BDT")
                                comMainVal = txtAssessableVal.Text;
                            else
                                comMainVal = txtCNFVUSD.Text;

                            if (grid.Cells[2].Text == "")
                                grid.Cells[2].Text = ".00";

                            if (grid.Cells[1].Text == "AMT")
                            {
                                txtCommission.Text = grid.Cells[2].Text;
                            }
                            else
                            {
                                txtCommission.Text = (Convert.ToDecimal(comMainVal) * ((Convert.ToDecimal(grid.Cells[2].Text)) / 100)).ToString("F");
                            }

                            txtExTP.Focus();
                        }
                        catch (Exception ex)
                        {
                            lblError.Visible = true;
                            lblError.Text = ex.Message;
                        }
                    }

                }
                else
                {
                    lblError.Visible = false;
                    txtCommission.Text = ".00";
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    GridView1.Visible = false;
                    txtExTP.Focus();
                }
            }
        }

        protected void txtAssessableVal_TextChanged(object sender, EventArgs e)
        {
            if (txtCNFVUSD.Text == "")
                txtCNFVUSD.Text = "0";
            else if (txtChangeRT.Text == "")
                txtChangeRT.Text = "0";

            if (txtPartyID.Text == "")
            {
                lblError.Visible = true;
                lblError.Text = "Select Party Name.";
                txtChangeRT.Text = ".00";
                txtAssessableVal.Text = ".00";
                txtPartyNM.Focus();
            }
            else
            {
                conn = new SqlConnection(Global.connection);
                conn.Open();

                cmdd = new SqlCommand("SELECT EXCTP, VALUETP, COMMAMT FROM CNF_COMMISSION WHERE (PARTYID = '" + txtPartyID.Text + "') AND JOBTP='" + ddlJobTp.Text + "'  AND " + txtAssessableVal.Text + " BETWEEN VALUEFR AND VALUETO AND EXCTP='BDT' " +
                        " UNION ALL " +
                        " SELECT EXCTP, VALUETP, COMMAMT FROM CNF_COMMISSION WHERE (PARTYID = '" + txtPartyID.Text + "') AND JOBTP='" + ddlJobTp.Text + "' AND " + txtCNFVUSD.Text + " BETWEEN VALUEFR AND VALUETO AND EXCTP='USD'", conn);

                SqlDataAdapter da = new SqlDataAdapter(cmdd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();

                if (ds.Tables[0].Rows.Count > 1)
                {
                    lblError.Visible = true;
                    lblError.Text = "An error occured. Please check party commission form.";
                    txtChangeRT.Text = ".00";
                    txtCommission.Text = ".00";
                    txtAssessableVal.Text = ".00";
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    GridView1.Visible = false;
                    txtChangeRT.Focus();
                }
                else if (ds.Tables[0].Rows.Count > 0)
                {
                    lblError.Visible = false;
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    GridView1.Visible = false;

                    foreach (GridViewRow grid in GridView1.Rows)
                    {
                        try
                        {
                            string comMainVal = "0";
                            if (grid.Cells[0].Text == "BDT")
                                comMainVal = txtAssessableVal.Text;
                            else
                                comMainVal = txtCNFVUSD.Text;

                            if (grid.Cells[2].Text == "")
                                grid.Cells[2].Text = ".00";

                            if (grid.Cells[1].Text == "AMT")
                            {
                                txtCommission.Text = grid.Cells[2].Text;
                            }
                            else
                            {
                                txtCommission.Text = (Convert.ToDecimal(comMainVal) * ((Convert.ToDecimal(grid.Cells[2].Text)) / 100)).ToString("F");
                            }

                            txtCRFNO.Focus();
                        }
                        catch (Exception ex)
                        {
                            lblError.Visible = true;
                            lblError.Text = ex.Message;
                        }
                    }

                }
                else
                {
                    lblError.Visible = false;
                    txtCommission.Text = ".00";
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    GridView1.Visible = false;
                    txtCRFNO.Focus();
                }
            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListExchangeTp(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            // Try to use parameterized inline query/sp to protect sql injection
            SqlCommand cmd = new SqlCommand("", conn);
            cmd = new SqlCommand("SELECT DISTINCT CNFV_ETP FROM CNF_JOB WHERE CNFV_ETP LIKE '" + prefixText + "%'", conn);
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["CNFV_ETP"].ToString());
            return CompletionSet.ToArray();
        }

        protected void txtExTP_TextChanged(object sender, EventArgs e)
        {
            txtCRFNO.Focus();
        }

        protected void txtCRFDT_TextChanged(object sender, EventArgs e)
        {
            txtInvoiceNo.Focus();
        }

        protected void txtInvoiceDT_TextChanged(object sender, EventArgs e)
        {
            txtBENO.Focus();
        }

        protected void txtBEDT_TextChanged(object sender, EventArgs e)
        {
            txtBLNO.Focus();
        }

        protected void txtWhDT_TextChanged(object sender, EventArgs e)
        {
            txtDelDT.Focus();
        }

        protected void txtDelDT_TextChanged(object sender, EventArgs e)
        {
            txtAssessableVal.Focus();
        }

        protected void txtBLDT_TextChanged(object sender, EventArgs e)
        {
            txtLCNO.Focus();
        }

        protected void txtLCDT_TextChanged(object sender, EventArgs e)
        {
            txtPermitNO.Focus();
        }

        protected void txtPermitDT_TextChanged(object sender, EventArgs e)
        {
            ddlStatus.Focus();
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSave.Focus();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                if (txtCompID.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select Company";
                    txtCompNM.Focus();
                }
                else if (txtCrDt.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select Job Creattion Date.";
                    txtCrDt.Focus();
                }
                else if (txtJobYear.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Type Job Year.";
                    txtJobYear.Focus();
                }
                else if (txtPartyID.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select Party.";
                    txtPartyNM.Focus();
                }
                else if (txtConsigneeNM.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select Consignee Name.";
                    txtConsigneeNM.Focus();
                }
                else if (txtGoodsDesc.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select Goods Desc.";
                    txtGoodsDesc.Focus();
                }
                else if (txtPkgDet.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select Packages Details.";
                    txtPkgDet.Focus();
                }
                else if (txtCNFVUSD.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select C&amp;F Value (USD/EUR).";
                    txtCNFVUSD.Focus();
                }
                else if (txtChangeRT.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select Exchange Rate &amp; Type&nbsp;.";
                    txtChangeRT.Focus();
                }
                else if (txtGrossWeight.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select Gross Weight.";
                    txtGrossWeight.Focus();
                }
                else if (txtNetWeight.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select Net Weight.";
                    txtNetWeight.Focus();
                }
                else if (txtInvoiceNo.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select Invoice No.";
                    txtInvoiceNo.Focus();
                }
                else if (txtInvoiceDT.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select Invoice Date .";
                    txtInvoiceDT.Focus();
                }
                else if (txtBENO.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select B/E  No.";
                    txtBENO.Focus();
                }
                else if (txtBEDT.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select B/E  Date .";
                    txtBEDT.Focus();
                }
                else if (txtLCNO.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select L/C No.";
                    txtLCNO.Focus();
                }
                else if (txtLCDT.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select L/C Date .";
                    txtLCDT.Focus();
                }
                else if (txtUdNo.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select UD NO.";
                    txtUdNo.Focus();
                }
                else if (txtUdDate.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select UD Date.";
                    txtUdDate.Focus();
                }
                else if (txtExpFNo.Text == "" && ddlJobTp.Text=="EXPORT")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select Exp. Form NO.";
                    txtExpFNo.Focus();
                }
                else if (txtExpFDate.Text == "" && ddlJobTp.Text == "EXPORT")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select Exp. Form Date .";
                    txtExpFDate.Focus();
                }
                else if (txtComRemarks.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select Remarks.";
                    txtComRemarks.Focus();
                }
                else
                {
                    iob.CompID = txtCompID.Text;
                    iob.JobTP = ddlJobTp.Text;
                    iob.JobCrDT = DateTime.Parse(txtCrDt.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    iob.JobYear = Convert.ToInt16(txtJobYear.Text);
                    if (btnEdit.Text == "Edit")
                    {
                        check_Job_No();
                        iob.JobNo = Convert.ToInt64(txtNo.Text);
                    }
                    else
                    {
                        if (ddlJobNo.Text == "Select")
                        {
                            lblError.Visible = true;
                            lblError.Text = "Select Job No.";
                            ddlJobNo.Focus();
                        }
                        else
                            iob.JobNo = Convert.ToInt64(ddlJobNo.Text);
                    }
                    iob.RegID = ddlRegID.Text;
                    iob.PartyID = txtPartyID.Text;
                    iob.ConsigneeName = txtConsigneeNM.Text;
                    iob.ConsigneeAddress = txtConsigneeAdd.Text;
                    iob.GoodsDesc = txtGoodsDesc.Text;
                    iob.PkgDetails = txtPkgDet.Text;
                    iob.ContainerNo = txtContainerNo.Text;
                    iob.SupplierNM = txtSuppNM.Text;
                    if (txtCNFVUSD.Text == "")
                        iob.CnfUSD = 0;
                    else
                        iob.CnfUSD = Convert.ToDecimal(txtCNFVUSD.Text);

                    if (txtChangeRT.Text == "")
                        iob.ExchangeRT = 0;
                    else
                        iob.ExchangeRT = Convert.ToDecimal(txtChangeRT.Text);

                    iob.ExTP = txtExTP.Text;

                    if (txtCNFVBDT.Text == "")
                        iob.CnfBDT = 0;
                    else
                        iob.CnfBDT = Convert.ToDecimal(txtCNFVBDT.Text);

                    if (txtGrossWeight.Text == "")
                        iob.GrossWeight = 0;
                    else
                        iob.GrossWeight = Convert.ToDecimal(txtGrossWeight.Text);

                    if (txtNetWeight.Text == "")
                        iob.NetWeight = 0;
                    else
                        iob.NetWeight = Convert.ToDecimal(txtNetWeight.Text);

                    iob.BeNO = txtBENO.Text;
                    if (txtBEDT.Text == "")
                        txtBEDT.Text = "1900-01-01";
                    iob.BeDT = DateTime.Parse(txtBEDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    if (txtWhDT.Text == "")
                        txtWhDT.Text = "1900-01-01";
                    iob.WharfentDT = DateTime.Parse(txtWhDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    if (txtDelDT.Text == "")
                        txtDelDT.Text = "1900-01-01";
                    iob.DelDT = DateTime.Parse(txtDelDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);

                    if (txtAssessableVal.Text == "")
                        iob.AssessableAMT = 0;
                    else
                        iob.AssessableAMT = Convert.ToDecimal(txtAssessableVal.Text);


                    if (txtCommission.Text == "")
                        iob.Commission = 0;
                    else
                        iob.Commission = Convert.ToDecimal(txtCommission.Text);

                    iob.CrfNO = txtCRFNO.Text;
                    if (txtCRFDT.Text == "")
                        txtCRFDT.Text = "1900-01-01";
                    iob.CrfDT = DateTime.Parse(txtCRFDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    iob.InNO = txtInvoiceNo.Text;
                    if (txtInvoiceDT.Text == "")
                        txtInvoiceDT.Text = "1900-01-01";
                    iob.InDT = DateTime.Parse(txtInvoiceDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    iob.BlNO = txtBLNO.Text;
                    if (txtBLDT.Text == "")
                        txtBLDT.Text = "1900-01-01";
                    iob.BlDT = DateTime.Parse(txtBLDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    iob.LcNO = txtLCNO.Text;
                    if (txtLCDT.Text == "")
                        txtLCDT.Text = "1900-01-01";
                    iob.LcDT = DateTime.Parse(txtLCDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    iob.PermitNO = txtPermitNO.Text;
                    if (txtPermitDT.Text == "")
                        txtPermitDT.Text = "1900-01-01";
                    iob.PermitDT = DateTime.Parse(txtPermitDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    iob.Awbno = txtAwbNo.Text;
                    if (txtAwbDT.Text == "")
                        txtAwbDT.Text = "1900-01-01";
                    iob.Awbdt = DateTime.Parse(txtAwbDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    iob.Hbl = txtHBlNo.Text;
                    if (txtHblDT.Text == "")
                        txtHblDT.Text = "1900-01-01";
                    iob.Hbldt = DateTime.Parse(txtHblDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    iob.Hawbno = txtHawbNo.Text;
                    if (txtHawbDT.Text == "")
                        txtHawbDT.Text = "1900-01-01";
                    iob.Hawbdt = DateTime.Parse(txtHawbDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    iob.UnderTakeNo = txtUnderTakeNo.Text;
                    if (txtUnderTakeDt.Text == "")
                        txtUnderTakeDt.Text = "1900-01-01";
                    iob.UnderTakeDt = DateTime.Parse(txtUnderTakeDt.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    iob.ComRemarks = txtComRemarks.Text;
                    iob.Status = ddlStatus.Text;

                    iob.InTM = Global.Timezone(DateTime.Now);
                    iob.UpTM = Global.Timezone(DateTime.Now);


                    iob.FVessel = txtVessel.Text;
                    iob.RotNo = txtRotno.Text;
                    iob.UdNo = txtUdNo.Text;
                    iob.ExpFNo = txtExpFNo.Text;

                    if (txtetaDate.Text == "")
                        txtetaDate.Text = "1900-01-01";
                    iob.EtaDate = DateTime.Parse(txtetaDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    if (txtetbDate.Text == "")
                        txtetbDate.Text = "1900-01-01";
                    iob.EtbDate = DateTime.Parse(txtetbDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    if (txtUdDate.Text == "")
                        txtUdDate.Text = "1900-01-01";
                    iob.UdDate = DateTime.Parse(txtUdDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    if (txtExpFDate.Text == "")
                        txtExpFDate.Text = "1900-01-01";
                    iob.ExpFDate = DateTime.Parse(txtExpFDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);


                    if (btnEdit.Text == "Edit")
                        iob.UserNM = HttpContext.Current.Session["UserName"].ToString();
                    else
                        iob.UpdateUser = HttpContext.Current.Session["UserName"].ToString();
                    iob.Ipaddress = HttpContext.Current.Session["IpAddress"].ToString();
                    iob.Userpc = HttpContext.Current.Session["PCName"].ToString();

                    if (btnEdit.Text == "Edit")
                        dob.save_cnf_job(iob);
                    else
                    {
                        if (btnEdit.Text == "Edit")
                            dob.save_cnf_job(iob);
                        else
                        {
                            try
                            {

                                string alldata =
                                    Global.StringData(@"SELECT CONVERT(NVARCHAR(50),JOBCDT,103)+'  '+COMPID+'  '+REGID+'  '+CONVERT(NVARCHAR(50),JOBYY,103)+'  '+JOBTP+' 
                         '+CONVERT(NVARCHAR(50),JOBNO,103)+'  '+ISNULL(PARTYID,'(NULL)')+'  '+ISNULL(CONSIGNEENM,'(NULL)')+'  
                         '+ISNULL(CONSIGNEEADD,'(NULL)')+'  '+ISNULL(SUPPLIERNM,'(NULL)')+'  '+ISNULL(PKGS,'(NULL)')+'  '+ISNULL(GOODS,'(NULL)')+'  
                         '+CONVERT(NVARCHAR(50),WTGROSS,103)+'  '+CONVERT(NVARCHAR(50),WTNET,103)+'  '+CONVERT(NVARCHAR(50),CNFV_USD,103)+'  
                         '+ISNULL(CNFV_ETP,'(NULL)')+'  '+CONVERT(NVARCHAR(50),CNFV_ERT,103)+'  '+CONVERT(NVARCHAR(50),CNFV_BDT,103)+'  
                         '+CONVERT(NVARCHAR(50),CRFV_USD,103)+'  '+CONVERT(NVARCHAR(50),ASSV_BDT,103)+'  '+CONVERT(NVARCHAR(50),COMM_AMT,103)+'  
                         '+ISNULL(CONTNO,'(NULL)')+'  '+ISNULL(DOCINVNO,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),DOCRCVDT,103),'(NULL)')+'  
                         '+ISNULL(CRFNO,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),CRFDT,103),'(NULL)')+'  '+ISNULL(BENO,'(NULL)')+'  
                         '+ISNULL(CONVERT(NVARCHAR(50),BEDT,103),'(NULL)')+'  '+ISNULL(BLNO,'(NULL)')+'  
                         '+ISNULL(CONVERT(NVARCHAR(50),BLDT,103),'(NULL)')+'  '+ISNULL(BTNO,'(NULL)')+'  
                         '+ISNULL(CONVERT(NVARCHAR(50),BTDT,103),'(NULL)')+'  '+ISNULL(LCNO,'(NULL)')+'  
                         '+ISNULL(CONVERT(NVARCHAR(50),LCDT,103),'(NULL)')+'  '+ISNULL(LCANO,'(NULL)')+'  
                         '+ISNULL(CONVERT(NVARCHAR(50),LCADT,103),'(NULL)')+'  '+ISNULL(PERMITNO,'(NULL)')+'  
                         '+ISNULL(CONVERT(NVARCHAR(50),PERMITDT,103),'(NULL)')+'  '+ISNULL(BILLNOM,'(NULL)')+'  
                         '+ISNULL(CONVERT(NVARCHAR(50),BILLDTM,103),'(NULL)')+'  '+ISNULL(MVESSEL,'(NULL)')+'  
                         '+ISNULL(FVESSEL,'(NULL)')+'  '+ISNULL(MARKSNO,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),ETB,103),'(NULL)')+'  
                         '+ISNULL(BERTHSNO,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),ARRIVEDT,103),'(NULL)')+'  '+ISNULL(ROTNO,'(NULL)')+'  
                         '+ISNULL(CONVERT(NVARCHAR(50),DELIVERYDT,103),'(NULL)')+'  
                         '+ISNULL(CONVERT(NVARCHAR(50),CLDT,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),WFDT,103),'(NULL)')+'  
                         '+ISNULL(AWBNO,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),AWBDT,103),'(NULL)')+'  '+ISNULL(HBLNO,'(NULL)')+'  
                         '+ISNULL(CONVERT(NVARCHAR(50),HBLDT,103),'(NULL)')+'  '+ISNULL(HAWBNO,'(NULL)')+'  
                         '+ISNULL(CONVERT(NVARCHAR(50),HAWBDT,103),'(NULL)')+'  '+ISNULL(UNTKNO,'(NULL)')+'  
                         '+ISNULL(CONVERT(NVARCHAR(50),UNTKDT,103),'(NULL)')+'  '+ISNULL(COM_REMARKS,'(NULL)')+'  
                         '+ISNULL(CONVERT(NVARCHAR(50),STATUS,103),'(NULL)')+'  '+ISNULL(USERPC,'(NULL)')+'  '+ISNULL(USERID,'(NULL)')+'  
                         '+ISNULL(UPDATEUSERID,'(NULL)')+'  '+CONVERT(NVARCHAR(50),INTIME,103)+'  
                         '+ISNULL(CONVERT(NVARCHAR(50),UPDATETIME,103),'(NULL)')+'  '+IPADDRESS+'  
                         '+ISNULL(CONVERT(NVARCHAR(50),ETADT,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),ETBDT,103),'(NULL)')+'  
                         '+ISNULL(UDNO,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),UDDT,103),'(NULL)')+'  '+ISNULL(EXPFNO,'(NULL)')+'  
                         '+ISNULL(CONVERT(NVARCHAR(50),EXPFDT,103),'(NULL)') FROM CNF_JOB
                        WHERE JOBYY =" + iob.JobYear + " AND JOBTP ='" + iob.JobTP + "' AND JOBNO =" + iob.JobNo + "");
                                SqlConnection con = new SqlConnection(Global.connection);
                                SqlCommand cmdinsert =
                                    new SqlCommand(
                                        "insert into ASL_DLT values('CNF_JOB-UPDATE',@DESCRP,@USERPC,@USERID,@INTIME,@IPADD)",
                                        con);
                                cmdinsert.Parameters.AddWithValue("@DESCRP", alldata);
                                cmdinsert.Parameters.AddWithValue("@USERPC", iob.Userpc);
                                cmdinsert.Parameters.AddWithValue("@USERID", iob.UpdateUser);
                                cmdinsert.Parameters.AddWithValue("@INTIME", iob.InTM);
                                cmdinsert.Parameters.AddWithValue("@IPADD", iob.Ipaddress);
                                con.Open();
                                cmdinsert.ExecuteNonQuery();
                                con.Close();

                            }
                            catch (Exception)
                            {

                                throw;
                            }

                            dob.update_cnf_job(iob);
                        }
                    }
                    Refresh();

                    if (btnEdit.Text == "Edit")
                    {
                        lblError.Visible = true;
                        lblError.Text = "Job No " + iob.JobNo + " Informations are Successfully Saved.";
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = "Job No " + iob.JobNo + " Informations are Successfully Updated.";
                    }
                }
            }
        }

        private void Refresh()
        {
            lblError.Visible = false;
            txtCompNM.Text = "";
            txtCompID.Text = "";
            ddlJobTp.SelectedIndex = -1;
            DateTime td = Global.Timezone(DateTime.Now);
            txtCrDt.Text = td.ToString("dd/MM/yyyy");
            txtJobYear.Text = td.ToString("yyyy");
            if (btnEdit.Text == "Edit")
            {
                check_Job_No();
            }
            else
            {
                ddlJobNo.SelectedIndex = -1;
            }
            ddlRegID.SelectedIndex = -1;
            txtPartyNM.Text = "";
            txtPartyID.Text = "";
            txtConsigneeNM.Text = "";
            txtConsigneeAdd.Text = "";
            txtGoodsDesc.Text = "";
            txtPkgDet.Text = "";
            txtContainerNo.Text = "";
            txtSuppNM.Text = "";
            txtCNFVUSD.Text = ".00";
            txtCRFVUSD.Text = ".00";
            txtChangeRT.Text = ".00";
            txtExTP.Text = "";
            txtCNFVBDT.Text = ".00";
            txtGrossWeight.Text = ".00";
            txtNetWeight.Text = ".00";
            txtCRFNO.Text = "";
            txtCRFDT.Text = "";
            txtInvoiceNo.Text = "";
            txtInvoiceDT.Text = "";
            txtBENO.Text = "";
            txtBEDT.Text = "";
            txtBLNO.Text = "";
            txtBLDT.Text = "";
            txtLCNO.Text = "";
            txtLCDT.Text = "";
            txtPermitNO.Text = "";
            txtPermitDT.Text = "";
            txtWhDT.Text = "";
            txtDelDT.Text = "";
            txtAssessableVal.Text = ".00";
            txtCommission.Text = ".00";
            txtAwbNo.Text = "";
            txtAwbDT.Text = "";
            txtHBlNo.Text = "";
            txtHblDT.Text = "";
            txtHawbDT.Text = "";
            txtUnderTakeNo.Text = "";
            txtUnderTakeDt.Text = "";
            txtContainerNo.Text = "";
            ddlStatus.SelectedIndex = -1;
            txtComRemarks.Text = "";
            txtVessel.Text = "";
            txtRotno.Text = "";

            txtetaDate.Text = "";
            txtetbDate.Text = "";
            txtUdNo.Text = "";
            txtUdDate.Text = "";
            txtExpFNo.Text = "";
            txtExpFDate.Text = "";

            txtCompNM.Focus();

        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (btnEdit.Text == "Edit")
            {
                txtPartyNM.ReadOnly = true;
                btnEdit.Text = "New";
                txtNo.Visible = false;
                ddlJobNo.Visible = true;
                btnSave.Text = "Update";
                Refresh();
                string uTp = HttpContext.Current.Session["UserTp"].ToString();
                string brCD = HttpContext.Current.Session["BrCD"].ToString();

                if (uTp == "ADMIN")
                {
                    Global.dropDownAddWithSelect(ddlJobNo, "SELECT JOBNO FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "'");
                }
                else
                {
                    Global.dropDownAddWithSelect(ddlJobNo, "SELECT JOBNO FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND COMPID ='" + brCD + "'");
                }
                ddlJobNo.Focus();
            }
            else
            {
                txtPartyNM.ReadOnly = false;
                btnEdit.Text = "Edit";
                txtNo.Visible = true;
                ddlJobNo.Visible = false;
                check_Job_No();
                Refresh();
                btnSave.Text = "Save";
                txtCompNM.Focus();
            }
        }

        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlJobNo.Text == "Select")
            {
                lblError.Visible = true;
                lblError.Text = "Select Job No.";
                ddlJobNo.Focus();
            }
            else
            {
                lblError.Visible = false;

                Global.txtAdd("SELECT COMPID FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtCompID);
                Global.txtAdd("SELECT ASL_COMPANY.BRANCHID + '-' + ASL_COMPANY.COMPNM FROM ASL_COMPANY INNER JOIN CNF_JOB ON ASL_COMPANY.COMPID = CNF_JOB.COMPID " +
                    " WHERE ASL_COMPANY.COMPID='" + txtCompID.Text + "'  AND CNF_JOB.JOBYY =" + txtJobYear.Text + " AND CNF_JOB.JOBTP ='" + ddlJobTp.Text + "' AND CNF_JOB.JOBNO =" + ddlJobNo.Text + "", txtCompNM);
                Global.lblAdd("SELECT JOBTP FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", lblJobTP);
                ddlJobTp.Text = lblJobTP.Text;
                Global.txtAdd("SELECT CONVERT(NVARCHAR(20),JOBCDT,103) AS JOBCDT FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtCrDt);
                Global.txtAdd("SELECT JOBYY FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtJobYear);
                Global.lblAdd("SELECT REGID FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", lblJobReg);
                ddlRegID.Text = lblJobReg.Text;
                Global.txtAdd("SELECT PARTYID FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtPartyID);
                Global.txtAdd("SELECT GL_ACCHART.ACCOUNTNM FROM GL_ACCHART INNER JOIN CNF_JOB ON GL_ACCHART.ACCOUNTCD = CNF_JOB.PARTYID WHERE CNF_JOB.JOBYY =" + txtJobYear.Text + " AND CNF_JOB.JOBTP ='" + ddlJobTp.Text + "' AND CNF_JOB.JOBNO =" + ddlJobNo.Text + "", txtPartyNM);
                Global.txtAdd("SELECT CONSIGNEENM FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtConsigneeNM);
                Global.txtAdd("SELECT CONSIGNEEADD FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtConsigneeAdd);
                Global.txtAdd("SELECT GOODS FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtGoodsDesc);
                Global.txtAdd("SELECT PKGS FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtPkgDet);
                Global.txtAdd("SELECT CONTNO FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtContainerNo);
                Global.txtAdd("SELECT SUPPLIERNM FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtSuppNM);
                Global.txtAdd("SELECT CNFV_USD FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtCNFVUSD);
                Global.txtAdd("SELECT CRFV_USD FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtCRFVUSD);
                Global.txtAdd("SELECT CNFV_ERT FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtChangeRT);
                Global.txtAdd("SELECT CNFV_ETP FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtExTP);
                Global.txtAdd("SELECT CNFV_BDT FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtCNFVBDT);
                Global.txtAdd("SELECT WTGROSS FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtGrossWeight);
                Global.txtAdd("SELECT WTNET FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtNetWeight);
                Global.txtAdd("SELECT CRFNO FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtCRFNO);
                Global.txtAdd("SELECT (CASE WHEN CRFDT ='1900-01-01' THEN NULL ELSE CONVERT(NVARCHAR(20),CRFDT,103) END) AS CRFDT FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtCRFDT);
                Global.txtAdd("SELECT DOCINVNO FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtInvoiceNo);
                Global.txtAdd("SELECT (CASE WHEN DOCRCVDT ='1900-01-01' THEN NULL ELSE CONVERT(NVARCHAR(20),DOCRCVDT,103) END) AS DOCRCVDT FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtInvoiceDT);
                Global.txtAdd("SELECT BENO FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtBENO);
                Global.txtAdd("SELECT (CASE WHEN BEDT ='1900-01-01' THEN NULL ELSE CONVERT(NVARCHAR(20),BEDT,103) END) AS BEDT FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtBEDT);
                Global.txtAdd("SELECT BLNO FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtBLNO);
                Global.txtAdd("SELECT (CASE WHEN BLDT ='1900-01-01' THEN NULL ELSE CONVERT(NVARCHAR(20),BLDT,103) END) AS BLDT FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtBLDT);
                Global.txtAdd("SELECT LCNO FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtLCNO);
                Global.txtAdd("SELECT (CASE WHEN LCDT ='1900-01-01' THEN NULL ELSE CONVERT(NVARCHAR(20),LCDT,103) END) AS LCDT FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtLCDT);
                Global.txtAdd("SELECT PERMITNO FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtPermitNO);
                Global.txtAdd("SELECT (CASE WHEN PERMITDT ='1900-01-01' THEN NULL ELSE CONVERT(NVARCHAR(20),PERMITDT,103) END) AS PERMITDT FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtPermitDT);
                Global.txtAdd("SELECT (CASE WHEN WFDT ='1900-01-01' THEN NULL ELSE CONVERT(NVARCHAR(20),WFDT,103) END) AS WFDT FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtWhDT);
                Global.txtAdd("SELECT (CASE WHEN DELIVERYDT ='1900-01-01' THEN NULL ELSE CONVERT(NVARCHAR(20),DELIVERYDT,103) END) AS DELIVERYDT FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtDelDT);
                Global.txtAdd("SELECT ASSV_BDT FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtAssessableVal);
                Global.txtAdd("SELECT COMM_AMT FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtCommission);
                Global.txtAdd("SELECT AWBNO FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtAwbNo);
                Global.txtAdd("SELECT (CASE WHEN AWBDT ='1900-01-01' THEN NULL ELSE CONVERT(NVARCHAR(20),AWBDT,103) END) AS AWBDT FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtAwbDT);
                Global.txtAdd("SELECT HBLNO FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtHBlNo);
                Global.txtAdd("SELECT (CASE WHEN HBLDT ='1900-01-01' THEN NULL ELSE CONVERT(NVARCHAR(20),HBLDT,103) END) AS AWBDT FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtHblDT);
                Global.txtAdd("SELECT HAWBNO FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtHawbNo);
                Global.txtAdd("SELECT (CASE WHEN HAWBDT ='1900-01-01' THEN NULL ELSE CONVERT(NVARCHAR(20),HAWBDT,103) END) AS AWBDT FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtHawbDT);
                Global.txtAdd("SELECT UNTKNO FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtUnderTakeNo);
                Global.txtAdd("SELECT (CASE WHEN UNTKDT ='1900-01-01' THEN NULL ELSE CONVERT(NVARCHAR(20),UNTKDT,103) END) AS UNTKDT FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtUnderTakeDt);

                Global.txtAdd("SELECT (CASE WHEN ETADT ='1900-01-01' THEN NULL ELSE CONVERT(NVARCHAR(20),ETADT,103) END) AS ETADT FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtetaDate);
                Global.txtAdd("SELECT (CASE WHEN ETBDT ='1900-01-01' THEN NULL ELSE CONVERT(NVARCHAR(20),ETBDT,103) END) AS ETBDT FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtetbDate);
                Global.txtAdd("SELECT (CASE WHEN UDDT ='1900-01-01' THEN NULL ELSE CONVERT(NVARCHAR(20),UDDT,103) END) AS UDDT FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtUdDate);
                Global.txtAdd("SELECT (CASE WHEN EXPFDT ='1900-01-01' THEN NULL ELSE CONVERT(NVARCHAR(20),EXPFDT,103) END) AS EXPFDT FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtExpFDate);

                Global.txtAdd("SELECT FVESSEL FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtVessel);
                Global.txtAdd("SELECT ROTNO FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtRotno);
                Global.txtAdd("SELECT UDNO FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtUdNo);
                Global.txtAdd("SELECT EXPFNO FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtExpFNo);


                Global.txtAdd("SELECT COM_REMARKS FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", txtComRemarks);
                Global.lblAdd("SELECT STATUS FROM CNF_JOB WHERE JOBYY =" + txtJobYear.Text + " AND JOBTP ='" + ddlJobTp.Text + "' AND JOBNO =" + ddlJobNo.Text + "", lblStatus);
                txtCompNM.Focus();
            }
        }

        protected void txtCNFVUSD_TextChanged(object sender, EventArgs e)
        {
            txtChangeRT.Text = ".00";
            txtCRFVUSD.Focus();
        }

        protected void txtAwbDT_TextChanged(object sender, EventArgs e)
        {
            txtHBlNo.Focus();
        }

        protected void txtHblDT_TextChanged(object sender, EventArgs e)
        {
            txtHawbNo.Focus();
        }

        protected void txtHawbDT_TextChanged(object sender, EventArgs e)
        {
            txtUnderTakeNo.Focus();
        }

        protected void txtInvoiceNo_TextChanged(object sender, EventArgs e)
        {
            conn = new SqlConnection(Global.connection);

            conn.Open();
            cmdd = new SqlCommand("SELECT DOCINVNO FROM CNF_JOB WHERE DOCINVNO =@DOCINVNO", conn);
            cmdd.Parameters.Clear();
            cmdd.Parameters.AddWithValue("@DOCINVNO", txtInvoiceNo.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmdd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblError.Visible = true;
                lblError.Text = "Invoice no used in other job no.";
                txtInvoiceNo.Text = "";
                txtInvoiceDT.Text = "";
                txtInvoiceNo.Focus();
            }
            else
            {
                lblError.Visible = false;
                txtInvoiceDT.Focus();
            }
        }

        protected void txtBENO_TextChanged(object sender, EventArgs e)
        {
            conn = new SqlConnection(Global.connection);

            conn.Open();
            cmdd = new SqlCommand("SELECT BENO FROM CNF_JOB WHERE BENO =@BENO", conn);
            cmdd.Parameters.Clear();
            cmdd.Parameters.AddWithValue("@BENO", txtBENO.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmdd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblError.Visible = true;
                lblError.Text = "B/E no used in other job no.";
                txtBENO.Text = "";
                txtBEDT.Text = "";
                txtBENO.Focus();
            }
            else
            {
                lblError.Visible = false;
                txtBEDT.Focus();
            }
        }

        protected void txtPermitNO_TextChanged(object sender, EventArgs e)
        {
            conn = new SqlConnection(Global.connection);

            conn.Open();
            cmdd = new SqlCommand("SELECT PERMITNO FROM CNF_JOB WHERE PERMITNO =@PERMITNO", conn);
            cmdd.Parameters.Clear();
            cmdd.Parameters.AddWithValue("@PERMITNO", txtPermitNO.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmdd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblError.Visible = true;
                lblError.Text = "Permit no used in other job no.";
                txtPermitNO.Text = "";
                txtPermitDT.Text = "";
                txtPermitNO.Focus();
            }
            else
            {
                lblError.Visible = false;
                txtPermitDT.Focus();
            }
        }

        protected void txtUnderTakeDt_TextChanged(object sender, EventArgs e)
        {
            txtContainerNo.Focus();
        }
    }
}