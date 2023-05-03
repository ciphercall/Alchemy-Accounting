﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace AlchemyAccounting.CNF.report.ui
{
    public partial class RptJobInfoForm : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["UserName"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                if (!IsPostBack)
                {
                    DateTime td = DateTime.Now;
                    txtJobYear.Text = td.ToString("yyyy");


                    Session["Companyid"] = "";
                    Session["company"] = "";
                    Session["JOBYY"] = "";
                    Session["JOBTP"] = "";
                    txtBranchID.Focus();
                }
            }
        }


        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListBranchID(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            string uTp = HttpContext.Current.Session["UserTp"].ToString();
            string brCD = HttpContext.Current.Session["BrCD"].ToString();
            // Try to use parameterized inline query/sp to protect sql injection
            SqlCommand cmd = new SqlCommand("", conn);
            if (uTp == "ADMIN")
            {
                cmd = new SqlCommand("SELECT  (COMPNM +'|'+ BRANCHID) AS CB FROM ASL_COMPANY WHERE COMPNM LIKE '" + prefixText + "%' ", conn);
            }
            else
            {
                cmd = new SqlCommand("SELECT (COMPNM +'|'+ BRANCHID) AS CB FROM ASL_COMPANY WHERE COMPNM LIKE '" + prefixText + "%' ", conn);
            }
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["CB"].ToString());
            return CompletionSet.ToArray();
        }



        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListYear(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            string uTp = HttpContext.Current.Session["UserTp"].ToString();
            string brCD = HttpContext.Current.Session["BrCD"].ToString();
            // Try to use parameterized inline query/sp to protect sql injection
            SqlCommand cmd = new SqlCommand("", conn);

            cmd = new SqlCommand("SELECT DISTINCT JOBYY FROM CNF_JOB WHERE JOBYY  LIKE '" + prefixText + "%' ", conn);

            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["JOBYY"].ToString());
            return CompletionSet.ToArray();
        }

        protected void txtBranchID_TextChanged(object sender, EventArgs e)
        {

            string companyName = "";
            string branch = "";

            string searchPar = txtBranchID.Text;

            int splitter = searchPar.IndexOf("|");
            if (splitter != -1)
            {
                string[] lineSplit = searchPar.Split('|');

                companyName = lineSplit[0];
                branch = lineSplit[1];


                txtBranchID.Text = companyName.Trim();
                lblbranch.Text = branch.Trim();

            }


            if (txtBranchID.Text == "")
            {
                lblbranchtag.Visible = false;
                Label1.Visible = false;
                lblbranch.Visible = false;
                txtBranchID.Focus();
            }
            else
            {

                lblbranchtag.Visible = true;
                Label1.Visible = true;
                lblbranch.Visible = true;
                Global.lblAdd("select COMPID from ASL_COMPANY where COMPNM='" + txtBranchID.Text + "' and BRANCHID='" + lblbranch.Text + "' ", lblCompanyID);
                ddlJobType.Focus();
            }



        }

        protected void txtJobYear_TextChanged(object sender, EventArgs e)
        {
            btnSubmit.Focus();
        }

        protected void ddlJobType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtJobYear.Focus();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            Session["Companyid"] = lblCompanyID.Text;
            Session["company"] = txtBranchID.Text;
            Session["JOBYY"] = txtJobYear.Text;
            Session["JOBTP"] = ddlJobType.Text;
            Session["branch"] = ddlJobType.Text;

            if (txtBranchID.Text == "")
            {
                lblErrmsg.Text = "";
                lblErrmsg.Visible = true;
                lblErrmsg.Text = "Office Name missing.";
                txtBranchID.Focus();
            }
            else if (txtJobYear.Text == "")
            {
                lblErrmsg.Text = "";
                lblErrmsg.Visible = true;
                lblErrmsg.Text = "Job Year missing.";
                txtJobYear.Focus();
            }

            else
            {
                lblErrmsg.Text = "";
                lblErrmsg.Visible = false;
                Page.ClientScript.RegisterStartupScript(
                         this.GetType(), "OpenWindow", "window.open('../vis-rep/RptJobInfo.aspx','_newtab');", true);
            }
        }


    }
}