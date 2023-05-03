using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace AlchemyAccounting.CNF.Party
{
    public partial class PartyInformation : System.Web.UI.Page
    {
        PartyInformationModel pim = new PartyInformationModel();
        PartyInformationDataAccess pid = new PartyInformationDataAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtPartynm.Focus();
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


        private bool Previousdata(string id)
        {
            bool bflag = false;
            DataTable table = new DataTable();

            try
            {
                PartyInformationDataAccess dob = new PartyInformationDataAccess();
                table = dob.ShowpartyInfo(id);
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


        protected void txtParty_TextChanged(object sender, EventArgs e)
        {

            lblerrmsg.Visible = false;

            Global.txtAdd("SELECT ACCOUNTCD FROM GL_ACCHART WHERE ACCOUNTNM='" + txtPartynm.Text + "' AND STATUSCD ='P'", txtPartyID);

            if (Previousdata(txtPartyID.Text) == true)
            {
                Global.txtAdd("SELECT ADDRESS FROM CNF_PARTY WHERE PARTYID='" + txtPartyID.Text + "' ", txtaddress);
                Global.txtAdd("SELECT CONTACTNO FROM CNF_PARTY WHERE PARTYID='" + txtPartyID.Text + "' ", txtcontact);
                Global.txtAdd("SELECT EMAILID FROM CNF_PARTY WHERE PARTYID='" + txtPartyID.Text + "' ", txtEmail);
                Global.txtAdd("SELECT WEBID FROM CNF_PARTY WHERE PARTYID='" + txtPartyID.Text + "' ", txtwebadd);
                Global.txtAdd("SELECT APNM FROM CNF_PARTY WHERE PARTYID='" + txtPartyID.Text + "' ", txtAPname);
                Global.txtAdd("SELECT APNO FROM CNF_PARTY WHERE PARTYID='" + txtPartyID.Text + "' ", txtapcontact);
            }

            else
            {
                txtaddress.Text = "";
                txtcontact.Text = "";
                txtEmail.Text = "";
                txtwebadd.Text = "";
                txtAPname.Text = "";
                txtapcontact.Text = "";

                lblerrmsg.Visible = false;

                txtaddress.Focus();
            }
        }


        protected void txtPartyID_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click1(object sender, EventArgs e)
        {

            if (Previousdata(txtPartyID.Text) == true)
            {
                lblerrmsg.Visible = true;
                lblerrmsg.Text = "Party Name Already Exist";

                txtPartynm.Focus();

            }
            else
            {
                try
                {
                    //pim.PartyNanme = txtPartynm.Text;
                    pim.PartyID = txtPartyID.Text;
                    pim.Address = txtaddress.Text;
                    pim.Contact = txtcontact.Text;
                    pim.Email = txtEmail.Text;
                    pim.Web = txtwebadd.Text;
                    pim.APName = txtAPname.Text;
                    pim.APContact = txtapcontact.Text;
                    pim.Status = ddlstatus.Text;

                    pim.InTime = DateTime.Now;
                    pim.UpdateTime = DateTime.Now;
                    pim.UserPC = HttpContext.Current.Session["PCName"].ToString();
                    pim.IPAddress = HttpContext.Current.Session["IpAddress"].ToString();

                    pid.CreateParty(pim);

                    Refresh();
                }

                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }

        }


        public void Refresh()
        {
            txtPartynm.Text = "";
            txtPartyID.Text = "";
            txtaddress.Text = "";
            txtcontact.Text = "";
            txtEmail.Text = "";
            txtwebadd.Text = "";
            txtAPname.Text = "";
            txtapcontact.Text = "";
            ddlstatus.SelectedIndex = -1;

            lblerrmsg.Visible = false;
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                //pim.PartyNanme = txtPartynm.Text;
                pim.PartyID = txtPartyID.Text;
                pim.Address = txtaddress.Text;
                pim.Contact = txtcontact.Text;
                pim.Email = txtEmail.Text;
                pim.Web = txtwebadd.Text;
                pim.APName = txtAPname.Text;
                pim.APContact = txtapcontact.Text;
                pim.Status = ddlstatus.Text;

                pim.InTime = DateTime.Now;
                pim.UpdateTime = DateTime.Now;
                pim.UserPC = HttpContext.Current.Session["PCName"].ToString();
                pim.IPAddress = HttpContext.Current.Session["IpAddress"].ToString();

                pid.UpdateParty(pim);

                Refresh();
            }

            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}