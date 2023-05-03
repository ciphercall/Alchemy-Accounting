using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace AlchemyAccounting.CNF.PartyCommission
{
    public partial class commission : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmdd;

        PartyCommissionDataAccess pcd = new PartyCommissionDataAccess();
        PartyCommissionModel pcm = new PartyCommissionModel();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                if (!IsPostBack)
                {
                    txtParty.Focus();
                    GridShow();
                }
            }
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

        protected void txtParty_TextChanged(object sender, EventArgs e)
        {
            if (txtParty.Text == "")
            {
                lblErrMsg.Visible = true;
                lblErrMsg.Text = "Select party name.";
                txtParty.Focus();
            }
            else
            {
                Global.txtAdd("SELECT ACCOUNTCD FROM GL_ACCHART WHERE ACCOUNTNM='" + txtParty.Text + "' AND STATUSCD ='P'", txtPartyID);
                if (txtPartyID.Text == "")
                {
                    lblErrMsg.Visible = true;
                    lblErrMsg.Text = "Select party name.";
                    txtParty.Text = "";
                    txtPartyID.Text = "";
                    txtParty.Focus();
                }
                else
                {
                    GridShow();
                    DropDownList ddlExctype = (DropDownList)gvDetails.FooterRow.FindControl("ddlExctype");
                    ddlExctype.Focus();
                }
            }
        }

        public void GridShow()
        {
            conn = new SqlConnection(Global.connection);
            conn.Open();

            cmdd = new SqlCommand("SELECT CNF_COMMISSION.COMMSL, CNF_COMMISSION.EXCTP, CNF_COMMISSION.VALUEFR, CNF_COMMISSION.VALUETO, CNF_COMMISSION.VALUETP, CNF_COMMISSION.COMMAMT FROM CNF_COMMISSION INNER JOIN GL_ACCHART ON CNF_COMMISSION.PARTYID = GL_ACCHART.ACCOUNTCD WHERE GL_ACCHART.ACCOUNTCD='" + txtPartyID.Text + "'", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmdd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();


            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
                gvDetails.Visible = true;

                DropDownList ddlExctype = (DropDownList)gvDetails.FooterRow.FindControl("ddlExctype");
                ddlExctype.Focus();

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

        protected void gvDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDetails.EditIndex = -1;
            GridShow();
        }

        protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Label lblSerial = (Label)gvDetails.FooterRow.FindControl("lblSerial");
            DropDownList ddlExctype = (DropDownList)gvDetails.FooterRow.FindControl("ddlExctype");
            TextBox txtfrom = (TextBox)gvDetails.FooterRow.FindControl("txtfrom");
            TextBox txtTO = (TextBox)gvDetails.FooterRow.FindControl("txtTO");
            DropDownList ddlvalueTp = (DropDownList)gvDetails.FooterRow.FindControl("ddlvalueTp");
            TextBox txtAmount = (TextBox)gvDetails.FooterRow.FindControl("txtAmount");


            if (e.CommandName.Equals("SaveCon"))
            {

                if (txtPartyID.Text == "")
                {
                    lblErrMsg.Visible = true;
                    lblErrMsg.Text = "particular input missing";
                    txtParty.Focus();
                }
                else if (txtfrom.Text == "" || txtfrom.Text == "0" || txtfrom.Text == ".00" || txtfrom.Text == "0.00")
                {
                    lblErrMsg.Visible = true;
                    lblErrMsg.Text = "particular input missing";
                    txtfrom.Focus();
                }
                else if (txtTO.Text == "" || txtTO.Text == "0" || txtTO.Text == ".00" || txtTO.Text == "0.00")
                {
                    lblErrMsg.Visible = true;
                    lblErrMsg.Text = "particular input missing";
                    txtTO.Focus();
                }
                else if (txtAmount.Text == "" || txtAmount.Text == "0" || txtAmount.Text == ".00" || txtAmount.Text == "0.00")
                {
                    lblErrMsg.Visible = true;
                    lblErrMsg.Text = "particular input missing";
                    txtAmount.Focus();
                }
                else
                {
                    lblErrMsg.Visible = false;

                    pcm.PARTYID = txtPartyID.Text;
                    pcm.COMMSL = Convert.ToInt64(gvDetails.FooterRow.Cells[0].Text);
                    pcm.EXCTP = ddlExctype.Text;
                    pcm.VALUEFROM = Convert.ToDecimal(txtfrom.Text);
                    pcm.VALUETO = Convert.ToDecimal(txtTO.Text);
                    pcm.VALUETP = ddlvalueTp.Text;
                    pcm.COMMAMT = Convert.ToDecimal(txtAmount.Text);
                    pcm.InTime = DateTime.Now;
                    pcm.UpdateTime = DateTime.Now;
                    pcm.UserPC = HttpContext.Current.Session["PCName"].ToString();
                    pcm.IPAddress = HttpContext.Current.Session["IpAddress"].ToString();

                    pcd.SaveExpenseInfo(pcm);

                    GridShow();

                }
            }
        }

        protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {

                Global.lblAdd(" select MAX (COMMSL) from CNF_COMMISSION where PARTYID='" + txtPartyID.Text + "' ", lblChkInternalID);

                if (lblChkInternalID.Text == "")
                {
                    string cid = "01";
                    e.Row.Cells[0].Text = cid;
                }

                else
                {
                    var id = Int32.Parse(lblChkInternalID.Text) + 1;
                    e.Row.Cells[0].Text = id.ToString();
                }
            }
        }

        protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Label lblSerial = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblSerial");


            pcm.PARTYID = txtPartyID.Text;
            pcm.COMMSL = Convert.ToInt64(lblSerial.Text);


            pcd.DeleteExpenseInfo(pcm);

            gvDetails.EditIndex = -1;
            GridShow();

            DropDownList ddlExctype = (DropDownList)gvDetails.FooterRow.FindControl("ddlExctype");
            ddlExctype.Focus();

        }

        protected void gvDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDetails.EditIndex = e.NewEditIndex;
            GridShow();

            Label lblSerialEdit = (Label)gvDetails.Rows[e.NewEditIndex].FindControl("lblSerialEdit");
            DropDownList ddlExctypeEdit = (DropDownList)gvDetails.Rows[e.NewEditIndex].FindControl("ddlExctypeEdit");
            Global.lblAdd("SELECT EXCTP FROM CNF_COMMISSION WHERE PARTYID ='" + txtPartyID.Text + "' AND COMMSL =" + lblSerialEdit.Text + "", lblValTP);
            ddlExctypeEdit.Text = lblValTP.Text;
            DropDownList ddlvalueTpEdit = (DropDownList)gvDetails.Rows[e.NewEditIndex].FindControl("ddlvalueTpEdit");
            Global.lblAdd("SELECT VALUETP FROM CNF_COMMISSION WHERE PARTYID ='" + txtPartyID.Text + "' AND COMMSL =" + lblSerialEdit.Text + "", lblValCommPer);
            ddlvalueTpEdit.Text = lblValCommPer.Text;
            ddlExctypeEdit.Focus();

        }

        protected void gvDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label lblSerialEdit = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblSerialEdit");
            DropDownList ddlExctypeEdit = (DropDownList)gvDetails.Rows[e.RowIndex].FindControl("ddlExctypeEdit");
            TextBox txtfromEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtfromEdit");
            TextBox txtToEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtToEdit");
            DropDownList ddlvalueTpEdit = (DropDownList)gvDetails.Rows[e.RowIndex].FindControl("ddlvalueTpEdit");
            TextBox txtAmountEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtAmountEdit");

            if (txtPartyID.Text == "")
            {
                lblErrMsg.Visible = true;
                lblErrMsg.Text = "particular input missing";
                txtParty.Focus();
            }
            else if (txtfromEdit.Text == "" || txtfromEdit.Text == "0" || txtfromEdit.Text == ".00" || txtfromEdit.Text == "0.00")
            {
                lblErrMsg.Visible = true;
                lblErrMsg.Text = "particular input missing";
                txtfromEdit.Focus();
            }
            else if (txtToEdit.Text == "" || txtToEdit.Text == "0" || txtToEdit.Text == ".00" || txtToEdit.Text == "0.00")
            {
                lblErrMsg.Visible = true;
                lblErrMsg.Text = "particular input missing";
                txtToEdit.Focus();
            }
            else if (txtAmountEdit.Text == "" || txtAmountEdit.Text == "0" || txtAmountEdit.Text == ".00" || txtAmountEdit.Text == "0.00")
            {
                lblErrMsg.Visible = true;
                lblErrMsg.Text = "particular input missing";
                txtAmountEdit.Focus();
            }

            else
            {
                lblErrMsg.Visible = false;

                pcm.PARTYID = txtPartyID.Text;
                pcm.COMMSL = Convert.ToInt64(lblSerialEdit.Text);
                pcm.EXCTP = ddlExctypeEdit.Text;
                pcm.VALUEFROM = Convert.ToDecimal(txtfromEdit.Text);
                pcm.VALUETO = Convert.ToDecimal(txtToEdit.Text);
                pcm.VALUETP = ddlvalueTpEdit.Text;
                pcm.COMMAMT = Convert.ToDecimal(txtAmountEdit.Text);
                pcm.InTime = DateTime.Now;
                pcm.UpdateTime = DateTime.Now;
                pcm.UserPC = HttpContext.Current.Session["PCName"].ToString();
                pcm.IPAddress = HttpContext.Current.Session["IpAddress"].ToString();

                pcd.UpdateExpenseInfo(pcm);

                gvDetails.EditIndex = -1;
                GridShow();

                DropDownList ddlExctype = (DropDownList)gvDetails.FooterRow.FindControl("ddlExctype");
                ddlExctype.Focus();

            }
        }

        protected void ddlExctype_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((DropDownList)sender).NamingContainer);
            DropDownList ddlExctype = (DropDownList)row.FindControl("ddlExctype");
            TextBox txtfrom = (TextBox)row.FindControl("txtfrom");
            txtfrom.Focus();
        }

        protected void ddlExctypeEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((DropDownList)sender).NamingContainer);
            TextBox txtfromEdit = (TextBox)row.FindControl("txtfromEdit");
            txtfromEdit.Focus();
        }

        protected void ddlvalueTp_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((DropDownList)sender).NamingContainer);
            TextBox txtAmount = (TextBox)row.FindControl("txtAmount");
            txtAmount.Focus();
        }

        protected void ddlvalueTpEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((DropDownList)sender).NamingContainer);
            TextBox txtAmountEdit = (TextBox)row.FindControl("txtAmountEdit");
            txtAmountEdit.Focus();
        }
    }
}